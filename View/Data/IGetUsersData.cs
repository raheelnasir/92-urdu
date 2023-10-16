using Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace View.Data
{
    public class IGetUsersData
    {
        private readonly HttpClient http;

        public IGetUsersData(HttpClient _http)
        {
            http = _http;
            _http.BaseAddress = new Uri("https://localhost:7170/"); // Replace with your actual API base URL.
        }

        public async Task<List<EntUserProfile>> GetUsersData(string role)
        {
            try
            {
                // Build the URL with the query parameter
                var url = $"api/UserProfile/getusersdata?role={role}";

                // Send a GET request to the API endpoint
                var response = await http.GetFromJsonAsync<List<EntUserProfile>>(url);
                Console.WriteLine($"Inter: {response}");

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching user data: {ex.Message}");
                return new List<EntUserProfile>(); // Return an empty list or handle the error as needed
            }
        }
    }
}
