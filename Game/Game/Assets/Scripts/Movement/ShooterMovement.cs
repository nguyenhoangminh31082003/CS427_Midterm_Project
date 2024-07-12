using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMovement : MovementBase
{
    [SerializeField] private float attackRange = 4f;
    [SerializeField] private float attackCD = 2f;
    [SerializeField] private bool stopWhenAttack = true;
    [SerializeField] private float deltaChangeDirection = 2f;
    private float timeRoaming = 0f;

    private bool canAttack = true;

    [Header("Shoot settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private int projectilesPerBurst;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    private bool isShooting = false;

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
            
            Attack();

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

    public void Attack() {
        if (!isShooting) {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        float startAngle, currentAngle, angleStep;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);

        _animator.SetTrigger("attack");

        for (int i = 0; i < burstCount; i++)
        {
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = (Vector2)(newBullet.transform.position - transform.position);

            
                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;
            }

            currentAngle = startAngle;

            yield return new WaitForSeconds(timeBetweenBursts);
            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);
        }

        
        if (stopWhenAttack) {
            StopMoving();
        }
        _animator.SetTrigger("charge");
        yield return new WaitForSeconds(restTime);
        _animator.SetTrigger("ready");
        isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep)
    {
        Vector2 targetDirection = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        float endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle) {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
