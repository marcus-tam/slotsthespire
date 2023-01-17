using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public Deck cardDeck;
    public List<SymbolInventoryItem> newDeck = new List<SymbolInventoryItem>();
    public int SlotSpace;
    public FloatVariable damage, shield;
    public BoolVariable isShielded;
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
            damage.ApplyChange(symbol.symbolData.Damage);
            shield.ApplyChange(symbol.symbolData.Shield);
            symbol.symbolData.PreformEffect();
            artworkList[i].sprite = newDeck[i].symbolData.artwork;
        }
        if (shield.Value <= 0)
            isShielded.setFalse();
        else
            isShielded.setTrue();
        
        battleSystem.PlayerTurn();
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

    public void ResetSymbols(){
        for (int i = 0; i < newDeck.Count; i++)
        {
            symbol = newDeck[i];
            symbol.symbolData.ResetEffect();
        }
        
    }
}

