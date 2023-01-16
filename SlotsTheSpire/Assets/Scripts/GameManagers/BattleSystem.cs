using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOSS }

public class BattleSystem : MonoBehaviour
{

    public GameObject PlayerPrefab;
    public GameObject enemyPrefab;
    public FloatVariable turn, damage, playerShield, enemyHealth, enemyMaxHealth, playerHealth, floorLevel;
    public BoolVariable isShielded;
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
        Instantiate(PlayerPrefab);
        resetBattle();
        state = BattleState.PLAYERTURN;
    }

    public void PlayerTurn() {
        turn.ApplyChange(1);
        dealDamage();
        if (state == BattleState.WON)
            return;
        applyShield();
        endPlayerTurn();

    }

    public void OnAttackButton() {
        if (state != BattleState.PLAYERTURN)
            return;
        slot.SpinMachine();
    }

    public void EnemyTurn() {
        Debug.Log("shielding for " + playerShield.Value + " before enemy");
        enemyPrefab.GetComponent<EnemyAction>().PerformAction();
        if (playerHealth.Value <= 0)
            Defeat();
        else
            endEnemyTurn();           
    }

    public void dealDamage() {
        enemyHealth.ApplyChange(damage, true);
        if (enemyHealth.Value <= 0) 
            Victory();
    }

    public void applyShield() {
        playerShield.ApplyChange(playerShield.Value);
        if (playerShield.Value == 0)
            isShielded.setFalse();
        else
            isShielded.setTrue();
    }

    public void endPlayerTurn() {

        damage.SetValue(0);
        state = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    public void endEnemyTurn() {
        turn.ApplyChange(1.0f);
        Debug.Log("shielding for " + playerShield.Value + " after enemy");
        playerShield.SetValue(0);
        state = BattleState.PLAYERTURN;
    }

    public void resetBattle() {
        //enemyMaxHealth.Value = enemyPrefab.GetComponent<UnitHealth>().GetMaxHp(enemyMaxHealth.Value);
        damage.SetValue(0);
        playerShield.SetValue(0);
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
        enemyPrefab = enemySpawner.GenerateCommonEnemy();
        Instantiate(enemyPrefab);
        Debug.Log(" Has Spawned");
        unitDisplay.UpdateDisplay(enemyPrefab);
    }
}
