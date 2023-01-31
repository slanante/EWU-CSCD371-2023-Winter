using System;
using System.Diagnostics.Contracts;

namespace CanHazFunny
{
    public class Jester
    {
        private JokeService _jokeService;
        private JokeWriter _jokeWriter;
        
        public Jester(JokeService jokeGetter, JokeWriter jokeWriter)
        {
            _jokeService = jokeGetter ?? throw new ArgumentNullException(nameof(jokeGetter));
            _jokeWriter = jokeWriter ?? throw new ArgumentNullException(nameof(jokeWriter));
        
        }

        public void TellJoke()
        {
            string joke = _jokeService.GetJoke();
            
            while (joke.Contains("Chuck Norris"))
            {
                joke = _jokeService.GetJoke();
            }
            _jokeWriter.SayJoke(joke);
        }


    }
}
