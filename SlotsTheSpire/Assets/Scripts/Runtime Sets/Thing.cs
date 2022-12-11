using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    public ThingRuntimeSet runtimeSet;

    public void OnEnable()
    {
        runtimeSet.Add(this);
    }
        

    public void OnDisable() 
    {
        runtimeSet.Remove(this);
    }
}
