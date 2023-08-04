using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
   public float damage, type, fire, weak, expose, shield;
   public FloatVariable strength, dex;

    public void Start(){
        ResetPlayer();
    }

    public void ResetPlayer(){
        ResetData();
        strength.SetValue(0);
        dex.SetValue(0);
    }

   public void ResetData(){
    damage = 0;
    type = 0;
    fire = 0;
    weak = 0;
    expose = 0;
    shield = 0;
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

   public void AddShield(float amount){
    shield += amount;
   }

    public void resetShield(){
        shield = 0;
    }

    public void resetTurn(){
        damage = 0;
        type = 0;
        fire = 0;
        weak = 0;
        expose = 0;
    }

    public void AddStrength(float amt){
        strength.ApplyChange(amt);
    }

    public void AddDex(float amt){
        dex.ApplyChange(amt);
    }

    public void SwitchType(float AttackType){
        // 0 - front(default), 1- flank, 2 - AOE
        if(type != AttackType && AttackType != 0)
        {
            if(AttackType == 1 && type != 2)
            type = 1;
            if(AttackType == 2)
            type = 2;
        }
        Debug.Log("attack type = " + type);
    }
}
