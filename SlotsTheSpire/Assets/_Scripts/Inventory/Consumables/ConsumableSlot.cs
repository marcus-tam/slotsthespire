using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableSlot : MonoBehaviour
{
    public Image icon;
    public string description;
    public Button button;
    public int ID;
    public GameEvent ConsumablePressed, OnHoveredEvent, OnUnfocusEvent;


    public void OnButtonPress(){
        ConsumablePressed.Raise(this, ID);
    }

    public void OnHovered(){
        OnHoveredEvent.Raise(this, description);
    }

    public void OnUnfocus(){
        OnUnfocusEvent.Raise(this, true);
    }

    public void ClearSlot()
    {
        icon.enabled = false;
    }


    public void DrawSlot(BaseConsumable Consumable){
        if(Consumable == null){
            ClearSlot();
            return;
        }

        icon.enabled = true;
        icon.sprite = Consumable.artwork;
        description = Consumable.description;
    }

    public void GetDescription(){
        Debug.Log(description);
    }

    public void setID(int id){
        ID = id;
    }
}
