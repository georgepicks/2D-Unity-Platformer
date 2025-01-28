using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class wizardEnemy : MonoBehaviour
{
    //Values related to the wizard attack
    [SerializeField] private float attackCD = 3f;
    private float attackTimer;
    [SerializeField] private float wizardRange;

    [SerializeField] private float attackDmg;
    private int minAttackDmg = 0;
    private int maxAttackDmg = 2;

    [SerializeField] public float maxHealth = 5.0f;
    public float currentHealth;

    //Values for the position and direction of each wizard enemy
    [SerializeField] private LayerMask playerLayer;
    Vector2 wizardPosition;
    Vector2 wizardDirection;

    private Animator wizardAnim;

    //References to other scripts/objects
    private PlayerHealthLogic health;
    private UnityEngine.Transform playerTransform;
    private UnityEngine.Transform wizardTransform;
    public EnemySpawner EnemySpawner;


    // Start is called before the first frame update
    void Start()
    {
        wizardTransform = transform;

        wizardAnim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        health = FindObjectOfType<PlayerHealthLogic>();
        EnemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerClose())
        {
            if (attackTimer <=0)
            {
                DamagePlayer();
                wizardAnim.SetTrigger("wizardAttack");
            }
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(CheckWizardDead());
        }

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private bool PlayerClose()
    {
        wizardPosition = wizardTransform.position;
        RaycastHit2D hitPlayer = Physics2D.Raycast(wizardPosition, wizardDirection, wizardRange, playerLayer);

        wizardDirection = (playerTransform.position.x < wizardPosition.x) ? Vector2.left : Vector2.right;

        if(wizardDirection == Vector2.left)
        {
            transform.localScale = new Vector2(-3, 3);
        } else
        {
            transform.localScale = new Vector2(3, 3);
        }

        if (hitPlayer.collider != null)
        {
            health = hitPlayer.transform.GetComponent<PlayerHealthLogic>();
            return true;
        }
        else {
            return false;
        }
    }
    private void DamagePlayer()
    {
        attackTimer = attackCD;
        attackDmg = UnityEngine.Random.Range(minAttackDmg, maxAttackDmg);
        FindObjectOfType<PlayerHealthLogic>().PlayerDamaged(attackDmg);
    }

    public void WizardHit(float attackDmg)
    {
        if (PlayerClose())
        {
            {
                currentHealth -= attackDmg;
                wizardAnim.SetTrigger("wizardHit");
            }
        }
    }

    private IEnumerator CheckWizardDead()
    {
        foreach (GameObject enemyObject in EnemySpawner.spawnedEnemies)
        {
            if (currentHealth <= 0)
            {
                heartDropScript heartDrop = FindObjectOfType<heartDropScript>();

                wizardAnim.SetTrigger("wizardDie");
                yield return new WaitForSeconds(1);
                gameObject.SetActive(false);

                if (heartDrop != null)
                {
                    heartDrop.GenerateHeart(transform.position);
                }
                else
                {
                    Debug.Log("no heartdrop script");
                }
            }
        }
    }
}