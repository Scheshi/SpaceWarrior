using Asteroids.Interfaces;
using UnityEngine;


namespace Asteroids.Models
{
    internal class WeaponLocker : IWeapon
    {
        private IWeapon _weapon;

        public bool IsLock { get; set; }

        
        public WeaponLocker(IWeapon weapon)
        {
            //IsLock = true;
            _weapon = weapon;
        }

        
        public void Fire()
        {
            if (!IsLock)
            {
                _weapon.Fire();
            }
            else Debug.Log("Оружие заблокировано");
        }
    }
}