using System.Net.Http;

namespace CanHazFunny
{
    public class JokeService : IGetJoke
    {
        private HttpClient HttpClient { get; } = new();

        public string? Joke { get; set; }

        public string GetJoke()
        {
            Joke = HttpClient.GetStringAsync("https://geek-jokes.sameerkumar.website/api").Result;
            return Joke;
        }
    }
}
