using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class FlagScript : MonoBehaviour
{

    private SpriteRenderer sr;
    private playerMovement player;

    private UIManager manager;

    [SerializeField] private Text abilityText1;
    [SerializeField] private Text abilityText2;
    [SerializeField] private Text CheckpointReachedText;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<playerMovement>();
        manager = FindObjectOfType<UIManager>();

        abilityText1.gameObject.SetActive(false);
        abilityText2.gameObject.SetActive(false);
        CheckpointReachedText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sr.color = Color.green;
            Checkpoint();
            
        }
        StartCoroutine(DisplayeAbilityText());
    }

    private void Checkpoint()
    {
        string checkpointName = gameObject.name;
        GameObject checkpoint = GameObject.Find(checkpointName);
        BoxCollider2D collider = checkpoint.GetComponent<BoxCollider2D>();

        Destroy(collider);

        if (player.currentCheckpoint < 5)
        {
            player.currentCheckpoint++;
            FindObjectOfType<PlayerHealthLogic>().AddHealth(5);
        }
    }

    private IEnumerator DisplayeAbilityText()
    {
        if (player.currentCheckpoint == 1)
        {
            player.ability1Unlocked = true;
            abilityText1.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            abilityText1.gameObject.SetActive(false);
        }


        if (player.currentCheckpoint == 2)
        {
            player.ability2Unlocked = true;
            abilityText2.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            abilityText2.gameObject.SetActive(false);
        }

        if(player.currentCheckpoint == 3)
        {
            CheckpointReachedText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            CheckpointReachedText.gameObject.SetActive(false);
        }

        if (player.currentCheckpoint == 4)
        {
            manager.GameWon();
        }
    }
}
