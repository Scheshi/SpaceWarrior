using System;
using Asteroids.Interfaces;
using Asteroids.Models;


namespace Asteroids.Fabrics
{
    internal sealed class EnemyFactoryComposite
    {
        public TEnemy Create<TEnemy>(Health health)
        {
            return (TEnemy)Create(health, typeof(TEnemy));
        }

        public IEnemy Create(Health health, Type type)
        {
            switch (type.Name)
            {
                case "AsteroidEnemy":
                    return new AsteroidFactory().Create(health);
                case "Comet":
                    return new CometFactory().Create(health);
                case "EnemyShip":
                    return new EnemyShipFactory().Create(health);
                default:
                    throw new NullReferenceException("Нет такого типа в композиции фабрик");
            }
        }
    }
}