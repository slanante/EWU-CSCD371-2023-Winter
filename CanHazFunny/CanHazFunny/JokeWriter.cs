using System;

namespace CanHazFunny
{
    public class JokeWriter : IWriteJoke
    {

        public virtual void SayJoke(string joke)
        {
            Console.WriteLine(joke);
        }
    }
}
