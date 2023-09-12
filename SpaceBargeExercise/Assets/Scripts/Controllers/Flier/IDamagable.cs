using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void DamageOrHeal(float damage);
    void Destroy();
    void Respawn();
}
