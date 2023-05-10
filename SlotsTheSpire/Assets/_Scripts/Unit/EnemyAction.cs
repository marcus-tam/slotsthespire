using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Action/Attack")]
public class EnemyAction : Action
{
    public FloatReference damageAmount, shieldAmount, weak, expose, fire;
    public FloatVariable E_incomingShield, E_OG_Damage, E_WeakCount;
    public BoolVariable E_IsShielded;
    public bool isDamage, isShield, hasEffect;
    public string attackSummary;

    public override void DoAction(EnemyActioner action, UnitHealth unit) {
        if(isDamage)
            CalculateDamage();
        if(isShield)
            ShieldSelf(unit);
        if(hasEffect)
            PreformEffect();
        }

    public void CalculateDamage(){
        if(E_WeakCount.Value > 0){
            E_OG_Damage.SetValue((float)Math.Ceiling(damageAmount.Value/2));
            Debug.Log("Enemy is weak incoming damage: " + E_OG_Damage.Value);
        }
        else{
            E_OG_Damage.SetValue(damageAmount);
        }
        
    }

    public void ShieldSelf(UnitHealth unit){
        unit.TakeShield(shieldAmount.Value);
    }

    public override int GetAttackType(){
        if(isDamage == true && isShield == true && hasEffect == true)
            return 0;
        else if(isDamage == true && isShield == true && hasEffect == false)
            return 1;
        else if (isDamage == true && isShield == false && hasEffect == true)
            return 2;
        else if(isDamage == true && isShield == false && hasEffect == false)
            return 3;
        else if(isDamage == false && isShield == true && hasEffect == true)
            return 4;
        else if(isDamage == false && isShield == true && hasEffect == false)
            return 5;
        else
            return 6;
    }

    public void PreformEffect(){

   }

}
