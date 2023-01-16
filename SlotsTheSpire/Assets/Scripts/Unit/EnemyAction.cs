using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public List<Action> actionList = new List<Action>();

    public void PerformAction() {
       // fore what ever turn it is do that action
        actionList[0].DoAction(this);
    }

}
