using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : LivingEntity
{
    private EnemyMovement enemyMovement;

    protected override void Awake()
    {
        base.Awake();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
    }

    void Update()
    {
        if (CurrentHealth <= 0 && !IsDead)
            OnDie();
    }

    public override void OnDie()
    {
        IsDead = true;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            OnDamage(1, collision.transform.position);
        }
    }
}
