using System.Collections.Generic;
using UnityEngine;

public class BossMove
{
    private Vector2 startPosition;
    private Vector2 targetDestination;

    public List<Vector2> PathToPlayer;

    public BossMove(Vector2 start, Vector2 target)
    {
        this.startPosition = start;
        this.targetDestination = target;

        BuildPath();
    }

    public void SetTargetPosition(Vector2 target)
    {
        if (this.targetDestination != target)
        {
            this.targetDestination = target;
            BuildPath();
        }
    }

    private void BuildPath()
    {
        PathToPlayer = new List<Vector2> { targetDestination };
    }
}