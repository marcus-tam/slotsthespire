using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UNITTYPE { Common, Uncommon, Rare, Elite, Boss, Special }

[CreateAssetMenu (menuName = "Unit/Data")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public Sprite unitArtwork;

    public UNITTYPE unitType; 

    public FloatReference maxHp;
    public FloatReference currentHp;

}
