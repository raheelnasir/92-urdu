using Entities;

namespace View.Data
{
    public class ISetContentDetails
    {
        private readonly HttpClient http;
        public ISetContentDetails(HttpClient _http)
        {
            http = _http;
            _http.BaseAddress = new Uri("https://localhost:7170/"); // Replace with your actual API base URL.
        }

        public async Task SetContentDetails(EntContentDetails cup)
        {
            Console.WriteLine($"{cup.ContentName} {cup.ContentType} COntrollerSET CONTENT");
            var response = await http.PostAsJsonAsync("api/Content/setcontentdetails", cup);
           
        }

    }
}
