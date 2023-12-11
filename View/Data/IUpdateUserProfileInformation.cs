using Entities;

namespace View.Data
{
    public class IUpdateUserProfileInformation

    {
        HttpClient http;
        public IUpdateUserProfileInformation(HttpClient _http)
        {
            http = _http;
            http.BaseAddress = DomainHelper.GetDomain();
        }
        public async Task<string> UpdateProfileInformation(EntUserProfile ent)
        {
            try
            {
                var response = await http.PutAsJsonAsync("api/UserProfile/updateuserprofileinformation", ent);

                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(message); // Print the message to the console
                    return message;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return "An Error Occurred. Please try again later.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: Error in UpdateProfileInformation");
                return "An Error Occurred. Please try again later.";
            }
        }
    }
}
