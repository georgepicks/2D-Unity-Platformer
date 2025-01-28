using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolinePlatform : MonoBehaviour
{
    private float bounciness = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null && collider.sharedMaterial == null)
        {
            PhysicsMaterial2D physicsMaterial = new PhysicsMaterial2D
            {
                bounciness = bounciness
            };

            collider.sharedMaterial = physicsMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
