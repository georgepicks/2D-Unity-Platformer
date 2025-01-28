using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class necromancerScript : MonoBehaviour
{
    //Variables controlling the direction/position of the necromancer
    [SerializeField] private float necromancerRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private PolygonCollider2D polygonCollider;
    Vector2 necromancerPosition;
    private Animator necromancerAnim;
    private Vector2 necromancerDirection;


    public bool hasFired = false;
    private float maxHealth = 5f;
    public float health;

    private Fireball fireball;

    //Manages the attack cooldown of the range attack
    [SerializeField] private float attackCooldown = 7.5f;
    private float attackTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        fireball = FindObjectOfType<Fireball>();
        necromancerAnim = GetComponent<Animator>();

        health = maxHealth;
        attackTimer = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerClose() && !hasFired)
        {
            RangedAttack();
            hasFired = true;
        }

        if (attackTimer <=0)
        {
            hasFired = false;
        }

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        facePlayer();
        CheckHealth();
    }

    private bool PlayerClose()
    {
        necromancerPosition = new Vector2(transform.position.x, transform.position.y-2);

        float boxWidth = 0.5f;

        RaycastHit2D hitPlayer = Physics2D.BoxCast(necromancerPosition, new Vector2(boxWidth, necromancerRange), 0f, necromancerDirection, necromancerRange, playerLayer);

        if (hitPlayer.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Creates a hit box so the player can damage the necromancer
    private bool HitBox()
    {
        Vector2 hitboxPosition = new Vector2(transform.position.x - 1, transform.position.y - 1.5f);
        Vector2 adjustedDirection = necromancerDirection;

        if (necromancerDirection == Vector2.right)
        {
            hitboxPosition.x += 1;
        }
        else
        {
            adjustedDirection = -necromancerDirection;
        }

        RaycastHit2D hitbox = Physics2D.Raycast(hitboxPosition, adjustedDirection, 1.6f, playerLayer);

        if (hitbox.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Triggers a ranged attack
    private void RangedAttack()
    {
        fireball.LaunchFireball();
        necromancerAnim.SetTrigger("rangedAttack");
        attackTimer = attackCooldown;
    }

    public void NecromancerHit(float dmg)
    {
        if (HitBox())
        {
            health -= dmg;
            necromancerAnim.SetTrigger("Hit");
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            necromancerAnim.SetTrigger("Death");
            Destroy(gameObject);
        }
    }

    //Faces the player depending on the position of the player
    private void facePlayer()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (playerPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-0.7f, 0.7f, 0);
            necromancerDirection = Vector2.left;
        }
        else
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 2);
            necromancerDirection = Vector2.right;
        }
    }
}