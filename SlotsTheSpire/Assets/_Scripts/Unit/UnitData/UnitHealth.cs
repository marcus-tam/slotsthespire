using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class UnitHealth : MonoBehaviour
{
    public float maxHP, currentHP, shield, exposeCount, fireCount, weakCount;
    public FloatVariable DMG;
    public bool isShielded;
    public UnitData unit;
    public Animator animator;
    public bool hasAnimator, isDead;
    
    public GameEvent DamageEvent, shieldEvent, DeathEvent;

    public bool ResetHP;
    public FloatReference StartingHP;

    private void Start()
    {
        if(hasAnimator)
        animator = this.GetComponent<Animator>();
        if (ResetHP) {
            maxHP = StartingHP;
            currentHP = StartingHP;
            DamageEvent.Raise(this, currentHP);
        }
            shield = 0f;
            exposeCount = 0;
            fireCount = 0;
            weakCount = 0;
            DMG.SetValue(0);
            isDead = false;
    }

    public void TakeDamage(FloatVariable incomingDamage){
        if(exposeCount > 0){
            incomingDamage.ApplyChange((float)Math.Ceiling(incomingDamage.Value/2));
        }
        // If enemy is unshielded, damage affects health
        if (!isShielded){
            currentHP -= incomingDamage.Value;
            Debug.Log("hit "+this+": " + incomingDamage.Value);
        }
        else
            {
                //If incoming damage can break shield, subtract shield value from tempDamage and apply new tempDamage to playerHealth. Set playerShield to 0
                if (incomingDamage.Value>= shield){
                    incomingDamage.ApplyChange(shield, true);
                    shield = 0f;
                    currentHP -= incomingDamage.Value;
                } 
                // Else, just change player shield hp
                else{
                    shield -= incomingDamage.Value;
                }
            }
        
        incomingDamage.SetValue(0);
        DamageEvent.Raise(this, currentHP);
        if(currentHP <= 0){
            DeathEvent.Raise(this, true);
            isDead = true;
            if(hasAnimator)
            animator.SetTrigger("OnDeath");
        } else{
            if(hasAnimator)
            animator.SetTrigger("OnDamaged");
        }
            
    }

    public void TakeDamage(float IC_Damage){ 
        DMG.Value = IC_Damage;
        TakeDamage(DMG);
        DMG.SetValue(0);
    }

    public void TakeShield(float incomingShield){
        shield += incomingShield;
        if(shield > 0)
            isShielded = true;
            else
                isShielded= false;
        shieldEvent.Raise(this, shield);
    }

    public void ZeroShield(){
        shield = 0;
    }

    public UnitData getData(){
        return unit;
    }

    public void Maintance(){
        shield = 0;
        if(exposeCount > 0)
        exposeCount--;
        if(fireCount > 0)
        {
            TakeDamage(fireCount);
            fireCount--;
        }
        
        if(weakCount > 0)
        weakCount--;
    }
    
    public void ApplyStatus(float fire, float weak, float expose){
        Debug.Log(this+"; fire: " + fire + " weak: " + weak +" Expose: " + expose );
        fireCount += fire;
        weakCount += weak;
        exposeCount += expose;
    }

    public void Heal(float amount){
        currentHP += amount;
        DamageEvent.Raise(this, amount);
    }

    public float getHealth(){
        return currentHP;
    }
}
