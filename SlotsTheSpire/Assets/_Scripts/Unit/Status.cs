using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/StatusUI")]
public class Status : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite artwork;
    public FloatVariable variable;
}
