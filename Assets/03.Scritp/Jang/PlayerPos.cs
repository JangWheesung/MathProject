using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void Update()
    {
        transform.position = player.transform.position;
    }
}
