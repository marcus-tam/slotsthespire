using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public FloatVariable P_Gold;
    public TMP_Text goldText;

    void Start()
    {
        goldText.text = "" + P_Gold.Value;
    }

   public void UpdateGoldText(){
        goldText.text = "" + P_Gold.Value;
   }
}
