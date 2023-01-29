using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> CommonList = new List<GameObject>();
    public List<GameObject> EliteList = new List<GameObject>();
    public List<GameObject> BossList = new List<GameObject>();

    public GameObject GenerateCommonEnemy(){
        int randomNumber = Random.Range(0,CommonList.Count);
        return CommonList[randomNumber];
    }

}
