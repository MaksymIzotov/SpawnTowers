using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnSettings[] settings;
    [SerializeField] private Transform spawnPoint;

    private int index;
    private float multiplier;

    private void Start()
    {
        index = 0;
        multiplier = 1;
        SpawnEnemies();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
            PrepareWave();
    }

    private void PrepareWave()
    {
        index++;

        if (index >= settings.Length)
        {
            index = 0;
            multiplier += 0.25f;
        }

        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < settings[index].enemyAmount; i++)
        {
            Vector3 pos = new Vector3(spawnPoint.position.x + Random.Range(-settings[index].radius, settings[index].radius), -3f, spawnPoint.position.z + Random.Range(-settings[index].radius, settings[index].radius));
            GameObject enemy = Instantiate(settings[index].enemiesPrefab[Random.Range(0, settings[index].enemiesPrefab.Length)], pos, Quaternion.identity);

            enemy.GetComponent<EnemyHealth>().GetMultiplier(multiplier);
        }
    }
}
