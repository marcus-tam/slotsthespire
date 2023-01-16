using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Attack")]
public class AttackAction : Action
{
    public float DamageAmount, tempDamage;
    public FloatVariable playerShield, playerHealth;
    public BoolVariable isShielded;
    public bool isDamage;

    public override void DoAction(EnemyAction action) {
        tempDamage = DamageAmount;
        if (isShielded == true)
        {
            if (tempDamage > playerShield.Value)
            {
                tempDamage -= playerShield.Value;
                playerShield.SetValue(0);
                playerHealth.ApplyChange(tempDamage, isDamage);
            }
            else
            {
                playerShield.ApplyChange(tempDamage, isDamage);
                if (playerShield.Value < 0)
                    playerShield.SetValue(0);
            }

        }

        Debug.Log("take " + DamageAmount + " Damage");
    }
}
