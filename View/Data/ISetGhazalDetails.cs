using Entities;

namespace View.Data
{
    public class ISetGhazalDetails
    {
        private readonly HttpClient http;

        public ISetGhazalDetails(HttpClient _http)
        {
            http = _http;
            http.BaseAddress = DomainHelper.GetDomain();
        }

        public async Task<int> SetGhazalDetails(EntContentDetails cup)
        {
            Console.WriteLine($"Controler agya ");

            var response = await http.PostAsJsonAsync("api/Content/setghazaldetails", cup);

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

            return -1;
        }
    }
}
