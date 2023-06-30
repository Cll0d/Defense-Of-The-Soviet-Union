using System.Collections;
using UnityEngine;

public class GunShotBehaviour : MonoBehaviour
{
    [SerializeField] private float _rangeRay;
    [SerializeField] private float _damage;
    [SerializeField] private float _reload;
    [SerializeField] private float _fullReload;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private Transform _transform;
    private Ray ray;
    private RaycastHit hit;
    private Enemy enemy;

    private void Start()
    {

    }
    private void Update()
    {
        DrawRay();
        if (CanShoot())
        {
            SearchTarget();
        }
        if (_fullReload > 0)
        {
            _fullReload -= Time.deltaTime;
        }
    }

    private void SearchTarget()
    {
        Transform nearestEnemy = null;
        float nearestEnemyDistance = Mathf.Infinity;
        Enemy enemy1 = GetComponent<Enemy>();

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
    private bool CanShoot()
    {
        if (_fullReload <= 0)
        {
            return true;
        }
        return false;
    }
    private void DrawRay()
    {
        ray = new Ray(_transform.position, -_transform.up);
        Debug.DrawRay(_transform.position, -_transform.up * _rangeRay, Color.yellow);

    }
    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_reload);
        if (Physics.Raycast(ray, out hit))
        {
            enemy = hit.collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
        }
        StartCoroutine(Shoot());
    }
}
