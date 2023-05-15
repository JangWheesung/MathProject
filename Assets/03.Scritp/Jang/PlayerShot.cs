using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShot : MonoBehaviour
{
    enum State { Normal, Ult }
    State state = State.Normal;

    [SerializeField] private Transform point;
    [SerializeField] private GameObject bullets;

    private Slider gaugeBar;

    private void Awake()
    {
        gaugeBar = FindObjectOfType<Slider>();
    }

    void Update()
    {
        Brain();
    }

    void Brain()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) && gaugeBar.value >= gaugeBar.maxValue)
            state = State.Ult;

        if (state == State.Normal)
            BulletShot();
        else if (state == State.Ult)
            Ultimate();
    }


    void BulletShot()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject bullet = Instantiate(bullets, point.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().vec = transform.rotation.y == 0 ? Vector2.right : Vector2.left;
        }
    }

    void Ultimate()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {

        }
    }
}
