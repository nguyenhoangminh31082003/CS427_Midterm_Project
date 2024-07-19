using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheChaseMovement : TheMovementBase
{
    public float chaseRadius;
    private Vector2 originalPosition;

    protected override void Start()
    {
        base.Start();
        originalPosition = transform.position;
    }

    public override void Roaming()
    {
        if (Vector2.Distance(transform.position, originalPosition) > 0.1f) {
            MoveTo((originalPosition - (Vector2)transform.position).normalized);
        } else {
            StopMoving();
        }
        
        if (Vector2.Distance(theFirstPlayer.transform.position, originalPosition) <= chaseRadius) {
            theEnemyController.SwitchToAttacking();
        }
    }

    public override void Attacking()
    {
        MoveTo(((Vector2)theFirstPlayer.transform.position - (Vector2)transform.position).normalized);
        
        if (Vector2.Distance(theFirstPlayer.transform.position, originalPosition) > chaseRadius) {
            theEnemyController.SwitchToRoaming();
        }
    }
}
