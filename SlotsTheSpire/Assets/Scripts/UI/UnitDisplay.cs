using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text unitName, shieldText, hpText, turnText;
    public Image artwork;
    public FloatVariable shield, unitHealth;

    void Update() {
        shieldText.text = "" + shield.Value;
        hpText.text = "" + unitHealth.Value;
    }

    public void UpdateDisplay(GameObject unitPrefab)
    {
        unit = unitPrefab.GetComponent<UnitHealth>().getData();
        unitName.text = unit.unitName;
        artwork.sprite = unit.unitArtwork;
        turnText.text = unitPrefab.GetComponent<EnemyActioner>().getAttack();
    }
}
