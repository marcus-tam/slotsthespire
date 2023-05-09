using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> CommonList = new List<GameObject>();
    public List<GameObject> EliteList = new List<GameObject>();
    public List<GameObject> BossList = new List<GameObject>();
    public GameObject[] EnemiesToSpawn = new GameObject[4];

    public GameObject GenerateCommonEnemy(){
        int randomNumber = Random.Range(0,CommonList.Count);
        return CommonList[randomNumber];
    }

    public GameObject GenerateBoss(){
        int randomNumber = Random.Range(0,BossList.Count);
        return BossList[randomNumber];
    }

    public GameObject[] GenerateEnemies(){
        //add some function with map and levels 
        int rand;
        for (int i = 0; i < EnemiesToSpawn.Length; i++)
        {
            if(i == EnemiesToSpawn.Length-1)
            EnemiesToSpawn[i] = GenerateBoss();
            else
            EnemiesToSpawn[i] = GenerateCommonEnemy();
        }
        return EnemiesToSpawn;
    }
}
