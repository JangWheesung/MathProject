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

    private AudioSource shotSource;
    private AudioSource audioSource;
    private Slider gaugeBar;
    private Color backGround;
    private Color fill;

    private void Awake()
    {
        gaugeBar = GameObject.Find("Gauge").GetComponent<Slider>();

        backGround = gaugeBar.transform.GetChild(0).GetComponent<Image>().color;
        fill = gaugeBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color;

        shotSource = point.GetComponent<AudioSource>();
        audioSource = point.GetComponent <AudioSource>();

        vCam = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        Brain();
    }

    void Brain()
    {
        GaugeFull();

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

    void GaugeFull()
    {
        if (gaugeBar.value >= gaugeBar.maxValue)//게이지가 다 찾다면
        {
            float expression = (Mathf.Cos(Time.time * 2 * Mathf.PI) + 1) * 0.5f;
            //시간이 흐름에 따라 코사인의 값이 변경된다.
            //그래프에 +1를 대해줘서 코사인의 간격을 -1 ~ 1에서 0 ~ 2로 바꿔준다.
            //마지막으로 그래프에 1/2를 곱해줘서 0 ~ 2범위를 0 ~ 1로 바꿔준다.
            //expression이 0 ~ 1로 계속 변환한다.

            gaugeBar.transform.GetChild(0).GetComponent<Image>().color = new Color(backGround.r, backGround.g, backGround.b, expression);
            gaugeBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(fill.r, fill.g, fill.b, expression);
        }
        else
        {
            gaugeBar.transform.GetChild(0).GetComponent<Image>().color = new Color(backGround.r, backGround.g, backGround.b, 255);
            gaugeBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(fill.r, fill.g, fill.b, 255);
        }
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
            range.transform.RotateAround(range.transform.GetChild(0).position, Vector3.forward, 1.5f);
        if (Input.GetKey(KeyCode.L))
            range.transform.RotateAround(range.transform.GetChild(0).position, Vector3.forward, -1.5f);

        if (Input.GetKeyDown(KeyCode.J))
        {
            UltimateShot();
        }
    }

    void UltimateShot()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, ultRadius, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enmy in cols)//플레이어의 감지(원)안에 들면 모든 적들 검사
        {
            Vector3 vec = enmy.gameObject.transform.position - transform.position;//바라보는 방향

            float degress = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(vec.normalized, range.transform.up));
            //바라보는 방향과 부채꼴의 위쪽방향 사이의 각도를 구하고 Acos으로 라디안으로 변한한뒤, 다시 도로 변환 
            if (degress <= angle / 2)
            {
                //범위 내 들어옴
                float dmg = 5;//데미지
                enmy.GetComponent<LivingEntity>().OnDamage(dmg, transform.position, 9);
                enmy.GetComponent<LivingEntity>().slider.value -= dmg;
            }
        }

        particle.transform.rotation = range.transform.rotation;
        particle.Play();
        shotSource.Play();

        StartCoroutine(ShakeCamera(0.3f));

        range.SetActive(false);
        state = State.Normal;
    }

    IEnumerator ShakeCamera(float time)
    {
        vCam.m_AmplitudeGain = 3;
        yield return new WaitForSeconds(time);
        vCam.m_AmplitudeGain = 0;
    }

    //private void OnDrawGizmos()
    //{
    //    Handles.color = Color.red;
    //    DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
    //    Handles.DrawSolidArc(transform.position, Vector3.forward, range.transform.up, angle / 2, ultRadius);
    //    Handles.DrawSolidArc(transform.position, Vector3.forward, range.transform.up, -angle / 2, ultRadius);
    //    Gizmos.DrawWireSphere(transform.position, ultRadius);
    //}
}
