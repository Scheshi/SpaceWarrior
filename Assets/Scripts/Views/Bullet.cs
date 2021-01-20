using Asteroid.Interfaces;
using Asteroid.ObjectPool;
using Asteroids;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private float _damage;

    public float Damage => _damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out var damagable) && !collision.gameObject.TryGetComponent<BarrelMarker>(out _))
        {
            damagable.Damage(_damage);
            Destroy();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy();
    }

    private void Destroy()
    {
        BulletObjectPool.ReturnToPool(this);
    }

    public static Bullet CreateBullet(GameObject gameObject, float damage)
    {
        var bullet = GameObject.Instantiate(gameObject).AddComponent<Bullet>();
        bullet._damage = damage;
        return bullet;
    }
}
