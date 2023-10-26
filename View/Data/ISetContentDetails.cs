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

        public async Task<int> SetContentDetails(EntContentDetails cup)
        {
            Console.WriteLine($"{cup.ContentName} {cup.ContentType} COntrollerSET CONTENT");
            var response = await http.PostAsJsonAsync("api/Content/setcontentdetails", cup);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (int.TryParse(content, out int contentId))
                {
                    return contentId;
                }
                else
                {
                    Console.WriteLine($"Failed to parse ContentId from response content: {content}");
                }
            }
            else
            {
                Console.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
            }

            // Return a default value or handle the error as appropriate for your application.
            return -1; // Replace with an appropriate default value or error handling.
        }
       






    }
}
