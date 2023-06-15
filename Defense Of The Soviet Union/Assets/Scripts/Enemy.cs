using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] float Armor;
    [SerializeField] float Damage;
    [SerializeField] private float _speedRun;
    [SerializeField] private int _coinForKill;
}
