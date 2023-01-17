using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Attack")]
public class AttackAction : Action
{
    public float DamageAmount, tempDamage, shieldAmount;
    public FloatVariable playerShield, playerHealth, enemyShield;
    public BoolVariable playerIsShielded, enemyIsShielded;
    public bool isDamage, isShield;
    public string attackSummary;

    public override void DoAction(EnemyAction action) {
        if(isDamage)
            DealDamage();
        if(isShield)
            ShieldSelf();
        }

    public void DealDamage(){
        tempDamage =  DamageAmount;
        // If player is unshielded, damage affects health
        if (!playerIsShielded)
            {
                playerHealth.ApplyChange(tempDamage, isDamage);
                Debug.Log("take " + DamageAmount + " Damage. Direct Attack!");

            }
        else
            {
                //If incoming damage can break shield, subtract shield value from tempDamage and apply new tempDamage to playerHealth. Set playerShield to 0
                if (tempDamage >= playerShield.Value){
                    Debug.Log("SHielding for" + playerShield.Value);
                    tempDamage -= playerShield.Value;
                    playerShield.SetValue(0);
                    Debug.Log("TempDamag is "+ tempDamage);
                    playerHealth.ApplyChange(tempDamage, isDamage);
                    Debug.Log("take " + DamageAmount + " Damage. Shield Broken!");

                } 
                // Else, just change player shield hp
                else{
                    playerShield.ApplyChange(tempDamage, isDamage);
                    Debug.Log("take " + DamageAmount + " Damage. Shield Intact!");

                }
            }
    }

    public void ShieldSelf(){
        enemyShield.ApplyChange(shieldAmount);
       if(enemyShield.Value > 0)
       enemyIsShielded.setTrue();
       else
       enemyIsShielded.setFalse();
    }

    public override string GetDescription(){
        return attackSummary;
    }


}
