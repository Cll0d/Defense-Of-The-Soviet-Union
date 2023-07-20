using System.Collections;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _armor;
    [SerializeField] private float _damage;
    [SerializeField] private float _speedRun;
    [SerializeField] private int _coinForKill;
    [SerializeField] private CoinManager _coinManager;
   // [SerializeField] private Animator _animator;
    [SerializeField] private Transform RunPoint;
    private HealthBase _healthBase;
    private bool _isAttack = false;
    private void Awake()
    {
        _coinManager = CoinManager.Instance;
    }

    private void Update()
    {
        if(!_isAttack)
        {
            Run();
        }    
    }
    public void TakeDamage(float damage)
    {
        _health -= damage * (1 -  (_armor / 100));
        if(_health < 1)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
        _coinManager.PayKill(_coinForKill);
    }
    private void Run()
    {
        transform.position = Vector3.MoveTowards(transform.position, RunPoint.position, _speedRun * Time.deltaTime);
    }
    private IEnumerator Attack()
    {
        if(_healthBase.Health > 0)
        {
            yield return new WaitForSeconds(1);
            _healthBase.TakeDamage(_damage);
            StartCoroutine(Attack());
        }
        Debug.Log("attack");
    }
    private void OnTriggerEnter(Collider other)
    {
        HealthBase  healthBase = other.gameObject.GetComponent<HealthBase>();
        if (healthBase)
        {
            _healthBase = healthBase;
            _isAttack = true;
            StartCoroutine(Attack());
        }
        else
        {
            _isAttack = false;
        }
    }
}
