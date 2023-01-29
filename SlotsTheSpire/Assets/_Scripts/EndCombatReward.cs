using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCombatReward : MonoBehaviour
{
    // 0 = common, 1 = uncommon, 2 = rare, 3 = elite, 4 = boss
    public int fightRarity, cardChance, newSymbolID, cardRewardAmount;
    //S_ Symbol
    public float goldDropMultiplyer, S_uncommonDropChance, S_rareDropChance, goldAmount;

    public Inventory inventory;
    public Deck deck;
    public SymbolDatabase symbolDatabase;
    public FloatVariable gold;
    public GameEvent OnCardSelect;
    public SymbolData tempSymbol,newSymbol;

    public List<Image> symbolImageList;
    public List<SymbolData> symbolList;

    public void SetupRewards(){
        for(int x = 0; x < cardRewardAmount; x++){
            newSymbol = GenerateCardReward();
            if(tempSymbol != newSymbol)
            {
                symbolList.Add(newSymbol);
            }
            else{
                while(tempSymbol == newSymbol)
                newSymbol = GenerateCardReward();
            }

            tempSymbol = newSymbol;

        }
        for(int i = 0; i < cardRewardAmount; i++){
            symbolImageList[i].sprite = symbolList[i].artwork;
            Debug.Log("Setting image "+ i);
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
        int randomNumber = Random.Range(10, 21); //gives 10 - 20 gold * mulitplayer
        goldAmount = Mathf.Round(randomNumber * goldDropMultiplyer);
        gold.ApplyChange(goldAmount);
    }

    public SymbolData GenerateCardReward(){
         cardChance = Random.Range(1, 101);
       /* if(cardChance < S_rareDropChance){
            //drop rare
        }else if(cardChance < S_uncommonDropChance){
            //drop uncommon
        }else{
            //drop common
        }
        */
        return symbolDatabase.GenerateRandom();
    }

    public void AddCard(int index){
        deck.AddToDeck(symbolList[index]);
    }



}
