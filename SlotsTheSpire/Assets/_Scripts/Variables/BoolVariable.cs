using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName="Variable/Bool")]
public class BoolVariable : ScriptableObject
{
    public bool boolean;

    public void SetFalse() {
        boolean = false;
    }

    public void SetTrue() {
        boolean = true;
    }

}
