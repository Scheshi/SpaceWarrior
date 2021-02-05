using Asteroids.Services;

namespace Asteroids.Interfaces
{
    public interface IAbility
    {
        AbilityType Type { get;}
        AttackAbilityType AttackType { get; }
        string Name { get; }
        int Points { get; }

        void Execute();
    }
}