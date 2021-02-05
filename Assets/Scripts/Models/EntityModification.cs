using Asteroids.Interfaces;

namespace Asteroids.Models
{
    public class EntityModification
    {
        protected IEnemy _enemy;
        private EntityModification _next;

        public EntityModification(IEnemy enemy)
        {
            _enemy = enemy;
        }

        public void Add(EntityModification next)
        {
            if (_next != null)
            {
                _next.Add(next);
            }
            else _next = next;
        }

        public virtual void Handle() => _next.Handle();

    }
}