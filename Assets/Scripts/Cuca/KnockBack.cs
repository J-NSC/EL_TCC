using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] float KnockUpForce;
    [SerializeField] float KnockBackForce;
    [SerializeField] float KnockTime;
    [SerializeField] float damage;
    [SerializeField] Player player;
    bool enemyDeath;

    public delegate void DeathEnemyHandle();
    public static event DeathEnemyHandle deathEnemy;

    void OnEnable()
    {
        Enemy.enemyIsDeath += OnChancheEnemyDeath;
    }

    void OnDisable()
    {
        Enemy.enemyIsDeath -= OnChancheEnemyDeath;
    }

    void OnChancheEnemyDeath(bool death)
    {
        enemyDeath = death;
    }

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if(player.rig.velocity.y < -1)
            {
                player.rig.AddForce(Vector2.up * KnockUpForce, ForceMode2D.Impulse);
                // deathEnemy?.Invoke();
                enemy.AnimDeath();
            }
            enemyDeath = enemy.enemyDeath;

            if(player.stateMachine.currentStateName != "Stagger" && !enemyDeath)
            {
                Vector2 knockBackDir = (player.rig.position.x > other.transform.position.x)
                    ? new Vector2(1.5f, 0.5f)
                    : new Vector2(-1.5f, 0.5f);
                player.rig.AddForce(knockBackDir.normalized * KnockBackForce, ForceMode2D.Impulse);
                player.Knock(KnockTime, damage);
            }
        }
    }
}
