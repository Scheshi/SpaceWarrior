using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Asteroids.ObjectPool
{
    internal sealed class BulletObjectPool
    {
        private static Dictionary<float, HashSet<Bullet>> _bulletCollection = new Dictionary<float, HashSet<Bullet>>();

        private static Bullet Create(GameObject gameObject, float damage)
        {
            var bullet = Bullet.CreateBullet(gameObject, damage);
            Rigidbody2D rigidbody;
            if(!bullet.TryGetComponent(out rigidbody))
            {
                rigidbody = bullet.gameObject.AddComponent<Rigidbody2D>();
            }
            rigidbody.gravityScale = 0.0f;
            _bulletCollection[damage].Add(bullet);

            return bullet;

        }

        public static Rigidbody2D GetBullet(GameObject gameObject, Vector3 position, float damage)
        {
            var bullet = GetListBullets(damage).FirstOrDefault(x => !x.gameObject.activeSelf);
            if(bullet == null)
            {
                bullet = Create(gameObject, damage);
            }
            bullet.transform.position = position;
            bullet.gameObject.SetActive(true);
            return bullet.GetComponent<Rigidbody2D>();
        }

        private static HashSet<Bullet> GetListBullets(float damage)
        {
            return _bulletCollection.ContainsKey(damage) ? _bulletCollection[damage] : _bulletCollection[damage] = new HashSet<Bullet>(); 
        }

        public static void ReturnToPool(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }
    }
}
