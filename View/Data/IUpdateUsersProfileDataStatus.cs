using Entities;

namespace View.Data
{
    public class IUpdateUsersProfileDataStatus
    {
        private readonly HttpClient http;
        public IUpdateUsersProfileDataStatus(HttpClient _http)
        {
            http = _http;
            http.BaseAddress = DomainHelper.GetDomain();
        }

        public async Task UpdateUsersProfileData(EntUserProfile ent)
        {
            if (ent != null)
            {
                var response = await http.PutAsJsonAsync("api/UserProfile/updateusersprofiledata", ent);
                Console.WriteLine($"{response} Interface");

            }
        }
    }
}
