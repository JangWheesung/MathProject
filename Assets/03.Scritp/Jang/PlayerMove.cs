using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;

    private PlayerAnim playerAnim;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerAnim = gameObject.GetComponent<PlayerAnim>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        AnimSetting();
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

    void AnimSetting()
    {
        playerAnim.MoveAnim(Input.GetAxisRaw("Horizontal"));
        playerAnim.JumpAnim(rb.velocity.y);
    }
}
