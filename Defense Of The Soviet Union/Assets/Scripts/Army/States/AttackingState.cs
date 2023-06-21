using System.Collections;
using UnityEngine;

public class AttackingState : State
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _projectileSpawPosition;

    private IEnumerator ShootProjectile()
    {
        Instantiate(_projectile, _projectileSpawPosition.position, Quaternion.identity);
        yield return new WaitForSeconds(10f);
        StartCoroutine(ShootProjectile());
    }

    private void OnEnable()
    {
        StartCoroutine(ShootProjectile());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
