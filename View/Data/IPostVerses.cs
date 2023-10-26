using Entities;
using System.Data.SqlClient;

namespace View.Data
{
    public class IPostVerses
    {
        private readonly HttpClient http;
        public IPostVerses(HttpClient _http)
        {
            http = _http;
            _http.BaseAddress = new Uri("https://localhost:7170/"); // Replace with your actual API base URL.
        }

        public async Task PostVerses(EntVerse cup)
        {
            Console.WriteLine($"{cup.Verse1}  COntrollerSET CONTENT");
            var response = await http.PostAsJsonAsync("api/Content/postverses", cup);

        }
    }
}
