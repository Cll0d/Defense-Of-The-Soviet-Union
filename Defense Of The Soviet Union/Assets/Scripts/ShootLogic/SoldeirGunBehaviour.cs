using System.Collections;
using UnityEngine;

public class SoldeirGunBehaviour : MonoBehaviour
{
    [SerializeField] private float _rangeRay;
    [SerializeField] private float _damage;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _speedAttack;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _currentAmmo;
    private bool _isReloading;
    [SerializeField] private Transform _transform;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private GameObject _flash;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    private Ray ray;
    private RaycastHit hit;
    private Enemy enemy;

    private void Start()
    {
        _currentAmmo = _maxAmmo;
    }
    private void Update()
    {
        DrawRay();
        if (_isReloading)
        {
            return;
        }
        if (_currentAmmo > 0)
        {
            SearchTarget();
        }
        else
        {
            StartCoroutine(Reload());
        }
    }
    private IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        _currentAmmo = _maxAmmo;
        _animator.SetBool("Shooting", false);
        _isReloading = false;
        _flash.SetActive(false);
        _audioSource.Stop();
    }

    private void SearchTarget()
    {
        Transform nearestEnemy = null;
        float nearestEnemyDistance = Mathf.Infinity;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float _currentDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (_currentDistance < nearestEnemyDistance && _currentDistance <= _rangeRay)
            {
                nearestEnemy = enemy.transform;
                nearestEnemyDistance = _currentDistance;
            }
        }
        if (nearestEnemy != null)
        {
            Aiming(nearestEnemy);
            StartCoroutine(Shoot());
        }
    }
    private void Aiming(Transform target)
    {
        Vector3 directionToTarget = target.position - _transform.position;
        directionToTarget.y = 0f;
        float currentRotationX = _transform.rotation.eulerAngles.x;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        Vector3 newEulerAngles = new Vector3(currentRotationX, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
        targetRotation = Quaternion.Euler(newEulerAngles);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _rotSpeed * Time.deltaTime);
    }
    private void DrawRay()
    {
        ray = new Ray(_transform.position, _transform.forward);
        Debug.DrawRay(_transform.position, _transform.forward * _rangeRay, Color.yellow);

    }
    private IEnumerator Shoot()
    {
        _flash.SetActive(true);
        _currentAmmo--;
        if (Physics.Raycast(ray, out hit))
        {
            if (_enemyMask == (_enemyMask | (1 << hit.collider.gameObject.layer)))
            {
                enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(_damage);
                    _animator.SetBool("Shooting", true);
                    _audioSource.Play();
                   if(enemy == null)
                    {
                        _audioSource.Stop();
                        _animator.SetBool("Shooting", false);
                    }
                }
            }
        }
        yield return new WaitForSeconds(_speedAttack);
        StartCoroutine(Shoot());
    }
}
