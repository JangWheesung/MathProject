using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        shake += shakeAomunt;//shake가 일정하게 더해진다.
        transform.position += new Vector3(0, Mathf.Sin(shake) / 400f, 0);
        //늘어나는 shake에 따라 더해지는 y가 1 ~ -1로 변한다.
    }

    void TrackingEnemy()
    {
        try
        {
            Vector2 forWord = transform.up;//적의 위쪽
            Vector2 vec = player.transform.position - transform.position; // 플레이어를 바라보는 방향
            rb.velocity = vec * speed;

            int flip = 0;
            if(Vector3.Cross(forWord, vec).z < 0)
                // 위쪽 수직과 플레이어를 바라보는 백터 사이의 각도가 0보다 작으면 왼쪽, 크면 오른쪽
                flip = 1;
            transform.rotation = Quaternion.Euler(0, flip * 180, 0);
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
