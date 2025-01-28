using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatformScript : MonoBehaviour
{
    private playerMovement player;

    private Vector2 playerPos;

    private BoxCollider2D bc;

    private BossScript boss;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerMovement>();
        boss = FindObjectOfType<BossScript>();
        bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;

        if (playerPos.y > transform.position.y+2 && boss != null)
        {
            //boss.TriggerBossJump();

            bc.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            bc.enabled = false;
        }

    }
}
