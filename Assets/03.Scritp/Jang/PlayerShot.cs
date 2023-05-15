using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject bullets;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject bullet = Instantiate(bullets, transform.position, Quaternion.identity);
        }
    }
}
