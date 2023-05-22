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
            Vector2 forWord = transform.up;//적이 화면을 바라보는 쪽
            Vector2 vec = player.transform.position - transform.position; // 플레이어를 바라보는 방향
            rb.velocity = vec * speed;

            int flip = 0;
            if(Cross(forWord, vec).z < 0)//z가 0보다 작다면 회전
                flip = 1;
            transform.rotation = Quaternion.Euler(0, flip * 180, 0);
        }
        catch (Exception exp) { }
    }

    Vector3 Cross(Vector3 a, Vector3 b)//백터의 외적을 반환
    {
        float crossX = a.y * b.z - a.z * b.y;
        float crossY = a.z * b.x - a.x * b.z;
        float crossZ = a.x * b.y - a.y * b.x;
        return new Vector3(crossX, crossY, crossZ);
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
