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
        }

        public async Task<List<EntUserProfile>> GetUsersData(string role)
        {
            try
            {
                var url = $"api/UserProfile/getusersdata?role={role}";
                var response = await http.GetFromJsonAsync<List<EntUserProfile>>(url);
                return response;
            }
            catch (Exception ex)
            {
                return new List<EntUserProfile>(); 
            }
        }
    }
}
