using Asteroids.Interfaces;
using Asteroids.Views;
using Asteroids.Models;
using Asteroids.Services;
using Models;
using UnityEngine;


namespace Asteroids.Fabrics
{
    class EnemyShipFactory : IEnemyFactory
    {

        public bool TryParse(SerializableObjectInfo serialized, out IEnemy enemy, GameController gameController, Vector3 position,
            Transform playerTransform)
        {
            if (serialized.Type.ToLower() == nameof(EnemyShip).ToLower())
            {
                enemy = Create(new Health(serialized.Health), position, playerTransform, gameController);
                return true;
            }
            else
            {
                enemy = default;
                return false;
            }
        }

        public IEnemy Create(Health health, Vector3 position, Transform playerTransform, GameController gameController)
        {
            var enemy = GameObject.Instantiate(Resources.Load<EnemyShip>("Prefabs/Enemy"));
            enemy.InjectHealth(health);
            health.Death += enemy.Death;
            var enemyShipTransform = enemy.transform;
            enemyShipTransform.position = position;
            //Еще один тип перемещения через Bridge
            var persecutionMove = new UpdatablePersecutionMove(enemyShipTransform, playerTransform, 3.0f,
                gameController);
            var persecutionRotation =
                new UpdatablePersecutionRotation(enemyShipTransform, playerTransform, gameController);
            var enemyShip = new Ship(persecutionMove, persecutionRotation);
            enemy.InjectMovement(persecutionMove);
            var enemyWeapon = new WeaponFactory(
                    //Временный костыль. Потом придумаю, как реализовать это через инспектор
                    new BulletData()
                    {
                        Bullet = Resources.Load<Rigidbody2D>("Prefabs/Bullet"),
                        Damage = 10.0f,
                        Force = 5.0f
                    })
                .Create(
                    enemy.GetComponentInChildren<BarrelMarker>(),
                    persecutionMove.Stoping,
                    enemy.GetComponent<EnemyShip>().Weapon
                );
            persecutionMove.Stoping += enemyWeapon.Fire;
            return enemy;
        }

        public IEnemy Create(GameObject obj)
        {
            return GameObject.Instantiate(obj).AddComponent<EnemyShip>();
        }
    }
}
