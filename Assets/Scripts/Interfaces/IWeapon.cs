using System;
using UnityEngine;

namespace Asteroids.Interfaces
{
    interface IWeapon : IFire, IDisposable
    {
        void SetFireSound(AudioClip fireClip, out AudioClip oldClip);

        void SetBarrelPosition(Transform barrel, out Transform oldBarrel);

        void SetForce(float force);

        void SetFireRate(float fireRate);

        void SetDamage(float damage);
    }
}
