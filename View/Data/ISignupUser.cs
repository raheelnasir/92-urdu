using Entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace View.Data
{
    public class ISignupUser : IUserProfile
    {
        private readonly HttpClient _http;

        public ISignupUser(HttpClient http)
        {
            _http = http;
            http.BaseAddress = DomainHelper.GetDomain();
        }

        public Task<SignupResponse> CreateUser(EntUserProfile cup)
        {
            throw new NotImplementedException();
        }

        public Task<List<EntUserProfile>> GetUsersData(string role)
        {
            throw new NotImplementedException();
        }

        public async Task<SignupResponse> SignupUser(EntUserProfile eup)
        {
            var response = await _http.PostAsJsonAsync("api/UserProfile/signupuser", eup);
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                // If the request was successful, deserialize the response content into a SignupResponse object
                var responseContent = await response.Content.ReadFromJsonAsync<SignupResponse>();
               return responseContent;
            }
            else
            {
                return new SignupResponse { Message = "Error during signup", Data = null };
            }
        }

    }
}
