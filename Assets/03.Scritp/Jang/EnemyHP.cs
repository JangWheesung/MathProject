using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : LivingEntity
{
    protected override void Update()
    {
        base.Update();
    }

    public void EnemyDie()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            OnDamage(1, collision.transform.position, 1);
        }
    }
}
