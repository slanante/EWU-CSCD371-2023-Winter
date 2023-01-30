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
            string json = JsonConvert.SerializeObject(obj);
            json = json.Replace("\"joke\":", "");
            json = json.Replace("{", "").Replace("}", "");
            return json;
        }
    }
}
