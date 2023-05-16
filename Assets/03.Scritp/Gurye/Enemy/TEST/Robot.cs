using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : EnemyFSM
{
    protected override void Awake()
    {
        base.Awake();
        _states = new IState[4];

        _states[(int)EnemyState.idle] = new EnemyStates.Idle();
        _states[(int)EnemyState.move] = new EnemyStates.Move();
        _states[(int)EnemyState.attack] = new EnemyStates.Attack();
        _states[(int)EnemyState.die] = new EnemyStates.Die();

        ChangeState((int)EnemyState.idle);
    }
}
