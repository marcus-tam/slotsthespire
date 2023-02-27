using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseEffect : ScriptableObject
{
   public abstract void DoEffect();

   public abstract void ResetEffect();

   public abstract void CountDown();

   public abstract string GetDescription();

   public abstract void Upgrade();

   public abstract void Downgrade();
}
