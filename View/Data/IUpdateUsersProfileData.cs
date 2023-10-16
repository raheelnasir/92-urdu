using Entities;

namespace View.Data
{
    public class IUpdateUsersProfileData
    {
        private readonly HttpClient http;
       public IUpdateUsersProfileData(HttpClient _http)
        {
            http = _http;
            http.BaseAddress = new Uri("https://localhost:7170/"); // Replace with your actual API base URL.
        }

        public async Task UpdateUsersProfileData(EntUserProfile ent)
        {
            if(ent!=null)
            {
               var response = await http.PutAsJsonAsync("api/UserProfile/updateusersprofiledata", ent);
                Console.WriteLine($"{response} Interface");

            }
        }
    }
}
