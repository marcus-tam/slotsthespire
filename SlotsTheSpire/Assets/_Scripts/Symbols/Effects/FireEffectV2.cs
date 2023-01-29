using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Symbols/Effects/FireV2")]
public class FireEffectV2 : BaseEffect
{
    public float amount, countAmount;
    public FloatVariable FireDMG, FireDMGCount;
    public string description;

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
}