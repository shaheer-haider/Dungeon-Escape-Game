using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    bool _canDamage = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null && _canDamage)
        {
            hit.Damage();
            _canDamage = false;
            Enemy.isHit = true;
            Invoke("ResetAttackTime", 0.2f);
        }
    }
    void ResetAttackTime()
    {
        _canDamage = true;
        Enemy.isHit = false;
    }
}
