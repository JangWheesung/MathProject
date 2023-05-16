using Cinemachine;
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

    [Header("Shot")]
    [SerializeField] private Transform point;
    [SerializeField] private GameObject bullets;

    [Header("FanRander")]
    [SerializeField] private GameObject range;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float ultRadius;
    [SerializeField] private float angle;

    [Header("Other")]
    [SerializeField] private CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin vCam;

    private Slider gaugeBar;

    private void Awake()
    {
        gaugeBar = GameObject.Find("Gauge").GetComponent<Slider>();
        vCam = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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
        range.SetActive(true);

        if(Input.GetKey(KeyCode.K))
            range.transform.RotateAround(range.transform.GetChild(0).position, Vector3.forward, 0.5f);
        if (Input.GetKey(KeyCode.L))
            range.transform.RotateAround(range.transform.GetChild(0).position, Vector3.forward, -0.5f);

        if (Input.GetKeyDown(KeyCode.J))
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, ultRadius, LayerMask.GetMask("Enemy"));

            foreach (Collider2D enmy in cols)
            {
                Vector3 vec = enmy.gameObject.transform.position - transform.position;

                float degress = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(vec.normalized, range.transform.up));
                Debug.Log(degress);

                if (degress <= angle / 2)
                {
                    //범위 내 들어옴
                    Debug.Log($"enemy : {enmy}");
                }
            }

            particle.transform.rotation = range.transform.rotation;
            particle.Play();

            StartCoroutine(ShakeCamera(0.3f));

            range.SetActive(false);
            state = State.Normal;
        }
    }

    IEnumerator ShakeCamera(float time)
    {
        vCam.m_AmplitudeGain = 3;
        yield return new WaitForSeconds(time);
        vCam.m_AmplitudeGain = 0;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position, Vector3.forward, range.transform.up, angle / 2, ultRadius);
        Handles.DrawSolidArc(transform.position, Vector3.forward, range.transform.up, -angle / 2, ultRadius);
    }
}
