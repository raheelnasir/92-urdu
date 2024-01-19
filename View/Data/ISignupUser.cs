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
            Console.WriteLine(response.Content.ToString());

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<SignupResponse>();
               return responseContent;
            }
            else
            {
                var responseContent = await response.Content.ReadFromJsonAsync<SignupResponse>();
                return responseContent;
            }

        }

    }
}
