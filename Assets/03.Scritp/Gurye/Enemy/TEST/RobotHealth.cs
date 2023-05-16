using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHealth : LivingEntity
{
    public override void Awake()
    {
        base.Awake();
        Rb = GetComponent<Rigidbody2D>();
    }
}
