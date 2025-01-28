using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
{
    private enum BossState
    {
        Idle,
        MeleeAttack,
        Enraged,
        MoveToPlayer
    }


    private BossState currentState;
    [SerializeField] private Transform player;

    private float meleeAttackCooldown = 4f;
    private float meleeAttackTimer;

    private float maxHealth = 10f;
    public float health;

    //Enraged state values
    private float enragedMeleeAttackCooldown = 2f;
    private float enragedMeleeAttackDmg = 2f;
    private float meleeAttackRange = 1f;
    private float meleeAttackDmg = 0.5f;

    private Animator bossAnim;
    private Vector2 playerPos;

    [SerializeField] private LayerMask playerLayer;
    private float distanceToPlayer;
    private float roomSize = 10f;
    private bool playerClose;
    private bool enraged = false;

    private Rigidbody2D rb;
    private BossMove pathfinding;
    public LayerMask obstacleLayer;

    private Vector2 playerPositon;


    [SerializeField] private Collider2D headCollider;
    [SerializeField] private Collider2D bodyCollider;

    // Damage values for different hit areas
    private float headDamage = 3f;
    private float bodyDamage = 1f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        currentState = BossState.Idle;
        bossAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StateMachineUpdate());

        pathfinding = new BossMove(transform.position, player.position);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private IEnumerator StateMachineUpdate()
    {
        while (true)
        {
            switch (currentState) 
            { 
                case BossState.Idle:
                    UpdateIdleState();
                    break;
                case BossState.MeleeAttack:
                    UpdateMeleeAttackState();
                    break;
                case BossState.Enraged:
                    UpdateEnragedState();
                    break;
                case BossState.MoveToPlayer:
                    UpdateMoveToPlayerState(); 
                    break;
            }
            yield return null;
        }
    }

    private void UpdateIdleState()
    {
        if (player != null)
        {
            if (distanceToPlayer>meleeAttackRange && distanceToPlayer < roomSize)
            {
                currentState = BossState.MoveToPlayer;
            }
            else if (distanceToPlayer <= meleeAttackRange)
            {
                currentState = BossState.MeleeAttack;
            }
            else
            {
                currentState = BossState.Idle;
            }
        }
    }

    private void UpdateMeleeAttackState()
    {
        playerClose = true;

        if (player != null && bossAnim != null)
        {
            if (Vector3.Distance(transform.position, player.position) < 3)
            {
                if (Time.time >= meleeAttackTimer)
                {
                    bossAnim.SetTrigger("wizardAttack");
                    FindObjectOfType<PlayerHealthLogic>().PlayerDamaged(meleeAttackDmg);

                    meleeAttackTimer = Time.time + meleeAttackCooldown;
                }
            } else
            {
                currentState = BossState.Idle;
            }
        }
    }
    private void UpdateEnragedState()
    {
        Renderer bossRenderer = GetComponent<Renderer>();
        bossRenderer.material.color = Color.red;

        meleeAttackCooldown = enragedMeleeAttackCooldown;
        meleeAttackDmg = enragedMeleeAttackDmg;

        currentState = BossState.Idle;
    }


    private void UpdateMoveToPlayerState()
    {
        playerClose = false;
        if (pathfinding.PathToPlayer != null && pathfinding.PathToPlayer.Count > 0 && distanceToPlayer > meleeAttackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, pathfinding.PathToPlayer[0], Time.deltaTime * 3);
            if (Vector3.Distance(transform.position, pathfinding.PathToPlayer[0]) < 0.1f)
            {
                pathfinding.PathToPlayer.RemoveAt(0);
            }
        } else
        {
            currentState = BossState.Idle;
        }
    }

    private IEnumerator BossDie()
    {
        if (health <= 0)
        {
            bossAnim.SetTrigger("wizardDie");
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        } else
        {
            yield return null;
        }
    }

    private void BossBeenHit(float dmg)
    {
        if (playerClose)
        {
            bossAnim.SetTrigger("wizardHit");

            health -= dmg;
        }
    }

    public void HandleCollisionWithHitArea()
    {
        float headDistance = Vector2.Distance(player.position, headCollider.ClosestPoint(transform.position));
        float bodyDistance = Vector2.Distance(player.position, bodyCollider.ClosestPoint(transform.position));

        if (headCollider != null && headDistance < bodyDistance)
        {
            BossBeenHit(headDamage);
        }
        else if (bodyCollider != null && headDistance > bodyDistance)
        {
            BossBeenHit(bodyDamage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y - 1);

        pathfinding.SetTargetPosition(player.position);

        if (health <= 3 && !enraged)
        {
            enraged = true;
            currentState = BossState.Enraged;
        }

        if (playerPos.x < transform.position.x)
        {
            transform.localScale = new Vector2(-6, 6);
        }
        else
        {
            transform.localScale = new Vector2(6, 6);
        }
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("BouncyPlatform"), true);
        StartCoroutine(BossDie());
    }
}

