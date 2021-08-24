using Asteroids.Interfaces;
using UnityEngine;


namespace Asteroids.Models
{
    internal class ForceModification : ModificationWeapon
    {
        private readonly AudioClip _fireClip;
        private readonly float _damage;
        private readonly float _fireRate;

        private AudioClip _oldClip;

        public ForceModification(AudioClip fireClip, float fireRate, float damage)
        {
            _fireClip = fireClip;
            _fireRate = fireRate;
            _damage = damage;
        }

        public override void AddModification(IWeapon weapon)
        {
            weapon.SetFireSound(_fireClip, out _oldClip);
            weapon.SetFireRate(_fireRate);
            weapon.SetDamage(_damage);
        }

        public override void RemoveModification(IWeapon weapon)
        {
            weapon.SetFireSound(_oldClip, out _);
            weapon.SetFireRate(-_fireRate);
            weapon.SetDamage(-_damage);
        }
    }
}