using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    public FloatVariable maxHP, currentHP;
    public UnitData unit;

    public bool ResetHP;
    public FloatReference StartingHP;

    private void Start()
    {
        if (ResetHP) {
            maxHP.SetValue(StartingHP);
            currentHP.SetValue(StartingHP);
        }
            
    }

    public UnitData getData(){
        return unit;
    }
}
