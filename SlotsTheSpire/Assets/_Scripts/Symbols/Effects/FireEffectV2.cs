using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Symbols/Effects/FireV2")]
public class FireEffectV2 : BaseEffect
{
    public float amount, upgradedAmount, startingAmount, countAmount, upgradedCountAmount, startingCountAmount;
    public FloatVariable FireDMG, FireDMGCount;
    public string description, upgradedDescription, startingDescription;

 public override void DoEffect(){
    FireDMG.ApplyChange(amount);
    FireDMGCount.ApplyChange(countAmount);
 }

 public override void ResetEffect(){
   FireDMG.SetValue(0);
 }

 public override void CountDown(){
    if(FireDMGCount.Value > 0){
         FireDMGCount.ApplyChange(1,true);
         if(FireDMGCount.Value == 0)
         FireDMG.SetValue(0);
    }
    
 }

 public override string GetDescription(){
        return description;
    }

 public override void Upgrade(){
        amount = upgradedAmount;
        countAmount = upgradedCountAmount;
        description = upgradedDescription;
    }

 public override void Downgrade(){
        amount = startingAmount;
        countAmount = startingCountAmount;
        description = startingDescription;
    }

}