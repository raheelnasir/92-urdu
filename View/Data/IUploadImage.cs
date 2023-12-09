namespace View.Data
{
    public class IUploadImage
    {
        private readonly HttpClient http;
        public IUploadImage(HttpClient _http)
        {
            http = _http;
        }

        public async void UploadImage(string path)
        {
            await http.PostAsJsonAsync("api/UserProfile/uploaduserprofileimage", path);

        }


    }
}
