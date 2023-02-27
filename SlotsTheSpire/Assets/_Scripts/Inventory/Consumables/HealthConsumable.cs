using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("Inventory/Consumable/health"))]
public class HealthConsumable : BaseConsumable
{
    public float amount;

    public override void Consume(GameObject target){
        float difference;
        difference = target.GetComponent<UnitHealth>().maxHP.Value - target.GetComponent<UnitHealth>().currentHP.Value;
        if(difference >= amount)
        target.GetComponent<UnitHealth>().Heal(amount);
        if(difference < amount )
        target.GetComponent<UnitHealth>().Heal(difference);

    }
}
