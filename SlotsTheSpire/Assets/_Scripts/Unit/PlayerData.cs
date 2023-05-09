using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
   public float OG_FrontDamage,OG_FlankDamage , OG_AOEDamage, OG_Fire, OG_Weak, OG_Expose, IC_Shield;

    public void Start(){
        resetPlayerTurn();
    }

   public void resetPlayerTurn(){
    OG_FrontDamage = 0;
    OG_FlankDamage = 0;
    OG_AOEDamage = 0;
    OG_Fire = 0;
    OG_Weak = 0;
    OG_Expose = 0;
    IC_Shield = 0;
   }

   public void addFrontDamage(float amount){
    OG_FrontDamage += amount;
   }

   public void addFlankDamage(float amount){
    OG_FlankDamage += amount;
   }

    public void addAOEDamage(float amount){
    OG_AOEDamage += amount;
   }

   public void addFire(float amount){
    OG_Fire += amount;
   }

   public void addWeak(float amount){
    OG_Weak += amount;
   }

   public void addExpose(float amount){
    OG_Expose += amount;
   }

   public void addShield(float amount){
    IC_Shield += amount;
   }

}
