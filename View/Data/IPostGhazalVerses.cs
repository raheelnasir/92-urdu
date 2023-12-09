using Entities;
using System.Data.SqlClient;

namespace View.Data
{
    public class IPostGhazalVerses
    {
        private readonly HttpClient http;
        public IPostGhazalVerses(HttpClient _http)
        {
            http = _http;
            http.BaseAddress = new Uri("https://localhost:7170/");

        }

        public async Task PostGhazalVerses(EntVerse cup)
        {
            Console.WriteLine($"{cup.Verse1}  ControllerSet CONTENT");
            await http.PostAsJsonAsync("api/Content/postghazalverses", cup);

        }
    }
}
