using NinjaShared;
using System.Numerics;

/*
The hardcoded dependency of weapons is an issue in this class because if we want to modify the set,
we must create a new one that define a new set of weapons; the idea to improve this is change a
hardcoded dependency for a dynamic one, using for that dependency injection.
*/

namespace NinjaBeforeOCP;

public class Ninja : IAttackable, IAttacker
{
    private readonly Weapon _sword = new Sword();
    private readonly Weapon _shuriken = new Shuriken();

    public string Name { get; }
    public Vector2 Position { get; set; }

    public Ninja(string name, Vector2? position = null)
    {
        Name = name;
        Position = position ?? Vector2.Zero;
    }

    public AttackResult Attack(IAttackable target)
    {
        var distance = this.DistanceFrom(target);
        if (_sword.CanHit(distance))
        {
            return new AttackResult(_sword, this, target);
        }
        else
        {
            return new AttackResult(_shuriken, this, target);
        }
    }
}