using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpSpeed;
    [SerializeField] float maxJumpcount;

    private Rigidbody2D rb;
    private IsGround isGround;

    public float jumpCount;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();                   
        isGround = transform.GetChild(0).GetComponent<IsGround>();
    }

    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            rb.velocity = Vector2.zero;
            rb.velocity += Vector2.up * jumpSpeed;
            jumpCount--;
        }

        if (isGround.Ground())
            jumpCount = maxJumpcount;
    }
}
