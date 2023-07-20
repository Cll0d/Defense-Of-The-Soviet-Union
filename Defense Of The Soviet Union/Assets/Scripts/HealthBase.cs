using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [SerializeField] private float _health;
    public float Health
    {
        get => _health;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if(_health < 1 )
        {
            Die();
        }    
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
}
