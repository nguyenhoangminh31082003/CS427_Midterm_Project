using System;
using UnityEngine;
using System.Collections;

public class TheDashMovement : TheMovementBase
{
    private float originalSpeed;

    public float chaseRadius    = 6f;
    public float dashSpeed      = 7f;
    public float dashRadius     = 4f;
    public bool isDashing       = false;
    public bool canDash         = true;

    public float dashCooldown   = 2.0f;
    private bool freezed        = true;

    protected override void Start() 
    {
        base.Start();
        this.originalSpeed = moveSpeed;
    }

    public override void Roaming()
    {

        float firstDistance = Vector2.Distance(this.transform.position, this.theFirstPlayer.transform.position),
              secondDistance = Vector2.Distance(this.transform.position, this.theSecondPlayer.transform.position),
              distance = Mathf.Min(firstDistance, secondDistance);

        if (this.freezed && distance <= this.chaseRadius) {
            this.freezed = false;
        }

        if (this.freezed) 
        { 
            return; 
        }

        if (distance <= this.dashRadius) {
            this.theEnemyController.SwitchToAttacking();
        }

        if (firstDistance < secondDistance)
            this.MoveTo(((Vector2)this.theFirstPlayer.transform.position - (Vector2)this.transform.position).normalized);
        else if (secondDistance < firstDistance)
            this.MoveTo(((Vector2)this.theSecondPlayer.transform.position - (Vector2)this.transform.position).normalized);
    }

    public override void Attacking()
    {
        float firstDistance = Vector2.Distance(this.transform.position, this.theFirstPlayer.transform.position),
              secondDistance = Vector2.Distance(this.transform.position, this.theSecondPlayer.transform.position),
              distance = Mathf.Min(firstDistance, secondDistance);

        if (distance > this.dashRadius) 
        {
            this.theEnemyController.SwitchToRoaming();
        }

        if (firstDistance < secondDistance)
            this.MoveTo(((Vector2)this.theFirstPlayer.transform.position - (Vector2)transform.position).normalized);
        else if (secondDistance < firstDistance)
            this.MoveTo(((Vector2)this.theSecondPlayer.transform.position - (Vector2)transform.position).normalized);

        if (this.canDash && !this.isDashing) 
        {
            this.canDash = false;
            this.isDashing = true;
            this.moveSpeed *= dashSpeed;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine() {
        float dashTime = .2f;
        yield return new WaitForSeconds(dashTime);
        this.isDashing = false;
        this.moveSpeed = originalSpeed;
        yield return new WaitForSeconds(dashCooldown);
        this.canDash = true;
    }
}