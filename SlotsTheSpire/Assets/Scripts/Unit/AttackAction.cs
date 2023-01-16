using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Attack")]
public class AttackAction : Action
{
    public float DamageAmount;
    public FloatVariable playerHealth, playerShield;
    public BoolVariable shielded;
    public bool isDamage;

    public override void DoAction(EnemyAction action) {

        if (shielded == true) {
            if (DamageAmount > playerShield.Value) {
                DamageAmount -= playerShield.Value;
                playerShield.SetValue(0);
                playerHealth.ApplyChange(DamageAmount, isDamage);
            }
            else {
                playerShield.ApplyChange(DamageAmount, isDamage);
                if (playerShield.Value < 0)
                    playerShield.SetValue(0);
            }
            
        }
        
        Debug.Log("take " + DamageAmount + " Damage");
    }
}
