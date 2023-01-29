using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public int gameStartScene, mainMenuScene;

    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }

    public void CombatDefeat(){
        SceneManager.LoadScene(mainMenuScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
