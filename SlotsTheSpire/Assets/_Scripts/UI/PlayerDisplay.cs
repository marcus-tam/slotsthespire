using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text unitName, shieldText, hpText, turnSummary;
    public Image artwork, weakIcon, exposedIcon, fireIcon;
    public Sprite exposedSpirte, fireSpirte, weakSpirte;
    public FloatVariable playerShield, unitHealth, playerDamage, E_IC_FireDamage, E_FireCount, P_exposedCount, P_fireCount, P_weakCount;


    void Start()
    {
        unitName.text = unit.unitName;
        artwork.sprite = unit.unitArtwork;
        UpdateHealth();
        UpdateShield();
        checkStatusEffects();
        exposedIcon.sprite = exposedSpirte;
        fireIcon.sprite = fireSpirte;
        weakIcon.sprite = weakSpirte;
    }
    
    public void UpdateHealth(){
        hpText.text = "" + unitHealth.Value;
    }

    public void UpdateShield(){
        shieldText.text = "" + playerShield.Value;
    }

    public void UpdateTurnText(){
        turnSummary.text = "Attack: " + playerDamage.Value + " \nShield: " + playerShield.Value + "\nFireDmg: " + E_IC_FireDamage.Value;
        checkStatusEffects();
    }

    public void checkStatusEffects(){
        if(P_exposedCount.Value > 0)
        exposedIcon.enabled = true;
        else{
            exposedIcon.enabled = false;
        }
        if(P_fireCount.Value > 0)
       fireIcon.enabled = true;
        else{
            fireIcon.enabled = false;
        }
        if(P_weakCount.Value > 0)
        weakIcon.enabled = true;
        else{
            weakIcon.enabled = false;
        }
    }
}