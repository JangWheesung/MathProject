using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    enum State { Idle, Tracking };
    State state = State.Idle;

    [SerializeField] private float radius;

    GameObject player;

    float shake = -180;
    const float shakeAomunt = 0.01f;

    private void Awake()
    {
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
        Vector2 vec = player.transform.position - transform.position;
    }

    bool PlayerRader()
    {
        return Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        state = State.Tracking;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, radius);
    }
}
