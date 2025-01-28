using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealthLogic playerHealth;
    [SerializeField] private UnityEngine.UI.Image currentHealthImage;



    // Start is called before the first frame updat190273
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealthLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthImage.fillAmount = playerHealth.currentHealth / 10;
    }
}
