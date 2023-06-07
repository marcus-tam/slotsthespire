using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScroll : MonoBehaviour
{
    public Slider slider;
    public GameObject square;
    private float minY;
    private float maxY;
    
    private void Start()
    {
        minY = -1f;
        maxY = 12f;
        
        slider.minValue = minY;
        slider.maxValue = maxY;

        slider.value = slider.maxValue;
    }

    private void Update()
    {
        square.transform.position = new Vector3(3f, slider.value, 0);
    }
}
