using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnSettings", order = 1)]
public class SpawnSettings : ScriptableObject
{
    public int enemyAmount;
    public float radius;
    public GameObject[] enemiesPrefab;
}
