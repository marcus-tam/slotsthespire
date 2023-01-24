using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseEffect : ScriptableObject
{
   public enum type{Resetable, CountDown, Permanent};

   public abstract void DoEffect();

   public abstract void ResetEffect();

   public abstract void CountDown();
}
