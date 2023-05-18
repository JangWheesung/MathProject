using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 newVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        rb.velocity = newVelocity;

        if (Input.GetAxisRaw("Horizontal") >= 1)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        if (Input.GetAxisRaw("Horizontal") <= -1)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
