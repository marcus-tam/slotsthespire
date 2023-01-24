using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Symbols/Effects/Exposed")]
public class ExposedEffect : BaseEffect
{
    public float countDownAmount;
    public FloatVariable E_ExposedCounter;
    public string description;

    public override void DoEffect(){
        E_ExposedCounter.ApplyChange(countDownAmount);
    }

    public override void CountDown(){
        if(E_ExposedCounter.Value > 0)
        E_ExposedCounter.ApplyChange(1,true);
    }

    public override void ResetEffect(){

    }

    public override string GetDescription(){
        return description;
    }
}
