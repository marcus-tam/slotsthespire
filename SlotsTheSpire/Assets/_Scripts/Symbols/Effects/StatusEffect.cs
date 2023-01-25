using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Symbols/Effects/Status")]
public class StatusEffect : BaseEffect
{
    public float amount;
    public FloatVariable StatusCounter;
    public string description;

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
}
