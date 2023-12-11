using Entities;

namespace View.Data
{
    public  class IPostPhrases
    {
        private readonly HttpClient http;

        public IPostPhrases(HttpClient _http)
        {
            http= _http;
            http.BaseAddress = DomainHelper.GetDomain();
        }
        public async Task PostPhrases(EntPhrases eup)
        {
            try
            {
                var res = await http.PostAsJsonAsync("api/Content/postphrases", eup);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
            }

        }

    }
}
