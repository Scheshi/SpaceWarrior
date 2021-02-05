using Asteroids.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Asteroids.ObjectPool
{
    internal sealed class BulletObjectPool : IPool
    {
        private readonly Dictionary<float, HashSet<Rigidbody2D>> _bulletCollection = new Dictionary<float, HashSet<Rigidbody2D>>();

        private Rigidbody2D Create(float damage)
        {
            var bullet = Bullet.CreateBullet(damage);
            Rigidbody2D rigidbody;
            if(!bullet.TryGetComponent(out rigidbody))
            {
                rigidbody = bullet.gameObject.AddComponent<Rigidbody2D>();
            }
            rigidbody.gravityScale = 0.0f;
            _bulletCollection[damage].Add(rigidbody);

            return rigidbody;

        }

        public Rigidbody2D Get<Rigidbody2D>(Vector3 position, float damage, Transform barrelTransform, GameController controller)
        {
            var bullet = GetListBullets(damage).FirstOrDefault(x => !x.gameObject.activeSelf);
            if(bullet == null)
            {
                bullet = Create(damage);
            }
            bullet.transform.position = position;
            bullet.gameObject.SetActive(true);
            return bullet.GetComponent<Rigidbody2D>();
        }

        private HashSet<Rigidbody2D> GetListBullets(float damage)
        {
            return _bulletCollection.ContainsKey(damage) ? _bulletCollection[damage] : _bulletCollection[damage] = new HashSet<Rigidbody2D>(); 
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
