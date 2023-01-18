using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Symbols/Effects")]
public class Effect : BaseEffect
{
    public FloatReference amount;
    public FloatVariable target;
    public bool resetEffect;

 public override void DoEffect(){
    target.ApplyChange(amount);
 }

 public void ResetEffect(){
  if(resetEffect)
   target.SetValue(target.StartingValue);
 }

}
