using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Symbols/Effects/Fire")]
public class FireEffect : BaseEffect
{
    public float amount, countDownAmount;
    public FloatVariable fireDamage, Fire_Status_Count;

    public override void DoEffect(){
        fireDamage.ApplyChange(amount);
        Fire_Status_Count.ApplyChange(countDownAmount);
    }

    public override void CountDown(){
        Fire_Status_Count.ApplyChange(countDownAmount, true);
        if(Fire_Status_Count.Value <= 0.0f)
            fireDamage.SetValue(fireDamage.StartingValue);
    }

    public override void ResetEffect(){

    }
}
