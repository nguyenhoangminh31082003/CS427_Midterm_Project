using System.Collections;
using UnityEngine;

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
        originalSpeed = moveSpeed;
    }

    public override void Roaming()
    {
        if (freezed && Vector2.Distance(transform.position, this.theFirstPlayer.transform.position) <= chaseRadius) {
            freezed = false;
        }

        if (freezed) { return; }

        if (Vector2.Distance(transform.position, this.theFirstPlayer.transform.position) <= dashRadius) {
            this.theEnemyController.SwitchToAttacking();
        }

        MoveTo(((Vector2)theFirstPlayer.transform.position - (Vector2)transform.position).normalized);
    }

    public override void Attacking()
    {
        if (Vector2.Distance(transform.position, this.theFirstPlayer.transform.position) > dashRadius) {
            this.theEnemyController.SwitchToRoaming();
        }

        MoveTo(((Vector2)this.theFirstPlayer.transform.position - (Vector2)transform.position).normalized);

        if (canDash && !isDashing) {
            canDash = false;
            isDashing = true;
            moveSpeed *= dashSpeed;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine() {
        float dashTime = .2f;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        moveSpeed = originalSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}