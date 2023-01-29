using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Action/Attack")]
public class EnemyAction : Action
{
    public FloatReference damageAmount, shieldAmount;
    public FloatVariable E_incomingShield, E_outgoingDamage, E_WeakCount;
    public BoolVariable E_IsShielded;
    public bool isDamage, isShield, hasEffect;
    public string attackSummary;
    public BaseEffect effect;

    public override void DoAction(EnemyActioner action) {
        if(isDamage)
            CalculateDamage();
        if(isShield)
            ShieldSelf();
        if(hasEffect)
            PreformEffect();

        }

    public void CalculateDamage(){
        if(E_WeakCount.Value > 0){
            E_outgoingDamage.SetValue((float)Math.Ceiling(damageAmount.Value/2));
            Debug.Log("Enemy is weak incoming damage: " + E_outgoingDamage.Value);
        }
        else{
            E_outgoingDamage.SetValue(damageAmount);
        }
        
    }

    public void ShieldSelf(){
        E_incomingShield.SetValue(shieldAmount);
    }

    public override string GetDescription(){
        return attackSummary;
    }

    public void PreformEffect(){
        if(hasEffect)
            effect.DoEffect();
        else
            return;
   }

}
