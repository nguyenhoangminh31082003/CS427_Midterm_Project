using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
        
        if (Math.Min(Vector2.Distance(theFirstPlayer.transform.position, originalPosition), Vector2.Distance(theSecondPlayer.transform.position, originalPosition)) <= chaseRadius) {
            theEnemyController.SwitchToAttacking();
        }
    }

    public override void Attacking()
    {

        float   firstDistance   =   Vector2.Distance(theFirstPlayer.transform.position, transform.position),
                secondDistance  =   Vector2.Distance(theSecondPlayer.transform.position, transform.position);

        if (firstDistance < secondDistance)
            this.MoveTo(((Vector2)theFirstPlayer.transform.position - (Vector2)transform.position).normalized);
        else if (secondDistance < firstDistance)
            this.MoveTo(((Vector2)theSecondPlayer.transform.position - (Vector2)transform.position).normalized);

        if (Math.Min(Vector2.Distance(theFirstPlayer.transform.position, originalPosition), Vector2.Distance(theSecondPlayer.transform.position, originalPosition)) > chaseRadius) {
            this.theEnemyController.SwitchToRoaming();
        }
    }
}
