using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitData : ScriptableObject
{
    public string unitName;
    public Sprite unitArtwork;

    public FloatReference maxHp;
    public FloatReference currentHp;

    public FloatReference damage;

}
