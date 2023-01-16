using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOSS }

public class BattleSystem : MonoBehaviour
{

    public GameObject PlayerPrefab;
    public GameObject enemyPrefab;
    public FloatVariable turn, damage, shield, enemyHealth, enemyMaxHealth;
    public BoolVariable isShielded;
    public EnemyAction enemyA;

    public Transform PlayerBattleStation;
    public Transform enemyBattleStation;

    public BattleState state;
    public SlotMachine slot;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        setupBattle();
    }


    void setupBattle()
    {
        damage.SetValue(0);
        shield.SetValue(0);
        turn.SetValue(1);
        enemyHealth.SetValue(enemyMaxHealth.Value);
        GameObject playerGO = Instantiate(PlayerPrefab);
        GameObject enemyGo = Instantiate(enemyPrefab);
        state = BattleState.PLAYERTURN;

    }

    public void PlayerTurn() {
        enemyHealth.ApplyChange(damage, true);
        shield.ApplyChange(shield.Value);
        if (shield.Value == 0)
            isShielded.setFalse();
        else
            isShielded.setTrue();
        

    }

    public void OnAttackButton() {

        if (state != BattleState.PLAYERTURN)
            return;
        slot.SpinMachine();
        state = BattleState.ENEMYTURN;
        damage.SetValue(0);
        EnemyTurn();
    }

    void EnemyTurn() {
        Debug.Log("shielding for " + shield.Value + " before enemy");
        enemyA.PerformAction();
        turn.ApplyChange(1.0f);
        state = BattleState.PLAYERTURN;
        Debug.Log("shielding for " + shield.Value + " after enemy");
        shield.SetValue(0);
    }

}
