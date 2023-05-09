using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameEvent onInventoryItemChange, onInventoryConsumableChange;
    public GameObject target;
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();
    public List<BaseConsumable> consumables = new List<BaseConsumable>();
    public FloatVariable consumableBeltCapacity;
    public int potionIndex;
    public StateManager stateManager;
    public GameState state;


    public void Start(){
        potionIndex = -1;
        state = GameState.POTIONSTATE;
    }


    public void AddItem(ItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            onInventoryItemChange.Raise(this, true);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData); 
            inventory.Add(newItem);
            itemDictionary.Add(itemData,  newItem);
            onInventoryItemChange.Raise(this, true);
        }
    }

    public void RemoveItem(ItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if(item.stackSize == 0 )
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
            onInventoryItemChange.Raise(this, false);
        }
     }

     public void AddConsumable(BaseConsumable consumable){
        if(consumables.Count < (int)consumableBeltCapacity.Value){
            consumables.Add(consumable);
            onInventoryConsumableChange.Raise(this, true);
        }
     }

     public void RemoveConsumable(BaseConsumable consumable){
        consumables.Remove(consumable);
        onInventoryConsumableChange.Raise(this, false);
     }

     public void ConsumeConsumable(Component sender, object data){
        potionIndex = (int) data;
        stateManager.SetState("POTIONSTATE");
     }

     public void SelectTarget(Component sender, object data){
        if(stateManager.GetState() == state && potionIndex != -1){
            target = (GameObject) data;

            if(target.tag == "Player")
            consumables[potionIndex].P_Consume(target);
            else
            consumables[potionIndex].Consume(target);
            RemoveConsumable(consumables[potionIndex]);
            potionIndex = -1;
        }
            
     }

}

