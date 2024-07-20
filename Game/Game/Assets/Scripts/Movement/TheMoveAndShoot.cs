using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheMoveAndShoot : TheMovementBase
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
        this.timeRoaming += Time.deltaTime;

        if (Math.Min(Vector2.Distance(theFirstPlayer.transform.position, transform.position), Vector2.Distance(theSecondPlayer.transform.position, transform.position)) <= attackRange) {
            theEnemyController.SwitchToAttacking();
        }
        
        if (timeRoaming > deltaChangeDirection) {
            MoveTo(ResetRoamingPosition());
        }
        
    }

    public override void Attacking()
    {
        float   firstDistance   =   Vector2.Distance(theFirstPlayer.transform.position, transform.position),
                secondDistance  =   Vector2.Distance(theSecondPlayer.transform.position, transform.position),
                distance        =   Math.Min(firstDistance, secondDistance);

        if (distance > attackRange) {
            this.theEnemyController.SwitchToRoaming();
        }

        if (attackRange > 0 && canAttack) {
            int randomness  =    UnityEngine.Random.Range(0, 3); 
           
            this.canAttack = false;
            
            if ((randomness == 1) || (randomness == 0 && firstDistance < secondDistance))
            {
                Vector2 targetDirection = theFirstPlayer.transform.position - transform.position;
                
                if (targetDirection.x < -0.1f) {
                    sr.flipX = true;
                } else if (targetDirection.x > 0.1f) {
                    sr.flipX = false;
                }
                this._animator.SetTrigger("attack");
                
                GameObject newBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                newBullet.transform.right = targetDirection;
            }
            else if ((randomness == 2) || (randomness == 0 && secondDistance < firstDistance))
            {
                Vector2 targetDirection = theSecondPlayer.transform.position - transform.position;

                if (targetDirection.x < -0.1f)
                {
                    sr.flipX = true;
                }
                else if (targetDirection.x > 0.1f)
                {
                    sr.flipX = false;
                }
                this._animator.SetTrigger("attack");

                GameObject newBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                newBullet.transform.right = targetDirection;
            }

            if (this.stopWhenAttack) {
                this.StopMoving();
            }

            StartCoroutine(AttackCooldownRoutine());
        }       
        
    }

    IEnumerator AttackCooldownRoutine() {
        yield return new WaitForSeconds(attackCD);
        this.canAttack = true;
    }

    private Vector2 ResetRoamingPosition() {
        this.timeRoaming = 0f;
        return new Vector2(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f,1f)).normalized;
    }
    

}
