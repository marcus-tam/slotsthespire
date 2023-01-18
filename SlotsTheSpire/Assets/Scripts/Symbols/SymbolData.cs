using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Symbols/Symbols")]
public class SymbolData : ScriptableObject
{
    public new string name;
    public string description;
    public int symbolID;

    public Sprite artwork;


    public FloatReference Size;
    public FloatReference Damage;
    public FloatReference Shield;
    public FloatReference Heal;
    public FloatReference Pos;
    public FloatReference Turn;
    public bool hasEffect, resetEffect;
    public Effect symbolEffect;
    

    public void Print(){
        Debug.Log(name +": " + description);
   }

    public void PreformEffect(){
        if(hasEffect)
            symbolEffect.DoEffect();
        else
            return;
   }

}
