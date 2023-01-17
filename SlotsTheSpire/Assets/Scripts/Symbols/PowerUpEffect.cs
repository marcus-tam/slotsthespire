using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Symbols/Effects")]
public class PowerUpEffect : Effect
{
    public FloatReference amount;
    public FloatVariable target;
   // public SymbolData symbolData;

 public override void DoEffect(){
    target.ApplyChange(amount);
 }

 public override void ResetSymbolEffect(){
   target.SetValue(target.StartingValue);
 }

}
