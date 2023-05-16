using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LivingEntity : MonoBehaviour, IDamage
{
    public float Health { get; protected set; }
    public bool IsDead { get; protected set; }
    private float CurrentHealth;
    public UnityEvent OnDeath;
    protected Rigidbody2D Rb;
    protected SpriteRenderer sp;

    public virtual void Awake()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
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

    public void OnDamage(float damage, Vector2 hitPoint, float knckbackValue = 3)
    {
        if (IsDead)//죽음 감지
            return;

        Vector2 vec;//넉백
        vec = transform.position.x > hitPoint.x ? Vector2.right : Vector2.left;
        Rb.AddForce(vec * knckbackValue);

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
