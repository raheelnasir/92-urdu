using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUserAuth
    {

        public static async Task<UserAccount> Authenticate(string username, string password)
        {
            UserAccount userProfile = new UserAccount();
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_LoginUserAuth", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await sdr.ReadAsync())
                            {
                                userProfile.UserName = sdr["UserName"].ToString();
                                userProfile.Role = sdr["Role"].ToString();
                                userProfile.UId = sdr["UId"].ToString(); // Adjust data type as needed
                            }
                        }
                    }
                }
                Console.WriteLine($"Inter: {userProfile}");

            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                Console.WriteLine($"Error: {ex.Message}");
                // Consider more sophisticated error handling/logging here
            }
            return userProfile;
        }

    }
}




