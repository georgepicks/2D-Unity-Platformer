using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLogic : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image keyFound;
    [SerializeField] private UnityEngine.UI.Image keyMissing;

    public bool hasKey;

    // Start is called before the first frame update
    void Start()
    {
        hasKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasKey)
        {
            keyFound.fillAmount = 1f;
            keyMissing.fillAmount = 0f;


        }
        else
        {
            keyMissing.fillAmount = 1f;
            keyFound.fillAmount = 0f;
        }
    }
}
