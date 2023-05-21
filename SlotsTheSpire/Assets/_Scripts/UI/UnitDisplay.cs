using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text shieldText, hpText;
    public Image exposeIcon,fireIcon,weakIcon, attackTypeIcon;
    public Sprite exposeSpirte, fireSpirte, weakSpirte;
    public List<Sprite> attackTypeSpirte = new List<Sprite>();
    public float shield, unitHealth, unitMaxHealth;
    int attackType;

    void Start(){
    exposeIcon.sprite = exposeSpirte;
    fireIcon.sprite = fireSpirte;
    weakIcon.sprite = weakSpirte;
    }

    public void UpdateDisplay(GameObject unitPrefab)
    {
        unitMaxHealth = unitPrefab.GetComponent<UnitHealth>().StartingHP;
        unitHealth = unitPrefab.GetComponent<UnitHealth>().currentHP;
        shield = unitPrefab.GetComponent<UnitHealth>().shield;
        shieldText.text = "" + shield;
        hpText.text = "" + unitHealth;
        unit = unitPrefab.GetComponent<UnitHealth>().getData();
        attackType = unitPrefab.GetComponent<EnemyActioner>().getAttack();
        switch (attackType)
        {
            case 0: //attack only
            attackTypeIcon.sprite = attackTypeSpirte[0];
            break;
            case 1://defend only
            attackTypeIcon.sprite = attackTypeSpirte[1];
            break;
            case 2://attack and defend
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
        if(unitPrefab.GetComponent<UnitHealth>().exposeCount > 0)
        exposeIcon.enabled = true;
        else{
            exposeIcon.enabled = false;
        }
        if(unitPrefab.GetComponent<UnitHealth>().fireCount > 0)
       fireIcon.enabled = true;
        else{
            fireIcon.enabled = false;
        }
        if(unitPrefab.GetComponent<UnitHealth>().weakCount > 0)
        weakIcon.enabled = true;
        else{
            weakIcon.enabled = false;
        }
    }

    public void Death(){
        exposeIcon.enabled = false;
        fireIcon.enabled = false;
        weakIcon.enabled = false;
        attackTypeIcon.enabled = false;
    }
}
