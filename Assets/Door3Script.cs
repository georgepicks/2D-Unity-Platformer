using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3Script : MonoBehaviour
{
    private BossScript boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = FindObjectOfType<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
