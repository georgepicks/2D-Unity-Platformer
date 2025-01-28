using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChestLogic : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Text missingCoinsText;
    [SerializeField] private Text coinCountText;

    private KeyLogic key;
    [SerializeField] public float coinCount = 0f;

    private Animator chestAnimator;

    private Transform chestTransform;

    // Start is called before the first frame update
    void Start()
    {
        chestAnimator = GetComponent<Animator>();
        key = FindObjectOfType<KeyLogic>();

        missingCoinsText.gameObject.SetActive(false);

        chestTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange() && Input.GetButtonDown("Interact") && coinCount == 5)
        {
            key.hasKey = true;
            chestAnimator.SetTrigger("chestOpen");
        } else if (PlayerInRange() && Input.GetButtonDown("Interact") && coinCount <5) 
        { 
            StartCoroutine(MissingCoins());
        }

        coinCountText.text = "Coins: " + coinCount.ToString();
    }

    
    private bool PlayerInRange()
    {
        RaycastHit2D playerNear = Physics2D.Raycast(chestTransform.position, Vector2.right, 1, playerLayer);

        if (playerNear.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator MissingCoins()
    {
        missingCoinsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        missingCoinsText.gameObject.SetActive(false);
    }
}
