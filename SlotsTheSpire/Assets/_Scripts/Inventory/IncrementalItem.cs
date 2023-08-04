using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("Inventory/item/Incremental"))]
public class IncrementalItem : ItemData
{
    public float modifyAmount;

    public override float ModifyPlayerFloat(float amt)
    {
        Debug.Log("Modifying: "+amt+" by "+ modifyAmount);
        return amt += modifyAmount;
    }
}
