using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text unitName, shieldText, hpText, turnText;
    public Image artwork,exposedIcon,fireIcon,weakIcon;
    public Sprite exposedSpirte, fireSpirte, weakSpirte;
    public FloatVariable shield, unitHealth, E_exposedCount, E_fireCount, E_weakCount;

    void Start(){
    exposedIcon.sprite = exposedSpirte;
    fireIcon.sprite = fireSpirte;
    weakIcon.sprite = weakSpirte;
    }

    void Update() {
        shieldText.text = "" + shield.Value;
        hpText.text = "" + unitHealth.Value;
        checkStatusEffects();
    }

    public void UpdateDisplay(GameObject unitPrefab)
    {
        unit = unitPrefab.GetComponent<UnitHealth>().getData();
        unitName.text = unit.unitName;
        artwork.sprite = unit.unitArtwork;
        turnText.text = unitPrefab.GetComponent<EnemyActioner>().getAttack();
    }

    public void checkStatusEffects(){
        if(E_exposedCount.Value > 0)
        exposedIcon.enabled = true;
        else{
            exposedIcon.enabled = false;
        }
        if(E_fireCount.Value > 0)
       fireIcon.enabled = true;
        else{
            fireIcon.enabled = false;
        }
        if(E_weakCount.Value > 0)
        weakIcon.enabled = true;
        else{
            weakIcon.enabled = false;
        }
    }
}
