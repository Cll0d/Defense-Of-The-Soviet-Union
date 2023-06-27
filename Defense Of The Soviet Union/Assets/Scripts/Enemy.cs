using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _armor;
    [SerializeField] private float _damage;
    [SerializeField] private float _speedRun;
    [SerializeField] private int _coinForKill;

    private void Update()
    {
        if(_health <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float damage)
    {
        _health = (damage - _armor);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
