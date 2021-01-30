using System;
using Asteroids.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Asteroids.Models
{
    internal class ForceModification : ModificationWeapon
    {
        private readonly AudioClip _fireClip;
        private readonly float _damage;
        private readonly float _fireRate;

        public ForceModification(AudioClip fireClip, float fireRate, float damage)
        {
            _fireClip = fireClip;
            _fireRate = fireRate;
            _damage = damage;
        }

        public override void AddModification(IWeapon weapon)
        {
            weapon.SetNewFireSound(_fireClip);
            weapon.SetNewFireRate(_fireRate);
            weapon.SetNewDamage(_damage);
        }

        public void RemoveMofication(IWeapon weapon)
        {
            weapon.ResetFireSound();
            weapon.ResetFireRate();
            weapon.ResetDamage(_damage);
        }
    }
}