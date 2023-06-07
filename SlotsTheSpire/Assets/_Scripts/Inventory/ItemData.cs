using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{

    public string displayName, description;
    public Sprite icon;

    //0 = damage, 1 = shield, 2 = fire
    public float modifierType; 
    
    public float modifyPlayer(float amt){
    return amt;
    }

}
