using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SymbolInventoryItem 
{

    public SymbolData symbolData;

    public SymbolInventoryItem(SymbolData symbol){
        symbolData = symbol;
    }
}
