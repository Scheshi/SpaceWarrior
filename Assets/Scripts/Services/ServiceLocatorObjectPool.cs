using Asteroids.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Asteroids.Services
{
    public static class ServiceLocatorObjectPool
    {
        private static readonly Dictionary<Type, IPool> _pools = new Dictionary<Type, IPool>();


        public static IPool Get<IPool>()
        {
            var result = (IPool)_pools.FirstOrDefault(x => x.Key == typeof(IPool)).Value;
            if (result == null) throw new NullReferenceException("Нет такого типа в ServiceLocatorObjectPool");
            else return result;
        }

        public static void Send(IPool pool)
        {
            if (_pools.ContainsKey(pool.GetType()))
            {
                Debug.LogError("Такой тип уже имеется в ServiceLocatorObjectPool");
                return;
            }
            _pools.Add(pool.GetType(), pool);
        }



    }
}
