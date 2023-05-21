using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolSlot : MonoBehaviour
{
    public Image icon;
    public string description;
    public int ID;
    public GameEvent OnHoveredEvent, OnUnfocusEvent;

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


    public void DrawSlot(SymbolInventoryItem symbol){
        if(symbol == null){
            ClearSlot();
            return;
        }

        icon.enabled = true;
        icon.sprite = symbol.symbolData.artwork;
        description = symbol.symbolData.description;
    }

    public void GetDescription(){
        Debug.Log(description);
    }

    public void setID(int id){
        ID = id;
    }
}
