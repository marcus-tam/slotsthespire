using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public List<Action> actionList = new List<Action>();
    public FloatVariable turn;

    public void PerformAction() {
        if(turn.Value == 0f)
        actionList[0].DoAction(this);
        else
        actionList[1].DoAction(this);
    }
    public string getAttack(){
        if(turn.Value == 0f)
        return actionList[0].GetDescription();
        else
        return actionList[1].GetDescription();
    }
}
