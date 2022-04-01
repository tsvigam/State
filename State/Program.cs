using System;

namespace State
{
    public class House
    {
        public IState State { get; set; }
        public uint LevelSecurity { get; set; }

        public House(IState st)
        {
            State = st;
            LevelSecurity = 0;
        }

        public void Lock()
        {
            State.Lock(this);
        }

        public void UnLock()
        {
            State.Unlock(this);
        }
    }
    public interface IState
    {
        public void Lock(House h);
        public void Unlock(House h);
    }

    public class GeneralState : IState
    {
        public void Unlock(House h)
        {
            Console.WriteLine("On plugs, open general door");
            if (h.LevelSecurity > 0)
                h.LevelSecurity--;
        }

        public void Lock(House h)
        {
            Console.WriteLine("Off plugs, close all doors, check cocks");
            h.State = new AdditionalState();
            h.LevelSecurity++;
        }
    }

    public class AdditionalState : IState
    {
        public void Unlock(House h)
        {
            Console.WriteLine("On water,unlock garage");
            h.State = new GeneralState();
            h.LevelSecurity--;
        }

        public void Lock(House h)
        {
            Console.WriteLine("Off water,lock garage");
            h.State = new ExtraLongState();
            h.LevelSecurity++;
        }
    }

    public class ExtraLongState : IState
    {
        public void Unlock(House h)
        {
            Console.WriteLine("On electricity, gas");
            h.State = new AdditionalState();
            h.LevelSecurity--;
        }

        public void Lock(House h)
        {
            Console.WriteLine("Off electricity, gas");
            if (h.LevelSecurity < 3)
                h.LevelSecurity++;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            House h = new House(new GeneralState());
            h.Lock();
            Console.WriteLine(h.LevelSecurity);
            h.Lock();
            Console.WriteLine(h.LevelSecurity);
            h.Lock();
            Console.WriteLine(h.LevelSecurity);
            h.Lock();
            Console.WriteLine(h.LevelSecurity);
            h.UnLock();
            h.UnLock();
            h.UnLock();
            Console.WriteLine(h.LevelSecurity);
        }
    }
}
