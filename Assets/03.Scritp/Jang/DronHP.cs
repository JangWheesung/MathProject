using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronHP : LivingEntity
{
    [SerializeField] private GameObject particle;

    private EnemyMovement enemyMovement;
    private AudioSource audioSource;
    private Shield shield;

    protected override void Awake()
    {
        base.Awake();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
        audioSource = gameObject.GetComponent<AudioSource>();
        shield = transform.GetChild(0).GetComponent<Shield>();
    }

    void Update()
    {
        if (CurrentHealth <= 0 && !IsDead)
            OnDie();
    }

    public override void OnDie()
    {
        IsDead = true;
        enemyMovement.enabled = false;

        GameObject part = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(part, 1);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet" && !shield.isShield)
        {
            audioSource.Play();
            OnDamage(1, collision.transform.position);
        }
        shield.isShield = false;
    }
}
