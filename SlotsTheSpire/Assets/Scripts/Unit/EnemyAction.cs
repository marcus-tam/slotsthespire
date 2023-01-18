using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Attack")]
public class EnemyAction : Action
{
    public FloatReference damageAmount, shieldAmount;
    public FloatVariable E_incomingShield, E_outgoingDamage;
    public BoolVariable E_IsShielded;
    public bool isDamage, isShield;
    public string attackSummary;

    public override void DoAction(EnemyActioner action) {
        if(isDamage)
            CalculateDamage();
        if(isShield)
            ShieldSelf();
        }

    public void CalculateDamage(){
       E_outgoingDamage.SetValue(damageAmount);
    }

    public void ShieldSelf(){
        E_incomingShield.SetValue(shieldAmount);
    }

    public override string GetDescription(){
        return attackSummary;
    }


}
