using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : LivingEntity
{
    [SerializeField] private GameObject prfHpBar;
    [SerializeField] private GameObject particle;

    private EnemyMovement enemyMovement;
    private AudioSource audioSource;
    const float height = 1.5f;

    Camera mainCam;

    protected override void Awake()
    {
        base.Awake();

        mainCam = Camera.main;
        canvers = GameObject.Find("Canvas");
        hpBar = Instantiate(prfHpBar, canvers.transform).GetComponent<RectTransform>();
        slider = hpBar.GetComponent<Slider>();

        enemyMovement = gameObject.GetComponent<EnemyMovement>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        slider.maxValue = Health;
    }

    void Update()
    {
        if (CurrentHealth <= 0 && !IsDead)
            OnDie();

        HpBar();
    }

    void HpBar()
    {
        Vector3 hpBarVec = new Vector3(transform.position.x, transform.position.y + height, 0);
        Vector3 hpBarPos = mainCam.WorldToScreenPoint(hpBarVec);
        hpBar.position = hpBarPos;
    }

    public override void OnDie()
    {
        base.OnDie();

        IsDead = true;
        enemyMovement.enabled = false;

        GameObject part = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(part, 1);
        hpBar.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        const int dmg = 1;
        if (collision.transform.tag == "Bullet")
        {
            audioSource.Play();
            OnDamage(dmg, collision.transform.position);
            slider.value -= dmg;
        }
    }
}
