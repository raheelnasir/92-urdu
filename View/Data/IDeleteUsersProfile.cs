using Entities;

namespace View.Data
{
    public class IDeleteUsersProfile
    {
        private readonly HttpClient http;
        public IDeleteUsersProfile(HttpClient _http)
        {
            http = _http;


        }
        public async Task DeleteUsersProfileData(EntUserProfile ent)
        {
            if (ent != null)
            {
                var response = await http.PutAsJsonAsync("api/UserProfile/deleteusersprofiledata", ent);

            }
        }


    }
}
