using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShoot : MonoBehaviour
{
    [SerializeField] private float _reload;
    [SerializeField] private float _currentReload;
    [SerializeField] private float _weaponRange;

    //public GameObject ProjectileTank;
    //public GameObject Dulo;

    public float AngleInDegrees;
    float g = Physics.gravity.y;

    public Transform SpawnTransform; //Откуда вылетают снаряды
    public GameObject Bullet; //Снаряд

    //void Shoot()
    //{
    //    if (nearestEnemy != null)
    //    {
    //        Vector3 SpawnPoint = Dulo.transform.position;
    //        Quaternion SpawnRoot = Dulo.transform.rotation;
    //        GameObject Projectile = Instantiate(ProjectileTank, SpawnPoint, SpawnRoot) as GameObject;

    //        Rigidbody Force = Projectile.GetComponent<Rigidbody>();

    //        Force.AddForce(Projectile.transform.forward * 30, ForceMode.Impulse);

    //        //Destroy(Projectile, 5);

    //    }
    //}


    void Update()
    {
        SpawnTransform.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f);

        if (CanShoot())
        {
            Reload();
            SearchNShot();
        }
        if (_currentReload > 0)
        {
            _currentReload -= Time.deltaTime;
        }

    }
    private bool CanShoot()
    {
        if (_currentReload <= 0)
        {
            return true;
        }
        return false;
    }
    void Reload()
    {
        _currentReload = _reload;
        Debug.Log("Выстрел");
    }
    public void SearchNShot()
    {
        Transform nearestEnemy = null;
        float nearestEnemyDistance = Mathf.Infinity;

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
            Vector3 fromTo = nearestEnemy.position - transform.position;
            Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

            transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

            float x = fromToXZ.magnitude;
            float y = fromTo.y;

            float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

            float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
            float v = Mathf.Sqrt(Mathf.Abs(v2));

            GameObject newBullet = Instantiate(Bullet, SpawnTransform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().velocity = SpawnTransform.forward * v;

        }
    }
}
