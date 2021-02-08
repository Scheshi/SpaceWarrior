using Asteroids.Interfaces;
using UnityEngine;

namespace Asteroids.Models
{
    internal abstract class ModificationWeapon : IFire
    {
        Weapon _weapon;

        public abstract void AddModification(IWeapon weapon);

        public abstract void RemoveModification(IWeapon weapon);

        public void Fire()
        {
            _weapon.Fire();
        }
    }
}