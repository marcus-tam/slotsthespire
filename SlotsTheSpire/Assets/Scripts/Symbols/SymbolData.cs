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


    public FloatReference Size;
    public FloatReference Damage;
    public FloatReference Shield;
    public FloatReference Heal;
    public FloatReference DOT;
    public FloatReference Pos;
    public FloatReference Turn;

    public void Print(){
        Debug.Log(name +": " + description);
    }
}
