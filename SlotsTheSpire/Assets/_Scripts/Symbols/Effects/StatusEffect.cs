using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Symbols/Effects/Status")]
public class StatusEffect : BaseEffect
{
    public float amount, upgradedAmount, startingAmount;
    public FloatVariable StatusCounter;
    public string description, upgradedDescription, startingDescription;

    public override void DoEffect(){
        StatusCounter.ApplyChange(amount);
    }

    public override void CountDown(){
        if(StatusCounter.Value > 0)
        StatusCounter.ApplyChange(1,true);
    }

    public override void ResetEffect(){
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
