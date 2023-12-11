using Entities;

namespace View.Data
{
    public class ISetNazamDetails
    {
        private readonly HttpClient http;

        public ISetNazamDetails(HttpClient _http)
        {
            http = _http;
            http.BaseAddress = DomainHelper.GetDomain();

        }

        public async Task<int> SetNazamDetails(EntContentDetails eup)
        {
            try
            {
                var request = await http.PostAsJsonAsync("api/Content/setnazamdetails", eup);
                var response = await request.Content.ReadFromJsonAsync<int>();
                Console.WriteLine($"response sent{response}");
                return Convert.ToInt32(response);
            }
            catch
            {
                return -1;
            }
        }
    }
}
