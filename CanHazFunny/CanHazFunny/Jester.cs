using System;
using System.Diagnostics.Contracts;

namespace CanHazFunny
{
    public class Jester
    {
        private JokeService _jokeService;
        private JokeWriter _jokeWriter;
        
        public Jester(JokeService getJoke, JokeWriter writeJoke)
        {
            _jokeService = getJoke ?? throw new ArgumentNullException(nameof(getJoke));
            _jokeWriter = writeJoke ?? throw new ArgumentNullException(nameof(writeJoke));
        
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
