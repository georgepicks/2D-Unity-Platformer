using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityPotion : MonoBehaviour
{ 

    private playerMovement player;
    private Renderer potionRenderer;
    private Collider2D potionCollider;
    private Vector3 initialPosition;

    private float bobSpeed = 1.0f;
    private float bobHeight = 0.3f;

    [SerializeField] private Text jumpPotionPickup;
    [SerializeField] private Text speedPotionPickup;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<playerMovement>();
        potionRenderer = GetComponent<Renderer>();
        potionCollider = GetComponent<Collider2D>();
        initialPosition = transform.position;

        jumpPotionPickup.gameObject.SetActive(false);
        speedPotionPickup.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = initialPosition + new Vector3(0, yOffset, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PotionLogic potionLogic = FindObjectOfType<PotionLogic>();

            string potionName = gameObject.name;

            if (potionName == "JumpPotion")
            {
                potionLogic.jumpPotionUnlocked = true;
                StartCoroutine(ShowJumpPotionPickup());
            } else if (potionName == "SpeedPotion")
            {
                potionLogic.speedPotionUnlocked = true;
                StartCoroutine(ShowSpeedPotionPickup());
            }


            //FindObjectOfType<PotionLogic>().speedPotionUnlocked = true;
            potionRenderer.enabled = false;
            potionCollider.enabled = false;
        }
    }

    private IEnumerator ShowJumpPotionPickup()
    {
        jumpPotionPickup.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        jumpPotionPickup.gameObject.SetActive(false);
    }

    private IEnumerator ShowSpeedPotionPickup()
    {
        speedPotionPickup.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        speedPotionPickup.gameObject.SetActive(false);
    }
}
