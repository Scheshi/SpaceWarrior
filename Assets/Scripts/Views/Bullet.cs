using Asteroids.Interfaces;
using Asteroids.ObjectPool;
using Asteroids;
using UnityEngine;
using Asteroids.Services;
using System;

public class Bullet : MonoBehaviour, IDisposable
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
        ServiceLocatorObjectPool.Get<BulletObjectPool>().ReturnToPool(gameObject);
    }

    public static Bullet CreateBullet(float damage)
    {
        var bullet = GameObject.Instantiate(Resources.Load<Bullet>("Prefabs/Bullet"));
        bullet._damage = damage;
        return bullet;
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }
}
