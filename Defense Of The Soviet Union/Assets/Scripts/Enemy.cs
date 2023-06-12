using UnityEngine;

public interface IMob
{
   public float Health { get; }
   public float Armor { get; }
   public float Damage { get; }
   public float AttackSpeed { get; }

   public void Die()
    {

    }
    public void TakeDamage(float damage, float health, float armor)
    {

    }
}

public class Enemy : MonoBehaviour
{
    private float _speed;
    private int _coinForKill;
}
