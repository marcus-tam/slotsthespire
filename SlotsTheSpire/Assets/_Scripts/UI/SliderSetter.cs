using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderSetter : MonoBehaviour
{
    public Slider Slider;
    public float maxVariable;
    public float Variable;
    public UnitDisplay unitDisplay;

    private void Update()
    {
        //if (Slider != null && Variable != null)
            Slider.maxValue = unitDisplay.unitMaxHealth;
            Slider.value = unitDisplay.unitHealth;
    }
}
