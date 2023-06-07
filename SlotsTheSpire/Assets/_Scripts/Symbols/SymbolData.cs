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

    public float damage, upgradedDamage, startingDamage;
    public float shield, upgradedShield, startingShield;
    public float Heal;
    public float Pos;
    public float weak, expose, fire;
    public bool upgraded;

    public string GetDescription(){
        return description;
   }

   public void CopySymbol(SymbolData data)
   {
    name = data.name;
    description = data.description;
    damage = data.damage;
    shield = data.shield;
    fire = data.fire;
    expose = data.expose;
    weak = data.weak;
    Heal = data.Heal;
   }
}
