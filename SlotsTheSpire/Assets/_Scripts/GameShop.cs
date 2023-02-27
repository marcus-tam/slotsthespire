using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameShop : MonoBehaviour
{
    public List<Image> symbolArt = new List<Image>();
    public List<TextMeshProUGUI> costText = new List<TextMeshProUGUI>();
    public List<int> symbolCost = new List<int>(); 

    public FloatVariable P_Gold;
    int AmountofSymbols;
    public List<SymbolData> symbolList;
    string symbolDescription;
    public SymbolDatabase symbolDatabase;
    public Deck deck;
    public GameEvent onGoldChange, OnHoveredEvent, OnUnfocusEvent;

    [ContextMenu("GenerateShop")]
    public void GenerateShop()
    {
        AmountofSymbols = 5;
        symbolList = GenerateSymbols();
        DisplayShop();
    }

    public void DisplayShop()
    {
        for(int i = 0; i < AmountofSymbols; i++){
            symbolCost[i] = Random.Range(65,101);
            symbolArt[i].sprite = symbolList[i].artwork;
            //needs implimentation for gold cost based off rarity
            costText[i].text = symbolCost[i].ToString();
            symbolArt[i].enabled = true;
            costText[i].enabled = true;
        }
    }

    public List<SymbolData> GenerateSymbols()
    {
    return symbolDatabase.GenerateRandomList(AmountofSymbols);
    }

    public void BuySymbol(int index){
        if(P_Gold.Value >= symbolCost[index]){
            deck.AddToDeck(symbolList[index]);
            P_Gold.ApplyChange((float)symbolCost[index], true);
            onGoldChange.Raise(this, symbolCost[index]);
        } else{
            Debug.Log("YOU CANNOT AFFORD THAT!");
        }
        
    }
    public void OnHovered(int index){
        symbolDescription = symbolList[index].GetDescription();
        OnHoveredEvent.Raise(this, symbolDescription);
    }

    public void OnUnfocus(){
        OnUnfocusEvent.Raise(this, true);
    }
}
