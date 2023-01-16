using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text unitName, shieldText, hpText;
    public Image artwork;
    public FloatVariable shield, unitHealth;

    void Start()
    {
        unitName.text = unit.unitName;
        artwork.sprite = unit.unitArtwork;
    }

    void Update() {
        shieldText.text = "" + shield.Value;
        hpText.text = "" + unitHealth.Value;
    }

    public void setHp()
    {
        
    }
}
