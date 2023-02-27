using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableBelt : MonoBehaviour
{
    
    public GameObject slotPrefab;
    public Inventory inventory;
    public FloatVariable consumableBeltCapacity;
    public List<ConsumableSlot> consumableSlots = new List<ConsumableSlot>(2);
    int idCounter;

    public void ResetBelt(){
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        consumableSlots = new List<ConsumableSlot>((int)consumableBeltCapacity.Value);
        idCounter = 0;
    }

    public void DrawBelt(List<BaseConsumable> consumable){
        ResetBelt();
        for(int i = 0; i<consumableSlots.Capacity; i++){
            CreateBeltSlot();
            idCounter++;
        }
        for(int i = 0; i < consumable.Count; i++){
            consumableSlots[i].DrawSlot(consumable[i]);
        }
    }

    public void DrawBelt(){
        DrawBelt(inventory.consumables);
    }

    public void CreateBeltSlot(){
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.GetComponent<ConsumableSlot>().setID(idCounter);
        newSlot.transform.SetParent(transform, false);

        ConsumableSlot newSlotComponent = newSlot.GetComponent<ConsumableSlot>();
        newSlotComponent.ClearSlot();

        consumableSlots.Add(newSlotComponent);
    }

}
