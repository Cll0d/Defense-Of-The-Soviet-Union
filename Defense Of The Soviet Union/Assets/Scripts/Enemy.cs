using System.Collections;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _armor;
    [SerializeField] private float _damage;
    [SerializeField] private int _coinForKill;
    [SerializeField] private CoinManager _coinManager;
    private Animator _animator;
    private HealthBase _healthBase;
    private bool _isAttack = false;
    private void Awake()
    {
        _coinManager = CoinManager.Instance;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!_isAttack)
        {
            _animator.SetBool(name: "Atack", value: false); 
            
        }
        else
        {
            _animator.SetBool(name: "Atack", value: true);
        }
    }
    public void TakeDamage(float damage)
    {
        _health -= damage * (1 -  (_armor / 100));
        if(_health < 1)
        {
            _animator.SetBool(name: "Die", value: true);
            Invoke("Die", 2.0f);
            //Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
        _coinManager.PayKill(_coinForKill);
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
