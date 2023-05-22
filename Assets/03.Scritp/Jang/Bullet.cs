using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    public Vector2 vec;

    private Slider gaugeBar;
    private Rigidbody2D rb;

    private void Awake()
    {
        gaugeBar = GameObject.Find("Gauge").GetComponent<Slider>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        Destroy(gameObject, 1);
    }

    void Update()
    {
        rb.velocity = vec * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            gaugeBar.value++;
        }
        Destroy(gameObject);
    }
}
