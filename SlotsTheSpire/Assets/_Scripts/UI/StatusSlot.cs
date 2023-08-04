using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusSlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
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
        text.enabled = false;
    }


    public void DrawSlot(Status status){
        if(status == null){
            ClearSlot();
            return;
        }

        icon.enabled = true;
        icon.sprite = status.artwork;
        description = status.description;
        text.enabled = true;
        text.text = ""+status.variable.Value;
    }

    public void GetDescription(){
        Debug.Log(description);
    }

    public void setID(int id){
        ID = id;
    }
}
