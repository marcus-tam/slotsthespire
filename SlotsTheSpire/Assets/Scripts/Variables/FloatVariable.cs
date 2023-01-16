using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Variable/Float")]
public class FloatVariable : ScriptableObject
{
   public float Value;

 
    public void SetValue(float value)
    {
        Value = value;
    }

    public void SetValue(FloatVariable value)
    {
        Value = value.Value;
    }

    public void ApplyChange(float amount) {
        Value += amount;
    }

    public void ApplyChange(float amount, bool isDamage)
    {
        if (isDamage)
            Value -= amount;
        else
            Value += amount;
    }

    public void ApplyChange(FloatVariable amount, bool isDamage)
    {
        if (isDamage)
            Value -= amount.Value;
        else
            Value += amount.Value;
    }
}

