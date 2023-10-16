using Entities;

namespace View.Data
{
    public class IDeleteUsersProfile
    {
        private readonly HttpClient http;
        public IDeleteUsersProfile(HttpClient _http)
        {
            http = _http;
            http.BaseAddress = new Uri("https://localhost:7170/"); // Replace with your actual API base URL.


        }
        public async Task DeleteUsersProfileData(EntUserProfile ent)
        {
            if (ent != null)
            {
                var response = await http.PutAsJsonAsync("api/UserProfile/deleteusersprofiledata", ent);
                Console.WriteLine($"{response} Interface");

            }
        }


    }
}
