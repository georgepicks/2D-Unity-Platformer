using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotionLogic : MonoBehaviour
{ 
    //Health potion images
    [SerializeField] private UnityEngine.UI.Image healthPotionImage;
    [SerializeField] private UnityEngine.UI.Image healthPotionFullImage;

    //Jump height potion images
    [SerializeField] private UnityEngine.UI.Image jumpPotionImage;
    [SerializeField] private UnityEngine.UI.Image jumpPotionFullImage;

    //Speed potion images
    [SerializeField] private UnityEngine.UI.Image speedPotionImage;
    [SerializeField] private UnityEngine.UI.Image speedPotionFullImage;

    
    //Manages the cooldown of the 3 potions
    private float potionCooldown = 30.0f;
    private float healthPotionTimer = 0.0f;
    private float jumpPotionTimer = 0.0f;
    private float speedPotionTimer = 0.0f;

    //Potion Values
    private float healthPotionValue = 3.0f;
    private float jumpPotionModifier = 10.0f;
    private float speedPotionModifier = 8.0f;

    //Controls if the potions have been used/unlocked or not
    private bool healthPotionUsed = false;
    private bool jumpPotionUsed = false;
    private bool speedPotionUsed = false;
    public bool jumpPotionUnlocked;
    public bool speedPotionUnlocked;

    private playerMovement playerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = FindObjectOfType<playerMovement>();

        jumpPotionUnlocked = false;
        speedPotionUnlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && healthPotionTimer <= 0 && !healthPotionUsed){
            potionAddHealth();
        }
        else if (Input.GetKeyDown(KeyCode.R) && jumpPotionTimer <=0 && !jumpPotionUsed && jumpPotionUnlocked)
        {
            JumpPotion();
        } 
        else if (Input.GetKeyDown(KeyCode.G) && speedPotionTimer <=0 && !speedPotionUsed && speedPotionUnlocked)
        {
            SpeedPotion();
        }

        if (healthPotionTimer > 0)
        {
            healthPotionTimer -= Time.deltaTime;
        }
        else
        {
            healthPotionUsed = false;
        }

        if (speedPotionTimer > 0)
        {
            speedPotionTimer -= Time.deltaTime;
        }
        else
        {
            speedPotionUsed = false;
        }

        if (jumpPotionTimer > 0)
        {
            jumpPotionTimer -= Time.deltaTime;
        }
        else
        {
            jumpPotionUsed = false;
        }
        DrawPotionsOnScreen();
    }
     
    //Manages the UI aspect of the potions
    private void DrawPotionsOnScreen()
    {
        if (healthPotionUsed)
        {
            healthPotionImage.fillAmount = healthPotionTimer / 50;
            healthPotionFullImage.fillAmount = 0f;
        }
        else if (!healthPotionUsed)
        {
            healthPotionFullImage.fillAmount = 1.0f;
        }

        if (jumpPotionUnlocked)
        {
            if (jumpPotionUsed)
            {
                jumpPotionImage.fillAmount = jumpPotionTimer / 50;
                jumpPotionFullImage.fillAmount = 0f;
            }
            else if (!jumpPotionUsed)
            {
                jumpPotionFullImage.fillAmount = 1.0f;
            }
        } else
        {
            jumpPotionImage.fillAmount = 1f;
            jumpPotionFullImage.fillAmount = 0f;
        }


        if (speedPotionUnlocked)
        {
            if (speedPotionUsed)
            {
                speedPotionImage.fillAmount = speedPotionTimer / 50;
                speedPotionFullImage.fillAmount = 0f;
            }
            else if (!speedPotionUsed)
            {
                speedPotionFullImage.fillAmount = 1.0f;
            }
        }
        else
        {
            speedPotionImage.fillAmount = 1f;
            speedPotionFullImage.fillAmount = 0f;
        }
    }

    //Adds health to the player if its off cooldown and unlocked
    private void potionAddHealth()
    {
        healthPotionUsed = true;
        healthPotionTimer = potionCooldown;

        FindObjectOfType<PlayerHealthLogic>().AddHealth(healthPotionValue);
    }

    //Modifies the player's jump height if its off cooldown and unlocked
    private void JumpPotion()
    {
        jumpPotionUsed = true;
        jumpPotionTimer = potionCooldown;

        playerMovementScript.StartCoroutine(playerMovementScript.ModifyJumpHeight(jumpPotionModifier));
    }

    //Modifies the player's move speed if its off cooldown and unlocked
    private void SpeedPotion()
    {
        speedPotionUsed = true;
        speedPotionTimer = potionCooldown;

        playerMovementScript.StartCoroutine(playerMovementScript.ModifyMovementSpeed(speedPotionModifier));
    }
}
