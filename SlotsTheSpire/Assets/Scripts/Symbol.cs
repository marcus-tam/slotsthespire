using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Symbol", menuName = "Symbol")]
public class Symbol : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;

    public int size;
    public int damage;
    public int block;
    public int heal;
    public int dot;
    public int turn;
}
