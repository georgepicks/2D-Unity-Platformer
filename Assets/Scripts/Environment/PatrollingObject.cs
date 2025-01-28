using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingTrap : MonoBehaviour
{
    [SerializeField] GameObject[] patrolPoints;
    private int pointIndex;

    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }

        float distanceToTarget = Vector2.Distance(patrolPoints[pointIndex].transform.position, transform.position);

        if (distanceToTarget < 0.1f)
        {
            pointIndex += 1;
            if (pointIndex == patrolPoints.Length)
            {
                pointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[pointIndex].transform.position, Time.deltaTime * speed);
    }
}
