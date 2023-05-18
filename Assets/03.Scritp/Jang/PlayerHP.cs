using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : LivingEntity
{
    [SerializeField] private Slider HPBar;
    [SerializeField] private GameObject particle;

    protected override void Awake()
    {
        base.Awake();
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
        StartCoroutine(GameManager.instance.GameOver(1f));
        Destroy(gameObject);
    }
}
