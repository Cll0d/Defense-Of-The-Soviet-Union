using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float PowerShot;
    [SerializeField] private float _rangeRay;
    [SerializeField] float _damage;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _reload;
    [SerializeField] private float _currentReoload;
    [SerializeField] private float _weaponRange;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private Transform _transform;

    public GameObject mortarprojectile;
    public GameObject mortor;

    private Ray ray;
    private RaycastHit hit;
    private Enemy enemy;

    private void Update()
    {
        DrawRay();
        if (CanShoot())
        {
            SearchTarget();
        }
        if (_currentReoload > 0)
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
        
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float _currentDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (_currentDistance < nearestEnemyDistance && _currentDistance <= _weaponRange)
            {
                nearestEnemy = enemy.transform;
                nearestEnemyDistance = _currentDistance;
            }
        }
        if (nearestEnemy != null)
        {
            Aiming(nearestEnemy);
            Shoot(nearestEnemy);
        }
    }
    private void Aiming(Transform target)
    {
        Vector3 directionToTarget = target.position - _transform.position;
        directionToTarget.y = 0f;
        float currentRotationX = _transform.rotation.eulerAngles.x;
        float currentRotationZ = _transform.rotation.eulerAngles.z;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        Vector3 newEulerAngles = new Vector3(currentRotationX, targetRotation.eulerAngles.y - 80, currentRotationZ);
        targetRotation = Quaternion.Euler(newEulerAngles);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _rotSpeed * Time.deltaTime);
    }
    private void Shoot(Transform enemy)
    {
        _currentReoload = _reload;
        Debug.Log("Shoot");
        GameObject projectile = Instantiate(mortarprojectile) as GameObject;
        projectile.transform.position = mortor.transform.position;
        projectile.transform.rotation = mortor.transform.rotation;
        projectile.transform.Translate(3f, 5f, 2.5f);
        projectile.GetComponent<Rigidbody>().AddRelativeForce(projectile.transform.forward * PowerShot, ForceMode.Force);
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
                Debug.Log("enemy");
            }
        }
        StartCoroutine(Shoot());
    }
}
