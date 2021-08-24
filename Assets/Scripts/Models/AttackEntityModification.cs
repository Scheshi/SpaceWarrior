using Asteroids.Interfaces;
using Asteroids.Models;

namespace Assets.Scripts.Models
{
    public sealed class AttackEntityModification : EntityModification
    {
        float _attack;
        public AttackEntityModification(IEnemy enemy, float attack) : base(enemy)
        {
            _attack = attack;
        }

        public override void Handle()
        {
            _enemy.Attack += _attack;
            base.Handle();
        }
    }
}