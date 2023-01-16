using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderSetter : MonoBehaviour
{
    public Slider Slider;
    public FloatVariable maxVariable;
    public FloatVariable Variable;

    private void Update()
    {
        if (Slider != null && Variable != null)
            Slider.maxValue = maxVariable.Value;
            Slider.value = Variable.Value;
    }
}
