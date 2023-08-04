using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Action/Attack")]
public class EnemyAction : Action
{
    //effect type 0 = null, 1 = apply status's 2 = powerup;
    public float damageAmount, shieldAmount, weak, expose, fire, effectType, strengthPowerUp, dexPowerUp;
    public PlayerData enemyData;
    public bool isDamage, isShield, hasEffect;
    public string attackSummary;

    public override void DoAction(EnemyActioner action, UnitHealth unit) {
        enemyData.ResetData();
        if(isDamage)
            CalculateDamage(unit);
        if(isShield)
            ShieldSelf(unit);
        if(hasEffect)
            applyEffect(effectType, unit);
        }

    public void CalculateDamage(UnitHealth unit){
        if(unit.weakCount > 0){
            enemyData.AddDamage((float)Math.Ceiling(damageAmount+ unit.strength/2));
            Debug.Log(unit + " is weak incoming damage: " +enemyData.damage);
        }
        else{
            enemyData.AddDamage(damageAmount);
        }
    }

    public void ShieldSelf(UnitHealth unit){
        unit.TakeShield(shieldAmount);
    }

    public void applyEffect(float type, UnitHealth unit){
        if(type == 0)
        return;
        else if(type == 1)
        ApplyStatus();
        else if (type == 2)
        ApplyPowerUp(unit);
    }

    public void ApplyStatus(){
        enemyData.AddWeak(weak);
        enemyData.AddFire(fire);
        enemyData.AddExpose(expose);
    }

    public void ApplyPowerUp(UnitHealth unit){
        unit.AddStrength(strengthPowerUp);
        unit.AddDex(dexPowerUp);
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

}
