using System.Collections;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //Movement Values
    [SerializeField] private float baseMovementSpeed = 4f;
    private float baseJumpHeight = 5f;

    public float currentJumpHeight;
    public float currentMovementSpeed;

    //Melee attack damage values
    private int minDamage = 1;
    private int maxDamage = 4;
    [SerializeField] private int playerDamage;

    //Special attack 1 damage values
    private int minSpecial1Damage = 2;
    private int maxSpecial1Damage = 5;
    [SerializeField] private int specialAttackDamage;

    //Special attack 2 damage values
    private int minSpecial2Damage = 3;
    private int maxSpecial2Damage = 6;
    [SerializeField] private int specialAttackDamage2;

    //Attack cooldown/timer values
    private float meleeAttackCooldown = 1f;
    private float specialAttackCooldown = 7.5f;
    private float specialAttackCooldown2 = 10f;

    private float meleeAttackTimer = 0.0f;
    public float specialAttackTimer = 0.0f;
    public float specialAttackTimer2 = 0.0f;

    //Animation triggers and variables to track certain aspects of player progression
    private bool grounded;
    private bool specialAttack = false;
    private bool specialAttack2 = false;
    public bool blocking = false;
    public bool playerBeenHit = false;
    public int currentCheckpoint = 0;
    private bool movingDirection = true;
    private bool canFallThrough = false;
    public bool ability1Unlocked;
    public bool ability2Unlocked;

    [SerializeField] private GameObject[] playerSpawnPoints;

    //Game Objects and scripts
    private PlayerHealthLogic playerHealth;
    public Rigidbody2D rb;
    private Animator playerAnimator;
    public GameObject playerObject;
    private UIManager manager;
    private EnemySpawner enemySpawner;

    //Holds player inputs
    private enum PlayerInputs
    {
        MoveRight,
        MoveLeft,
        Jump,
        SpecialAttack1,
        SpecialAttack2,
        MeleeAttack,
        Block,
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerHealth = FindObjectOfType<PlayerHealthLogic>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        manager = FindObjectOfType<UIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ability1Unlocked = false;
        ability2Unlocked = false;

        currentJumpHeight = baseJumpHeight;
        currentMovementSpeed = baseMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!blocking && !playerBeenHit)
        {
            HandleMovementInput();
            HandleJumpInput();
            HandleAttackInput();
            UpdateAttackTimers();
        }
        HandleBlockInput();

        if (rb.position.y < -15)
        {
            RespawnPlayer(currentCheckpoint);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            manager.PauseMenu();
        }

        playerAnimator.SetBool("grounded", grounded);

        void HandleMovementInput()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            playerAnimator.SetBool("running", Mathf.Abs(horizontalInput) > 0f);

            if (Mathf.Abs(horizontalInput) > 0f)
            {
                transform.localScale = new Vector2(Mathf.Sign(horizontalInput) * 2, 2.2f);
                movingDirection = horizontalInput > 0f;
            }
            else
            {
                transform.localScale = new Vector2(movingDirection ? 2 : -2, 2.2f);
            }

            rb.velocity = new Vector2(currentMovementSpeed * horizontalInput, rb.velocity.y);

        }

        void HandleJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, currentJumpHeight);
                grounded = false;
            }
        }

        void HandleAttackInput()
        {
            if (Input.GetButtonDown("SpecialAttack1") && specialAttackTimer <= 0 && ability1Unlocked)
            {
                playerAnimator.SetTrigger("specialAttack");
                specialAttack = true;
                StartCoroutine(HandleMeleeAttack());
                specialAttackTimer = specialAttackCooldown;
            }
            else
            {
                specialAttack = false;
                playerAnimator.ResetTrigger("specialAttack");
            }

            if (Input.GetButtonDown("SpecialAttack2") && specialAttackTimer2 <= 0 && ability2Unlocked)
            {
                playerAnimator.SetTrigger("specialAttack2");
                specialAttack2 = true;
                StartCoroutine(HandleMeleeAttack());
                specialAttackTimer2 = specialAttackCooldown2;
            }
            else
            {
                playerAnimator.ResetTrigger("specialAttack2");
                specialAttack2 = false;
            }

            if (Input.GetButtonDown("MeleeAttack") && meleeAttackTimer <= 0 && !specialAttack && !specialAttack2)
            {
                playerAnimator.SetTrigger("meleeAttack");
                StartCoroutine(HandleMeleeAttack());
                meleeAttackTimer = meleeAttackCooldown;
            }
            else
            {
                playerAnimator.ResetTrigger("meleeAttack");
            }
        }

        void HandleBlockInput()
        {
            if (Input.GetButtonDown("Block") && !playerAnimator.GetBool("running"))
            {
                blocking = true;
                playerAnimator.ResetTrigger("playerNotBlocking");
                playerAnimator.SetTrigger("playerBlocking");
            }

            if (Input.GetButtonUp("Block"))
            {
                blocking = false;
                playerAnimator.ResetTrigger("playerBlocking");
                playerAnimator.SetTrigger("playerNotBlocking");
            }
        }

        void UpdateAttackTimers()
        {
            if (meleeAttackTimer > 0)
            {
                meleeAttackTimer -= Time.deltaTime;
            }

            if (specialAttackTimer > 0)
            {
                specialAttackTimer -= Time.deltaTime;
            }

            if (specialAttackTimer2 > 0)
            {
                specialAttackTimer2 -= Time.deltaTime;
            }
        }

        IEnumerator HandleMeleeAttack()
        {
            playerDamage = Random.Range(minDamage, maxDamage);
            specialAttackDamage = Random.Range(minSpecial1Damage, maxSpecial1Damage);
            specialAttackDamage2 = Random.Range(minSpecial2Damage, maxSpecial2Damage);

            foreach (GameObject wizardEnemy in enemySpawner.spawnedEnemies)
            {
                wizardEnemy wizard = wizardEnemy.GetComponent<wizardEnemy>();

                float delay = specialAttack ? 1.0f : specialAttack2 ? 1.4f : 0.5f;
                yield return new WaitForSeconds(0);

                wizard.WizardHit(specialAttack ? specialAttackDamage : specialAttack2 ? specialAttackDamage2 : playerDamage);
            }

            necromancerScript necromancer = FindObjectOfType<necromancerScript>();
            if (necromancer != null)
            {
                necromancer.NecromancerHit(specialAttack ? specialAttackDamage : specialAttack2 ? specialAttackDamage2 : playerDamage);

            }

            BossScript boss = FindObjectOfType<BossScript>();
            if (boss != null)
            {
                boss.HandleCollisionWithHitArea();
            }
            specialAttack = false;
            specialAttack2 = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Tree")
        {
            if (!canFallThrough)
            {
                grounded = true;
            }
        }
    }

    public void RespawnPlayer(int checkpointNum)
    {
        Vector2 spawnPoint = playerSpawnPoints[checkpointNum].transform.position;

        rb.position = spawnPoint;
    }

    public IEnumerator ModifyJumpHeight(float jumpModifier)
    {
        currentJumpHeight = jumpModifier;
        yield return new WaitForSeconds(10);
        currentJumpHeight = baseJumpHeight;
    }

    public IEnumerator ModifyMovementSpeed(float speedModifier)
    {
        currentMovementSpeed = speedModifier;
        yield return new WaitForSeconds(10);
        currentMovementSpeed = baseMovementSpeed;
    }
}