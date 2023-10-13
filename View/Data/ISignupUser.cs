using Entities;

namespace View.Data
{
    public class ISignupUser : IUserProfile
    {
        private readonly HttpClient _http;
        public ISignupUser(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://localhost:7170/"); // Replace with your actual API base URL.
        }

        public async Task SignupUser(EntUserProfile eup)
        {

            await _http.PostAsJsonAsync("https://localhost:7170/api/UserProfile/signupuser", eup);
        }      
    }
}
