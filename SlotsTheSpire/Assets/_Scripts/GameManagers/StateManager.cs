using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { TEST ,BATTLESTATE, POTIONSTATE, MAPSTATE, GAMEVICTORY, GAMEDEFEAT, MENUSTATE}

public class StateManager : MonoBehaviour
{

    public GameState state;


    public void SetState(string s){
        //BEST CODE EVER!
        switch(s)
        {
            case "BATTLESTATE": 
            state = GameState.BATTLESTATE;
            Debug.Log("Game State is BATTLESTATE");
            break;
            case "POTIONSTATE": 
            state = GameState.POTIONSTATE;
            Debug.Log(GetState().ToString());
            break;
            case "MAPSTATE": 
            state = GameState.MAPSTATE;
            break;
            case "GAMEVICTORY": 
            state = GameState.GAMEVICTORY;
            break;
            case "GAMEDEFEAT": 
            state = GameState.GAMEDEFEAT;
            break;
            case "MENUSTATE": 
            state = GameState.MENUSTATE;
            break;
            default:
            Debug.Log("YOU SUCK AT CODING!");
            break;
        }


   }

   public GameState GetState(){
    return state;
   }
}
