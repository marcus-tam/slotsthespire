using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Symbols/Effects/PowerupEffect")]
public class PowerUpEffect : BaseEffect
{
    public float amount, upgradedAmount, startingAmount;
    public FloatVariable target;
    public string description, upgradedDescription, startingDescription;

 public override void DoEffect(){
    target.ApplyChange(amount);
 }

 public override void ResetEffect(){
   target.SetValue(0);
 }

 public override void CountDown(){
 }

 public override string GetDescription(){
        return description;
    }

 public override void Upgrade(){
        amount = upgradedAmount;
        description = upgradedDescription;
    }

 public override void Downgrade(){
        amount = startingAmount;
        description = startingDescription;
    }
}

