using Entities;

namespace View.Data
{
    public class IUploadImage
    {
        private readonly HttpClient http;
        public IUploadImage(HttpClient _http)
        {
            http = _http;
            http.BaseAddress = DomainHelper.GetDomain();
        }

        public async void UploadImage(EntProfileImage eup)
        {
            await http.PostAsJsonAsync("api/UserProfile/uploaduserprofileimage", eup);

        }


    }
}
