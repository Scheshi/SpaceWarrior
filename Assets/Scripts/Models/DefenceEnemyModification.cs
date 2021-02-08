using Asteroids.Interfaces;
using Asteroids.Models;

namespace Assets.Scripts.Models
{
    public class DefenceEnemyModification : EntityModification
    {
        private float _defence;
        public DefenceEnemyModification(IEnemy enemy, float defence) : base(enemy)
        {
            _defence = defence;
        }

        public override void Handle()
        {
            _enemy.Defence += _defence;
            base.Handle();
        }
    }
}