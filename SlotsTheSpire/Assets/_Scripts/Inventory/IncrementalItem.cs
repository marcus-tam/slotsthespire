using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("Inventory/item/Incremental"))]
public class IncrementalItem : ItemData
{
    float modifyAmount;
    
    public float ModifyPlayer(float amt)
    {
        return amt += modifyAmount;
    }
}
