using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENDTURN, ENEMYTURN, WON, LOSS }

public class BattleSystem : MonoBehaviour
{

    public GameObject PlayerPrefab;
    public GameObject enemyPrefab;
    public float tempDamage;
    public FloatVariable turn, damage, playerShield, enemyHealth, enemyShield, enemyMaxHealth, playerHealth, floorLevel;
    public BoolVariable isShielded, enemyIsShielded;
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
       state = BattleState.ENDTURN;
    }

    public void OnEndTurnButton(){
        if (state != BattleState.ENDTURN)
            return;
        dealDamage();
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
        enemyPrefab.GetComponent<EnemyAction>().PerformAction();
        if (playerHealth.Value <= 0)
            Defeat();
        else
            endEnemyTurn();           
    }

    public void dealDamage() {
        tempDamage =  damage.Value;
        // If enemy is unshielded, damage affects health
        if (!enemyIsShielded){
            enemyHealth.ApplyChange(tempDamage, true);
        }
        else
            {
                //If incoming damage can break shield, subtract shield value from tempDamage and apply new tempDamage to playerHealth. Set playerShield to 0
                if (tempDamage >= enemyShield.Value){
                    tempDamage -= enemyShield.Value;
                    enemyShield.SetValue(0);
                    Debug.Log("TempDamag is "+ tempDamage);
                    enemyHealth.ApplyChange(tempDamage, true);

                } 
                // Else, just change player shield hp
                else{
                    enemyShield.ApplyChange(tempDamage, true);
                }
            }
        if (enemyHealth.Value <= 0) 
            Victory();
    }

    public void endPlayerTurn() {

        damage.SetValue(0);
        enemyShield.SetValue(0);
        state = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    public void endEnemyTurn() {
        turn.ApplyChange(1.0f);
        playerShield.SetValue(0);
        unitDisplay.UpdateDisplay(enemyPrefab);
        state = BattleState.PLAYERTURN;
    }

    public void resetBattle() {
        damage.SetValue(0);
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
        enemyPrefab = enemySpawner.GenerateCommonEnemy();
        Instantiate(enemyPrefab);
        Debug.Log(" Has Spawned");
        unitDisplay.UpdateDisplay(enemyPrefab);
    }
}
