using System;
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
            string path = "Prefabs/Enemy";
            var prefab = Resources.Load<EnemyShip>(path);
            if (prefab)
            {
                var enemy = GameObject.Instantiate(prefab);
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
                var bulletPrefab = Resources.Load<Rigidbody2D>("Prefabs/Bullet");
                if (bulletPrefab)
                {
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
                }
                else throw new NullReferenceException("Bullet is null");

                return enemy;
            }
            else throw new NullReferenceException(path + " is " + prefab);
        }

        public IEnemy Create(GameObject obj)
        {
            return GameObject.Instantiate(obj).AddComponent<EnemyShip>();
        }
    }
}
