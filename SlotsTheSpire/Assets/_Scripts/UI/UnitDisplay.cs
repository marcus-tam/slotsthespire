using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text shieldText, hpText;
    public Image exposedIcon,fireIcon,weakIcon, attackTypeIcon;
    public Sprite exposedSpirte, fireSpirte, weakSpirte;
    public List<Sprite> attackTypeSpirte = new List<Sprite>();
    public FloatVariable shield, unitHealth, E_exposedCount, E_fireCount, E_weakCount;
    int attackType;

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
        attackType = unitPrefab.GetComponent<EnemyActioner>().getAttack();
        switch (attackType)
        {
            case 0:
            attackTypeIcon.sprite = attackTypeSpirte[0];
            break;
            case 1:
            attackTypeIcon.sprite = attackTypeSpirte[1];
            break;
            case 2:
            attackTypeIcon.sprite = attackTypeSpirte[2];
            break;
            case 3:
            attackTypeIcon.sprite = attackTypeSpirte[3];
            break;
            case 4:
            attackTypeIcon.sprite = attackTypeSpirte[4];
            break;
            case 5:
            attackTypeIcon.sprite = attackTypeSpirte[5];
            break;
            case 6:
            attackTypeIcon.sprite = attackTypeSpirte[6];
            break;
            default:
            Debug.Log("AttackType Cases defaulted");
            break;
        }

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
