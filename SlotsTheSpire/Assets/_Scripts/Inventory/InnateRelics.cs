using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("Inventory/item/Innate"))]
public class InnateRelics : ItemData
{
    public FloatVariable target;
    public new float amount;

    public override float ModifyPlayerFloat(float amt){
        target.ApplyChange(amount);
        return amt;
    }

}
