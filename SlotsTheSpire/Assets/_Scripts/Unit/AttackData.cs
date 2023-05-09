using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AttackData")]
public class AttackData : ScriptableObject
{
    public float damage, fire, weak, expose, IC_Shield, count;

    public void ResetAttack(){
        damage = 0;
        fire = 0;
        weak = 0;
        expose = 0;
        IC_Shield = 0;
        count = 0;
    }

    public void AddDamage(float amount){
        damage += amount;
    }

    public void AddFire(float amount){
        fire += amount;
    }

    public void AddWeak(float amount){
        weak += amount;
    }

    public void AddExpose(float amount){
        expose += amount;
    }

    public void AddICShield(float amount){
        IC_Shield += amount;
    }

    public void AddCount(){
        count++;
    }
}
