using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOSS }

public class BattleSystem : MonoBehaviour
{

    public GameObject P_Prefab, E_Prefab, selectedTarget;
    public PlayerData playerData;
    public FloatVariable turn, E_Shield, floorLevel,P_Rolls, P_MaxRolls,E_OG_Damage;
    public FloatVariable[] E_Health, E_MaxHP= new FloatVariable[4];
    public Transform P_BattleStation;
    public Transform[] E_BattleStation = new Transform[4];
    public bool hasRolled, E_Alive;
    GameObject P_GameObject;
    public GameObject[] E_GameObject = new GameObject[4];
    public GameObject[] E_BattleHUD = new GameObject[4];

    public BattleState state;
    public EnemySpawner E_Spawner;
    public UnitDisplay[] unitDisplay;
    GameObject[] Units = new GameObject[5];
    public int check;
    public Inventory inventory;

    public GameEvent P_Spin, P_Death, P_Victory, CombatStart, P_EndTurn, P_Reroll, OnUnitTarget, P_ShieldReset;

    void Start()
    {
        state = BattleState.START;
        setupBattle();
    }


    void setupBattle()
    {
        for(int i = 0; i < E_BattleHUD.Length; i++){
            E_BattleHUD[i].SetActive(true);
        }
        
        P_GameObject = Instantiate(P_Prefab, P_BattleStation);
        Units[0] = P_GameObject;
        GenerateFloorEnemy();
        ResetBattle();
        inventory.StartOfCombatEffects();
        state = BattleState.PLAYERTURN;
    }

    public void OnEndTurnButton(){
        if (state != BattleState.PLAYERTURN || !hasRolled)
            return;
        DealDMG(E_GameObject[1], 0);
        P_GameObject.GetComponent<PlayerHealth>().TakeShield(playerData.shield);
        P_GameObject.GetComponent<PlayerHealth>().DecreaseStatusEffects();
        for(int k = 0; k<E_GameObject.Length; k++)
        {
            if(E_GameObject[k] != null)
            unitDisplay[k].UpdateDisplay(E_GameObject[k]);
        }
        P_EndTurn.Raise(this, 1);
        EndPlayerTurn();
    }

    public void OnSpinButton() {
        if (state != BattleState.PLAYERTURN || P_Rolls.Value == 0)
            return;
        if(P_Rolls.Value != P_MaxRolls.Value){   
            P_Reroll.Raise(this, 1);
        }
        P_Spin.Raise(this, true);
        hasRolled = true;
    }

    public void EnemyTurn() {
        for(int i = 0; i < E_GameObject.Length; i++)
        {
            if(E_GameObject[i] != null)
            {
                E_GameObject[i].GetComponent<UnitHealth>().Maintance();
                E_GameObject[i].GetComponent<EnemyActioner>().PerformAction(E_GameObject[i].GetComponent<UnitHealth>());
                DealDMG(P_GameObject, i);
            }
        }
        EndEnemyTurn();           
    }

    public void DealDMG(GameObject target, int index) {
        if(target == P_GameObject)
        {
            Debug.Log(E_GameObject[index]+ " is hitting the player for " + E_OG_Damage.Value);
            P_GameObject.GetComponent<PlayerHealth>().TakeDamage(E_OG_Damage.Value);
        }
        
        else{
            if(playerData.type == 2) //Aoe Damage
            {
                Debug.Log("Dealing aoe damage");
                for(int i = 0; i < E_GameObject.Length; i++)
                    {
                        if(E_GameObject[i]== null)
                        continue;
                        E_GameObject[i].GetComponent<UnitHealth>().TakeDamage(playerData.damage);
                        E_GameObject[i].GetComponent<UnitHealth>().ApplyStatus(playerData.fire, playerData.weak, playerData.expose);
                        if(E_GameObject[i].GetComponent<UnitHealth>().currentHP <= 0)
                        EnemyDeath(i);
                    }
            }
            else if(playerData.type == 1) //flank damage
            {
                for(int j = E_GameObject.Length - 1; j >= 0; j--)
                {
                    if(E_GameObject[j] == null)
                        continue;
                    E_GameObject[j].GetComponent<UnitHealth>().TakeDamage(playerData.damage);
                    E_GameObject[j].GetComponent<UnitHealth>().ApplyStatus(playerData.fire, playerData.weak, playerData.expose);
                    if(E_GameObject[j].GetComponent<UnitHealth>().currentHP <= 0)
                        EnemyDeath(j);
                    break;
                }
            }
            else //front damage
            {
                Debug.Log("Dealing Front damage");
                for(int i = 0; i < E_GameObject.Length; i++)
                {
                    if(E_GameObject[i] == null)
                        continue;
                    E_GameObject[i].GetComponent<UnitHealth>().TakeDamage(playerData.damage);
                    E_GameObject[i].GetComponent<UnitHealth>().ApplyStatus(playerData.fire, playerData.weak, playerData.expose);
                    if(E_GameObject[i].GetComponent<UnitHealth>().currentHP <= 0)
                        EnemyDeath(i);
                    break;
                }
            }
        }
    }

