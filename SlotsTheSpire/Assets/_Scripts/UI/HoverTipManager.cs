using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class HoverTipManager : MonoBehaviour
{

    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;
    private float timeToWait = 0.5f;

    void Start()
    {
        HideTip();
    }

    public void GetTip(Component c, object tip){
        ShowTip(this, tip.ToString(), Input.mousePosition);
    }

    public void ShowTip(Component c, string tip, Vector2 mousePos)
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());

        tipText.text = tip;
        tipWindow .sizeDelta = new Vector2(tipText.preferredWidth > 400 ? 400 : tipText.preferredWidth, tipText.preferredHeight);
        
        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x, mousePos.y );
    }

    public void HideTip()
    {
        StopAllCoroutines();
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);
    }
}
