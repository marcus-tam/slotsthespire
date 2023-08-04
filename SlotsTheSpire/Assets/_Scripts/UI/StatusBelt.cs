using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBelt : MonoBehaviour
{
    public GameObject slotPrefab;
    public int totalStatus;
    public List<StatusSlot> StatusSlot = new List<StatusSlot>();
    public List<Status> statusList = new List<Status>();
    public List<Status> activeStatusList = new List<Status>();
    public GameEvent OnHoveredEvent, OnUnfocusEvent;
    int idCounter;

    public void ResetBelt(){
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        StatusSlot = new List<StatusSlot>(totalStatus);
        idCounter = 0;
    }

    public void DrawBelt(List<Status> status){
        ResetBelt();
        for(int i = 0; i<StatusSlot.Capacity; i++){
            CreateBeltSlot();
            idCounter++;
        }
        for(int i = 0; i < status.Count; i++){
            StatusSlot[i].DrawSlot(status[i]);
        }
    }

    public void DrawBelt(){
        activeStatusList.Clear();
        foreach(Status i in statusList){
            if(i.variable.Value > 0)
            activeStatusList.Add(i);
        }
        DrawBelt(activeStatusList);
    }

    public void CreateBeltSlot(){
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.GetComponent<StatusSlot>().setID(idCounter);
        newSlot.transform.SetParent(transform, false);

        StatusSlot newSlotComponent = newSlot.GetComponent<StatusSlot>();
        newSlotComponent.ClearSlot();

        StatusSlot.Add(newSlotComponent);
    }
}
