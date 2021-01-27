using System;
using Asteroids.Interfaces;
using Asteroids.Models;
using Asteroids.Services;
using Asteroids.Views;
using UnityEngine;


namespace Asteroids.Fabrics
{
    internal sealed class EnemyFactoryComposite
    {
        public TEnemy Create<TEnemy>(Health health, Transform playerTransform, GameController gameController, Vector3 position)
        {
            return (TEnemy)Create(health, typeof(TEnemy).Name, playerTransform, gameController, position);
        }

        public IEnemy Create(Health health, string typeName, Transform playerTransform, 
            GameController gameController, Vector3 position)
        {
            IEnemy enemy;
            switch (typeName)
            {
                case "Asteroid":
                    enemy = new AsteroidFactory().Create(health);
                    if (enemy.TryGetAbstract<MonoBehaviour>(out var monoAsteroid))
                    {
                        monoAsteroid.transform.position = position;
                    }
                    break;
                case "Comet":
                    enemy = new CometFactory().Create(health);
                    if (enemy.TryGetAbstract<MonoBehaviour>(out var monoEnemy))
                    {
                        var cometTransform = monoEnemy.transform;
                        cometTransform.position = position;
                        cometTransform.up = playerTransform.position - cometTransform.position;
                        new CometMove(new MoveTransform(cometTransform, 1.0f), gameController)
                            .Move(cometTransform.up.x, cometTransform.up.y, Time.deltaTime);
                    }
                    else Debug.LogWarning("Этот тип не является MonoBehaviour");

                    break;

                case "EnemyShip":
                    enemy = new EnemyShipFactory().Create(health);
                    if (enemy.TryGetAbstract<MonoBehaviour>(out var mono))
                    {
                        var enemyShipTransform = mono.transform;
                        enemyShipTransform.position = position;
                        //Еще один тип перемещения через Bridge
                        var persecutionMove = new UpdatablePersecutionMove(enemyShipTransform, playerTransform, 3.0f,
                            gameController);
                        var persecutionRotation =
                            new UpdatablePersecutionRotation(enemyShipTransform, playerTransform, gameController);
                        var enemyShip = new Ship(persecutionMove, persecutionRotation);
                        enemy.TryGetAbstract<EnemyShip>(out var ship);
                        ship.InjectMovement(persecutionMove);
                        var enemyWeapon = new WeaponFactory(
                                //Временный костыль. Потом придумаю, как реализовать это через инспектор
                                new BulletData()
                                {
                                    Bullet = Resources.Load<Rigidbody2D>("Prefabs/Bullet"),
                                    Damage = 10.0f,
                                    Force = 5.0f
                                })
                            .Create(
                                mono.GetComponentInChildren<BarrelMarker>(),
                                persecutionMove.Stoping,
                                mono.GetComponent<EnemyShip>().Weapon
                            );
                        persecutionMove.Stoping += enemyWeapon.Fire;
                    }
                    else Debug.LogWarning("Тип не является MonoBehaviour");

                    break;
                
                default:
                    throw new NullReferenceException("Нет такого типа в композиции фабрик");
            }
            return enemy;
        }
    }
}