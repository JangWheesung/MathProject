using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpSpeed;

    private Rigidbody2D rb;
    private IsGround isGround;

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
        if (Input.GetKeyDown(KeyCode.Space) && isGround.Ground())
        {
            Debug.Log(333);
            rb.velocity += Vector2.up * jumpSpeed;
        }
    }
    }
