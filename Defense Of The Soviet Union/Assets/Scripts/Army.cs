using UnityEngine;

public class Army : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] float Armor;
    [SerializeField] float Damage;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _reoload;
    [SerializeField] private float _currentReoload;
    [SerializeField] private float _weaponRange;

    private AudioSource _audioSource;
    private AudioSource _audioShootSource;

    private void Update()
    {
        if (CanShoot())
        {
            SearchTarget();
        }

        if(_currentReoload > 0)
        {
            _currentReoload -= Time.deltaTime;
        }    
    }

    private bool CanShoot()
    {
        if (_currentReoload <= 0)
        {
            return true;
        }
        return false;
    }

    private void SearchTarget()
    {
        Transform nearestEnemy = null;
        float nearestEnemyDistance = Mathf.Infinity;
        Enemy enemy1 = GetComponent<Enemy>();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float _currentDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (_currentDistance < nearestEnemyDistance && _currentDistance <= _weaponRange)
            {
                nearestEnemy = enemy.transform;
                nearestEnemyDistance = _currentDistance;
            }
        }
        if (nearestEnemy != null)
        {
            Shoot(nearestEnemy);
        }
    }

    private void Shoot(Transform enemy)
    {

        _currentReoload = _reoload;
        Debug.Log("Shoot");
    }

}
