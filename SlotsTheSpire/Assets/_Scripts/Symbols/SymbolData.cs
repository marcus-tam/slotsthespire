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

    public FloatReference Damage, upgradedDamage, startingDamage;
    public FloatReference Shield, upgradedShield, startingShield;
    public FloatReference Heal;
    public FloatReference Pos;
    public bool hasEffect, upgradeEffect;
    public bool upgraded ;

    public BaseEffect symbolEffect;
    //add list of effects

    public void PreformEffect(){
        if(hasEffect)
            symbolEffect.DoEffect();
        else
            return;
   }

   public string GetDescription(){
        return description;
   }

    public void Upgrade(){
        if(upgradeEffect)
        symbolEffect.Upgrade();

        Damage = upgradedDamage;
        Shield = upgradedShield;
        description = upgradedDescription;
    }

    public void Downgrade(){
        if(upgradeEffect)
        symbolEffect.Downgrade();
        
        Damage = startingDamage;
        Shield = startingShield;
        description = startingDescription;
    }
}
