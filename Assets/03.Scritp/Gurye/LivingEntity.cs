using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LivingEntity : MonoBehaviour, IDamage
{
    public float Health;
    public bool IsDead { get; protected set; }
    public float CurrentHealth { get; protected set; }
    public UnityEvent OnDeath;
    protected Rigidbody2D rb;
    protected SpriteRenderer sp;

    public virtual void Awake()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Reset();
    }

    public virtual void Reset()
    {
        IsDead = false;
        CurrentHealth = Health;
    }

    public virtual void OnDie()
    {
        IsDead = true;
        OnDeath?.Invoke();
    }

    public void OnDamage(float damage, Vector2 hitPoint, float knckbackValue = 5)
    {
        if (IsDead)//죽음 감지
            return;

        Vector2 vec;//넉백
        vec = transform.position.x > hitPoint.x ? Vector2.right : Vector2.left;

        rb.AddForceAtPosition(vec * knckbackValue * 100, gameObject.transform.position);

        CurrentHealth -= damage;//피 깍임

        StartCoroutine(DamageColor(0.1f));
    }

    IEnumerator DamageColor(float time)
    {
        for (int i = 0; i < 3; i++)
        {
            sp.color = Color.red;
            yield return new WaitForSeconds(time);
            sp.color = Color.white;
            yield return new WaitForSeconds(time);
        }
    }
}
