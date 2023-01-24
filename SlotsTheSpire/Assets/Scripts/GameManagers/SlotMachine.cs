using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public Deck cardDeck;
    public List<SymbolInventoryItem> newDeck = new List<SymbolInventoryItem>();
    public int SlotSpace;
    public FloatVariable OG_Damage, IC_Shield;
    public BattleSystem battleSystem;
    public SymbolInventoryItem symbol;
    public List<Image> artworkList = new List<Image>();

    public void Start() {
        CopyDeck();
        for (int i = 0; i <= SlotSpace; i++)
            artworkList[i].sprite = newDeck[i].symbolData.artwork;
    }

    public void SpinMachine() {
        Shuffle(newDeck);

        for (int i = 0; i <= SlotSpace; i++)
        {
            symbol = newDeck[i];
            CalculateTurn(i);
        }

        battleSystem.PlayerTurn();
    }

    public void CalculateTurn(int o){
        OG_Damage.ApplyChange(symbol.symbolData.Damage);
        IC_Shield.ApplyChange(symbol.symbolData.Shield);
        artworkList[o].sprite = newDeck[o].symbolData.artwork;
    }

    public void CopyDeck() {
        newDeck = cardDeck.deck;
    }

    public void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();

        int n = list.Count;
        while (n > 1) {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }

    }

    public void TriggerEffects(){
        for (int j = 0; j <= SlotSpace; j++){
            newDeck[j].symbolData.PreformEffect();
            if(newDeck[j].symbolData.hasEffect)
            Debug.Log(" "+newDeck[j].symbolData.symbolEffect.GetDescription());
        }
            
    }


    public void EffectCountDown(){
        for (int q = 0; q < newDeck.Count; q++)
            {
                symbol = newDeck[q];
                if(symbol.symbolData.hasEffect)
                symbol.symbolData.symbolEffect.CountDown();
            }
    }

    public void ResetSymbols(){
        for (int l = 0; l < newDeck.Count; l++)
        {
            symbol = newDeck[l];
            if(symbol.symbolData.hasEffect)
            symbol.symbolData.symbolEffect.ResetEffect();
        }
        
    }
}

