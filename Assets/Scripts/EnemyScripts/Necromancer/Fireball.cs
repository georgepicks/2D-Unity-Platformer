using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fireball : MonoBehaviour
{
    [SerializeField] private GameObject fireballObject;
    [SerializeField] private GameObject spawnPoint;
    private List<GameObject> listOfFireballs = new List<GameObject>();

    private GameObject spawnedFireball;
    private playerMovement player;
    private PlayerHealthLogic health;

    private float fireballDmg = 1f;

    private Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerMovement>();
        health = FindObjectOfType<PlayerHealthLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y-1);

        if (spawnedFireball != null)
        {
            StartCoroutine(PathToPlayer(spawnedFireball.transform, playerPos));
        }
    }

    public void LaunchFireball()
    {
        Vector2 spawnPosition = spawnPoint.transform.position;
        spawnedFireball = Instantiate(fireballObject, spawnPosition, Quaternion.identity);
        listOfFireballs.Add(spawnedFireball);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            health.PlayerDamaged(fireballDmg);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator PathToPlayer(Transform fireballPos, Vector2 playerPosition)
    {
        if (spawnedFireball != null)
        {
            List<GameObject> currentFireballs = new List<GameObject>(listOfFireballs);
            foreach (GameObject fireball in currentFireballs)
            {

                while (Vector2.Distance(fireballPos.position, playerPos) > 0.01f)
                {
                    fireballPos.position = Vector2.MoveTowards(fireballPos.position, playerPosition, Time.deltaTime * 0.03f);
                    yield return null;
                }
            }
        }
    }
}
