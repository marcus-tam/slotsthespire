using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text  shieldText, hpText, turnSummary;
    public Image weakIcon, exposedIcon, fireIcon;
    public Sprite exposedSpirte, fireSpirte, weakSpirte;
    public FloatVariable playerShield, playerHealth, playerDamage, E_IC_FireDamage, E_FireCount, P_exposedCount, P_fireCount, P_weakCount;
    public PlayerData playerData;


    void Start()
    {
        UpdateHealth();
        UpdateShield();
        checkStatusEffects();
        exposedIcon.sprite = exposedSpirte;
        fireIcon.sprite = fireSpirte;
        weakIcon.sprite = weakSpirte;
    }
    
    public void UpdateHealth(){
        hpText.text = "" + playerHealth.Value;
        checkStatusEffects();
    }

    public void UpdateShield(){
        shieldText.text = "" + playerShield.Value;
    }

    public void UpdateTurnText(){
        turnSummary.text = "Attack: " + playerData.damage + " \nShield: " + playerData.shield;
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