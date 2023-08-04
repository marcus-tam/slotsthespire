using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text  shieldText, hpText, turnSummary;
    public FloatVariable playerHealth, playerDamage, E_FireCount, P_exposedCount, P_fireCount, P_weakCount, P_Strength, P_Dex;
    public PlayerData playerData;


    void Start()
    {
        UpdateHealth();
        UpdateShield();
    }
    
    public void UpdateHealth(){
        hpText.text = "" + playerHealth.Value;
    }

    public void UpdateShield(){
        shieldText.text = "" + playerData.shield;
    }

    public void UpdateTurnText(){
        turnSummary.text = "Attack: " + playerData.damage + " \nShield: " + playerData.shield;
    }

}