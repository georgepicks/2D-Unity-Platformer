using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    private float gravity = 3f;

    private playerMovement player;

    private Rigidbody2D playerRB;

    private void Start()
    {
        player = FindObjectOfType<playerMovement>();
        playerRB = player.rb;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerRB != null)
            {
                playerRB.gravityScale = gravity;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerRB != null)
            {
                playerRB.gravityScale = 1.0f;
            }
        }
    }
}
