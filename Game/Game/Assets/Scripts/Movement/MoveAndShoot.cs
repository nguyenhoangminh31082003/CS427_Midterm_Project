using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndShoot : MovementBase
{
    [SerializeField] private float attackRange = 4f;
    [SerializeField] private float attackCD = 2f;
    [SerializeField] private bool stopWhenAttack = true;
    [SerializeField] private float deltaChangeDirection = 2f;
    [SerializeField] private GameObject bulletPrefab;

    private float timeRoaming = 0f;

    private bool canAttack = true;


    protected override void Start()
    {
        base.Start();
    }

    public override void Roaming()
    {
        timeRoaming += Time.deltaTime;
        if (Vector2.Distance(player.transform.position, transform.position) <= attackRange) {
            enemyController.SwitchToAttacking();
        }
        
        if (timeRoaming > deltaChangeDirection) {
            MoveTo(ResetRoamingPosition());
        }
        
    }

    public override void Attacking()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > attackRange) {
            enemyController.SwitchToRoaming();
        }

        if (attackRange > 0 && canAttack) {
            canAttack = false;
            { // attack block
                Vector2 targetDirection = player.transform.position - transform.position;
                
                if (targetDirection.x < -0.1f) {
                    sr.flipX = true;
                } else if (targetDirection.x > 0.1f) {
                    sr.flipX = false;
                }
                _animator.SetTrigger("attack");
                GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                newBullet.transform.right = targetDirection;
            }
            if (stopWhenAttack) {
                StopMoving();
            }

            StartCoroutine(AttackCooldownRoutine());
        }       
        
    }

    IEnumerator AttackCooldownRoutine() {
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }

    private Vector2 ResetRoamingPosition() {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized;
    }
    

}