    public void EndPlayerTurn() {
        state = BattleState.ENEMYTURN;
        hasRolled = false;
        P_GameObject.GetComponent<PlayerHealth>().animator.SetTrigger("OnAttack");
        EnemyTurn();
    }

    public void EndEnemyTurn() {
        turn.ApplyChange(1.0f);
        for(int i = 0; i < E_GameObject.Length; i++){
            if(E_GameObject[i] == null)
            continue;
        if(E_GameObject[i].GetComponent<UnitHealth>().hasAnimator)
            E_GameObject[i].GetComponent<UnitHealth>().animator.SetTrigger("OnAttack");
            unitDisplay[i].UpdateDisplay(E_GameObject[i]);
        }
        P_ShieldReset.Raise(this,playerData.shield);
        state = BattleState.PLAYERTURN;
    }

    public void EnemyDeath(int index){
        Destroy(E_GameObject[index]);
        check = 0;
        E_BattleHUD[index].SetActive(false);
        E_GameObject[index] = null;
        for(int i = 0; i < E_GameObject.Length; i++){

            if(E_GameObject[i] != null){
                check++;
                break;
            }
        }
        if(check == 0){
           Victory();
        }
    }

    public void ResetBattle() {
        turn.SetValue(0);
        for(int i = 0; i < E_GameObject.Length; i++){
            E_GameObject[i].GetComponent<UnitHealth>().ZeroShield();
        }
        E_OG_Damage.SetValue(0);
        playerData.ResetPlayer();
        hasRolled = false;
        CombatStart.Raise(this,1);
    }

    public void Victory() {
        for(int i = 0; i < E_Health.Length; i++)
        E_Health[i].SetValue(0);
        state = BattleState.WON;
        Debug.Log("You Win");
        //reward screen
        P_Victory.Raise(this, true);
        Destroy(P_GameObject);
        for(int i = 0; i < E_GameObject.Length; i++){
            Destroy(E_GameObject[i]);
        }
        //change scene to map
        //ResetBattle();
    }

    public void Defeat() {
        state = BattleState.LOSS;
        Debug.Log("You Lose");
        //defeat screen
        Destroy(P_GameObject);
        for(int i = 0; i < E_GameObject.Length; i++){
            Destroy(E_GameObject[i]);
        }
        
        //review deck & stats
        //main menu & retry
        //ResetBattle();
    }

    public void GenerateFloorEnemy(){
        GameObject[] enemies = E_Spawner.GenerateEnemies();
        int count = 1;
        for(int i = 0; i < enemies.Length; i++){
            Units[count] = enemies[i];
            E_Prefab = Units[count];
            E_BattleStation[i].Rotate(new Vector3(0,180f, 0));
            E_GameObject[i] = Instantiate(E_Prefab, E_BattleStation[i]);
            unitDisplay[i].UpdateDisplay(E_GameObject[i]);
            count++;
        } 
    }

    public void SelectGameObject(int index){
        if(index == 0)
            selectedTarget = P_GameObject;
        if(index > 0)
            selectedTarget = E_GameObject[index-1];
        OnUnitTarget.Raise(this, selectedTarget);
    }

    public void UpdateEnemyDisplay(){
        for(int i = 0; i < E_GameObject.Length; i++){
            if(E_GameObject[i]!= null)
            unitDisplay[i].UpdateDisplay(E_GameObject[i]);
        }
    }

    public void dealDMGAOE(Component sender, object data){
         for(int i = 0; i < E_GameObject.Length; i++)
                    {
                        if(E_GameObject[i]== null)
                        continue;
                        E_GameObject[i].GetComponent<UnitHealth>().TakeDamage((float)data);
                        if(E_GameObject[i].GetComponent<UnitHealth>().currentHP <= 0)
                        EnemyDeath(i);
                    }
    }
}
