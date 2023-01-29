using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActioner : MonoBehaviour
{
    public List<Action> actionList = new List<Action>();
    public FloatVariable turn;
    int randomNumber;

    public void PerformAction() {
        if(turn.Value == 1)
            actionList[1].DoAction(this);
            else {
                randomNumber = Random.Range(0,actionList.Count);
                actionList[randomNumber].DoAction(this);
            }

        
    }
    public string getAttack(){
        if(turn.Value == 1 || turn.Value == 0)
            return actionList[1].GetDescription();
            else
                return actionList[randomNumber].GetDescription();
    }
}
