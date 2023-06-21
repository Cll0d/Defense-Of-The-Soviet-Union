using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyBehaviour : MonoBehaviour
{
    [SerializeField] private float _healt;
    [SerializeField] private float _damage;
    [SerializeField] private float _armor;
    [SerializeField] private float _radius;
    [SerializeField] private float _reload;

    private void Update()
    {
        CheckInRadius(_radius);
    }

    private void CheckInRadius(float radius)
    {
        Ray ray = new Ray(transform.position, transform.right*radius);
        Ray rayLeft = new Ray(transform.position, transform.right);
        Ray rayRight = new Ray(transform.position, transform.right);
        Debug.DrawRay(transform.position, transform.right * radius, Color.yellow);
        RaycastHit hit;
        Enemy enemy = GetComponent<Enemy>();
        if (Physics.Raycast(ray, out hit))
        {
            if (enemy)
            {
                Shoot();
                enemy.TakeDamage(_damage);
            }
        }
    }
    private void Shoot()
    {
        Debug.Log("Shoot");
    }
}
