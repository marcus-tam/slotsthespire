using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolDisplay : MonoBehaviour
{

    public Symbol symbol;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(symbol.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
