using System.Net.Http;
using Newtonsoft.Json;

namespace CanHazFunny
{
    public class JokeService : IGetJoke
    {
        private HttpClient HttpClient { get; } = new();

        public string? Joke { get; set; }

        public virtual string GetJoke()
        {
            Joke = HttpClient.GetStringAsync("https://geek-jokes.sameerkumar.website/api?format=json").Result;
            dynamic? obj = JsonConvert.DeserializeObject(Joke);
            string json = obj!.joke;
            return json;
        }
    }
}
