
namespace Test
{
    internal sealed class Test
    {
        private void NameMethod()
        {
            Animals b = new Birds(new Fly());
            Animals m = new Mammals(new Run());
            
            b.Run.Move();
            m.Run.Move();
        } 
    }

    internal abstract class Animals
    {    
        public abstract IMove Run { get; }
    }

    internal class Birds : Animals
    {
        public override IMove Run { get; }
        public Birds(IMove run)
        {
            Run = run;
        }
    }

    internal class Mammals : Animals
    {
        public override IMove Run { get; }

        public Mammals(IMove run)
        {
            Run = run;
        }
    }

    internal class Run : IMove
    {
        public void Move()
        {
            
        }
    }

    internal class Fly : IMove
    {
        public void Move()
        {
            
        }
    }

    internal interface IMove
    {
        void Move();
    }
}
