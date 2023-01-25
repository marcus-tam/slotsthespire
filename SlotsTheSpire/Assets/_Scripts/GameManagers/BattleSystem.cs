using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENDTURN, ENEMYTURN, WON, LOSS }

public class BattleSystem : MonoBehaviour
{

    public GameObject P_Prefab;
    public GameObject E_Prefab;
    public FloatVariable turn, E_Health, P_Shield, E_Shield, E_MaxHP, P_Health, floorLevel, E_OG_Damage, P_OG_Damage, P_IC_Shield, E_IC_Shield, P_IC_FireDamage, E_IC_FireDamage;
    public Transform P_BattleStation;
    public Transform E_BattleStation;

    public BattleState state;
    public SlotMachine slot;
    public EnemySpawner E_Spawner;
    public UnitDisplay unitDisplay;

    public GameEvent P_ChangeTurn;

    void Start()
    {
        P_Health.SetValue(25);
        state = BattleState.START;
        setupBattle();
    }


    void setupBattle()
    {
        GenerateFloorEnemy();
        Instantiate(P_Prefab);
        ResetBattle();
        state = BattleState.PLAYERTURN;
    }

    public void PlayerTurn() {
        P_Shield.SetValue(0);
        state = BattleState.ENDTURN;
    }

    public void OnEndTurnButton(){
        if (state != BattleState.ENDTURN)
            return;
        DealDMG(E_Prefab);
        slot.TriggerEffects();
        DealFireDMG(E_Prefab);
        GainShield(P_Prefab);
        P_Prefab.GetComponent<UnitHealth>().DecreaseStatusEffects();
        if(E_Health.Value <= 0){
            Victory();
            return;
        }
        EndPlayerTurn();
    }

    public void OnSpinButton() {
        if (state != BattleState.PLAYERTURN)
            return;
        slot.SpinMachine();
        P_ChangeTurn.Raise(this, true);
    }

    public void EnemyTurn() {
        E_Shield.SetValue(0);
        E_Prefab.GetComponent<UnitHealth>().DecreaseStatusEffects();
        E_Prefab.GetComponent<EnemyActioner>().PerformAction();
        DealDMG(P_Prefab);
        DealFireDMG(P_Prefab);
        GainShield(E_Prefab);
        if (P_Health.Value <= 0)
            Defeat();
        else
            EndEnemyTurn();           
    }

    public void DealDMG(GameObject target) {
        if(target == E_Prefab)
        E_Prefab.GetComponent<UnitHealth>().TakeDamage(P_OG_Damage);
        if(target == P_Prefab)
        P_Prefab.GetComponent<UnitHealth>().TakeDamage(E_OG_Damage);
    }

    public void DealFireDMG(GameObject target){
        if(target == E_Prefab)
        E_Prefab.GetComponent<UnitHealth>().TakeFireDamage(E_IC_FireDamage);
        if(target == P_Prefab)
        P_Prefab.GetComponent<UnitHealth>().TakeFireDamage(P_IC_FireDamage);
    }

    public void GainShield(GameObject target){
        if(target == E_Prefab)
        E_Prefab.GetComponent<UnitHealth>().TakeShield(E_IC_Shield);
        if(target == P_Prefab)
        P_Prefab.GetComponent<UnitHealth>().TakeShield(P_IC_Shield);
    }

    public void EndPlayerTurn() {
        state = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    public void EndEnemyTurn() {
        turn.ApplyChange(1.0f);
        unitDisplay.UpdateDisplay(E_Prefab);
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
    }

    public void Victory() {
        E_Health.SetValue(0);
        state = BattleState.WON;
        Debug.Log("You Win");
        //reward screen
        //change scene to map
        //ResetBattle();
    }

    public void Defeat() {
        P_Health.SetValue(0);
        state = BattleState.LOSS;
        Debug.Log("You Lose");
        //defeat screen
        //review deck & stats
        //main menu & retry
        //ResetBattle();
    }

    public void GenerateFloorEnemy(){
        E_Prefab = E_Spawner.GenerateCommonEnemy();
        Instantiate(E_Prefab);
        Debug.Log(" Has Spawned");
        unitDisplay.UpdateDisplay(E_Prefab);
    }
}
