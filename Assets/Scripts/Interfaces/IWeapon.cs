using System;
using UnityEngine;

namespace Asteroids.Interfaces
{
    interface IWeapon : IFire, IDisposable
    {
        void SetNewFireSound(AudioClip fireClip);

        void SetNewBarrelPosition(Transform barrel);

        void SetNewForce(float force);

        void SetNewFireRate(float fireRate);

        void SetNewDamage(float damage);

        void ResetFireSound();

        void ResetFireRate();
    }
}
