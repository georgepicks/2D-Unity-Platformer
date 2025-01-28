using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door2Script : MonoBehaviour
{

    private necromancerScript Necromancer;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Text interactText;


    // Start is called before the first frame update
    void Start()
    {
        Necromancer = FindObjectOfType<necromancerScript>();
        interactText.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Necromancer.health <= 0 && PlayerClose())
        {
            OpenDoor();
        } else if (PlayerClose())
        {
            StartCoroutine(ShowInteractText());
        }
    }


    private void OpenDoor()
    {
        Destroy(gameObject);
    }


    private bool PlayerClose()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        RaycastHit2D hitPlayer = Physics2D.BoxCast(pos, new Vector2(0.5f, 1), 0f, Vector2.left, 1, playerLayer);

        if (hitPlayer.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator ShowInteractText()
    {
        interactText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        interactText.gameObject.SetActive(false);

    }
}
