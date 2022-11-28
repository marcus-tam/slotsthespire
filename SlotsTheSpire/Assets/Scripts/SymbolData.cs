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

    public int size;
    public int damage;
    public int block;
    public int heal;
    public int dot;
    public int position;
    public int turn;

    

    public void Print(){
        Debug.Log(name +": " + description);
    }
}
