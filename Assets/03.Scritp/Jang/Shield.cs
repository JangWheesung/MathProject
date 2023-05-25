using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool isShield;

    void Update()
    {
        transform.RotateAround(transform.GetChild(0).position, Vector3.forward, 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet" && true)
        {
            isShield = true;
        }
    }
}
