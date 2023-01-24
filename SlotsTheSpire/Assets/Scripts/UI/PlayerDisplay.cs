using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text unitName, shieldText, hpText, turnSummary;
    public Image artwork;
    public FloatVariable playerShield, unitHealth, playerDamage, E_IC_FireDamage, E_FireCount;

    void Start()
    {
        unitName.text = unit.unitName;
        artwork.sprite = unit.unitArtwork;
    }

    void Update() {
        shieldText.text = "" + playerShield.Value;
        hpText.text = "" + unitHealth.Value;
        turnSummary.text = "Attack: " + playerDamage.Value + " \nShield: " + playerShield.Value + "\nFireDmg: " + E_IC_FireDamage.Value + "\nFireCount: " + E_FireCount.Value;
    }


}