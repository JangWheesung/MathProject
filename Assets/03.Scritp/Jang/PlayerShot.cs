using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerShot : MonoBehaviour
{
    enum State { Normal, Ult }
    State state = State.Normal;

    [SerializeField] private Transform point;
    [SerializeField] private GameObject bullets;

    [SerializeField] private GameObject range;
    [SerializeField] private float ultRadius;

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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gaugeBar.value >= gaugeBar.maxValue)
        {
            gaugeBar.value = 0;
            state = State.Ult;
        }

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
        Vector3 pos;
        range.SetActive(true);

        if(Input.GetKey(KeyCode.K))
            range.transform.RotateAround(range.transform.GetChild(0).position, Vector2.left, 1);
        if (Input.GetKey(KeyCode.L))
            range.transform.RotateAround(range.transform.GetChild(0).position, Vector2.left, -1);

        if (Input.GetKeyDown(KeyCode.J))
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, ultRadius, LayerMask.GetMask("Enemy"));

            foreach (Collider2D enmy in cols)
            {
                Vector3 vec = enmy.gameObject.transform.position - transform.position;

                float degress = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(vec.normalized, range.transform.forward));

                if (degress <= 50)
                {
                    //범위 내 들어옴
                    Debug.Log(enmy);
                }
            }

            range.SetActive(false);
            state = State.Normal;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        //Handles.DrawSolidArc(transform.position, Vector3.forward, transform.up, 100 / 2, 2);
        //Handles.DrawSolidArc(transform.position, Vector3.forward, transform.up, -100 / 2, 2);
    }
}
