using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScreen : MonoBehaviour
{

    public GameObject slotPrefab;
    public List<SymbolInventoryItem> newDeck = new List<SymbolInventoryItem>();
    public List<SymbolSlot> symbolSlots = new List<SymbolSlot>(2);
    int idCounter;

    public void ResetDeck(int symbolCount){
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        symbolSlots = new List<SymbolSlot>(symbolCount);
        idCounter = 0;
    }

    public void getDeck(Component c, object deck){
        Debug.Log("Getting Deck");
        newDeck = (List<SymbolInventoryItem>)deck;
        ResetDeck(newDeck.Count);
        for(int i = 0; i<symbolSlots.Capacity; i++){
            CreateDeckSlot();
            idCounter++;
        }
        for(int i = 0; i < newDeck.Count; i++){
            symbolSlots[i].DrawSlot(newDeck[i]);
        }
        
    }

    public void CreateDeckSlot(){
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.GetComponent<SymbolSlot>().setID(idCounter);
        newSlot.transform.SetParent(transform, false);

        SymbolSlot newSlotComponent = newSlot.GetComponent<SymbolSlot>();
        newSlotComponent.ClearSlot();

        symbolSlots.Add(newSlotComponent);
    }
}
