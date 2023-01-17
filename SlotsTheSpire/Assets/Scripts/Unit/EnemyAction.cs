using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public List<Action> actionList = new List<Action>();

    public void PerformAction() {

        actionList[0].DoAction(this);
    }
    public string getAttack(){
        return actionList[0].GetDescription();
    }
}
