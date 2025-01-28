using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCooldowns : MonoBehaviour
{

    [SerializeField] private playerMovement player;
    [SerializeField] private UnityEngine.UI.Image cd1;
    [SerializeField] private UnityEngine.UI.Image cd2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cd1.fillAmount = player.specialAttackTimer / 10;
        cd2.fillAmount = player.specialAttackTimer2 / 10;
    }
}
