using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class SlotMachine : MonoBehaviour
{
    public Deck cardDeck;
    public List<SymbolInventoryItem> newDeck = new List<SymbolInventoryItem>();
    public int  LockIndex;
    public FloatVariable OG_Damage, IC_Shield, P_WeakCount, P_Rolls, slotSpace;
    public BoolVariable P_Locked;
    public BattleSystem battleSystem;
    public SymbolInventoryItem symbol;
    public List<Image> artworkList = new List<Image>();
    public TextMeshProUGUI rerollsText;

    System.Random spin = new System.Random();
    int slotNum, symbNum;
    bool spinning; 

    public void Start() {
        CopyDeck();
        SetupArt();
        LockIndex = -1;
        P_Locked.SetFalse();
        P_Rolls.SetValue(2);
        rerollsText.text = "Spin (" + P_Rolls.Value.ToString()+")";
    }

    public void Update(){
        if(battleSystem.state == BattleState.PLAYERTURN){}
           // RandomizeArt();
    }

    public void SpinMachine() {
        ResetValues();
        Shuffle(newDeck);

        for (int i = 0; i < slotSpace.Value; i++)
        {
            symbol = newDeck[i];
            CalculateTurn(i);
        }
        if(P_WeakCount.Value > 0){
            OG_Damage.ApplyChange((float)Math.Ceiling(OG_Damage.Value/2), true);
            Debug.Log("you are weak: " + OG_Damage.Value);
        }
        if(LockIndex != -1)
        Unlock();

        P_Rolls.ApplyChange(1,true);
        rerollsText.text = "Spin (" + P_Rolls.Value.ToString()+")";
    }

    public void SetupArt(){
        for (int i = 0; i < slotSpace.Value; i++)
            artworkList[i].sprite = newDeck[i].symbolData.artwork;
    }

    public void RandomizeArt(){
        symbNum = spin.Next(0, newDeck.Count);
        slotNum = spin.Next(0, (int)slotSpace.Value);
        artworkList[slotNum].sprite = newDeck[symbNum].symbolData.artwork;
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
        T lockInTemp= default(T);
        if(LockIndex != -1){
            lockInTemp = list[LockIndex];
        }
        int n = list.Count;
        while (n > 1) {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
        if(LockIndex != -1){
            list[LockIndex] = lockInTemp;
        }
    }

    public void TriggerEffects(){
        for (int j = 0; j < slotSpace.Value; j++){
            newDeck[j].symbolData.PreformEffect();
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

    public void ResetValues(){
        OG_Damage.SetValue(0);
        IC_Shield.SetValue(0);
    }

    public void LockIn(int index){
        if(P_Locked.Value == false && P_Rolls.Value != 0 && P_Rolls.Value != 2){
            LockIndex = index;
            artworkList[index].color = Color.blue;
            P_Locked.SetTrue();
        }
        
    }

    public void Unlock(){ 
        artworkList[LockIndex].color = Color.white;
        LockIndex = -1;
        P_Locked.SetFalse();
    }
}

