using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Symbols/Effects/Exposed")]
public class ExposedEffect : BaseEffect
{
    public float countDownAmount;
    public FloatVariable E_ExposedCounter;

    public override void DoEffect(){
        E_ExposedCounter.ApplyChange(countDownAmount);
    }

    public override void CountDown(){
        E_ExposedCounter.ApplyChange(1,true);
    }

    public override void ResetEffect(){

    }
}
