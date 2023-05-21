using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.MapManager;
using UnityEngine;

public class NodeButton : MonoBehaviour
{
    public Point point;
    public int[] asd = new int[2];
    public int x, y;
    public GameEvent onNodeClick;
    
    private void OnMouseDown()
    {
        asd[0] = x;
        asd[1] = y;
        onNodeClick.Raise(this, asd);
    }
}
