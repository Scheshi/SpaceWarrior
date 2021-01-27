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
        public TEnemy Create<TEnemy>(Health health, Transform playerTransform, GameController gameController)
        {
            return (TEnemy)Create(health, typeof(TEnemy).Name, playerTransform, gameController);
        }

        public IEnemy Create(Health health, string typeName, Transform playerTransform, GameController gameController)
        {
            switch (typeName)
            {
                case "AsteroidEnemy":
                    return new AsteroidFactory().Create(health);
                case "Comet":
                    var enemy = (Comet)new CometFactory().Create(health);
                    var cometTransform = enemy.transform;
                    new CometMove(new MoveTransform(enemy.transform, 1.0f), gameController)
                        .Move(cometTransform.up.x, cometTransform.up.y, Time.deltaTime);
                    return enemy;
                case "EnemyShip":
                    var enemyTemp = new EnemyShipFactory().Create(health);
                    enemyTemp.TryGetAbstract<MonoBehaviour>(out var mono);
                    var enemyShipTransform = mono.transform;
                    //Еще один тип перемещения через Bridge
                    var persecutionMove = new UpdatablePersecutionMove(enemyShipTransform, playerTransform, 3.0f,
                        gameController);
                    var persecutionRotation = new UpdatablePersecutionRotation(enemyShipTransform, playerTransform, gameController);
                    var enemyShip = new Ship(persecutionMove, persecutionRotation);
                    enemyTemp.TryGetAbstract<EnemyShip>(out var ship);
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
                            persecutionMove.Stoping
                        );
            
                    persecutionMove.Stoping += enemyWeapon.Fire;
                    return enemyTemp;
                default:
                    throw new NullReferenceException("Нет такого типа в композиции фабрик");
            }
        }
    }
}