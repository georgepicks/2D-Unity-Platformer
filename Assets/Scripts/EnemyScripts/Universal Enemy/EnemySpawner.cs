using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] spawnPoints;
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    private int totalEnemyCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i <= totalEnemyCount; i++)
        {
            int randomPoint = Random.Range(0, 16);

            GameObject spawnPoint = spawnPoints[randomPoint];
            Vector2 spawnPosition = spawnPoint.transform.position;

            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemy.transform.localScale = new Vector3(10f,10f,1);
            spawnedEnemies.Add(spawnedEnemy);

            wizardEnemy enemyScript = spawnedEnemy.GetComponent<wizardEnemy>();
            if (enemyScript != null)
            {
                enemyScript.currentHealth = 2f;
            }
        }
    }
}
