using Asteroids.Interfaces;
using Asteroids.Services;


namespace Asteroids.Models
{
    internal sealed class VoidAbility : IAbility
        {
            public string Name { get; }
            public int Points { get; }
            

            public AbilityType Type { get; }
            public AttackAbilityType AttackType { get; }

            public VoidAbility(string name, int damage, AbilityType type, AttackAbilityType
                damageType)
            {
                Name = name;
                Type = type;
                AttackType = damageType;
                Points = damage;
            }
            public override string ToString()
            {
                return Name;
            }
            
            public void Execute()
            {
                return;
            }
        }
    }