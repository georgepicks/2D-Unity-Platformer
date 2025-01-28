using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartDropScript : MonoBehaviour
{

    private float addHealthAmount = 1.0f;
    private float heartDropController;
    public bool hasSpawnedHeart = false;

    [SerializeField] private GameObject[] heartPrefabs;
    [SerializeField] private GameObject[] spawnPoints;
    public List<GameObject> spawnedHearts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnHearts();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnHearts()
    {
        for (int i = 0; i <= spawnPoints.Length - 1; i++)
        {
            GameObject heartToSpawn = heartPrefabs[0];

            GameObject spawnPoint = spawnPoints[i];
            Vector2 spawnPosition = spawnPoint.transform.position;

            GameObject spawnedHeart = Instantiate(heartToSpawn, spawnPosition, Quaternion.identity);
            spawnedHearts.Add(spawnedHeart);
        }
    }

    public void GenerateHeart(Vector2 heartPos)
    {
        hasSpawnedHeart = false;
        if (heartPrefabs.Length > 0 && !hasSpawnedHeart)
        {
            heartDropController = Random.Range(1, 100);
            GameObject heartToSpawn = heartPrefabs[0];

            if (heartDropController <= 25)
            {
                GameObject spawnedHeart = Instantiate(heartToSpawn, heartPos, Quaternion.identity);
                hasSpawnedHeart = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerHealthLogic>().AddHealth(addHealthAmount); ;
            Destroy(gameObject);
        }
    }
}
