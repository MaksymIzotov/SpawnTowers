using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerData", order = 1)]
public class TowerData : ScriptableObject
{
    public int level;

    public float shootingSpeed;

    public GameObject bulletPrefab;

    public float damage;
}
