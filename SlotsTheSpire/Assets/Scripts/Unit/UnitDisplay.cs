using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{

    public UnitData unit;
    public Text unitName;
    public Image artwork;

    void Start()
    {
        unitName.text = unit.unitName;
        artwork.sprite = unit.unitArtwork;
    }

    public void setHp()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
