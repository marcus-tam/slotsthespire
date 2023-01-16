using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Effect : ScriptableObject
{
   public abstract void DoEffect();

   public abstract void ResetSymbolEffect();
}
