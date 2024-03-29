using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndCombatReward : MonoBehaviour
{
    // 0 = common, 1 = uncommon, 2 = rare, 3 = elite, 4 = boss
    public int fightRarity, cardChance, newSymbolID, symbolRewardAmount;
    //S_ Symbol
    public float goldDropMultiplyer, S_uncommonDropChance, S_rareDropChance, goldAmount;

    public Inventory inventory;
    public Deck deck;
    public SymbolDatabase symbolDatabase;
    public FloatVariable gold;
    public GameEvent OnCardSelect, OnChangeGold, OnHoveredEvent, OnUnfocusEvent;
    //public SymbolData tempSymbol,newSymbol;
    public TMPro.TMP_Text goldText;
    public List<Image> symbolImageList;
    public List<SymbolData> symbolList;
    int randGold;
    string symbolDescription;

    public void SetupRewards(){
         randGold = Random.Range(10, 21);
         goldText.text = "Gold: " + randGold;
       /* for(int x = 0; x < symbolRewardAmount; x++){
            newSymbol = GenerateSymbolReward();
                while(tempSymbol == newSymbol){
                    newSymbol = GenerateSymbolReward();
                }
            Debug.Log("Adding " + newSymbol.name + " to deck");
            symbolList.Add(newSymbol);
            tempSymbol = newSymbol;
        
        }*/
        symbolList = GenerateSymbolReward();
        for(int i = 0; i < symbolRewardAmount; i++){
            symbolImageList[i].sprite = symbolList[i].artwork;
        }
    }

    public void setupChances(int rarity){
        fightRarity = rarity;
        switch (fightRarity){
            case 0:
                goldDropMultiplyer = 1.0f;
                S_uncommonDropChance = 25f;
                S_rareDropChance = 5f;
                break;
            case 1: 
                goldDropMultiplyer = 1.25f;
                S_uncommonDropChance = 35f;
                S_rareDropChance = 10f;
                break;
            case 2: 
                goldDropMultiplyer = 1.50f;
                S_uncommonDropChance = 45f;
                S_rareDropChance = 15f;
                break;
            case 3:
                goldDropMultiplyer = 2f;
                S_uncommonDropChance = 55f;
                S_rareDropChance = 25f;
                break;
            case 4:
                goldDropMultiplyer = 3f;
                S_uncommonDropChance = 0f;
                S_rareDropChance = 100f;
                break;
        }
    }


    public void GiveGold(){
        goldAmount = Mathf.Round(randGold * goldDropMultiplyer);
        gold.ApplyChange(goldAmount);
        OnChangeGold.Raise(this, gold.Value);
    }

    public List<SymbolData> GenerateSymbolReward(){
        Debug.Log("Im generating symbols");
         cardChance = Random.Range(1, 101);
       /* if(cardChance < S_rareDropChance){
            //drop rare
        }else if(cardChance < S_uncommonDropChance){
            //drop uncommon
        }else{
            //drop common
        }
        */
        Debug.Log("Calling Generate Random with an count of " + symbolRewardAmount);
        return symbolDatabase.GenerateRandomList(symbolRewardAmount);
    }

    public void AddCard(int index){
        deck.AddToDeck(symbolList[index]);
        OnCardSelect.Raise(this, symbolList[index]);
    }

     public void OnHovered(int index){
        symbolDescription = symbolList[index].GetDescription();
        OnHoveredEvent.Raise(this, symbolDescription);
    }

    public void OnUnfocus(){
        OnUnfocusEvent.Raise(this, true);
    }
}
