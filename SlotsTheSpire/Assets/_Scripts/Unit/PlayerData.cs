using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
   public float damage, type, fire, weak, expose, shield;

    public void Start(){
        ResetPlayer();
    }

   public void ResetPlayer(){
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

    public void SwitchType(float AttackType){
        // 0 - front(default), 1- flank, 2 - AOE
        if(type != AttackType && AttackType != 0)
        {
            if(type == 1 && type != 2)
            type = 1;
            if(type == 2)
            type = 2;
        }
    }
}
