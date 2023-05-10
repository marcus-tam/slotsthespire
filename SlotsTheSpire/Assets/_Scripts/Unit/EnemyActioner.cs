using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActioner : MonoBehaviour
{
    public List<Action> actionList = new List<Action>();
    public FloatVariable turn;
    int randomNumber;

    public void PerformAction(UnitHealth unit) {
                randomNumber = Random.Range(0,actionList.Count);
                actionList[randomNumber].DoAction(this, unit);
    }
    public int getAttack(){
        return actionList[randomNumber].GetAttackType();
    }
}
