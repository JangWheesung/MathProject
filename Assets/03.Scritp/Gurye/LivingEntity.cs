using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LivingEntity : MonoBehaviour, IDamage
{
    public float Health;
    public bool IsDead { get; protected set; }
    public float CurrentHealth;
    public UnityEvent OnDeath;
    protected Rigidbody2D rb;
    protected SpriteRenderer sp;

    protected virtual void Awake()
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

    protected virtual void Update()
    {
        Debug.Log("up");
        if (CurrentHealth <= Health && !IsDead)
            OnDie();
    }

    public virtual void OnDie()
    {
        IsDead = true;
        OnDeath?.Invoke();
    }

    public void OnDamage(float damage, Vector2 hitPoint, float knckbackValue = 3)
    {
        if (IsDead)//죽음 감지
            return;

        Vector2 vec;//넉백
        vec = transform.position.x > hitPoint.x ? Vector2.right : Vector2.left;
        rb.AddForce(vec * knckbackValue);

        CurrentHealth -= damage;//피 깍임

        StartCoroutine(DamageColor(0.5f));
    }

    IEnumerator DamageColor(float time)
    {
        sp.color = Color.red;
        yield return new WaitForSeconds(time);
        sp.color = Color.white;
    }
}
