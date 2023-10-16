using Entities;
using System.Data.SqlClient;

namespace View.Data
{
    public class ICreateUser: IUserProfile
    {
        private readonly HttpClient http;
        public ICreateUser(HttpClient _http)
        {
            http = _http;
            _http.BaseAddress = new Uri("https://localhost:7170/"); // Replace with your actual API base URL.
        }

        public async Task<SignupResponse> CreateUser(EntUserProfile cup)
        {
            var response = await http.PostAsJsonAsync("api/UserProfile/createaccount", cup);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<SignupResponse>();
                return responseContent;
            }
            else
            {
                return new SignupResponse { Message = "Error during signup", Data = null };
            } 
        }

        public Task<List<EntUserProfile>> GetUsersData(string role)
        {
            throw new NotImplementedException();
        }

        public Task<SignupResponse> SignupUser(EntUserProfile eup)
        {
            throw new NotImplementedException();
        }
    }
}
