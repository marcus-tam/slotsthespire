using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SymbolInventoryItem 
{

    public SymbolData symbolData;
    public int symbolCount;

    public SymbolInventoryItem(SymbolData symbol){
        symbolData = symbol;
        AddToCount();
    }

     public void AddToCount(){
        symbolCount++;
    }

    public void RemoveFromCount(){
        symbolCount--;
    } 
}
