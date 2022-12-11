using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Symbol", menuName = "Symbol")]
public class SymbolData : ScriptableObject
{
    public new string name;
    public string description;
    public int symbolID;

    public Sprite artwork;


    public FloatReference size;
    public FloatReference damage;
    public FloatReference block;
    public FloatReference heal;
    public FloatReference dot;
    public FloatReference position;
    public FloatReference turn;

    public void Print(){
        Debug.Log(name +": " + description);
    }
}
