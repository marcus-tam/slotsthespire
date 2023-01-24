using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Symbols/Effects/PowerupEffect")]
public class PowerUpEffect : BaseEffect
{
    public FloatReference amount;
    public FloatVariable target;

 public override void DoEffect(){
    target.ApplyChange(amount);
 }

 public override void ResetEffect(){
   target.SetValue(target.StartingValue);
 }

 public override void CountDown(){
   return;
 }

}
