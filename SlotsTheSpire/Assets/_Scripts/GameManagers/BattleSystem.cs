using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOSS }

public class BattleSystem : MonoBehaviour
{

    public GameObject P_Prefab;
    public GameObject E_Prefab;
    public FloatVariable turn, E_Health, P_Shield, E_Shield, E_MaxHP, P_Health, floorLevel, E_OG_Damage, P_OG_Damage, P_IC_Shield, E_IC_Shield, P_IC_FireDamage, E_IC_FireDamage, P_Rolls;
    public Transform P_BattleStation;
    public Transform E_BattleStation;
    public bool hasRolled;
    GameObject P_GameObject, E_GameObject;

    public BattleState state;
    public SlotMachine slot;
    public EnemySpawner E_Spawner;
    public UnitDisplay unitDisplay;

    public GameEvent P_ChangeTurn, P_Death, P_Victory;

    void Start()
    {
        P_Health.SetValue(25);
        state = BattleState.START;
        setupBattle();
    }


    void setupBattle()
    {
        GenerateFloorEnemy();
        P_GameObject = Instantiate(P_Prefab, P_BattleStation);
        ResetBattle();
        state = BattleState.PLAYERTURN;
    }

    public void OnEndTurnButton(){
        if (state != BattleState.PLAYERTURN || !hasRolled)
            return;
        DealDMG(E_GameObject);
        slot.TriggerEffects();
        DealFireDMG(E_GameObject);
        GainShield(P_GameObject);
        P_GameObject.GetComponent<UnitHealth>().DecreaseStatusEffects();
        if(E_Health.Value <= 0){
            Victory();
            return;
        }
        EndPlayerTurn();
    }

    public void OnSpinButton() {
        if (state != BattleState.PLAYERTURN || P_Rolls.Value == 0)
            return;
        slot.SpinMachine();
        P_ChangeTurn.Raise(this, true);
        hasRolled = true;
    }

    public void EnemyTurn() {
        E_Shield.SetValue(0);
        E_GameObject.GetComponent<UnitHealth>().DecreaseStatusEffects();
        E_GameObject.GetComponent<EnemyActioner>().PerformAction();
        DealDMG(P_GameObject);
        DealFireDMG(P_GameObject);
        GainShield(E_GameObject);
        if (P_Health.Value <= 0)
            Defeat();
        else
            EndEnemyTurn();           
    }

    public void DealDMG(GameObject target) {
        if(target == E_GameObject)
        E_GameObject.GetComponent<UnitHealth>().TakeDamage(P_OG_Damage);
        if(target == P_GameObject)
        P_GameObject.GetComponent<UnitHealth>().TakeDamage(E_OG_Damage);
    }

    public void DealFireDMG(GameObject target){
        if(target == E_GameObject)
        E_GameObject.GetComponent<UnitHealth>().TakeFireDamage(E_IC_FireDamage);
        if(target == P_GameObject)
        P_GameObject.GetComponent<UnitHealth>().TakeFireDamage(P_IC_FireDamage);
    }

    public void GainShield(GameObject target){
        if(target == E_GameObject)
        E_GameObject.GetComponent<UnitHealth>().TakeShield(E_IC_Shield);
        if(target == P_GameObject)
        P_GameObject.GetComponent<UnitHealth>().TakeShield(P_IC_Shield);
    }

    public void EndPlayerTurn() {
        state = BattleState.ENEMYTURN;
        P_Rolls.SetValue(2);
        slot.rerollsText.text = "Spin (" + P_Rolls.Value.ToString()+")";
        hasRolled = false;
        EnemyTurn();
    }

    public void EndEnemyTurn() {
        turn.ApplyChange(1.0f);
        P_Shield.SetValue(0);
        E_GameObject.GetComponent<UnitHealth>().animator.SetTrigger("OnAttack");
        unitDisplay.UpdateDisplay(E_GameObject);
        state = BattleState.PLAYERTURN;
    }

    public void ResetBattle() {
        turn.SetValue(0);
        E_IC_FireDamage.SetValue(0);
        P_IC_FireDamage.SetValue(0);
        P_OG_Damage.SetValue(0);
        E_OG_Damage.SetValue(0);
        P_IC_Shield.SetValue(0);
        E_IC_Shield.SetValue(0);
        slot.ResetSymbols();
        hasRolled = false;
    }

    public void Victory() {
        E_Health.SetValue(0);
        state = BattleState.WON;
        Debug.Log("You Win");
        //reward screen
        P_Victory.Raise(this, true);
        Destroy(P_GameObject);
        Destroy(E_GameObject);
        //change scene to map
        //ResetBattle();
    }

    public void Defeat() {
        P_Health.SetValue(0);
        state = BattleState.LOSS;
        Debug.Log("You Lose");
        //defeat screen
        P_Death.Raise(this, true);
        Destroy(P_GameObject);
        Destroy(E_GameObject);
        //review deck & stats
        //main menu & retry
        //ResetBattle();
    }

    public void GenerateFloorEnemy(){
        //E_Prefab = E_Spawner.GenerateCommonEnemy();
        E_Prefab = E_Spawner.GenerateBoss();
        E_BattleStation.Rotate(new Vector3(0,180f, 0));
        E_GameObject = Instantiate(E_Prefab, E_BattleStation);
        Debug.Log(" Has Spawned");
        unitDisplay.UpdateDisplay(E_GameObject);
    }
}
