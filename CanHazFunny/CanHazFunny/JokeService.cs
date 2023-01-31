using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace CanHazFunny
{
    public class JokeService : IGetJoke
    {
        private HttpClient HttpClient { get; } = new();

        public string? Joke { get; set; }

        public string GetJoke()
        {
            Joke = HttpClient.GetStringAsync("https://geek-jokes.sameerkumar.website/api?format=json").Result;
            dynamic? obj = JsonConvert.DeserializeObject(Joke);
            if (obj != null)
            {
                string json = obj.joke;
                return json;
            }
            else
            {
                throw new ArgumentException("Joke cannot be null, geek-jokes API did not return a joke", nameof(Joke));
            }
        }
    }
}
