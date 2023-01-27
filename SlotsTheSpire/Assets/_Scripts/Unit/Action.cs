using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action: ScriptableObject
{
    public abstract void DoAction(EnemyActioner action);
    public abstract string GetDescription();
}