using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private GameObject[] coins;
    [SerializeField] private GameObject[] spawnPoints;
    public List<GameObject> spawnedCoins = new List<GameObject>();

    private ChestLogic chest;

    private void Awake()
    {
        chest = FindObjectOfType<ChestLogic>();
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < spawnPoints.Length-1; i++)
        {
            GameObject coin = Instantiate(coins[0], spawnPoints[i].transform.position, Quaternion.identity);
            spawnedCoins.Add(coin);
            coin.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chest.coinCount++;
            gameObject.SetActive(false);
        }
    }
}
