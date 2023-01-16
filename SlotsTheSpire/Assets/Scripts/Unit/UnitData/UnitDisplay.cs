using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text unitName, shieldText;
    public Image artwork;
    public FloatVariable shield;

    void Start()
    {
        unitName.text = unit.unitName;
        artwork.sprite = unit.unitArtwork;
    }

    void Update() {
        shieldText.text = "" + shield.Value;
    }

    public void setHp()
    {
        
    }
}
