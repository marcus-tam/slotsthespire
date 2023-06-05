using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("Inventory/Consumable/AOEdamage"))]
public class AOEDamageConsumable : BaseConsumable
{
   public float amount;
   public GameEvent OnAoeConsumable;

    public override void Consume(GameObject Target)
    {
        OnAoeConsumable.Raise(null, amount);
    }

    public override void P_Consume(GameObject Target)
    {
        Target.GetComponent<PlayerHealth>().TakeDamage(amount);
    }
}
