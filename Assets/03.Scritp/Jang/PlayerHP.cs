using Cinemachine;
using Cinemachine.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerHP : LivingEntity
{
    [SerializeField] private Slider HPBar;
    [SerializeField] private GameObject particle;
    CinemachineVirtualCamera vcam;
    SpriteRenderer sp;

    protected override void Awake()
    {
        base.Awake();
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        HPBar.maxValue = Health;
    }

    private void Update()
    {
        HPBar.value = CurrentHealth;

        if (CurrentHealth <= 0 && !IsDead)
        {
            IsDead = true;
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        GameObject part = Instantiate(particle, transform.position, Quaternion.identity);
        StopAllCoroutines();
        StartCoroutine(GameManager.instance.GameOver(1f));
        vcam.Follow = null;
        sp.enabled = false;
    }
}
