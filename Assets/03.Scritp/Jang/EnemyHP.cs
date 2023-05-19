using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : LivingEntity
{
    [SerializeField] private GameObject particle;

    private EnemyMovement enemyMovement;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (CurrentHealth <= 0 && !IsDead)
            OnDie();
    }

    public override void OnDie()
    {
        base.OnDie();

        IsDead = true;
        enemyMovement.enabled = false;

        GameObject part = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(part, 1);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            audioSource.Play();
            OnDamage(1, collision.transform.position);
        }
    }
}
