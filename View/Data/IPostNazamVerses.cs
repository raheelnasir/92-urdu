using Entities;

namespace View.Data
{
    public class IPostNazamVerses
    {
        private readonly HttpClient http;
        public IPostNazamVerses(HttpClient _http)
        {
            http = _http;
            http.BaseAddress = DomainHelper.GetDomain();

        }
        public async Task PostNazamVerses(EntVerse obj)
        {
            await http.PostAsJsonAsync("api/Content/postnazamverses", obj);
        }
    }
}
