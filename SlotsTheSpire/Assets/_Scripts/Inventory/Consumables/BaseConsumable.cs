using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseConsumable : ScriptableObject
{
   public string description;
   public Sprite artwork;
   public abstract void Consume(GameObject target);
   public abstract void P_Consume(GameObject target);
   public string GetDescription(){
        return description;
    }

}
