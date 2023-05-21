using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    enum State { Idle, Tracking };
    State state = State.Idle;

    [SerializeField] private float radius;
    [SerializeField] private float speed;

    SpriteRenderer sp;
    Rigidbody2D rb;
    GameObject player;

    float shake = -180;
    const float shakeAomunt = 0.01f;

    private void Awake()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Floating();
        FSM();
    }

    void FSM()
    {
        if (PlayerRader()) 
            state = State.Tracking;

        if (state == State.Tracking)
            TrackingEnemy();
    }

    void Floating()
    {
        shake += shakeAomunt;
        transform.position += new Vector3(0, Mathf.Sin(shake) / 400f, 0);
    }

    void TrackingEnemy()
    {
        try
        {
            Vector2 vec = player.transform.position - transform.position;
            rb.velocity = vec * speed;

            sp.flipX = vec.x > 0 ? true : false;
        }
        catch (Exception exp) { }
    }

    bool PlayerRader()
    {
        return Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        state = State.Tracking;

        if (collision.transform.tag == "Player")
        {
            try
            {
                collision.transform.GetComponent<PlayerHP>().OnDamage(1, transform.position, 20);
            }
            catch (Exception exp) { }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
