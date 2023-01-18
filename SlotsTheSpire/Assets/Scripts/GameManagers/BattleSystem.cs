using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENDTURN, ENEMYTURN, WON, LOSS }

public class BattleSystem : MonoBehaviour
{

    public GameObject P_Prefab;
    public GameObject E_Prefab;
    public FloatVariable turn, enemyHealth, playerShield, enemyShield, enemyMaxHealth, playerHealth, floorLevel, E_outgoingDamage, P_outgoingDamage, P_incomingShield, E_incomingShield;
    public Transform PlayerBattleStation;
    public Transform enemyBattleStation;

    public BattleState state;
    public SlotMachine slot;
    public EnemySpawner enemySpawner;
    public UnitDisplay unitDisplay;

    void Start()
    {
        playerHealth.SetValue(25);
        state = BattleState.START;
        setupBattle();
    }


    void setupBattle()
    {
        GenerateFloorEnemy();
        Instantiate(P_Prefab);
        resetBattle();
        state = BattleState.PLAYERTURN;
    }

    public void PlayerTurn() {
        playerShield.SetValue(0);
        state = BattleState.ENDTURN;
    }

    public void OnEndTurnButton(){
        if (state != BattleState.ENDTURN)
            return;
        dealDamage(E_Prefab);
        gainShield(P_Prefab);
        if (state == BattleState.WON)
            return;
        endPlayerTurn();
    }

    public void OnSpinButton() {
        if (state != BattleState.PLAYERTURN)
            return;
        slot.SpinMachine();
    }

    public void EnemyTurn() {
        enemyShield.SetValue(0);
        E_Prefab.GetComponent<EnemyActioner>().PerformAction();
        dealDamage(P_Prefab);
        gainShield(E_Prefab);
        if (playerHealth.Value <= 0)
            Defeat();
        else
            endEnemyTurn();           
    }

    public void dealDamage(GameObject target) {
        if(target == E_Prefab)
        E_Prefab.GetComponent<UnitHealth>().TakeDamage(P_outgoingDamage);
        if(target == P_Prefab)
        P_Prefab.GetComponent<UnitHealth>().TakeDamage(E_outgoingDamage);
    }

    public void gainShield(GameObject target){
        if(target == E_Prefab)
        E_Prefab.GetComponent<UnitHealth>().TakeShield(E_incomingShield);
        if(target == P_Prefab)
        P_Prefab.GetComponent<UnitHealth>().TakeShield(P_incomingShield);
    }

    public void endPlayerTurn() {
        state = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    public void endEnemyTurn() {
        turn.ApplyChange(1.0f);
        unitDisplay.UpdateDisplay(E_Prefab);
        state = BattleState.PLAYERTURN;
    }

    public void resetBattle() {
        playerShield.SetValue(0);
        enemyShield.SetValue(0);
        turn.SetValue(0);
        slot.ResetSymbols();
    }

    public void Victory() {
        enemyHealth.SetValue(0);
        state = BattleState.WON;
        Debug.Log("You Win");
        //reward screen
        //change scene to map
        //resetBattle();
    }

    public void Defeat() {
        playerHealth.SetValue(0);
        state = BattleState.LOSS;
        Debug.Log("You Lose");
        //defeat screen
        //review deck & stats
        //main menu & retry
        //resetBattle();
    }

    public void GenerateFloorEnemy(){
        E_Prefab = enemySpawner.GenerateCommonEnemy();
        Instantiate(E_Prefab);
        Debug.Log(" Has Spawned");
        unitDisplay.UpdateDisplay(E_Prefab);
    }
}
