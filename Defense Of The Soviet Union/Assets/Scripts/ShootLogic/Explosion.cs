using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Explosion : MonoBehaviour
{
    [SerializeField] float _damage;
    [SerializeField] private AudioSource _source;
    public float Radius;
    public float Force;

    public GameObject ExplosionEffect;

    private void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, Radius);// Все коллайдеры которые попали в сферу помещаются в массив


        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.AddExplosionForce(Force, transform.position, Radius);
            }
            
            Enemy enemy = overlappedColliders[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                _source.Play();
                enemy.TakeDamage(_damage);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Doroga")
        {
            _source.Play();
            Explode();
            Destroy(gameObject);
            Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
