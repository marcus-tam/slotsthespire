using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SlotMachine : MonoBehaviour
{
    public Deck cardDeck;
    public PlayerData playerData;
    public Inventory inventory;
    public List<SymbolInventoryItem> newDeck = new List<SymbolInventoryItem>();
    public List<SymbolInventoryItem> discardDeck = new List<SymbolInventoryItem>();
    public List<SymbolInventoryItem> activeDeck = new List<SymbolInventoryItem>();
    public List<SymbolInventoryItem> container = new List<SymbolInventoryItem>();
    public int  LockIndex, listSize;
    public FloatVariable P_WeakCount, P_Rolls, slotSpace;
    public BoolVariable P_Locked;
    public BattleSystem battleSystem;
    public SymbolInventoryItem symbol, lockInSymbol;
    public SymbolData cloneSymbolData;
    public List<Image> artworkList = new List<Image>();
    public TextMeshProUGUI rerollsText, deckText, discardText;
    public GameEvent OnHoveredEvent, OnUnfocusEvent, CheckDeck, CheckDiscard, EndTurn, reroll;
    public Button deckButton, discardButton;

    System.Random spin = new System.Random();
    int slotNum, symbNum;
    bool spinning; 

    public void Start() {
        CopyDeck();
        SetupArt();
        newDeck = Shuffle(newDeck);
        LockIndex = -1;
        P_Locked.SetFalse();
        P_Rolls.SetValue(2);
        UpdateText();
    }

    public void Update(){
        if(battleSystem.state == BattleState.PLAYERTURN){}
           // RandomizeArt();
    }

    public void SpinMachine() {
        for (int i = 0; i < slotSpace.Value; i++)
        {
            if(newDeck.Count == 0 && discardDeck.Count != 0)
            {
                ShuffleNewDeck();
            }
            if(newDeck.Count == 0 && discardDeck.Count == 0)
            {
                symbol = null;
                continue;
            }
            if(i == LockIndex){
               symbol = lockInSymbol; 
            }else {
                symbol = newDeck[0];
                newDeck.RemoveAt(0);
            }
            if(symbol != null){
            activeDeck.Add(symbol);
            CalculateTurn(i); 
            }
            if(symbol == null)
            Debug.Log("Empty symbol slot: " +i);
        }
        if(LockIndex != -1)
        Unlock();
        SetupArt();
        P_Rolls.ApplyChange(1,true);
        if(newDeck.Count == 0 ){
            ShuffleNewDeck();
        }
        UpdateText();
    }

    public void ShuffleNewDeck(){
        newDeck = new List<SymbolInventoryItem>(Shuffle(discardDeck));
        discardDeck.Clear();
    }

    public void Discard(){
        for(int i = activeDeck.Count-1 ; i >= 0; i--){
            if(i == LockIndex){
                newDeck.Remove(activeDeck[i]);
            }
            else{
                discardDeck.Add(activeDeck[i]);
                newDeck.Remove(activeDeck[i]);
            }
            
        }
    }

    public void SetupArt(){
        for (int i = 0; i < slotSpace.Value; i++){
            if(activeDeck.Count != 0 && i != activeDeck.Count)
            artworkList[i].sprite = activeDeck[i].symbolData.artwork;
        }
    }

    public void RandomizeArt(){
        symbNum = spin.Next(0, newDeck.Count);
        slotNum = spin.Next(0, (int)slotSpace.Value);
        artworkList[slotNum].sprite = newDeck[symbNum].symbolData.artwork;
    }

    public void CalculateTurn(int o){
        cloneSymbolData = inventory.checkItemModifier(activeDeck[o].symbolData);
        if(playerData.strength.Value != 0 && cloneSymbolData.damage != 0){
            cloneSymbolData.ApplyStrength(playerData.strength.Value);
            Debug.Log("Applying " +playerData.strength.Value+ " strength");
        }
        
        if(playerData.dex.Value != 0 && cloneSymbolData.shield != 0){
            cloneSymbolData.ApplyDex(playerData.dex.Value);
            Debug.Log("Applying " +playerData.dex.Value+ " dex");
        }
        
            switch (symbol.symbolData.Pos)
            {
                case 0:
                //Front Enemy
                    break;
                case 1:
                //Flank Enemy
                    playerData.SwitchType(1);
                    break;
                case 2:
                //AOE (All enemies)
                    playerData.SwitchType(2);
                    break;
                default:
                    Debug.Log("Symbol " + symbol.symbolData.name + " doesnt not have a position target");
                    break;
            }
        Debug.Log(""+symbol.symbolData.Pos);
        artworkList[o].sprite = activeDeck[o].symbolData.artwork;
        CalculatePlayerData(cloneSymbolData);
        artworkList[o].sprite = activeDeck[o].symbolData.artwork;
    }

    public void CalculatePlayerData(SymbolData calculatedData){

        playerData.AddDamage(calculatedData.damage);
        playerData.AddShield(calculatedData.shield);
        playerData.AddFire(calculatedData.fire);
        playerData.AddWeak(calculatedData.weak);
        playerData.AddExpose(calculatedData.expose);
    }

    public void CopyDeck() {
        for(int i = 0; i < cardDeck.deck.Count; i++){
            newDeck.Add(cardDeck.deck[i]);
        }
        
    }

    public List<SymbolInventoryItem> Shuffle(List<SymbolInventoryItem> list){
        listSize = list.Count;
        var rand = new System.Random();
        int randomIndex=0;
            for(int i = 0; i < listSize; i++)
            {
            container[0] = list[i];
            randomIndex = (int)rand.Next(i, listSize);
            list[i] = list[randomIndex];
            list[randomIndex] = container[0];
            }
        return list;
    }

    public void ResetSymbols(){
        for (int l = 0; l < newDeck.Count; l++)
        {
            symbol = newDeck[l];
        }
    }

    public void LockIn(int index){

        if(P_Locked.Value == false && P_Rolls.Value != 0 && P_Rolls.Value != 2 && activeDeck.Count-1 >= index){
            LockIndex = index;
            lockInSymbol = activeDeck[LockIndex];
            artworkList[index].color = Color.blue;
            P_Locked.SetTrue();
        }
        else if(P_Locked.Value == true && index == LockIndex)
        {
            Unlock();
        }
        
    }

    public void Unlock(){ 
        artworkList[LockIndex].color = Color.white;
        LockIndex = -1;
        P_Locked.SetFalse();
    }

    public void OnHovered(int index){
        if(activeDeck.Count!=0 && activeDeck.Count-1 >= index){
            string symbolDescription = activeDeck[index].symbolData.GetDescription();
            OnHoveredEvent.Raise(this, symbolDescription);
        }
    }

    public void OnUnfocus(){
        OnUnfocusEvent.Raise(this, true);
    }

    public void OnDeckPress(){
        CheckDeck.Raise(this, newDeck);
    }

    public void OnDiscardPress(){
        CheckDiscard.Raise(this, discardDeck);
    }

    public void OnEndTurn(){
        P_Rolls.SetValue(2);
        UpdateText();
        activeDeck.Clear();    
    }

    public void OnReRoll(){
        activeDeck.Clear();
        ResetAttacks();
        UpdateText();
    }

    public void ResetAttacks(){
        playerData.ResetData();
    }

    public void UpdateText(){
        rerollsText.text = "Spin (" + P_Rolls.Value.ToString()+")";
        deckText.text = ""+newDeck.Count;
        discardText.text = ""+discardDeck.Count;
    }
}

