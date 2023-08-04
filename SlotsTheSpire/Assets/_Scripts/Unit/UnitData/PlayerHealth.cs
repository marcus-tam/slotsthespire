using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerHealth : MonoBehaviour
{
    public FloatVariable maxHP, currentHP, shield, exposedCount, fireCount, weakCount,DMG;
    public BoolVariable isShielded;
    public UnitData unit;
    public Animator animator;
    public bool hasAnimator;
    
    public GameEvent DamageEvent, shieldEvent, DeathEvent;

    public bool ResetHP;
    public FloatReference StartingHP;

    private void Start()
    {

        if(hasAnimator)
        animator = this.GetComponent<Animator>();
        if (ResetHP) {
            maxHP.SetValue(StartingHP);
            currentHP.SetValue(StartingHP);
        }
    }

    public void TakeDamage(FloatVariable incomingDamage){
        if(exposedCount.Value > 0){
            incomingDamage.ApplyChange((float)Math.Ceiling(incomingDamage.Value/2));
            Debug.Log("Incoming damage (Exposed) is: " + incomingDamage.Value);
        }
        // If enemy is unshielded, damage affects health
        if (!isShielded){
            currentHP.ApplyChange(incomingDamage, true);
            Debug.Log("Player was hit for: "+ incomingDamage.Value);
        }
        else
            {
                //If incoming damage can break shield, subtract shield value from tempDamage and apply new tempDamage to playerHealth. Set playerShield to 0
                if (incomingDamage.Value >= shield.Value){
                    incomingDamage.ApplyChange(shield.Value, true);
                    currentHP.ApplyChange(incomingDamage, true);
                    Debug.Log("Player blocked: "+shield.Value+" and was hit for: "+ incomingDamage.Value);
                    shield.SetValue(0);
                } 
                // Else, just change player shield hp
                else{
                    shield.ApplyChange(incomingDamage, true);
                    Debug.Log("Player blocked: "+ incomingDamage.Value);
                }
            }
        
        incomingDamage.SetValue(0);
        DamageEvent.Raise(this, currentHP.Value);
        if(currentHP.Value <= 0){
            DeathEvent.Raise(this, true);
            if(hasAnimator)
            animator.SetTrigger("OnDeath");
        } else{
            if(hasAnimator)
            animator.SetTrigger("OnDamaged");
        }
            
    }

    public void TakeDamage(float IC_Damage){
        DMG.SetValue(IC_Damage);
        TakeDamage(DMG);
    }

    public void TakeFireDamage(){
        currentHP.ApplyChange(fireCount, true);
        animator.SetTrigger("OnFire");
    }

    public void TakeShield(float incomingShield){
        shield.ApplyChange(incomingShield);
        if(shield.Value > 0)
            isShielded.SetTrue();
            else
                isShielded.SetFalse();
        shieldEvent.Raise(this, shield.Value);
    }

    public UnitData getData(){
        return unit;
    }

    public void TakeStatus(PlayerData unit){
        weakCount.ApplyChange(unit.weak);
        exposedCount.ApplyChange(unit.expose);
        fireCount.ApplyChange(unit.fire);
    }

    public void DecreaseStatusEffects(){
        if(exposedCount.Value > 0)
        exposedCount.ApplyChange(1,true);
        if(fireCount.Value > 0)
        {
            TakeFireDamage();
            fireCount.ApplyChange(1,true);
        }
        if(weakCount.Value > 0)
        weakCount.ApplyChange(1,true);
    }
    
    public void Heal(float amount){
        currentHP.ApplyChange(amount);
        DamageEvent.Raise(this, amount);
    }
    
    public void ResetPlayer(){
        shield.SetValue(0);
        exposedCount.SetValue(0);
        fireCount.SetValue(0);
        weakCount.SetValue(0);
        DMG.SetValue(0);
    }

}