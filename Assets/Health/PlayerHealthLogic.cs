using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthLogic : MonoBehaviour
{
    [SerializeField] private const float maxHealth = 5;
    [SerializeField] private const float maxLives = 3;

    public float currentHealth { get; private set; }
    public float currentLives { get; private set; }

    private Animator playerAnimator;
    private playerMovement player;
    private UIManager manager;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        player = FindObjectOfType<playerMovement>();
        manager = FindObjectOfType<UIManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;


    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 5)
        {
            currentHealth = 5.0f;
        }
    }

    public void PlayerDamaged(float dmg)
    {
        if (currentLives > 0 && currentHealth > 0 && !player.blocking)
        {
            currentHealth -= dmg;
            playerAnimator.SetTrigger("playerHit");
            player.playerBeenHit = true;
            StartCoroutine(hitCooldown());
        }
        else if (currentHealth <= 0)
        {
            currentLives -= 1;
            currentHealth = maxHealth;
            playerAnimator.SetTrigger("playerDie");
            player.RespawnPlayer(player.currentCheckpoint);

        }
    

        if(currentLives == 0)
        {
            manager.GameOver();
        }
    }

    public void AddHealth(float healthIncrease)
    {
        if (currentHealth >= 0 && currentHealth < 5) {
            currentHealth += healthIncrease;
        }
    }


    IEnumerator hitCooldown()
    {
        yield return new WaitForSeconds(2);
        player.playerBeenHit = false;
    }
}
