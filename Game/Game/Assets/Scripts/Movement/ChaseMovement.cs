using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMovement : MovementBase
{
    public float chaseRadius;
    private Vector2 originalPosition;

    protected override void Start()
    {
        originalPosition = transform.position;
    }

    public override void Roaming()
    {
        moveDir = (originalPosition - (Vector2)transform.position).normalized;
        
        if (Vector2.Distance(player.transform.position, originalPosition) <= chaseRadius) {
            enemyController.SwitchToAttacking();
        }
    }

    public override void Attacking()
    {
        moveDir = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        
        if (Vector2.Distance(player.transform.position, originalPosition) > chaseRadius) {
            enemyController.SwitchToRoaming();
        }
    }
}
