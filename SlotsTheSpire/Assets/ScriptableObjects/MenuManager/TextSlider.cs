using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;

public class TextSlider : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public void setNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}
