using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Unit/Data")]
public class UnitData : UnitBase
{
    public string unitName;
    public Sprite unitArtwork;

    public FloatReference maxHp;
    public FloatReference currentHp;

    public FloatReference damage;

    public override void DoTurn() { }
}
