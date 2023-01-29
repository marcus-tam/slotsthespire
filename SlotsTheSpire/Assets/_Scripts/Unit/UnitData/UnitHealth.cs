using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class UnitHealth : MonoBehaviour
{
    public FloatVariable maxHP, currentHP, shield, exposedCount, fireCount, weakCount;
    public BoolVariable isShielded;
    public UnitData unit;
    
    public GameEvent DamageEvent,shieldEvent;
    public GameEvent DeathEvent;

    public bool ResetHP;
    public FloatReference StartingHP;

    private void Start()
    {
        if (ResetHP) {
            maxHP.SetValue(StartingHP);
            currentHP.SetValue(StartingHP);
        }
            shield.SetValue(0);
            exposedCount.SetValue(0);
            fireCount.SetValue(0);
            weakCount.SetValue(0);
    }

    public void TakeDamage(FloatVariable incomingDamage){
        if(exposedCount.Value > 0){
            incomingDamage.ApplyChange((float)Math.Ceiling(incomingDamage.Value/2));
            Debug.Log("Incoming damage (Exposed) is: " + incomingDamage.Value);
        }
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
        DamageEvent.Raise(this, currentHP.Value);
        if(currentHP.Value <= 0)
            DeathEvent.Raise(this, true);
    }

    public void TakeShield(FloatVariable incomingShield){
        shield.ApplyChange(incomingShield.Value);
        if(shield.Value > 0)
            isShielded.SetTrue();
            else
                isShielded.SetFalse();
        incomingShield.SetValue(0);
        shieldEvent.Raise(this, shield.Value);
    }
    public void TakeFireDamage(FloatVariable incomingFireDamage){
        if(fireCount.Value>0)
        currentHP.ApplyChange(incomingFireDamage.Value, true);
        DamageEvent.Raise(this,currentHP.Value);
    }

    public UnitData getData(){
        return unit;
    }

    public void DecreaseStatusEffects(){
        if(exposedCount.Value > 0)
        exposedCount.ApplyChange(1,true);
        if(fireCount.Value > 0)
        fireCount.ApplyChange(1,true);
        if(weakCount.Value > 0)
        weakCount.ApplyChange(1,true);
    }
}
