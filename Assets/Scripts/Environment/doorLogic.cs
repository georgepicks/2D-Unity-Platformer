using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class doorLogic : MonoBehaviour
{
    private KeyLogic key;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Text interactText;

    // Start is called before the first frame update
    void Start()
    {
        key = FindObjectOfType<KeyLogic>();
        interactText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && key.hasKey && PlayerInRange())
        {
            Destroy(gameObject);
        }
        else if (PlayerInRange() && !key.hasKey && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ShowInteractText());
        }
    }

    private bool PlayerInRange()
    {
        RaycastHit2D playerNear = Physics2D.BoxCast(transform.position, new Vector2(2, 1), 0f, Vector2.left, 1, playerLayer);

        if (playerNear.collider != null)
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