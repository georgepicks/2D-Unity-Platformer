using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLives : MonoBehaviour
{

    [SerializeField] private PlayerHealthLogic numPlayerLives;

    [SerializeField] private UnityEngine.UI.Image Life_1;
    [SerializeField] private UnityEngine.UI.Image Life_2;
    [SerializeField] private UnityEngine.UI.Image Life_3;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLives();
    }

    // Update is called once per frame
    void Update()
    {
        if (numPlayerLives != null)
        {
            UpdateLives();
        }
    }

    void UpdateLives()
    {
        if (numPlayerLives.currentLives == 1)
        {
            Life_1.gameObject.SetActive(true);
            Life_2.gameObject.SetActive(false);
            Life_3.gameObject.SetActive(false);

        }
        else if (numPlayerLives.currentLives == 2)
        {
            Life_2.gameObject.SetActive(true);
            Life_1.gameObject.SetActive(false);
            Life_3.gameObject.SetActive(false);

        }
        else
        {
            Life_3.gameObject.SetActive(true);
            Life_1.gameObject.SetActive(false);
            Life_2.gameObject.SetActive(false);
        }
    }
}
