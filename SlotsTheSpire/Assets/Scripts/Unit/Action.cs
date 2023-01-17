using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action: ScriptableObject
{
    public abstract void DoAction(EnemyAction action);
    public abstract string GetDescription();
}
