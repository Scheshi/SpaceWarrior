using Asteroid.Interfaces;
using Asteroids;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out var damagable) && !collision.gameObject.TryGetComponent<BarrelMarker>(out _))
        {
            damagable.Damage(Damage);
            //TO-DO ObjectPool
            Destroy(gameObject);
        }
    }
}
