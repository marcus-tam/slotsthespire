using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public List<BaseEffect> effectList = new List<BaseEffect>();

    public void ApplyEffect(){
        foreach (BaseEffect effect in effectList)
        {
            effect.DoEffect();
        }
        
    }

}
