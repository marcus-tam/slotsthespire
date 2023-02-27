using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("Inventory/Consumable/damage"))]
public class DamageConsumable : BaseConsumable
{
    public float amount;
    
    public override void Consume(GameObject Target){
        Target.GetComponent<UnitHealth>().TakeDamage(amount);
    }
}