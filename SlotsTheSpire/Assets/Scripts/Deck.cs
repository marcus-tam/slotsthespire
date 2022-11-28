using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<SymbolInventoryItem> deck = new List<SymbolInventoryItem>();
    private Dictionary<SymbolData, SymbolInventoryItem> symbolDictionary = new Dictionary<SymbolData, SymbolInventoryItem>();

    public void AddToDeck(SymbolData symbolData){
        if(symbolDictionary.TryGetValue(symbolData, out SymbolInventoryItem symbol)){
            symbol.AddToCount();
        }
        else{
            SymbolInventoryItem newSymbol = new SymbolInventoryItem(symbolData);
            deck.Add(newSymbol);
            symbolDictionary.Add(symbolData, newSymbol);
        }
    }

    public void RemoveFromDeck(SymbolData symbolData){

        if(symbolDictionary.TryGetValue(symbolData, out SymbolInventoryItem symbol)){
            symbol.RemoveFromCount();
            if(symbol.symbolCount == 0){
                deck.Remove(symbol);
                symbolDictionary.Remove(symbolData);
            }
        }
    }
}
