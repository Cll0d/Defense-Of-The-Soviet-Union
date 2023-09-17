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
<<<<<<< Updated upstream
=======
<<<<<<< HEAD

=======
>>>>>>> Stashed changes
    [SerializeField] private AudioSource _audioSource;
    
>>>>>>> 24bf2a693cde8a8c482608ae2ce2e22fbddea287
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
        //if (!_isAttack)
        //{
        //    _animator.SetBool(name: "Atack", value: false);

        //}
        //else
        //{
        //    _animator.SetBool(name: "Atack", value: true);
        //}
    }
    public void TakeDamage(float damage)
    {
        _health -= damage * (1 - (_armor / 100));
        if (_health < 1)
        {
            _animator.SetBool(name: "Die", value: true);
            _audioSource.Play();
            Invoke("Die", 2.0f);
        }
    }
    private void Die()
    {
        Destroy(gameObject);
        _coinManager.PayKill(_coinForKill);
        Destroy(transform.parent.gameObject);
    }
    private IEnumerator Attack()
    {
        if (_healthBase.Health > 0)
        {
            yield return new WaitForSeconds(1);
            _healthBase.TakeDamage(_damage);
            StartCoroutine(Attack());

        }
        else
        {
            _animator.SetBool(name: "Walk", value: true); 
            _animator.SetBool(name: "Atack", value: false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        HealthBase healthBase = other.gameObject.GetComponent<HealthBase>();
        if (healthBase != null)
        {
            _healthBase = healthBase;
            _isAttack = true;
            StartCoroutine(Attack());
            _animator.SetBool(name: "Atack", value: true);
        }
        else
        {
            _isAttack = false;
                _animator.SetBool(name: "Walk", value: true);
        }
    }
}
