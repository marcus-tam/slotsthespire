using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Symbols/Symbols")]
public class SymbolData : ScriptableObject
{
    public new string name;
    public string description, upgradedDescription, startingDescription;
    public int ID = -1;

    public Sprite artwork;

    public FloatReference damage, upgradedDamage, startingDamage;
    public FloatReference shield, upgradedShield, startingShield;
    public FloatReference Heal;
    public FloatReference Pos;
    public float weak, expose, fire;
    public bool upgraded ;

    public string GetDescription(){
        return description;
   }
}
