using System;

namespace CanHazFunny
{
    public class JokeWriter : IWriteJoke
    {

        public void SayJoke(string joke)
        {
            Console.WriteLine(joke);
        }
    }
}
