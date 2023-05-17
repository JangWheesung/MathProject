using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    [SerializeField] private GameObject player;


    private void FixedUpdate()
    {
        try
        {
            transform.position = player.transform.position;
        }
        catch (Exception exp) { }
    }
}
