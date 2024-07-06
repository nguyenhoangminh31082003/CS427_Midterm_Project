using System.Collections;
using UnityEngine;

public class DashMovement : MovementBase
{
    private float originalSpeed;
    public float dashSpeed = 7f;
    public float dashRadius = 4f;
    public bool isDashing = false;
    public bool canDash = true;

    protected override void Start() 
    {
        originalSpeed = moveSpeed;
    }

    public override void Roaming()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= dashRadius) {
            enemyController.SwitchToAttacking();
        }

        MoveTo(((Vector2)player.transform.position - (Vector2)transform.position).normalized);
    }

    public override void Attacking()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > dashRadius) {
            enemyController.SwitchToRoaming();
        }

        MoveTo(((Vector2)player.transform.position - (Vector2)transform.position).normalized);

        if (canDash && !isDashing) {
            canDash = false;
            isDashing = true;
            moveSpeed *= dashSpeed;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine() {
        float dashTime = .2f;
        float dashCooldown = .25f;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        moveSpeed = originalSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}