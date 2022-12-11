using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    public FloatVariable HP;
    public UnitData unit;

    public bool ResetHP;
    public FloatReference StartingHP;

    private void Start()
    {
        if (ResetHP)
            HP.SetValue(StartingHP);
    }

    public void OnDamage() 
    {
        HP.ApplyChange(unit.damage);
    }

}
