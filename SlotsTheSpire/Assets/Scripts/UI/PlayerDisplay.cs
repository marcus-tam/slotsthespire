using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text unitName, shieldText, hpText, turnSummary;
    public Image artwork;
    public FloatVariable playerShield, unitHealth, playerDamage;

    void Start()
    {
        unitName.text = unit.unitName;
        artwork.sprite = unit.unitArtwork;
    }

    void Update() {
        shieldText.text = "" + playerShield.Value;
        hpText.text = "" + unitHealth.Value;
        turnSummary.text = "Attacking for" + playerDamage.Value + " \nShielding for: " + playerShield.Value;
    }


}