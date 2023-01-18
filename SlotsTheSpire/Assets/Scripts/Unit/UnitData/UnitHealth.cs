using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    public FloatVariable maxHP, currentHP, shield;
    public BoolVariable isShielded, OnFire, isWeakened, isExposed;
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

    public void TakeDamage(FloatVariable incomingDamage){
        // If enemy is unshielded, damage affects health
        if (!isShielded){
            currentHP.ApplyChange(incomingDamage, true);
        }
        else
            {
                //If incoming damage can break shield, subtract shield value from tempDamage and apply new tempDamage to playerHealth. Set playerShield to 0
                if (incomingDamage.Value >= shield.Value){
                    incomingDamage.ApplyChange(shield.Value, true);
                    shield.SetValue(0);
                    currentHP.ApplyChange(incomingDamage, true);

                } 
                // Else, just change player shield hp
                else{
                    shield.ApplyChange(incomingDamage, true);
                }
            }
        incomingDamage.SetValue(0);
    }

    public void TakeShield(FloatVariable incomingShield){
        shield.ApplyChange(incomingShield.Value);
        Debug.Log(this.unit.unitName + " received "+shield.Value+" shield");
        if(shield.Value > 0)
            isShielded.SetTrue();
            else
                isShielded.SetFalse();
        incomingShield.SetValue(0);
    }

    public UnitData getData(){
        return unit;
    }
}
