using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Symbols/Symbols")]
public class SymbolData : ScriptableObject
{
    public new string name;
    public string description;
    public int ID = -1;

    public Sprite artwork;


    public FloatReference Size;
    public FloatReference Damage;
    public FloatReference Shield;
    public FloatReference Heal;
    public FloatReference Pos;
    public FloatReference Turn;
    public bool hasEffect;

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

}
