using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerSliderSetting : MonoBehaviour
{
    public Slider Slider;
    public FloatVariable p_MaxHP;
    public FloatVariable p_CurrentHP;

    void Update()
    {
         if (Slider != null && p_CurrentHP != null)
            Slider.maxValue = p_MaxHP.Value;
            Slider.value = p_CurrentHP.Value;
    }
   
}
