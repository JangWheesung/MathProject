using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public void OnDamage(float Damage, Vector2 hitPoint, float knckbackValue = 3f);
}
