using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
    public class DALUsersData
    {
        public static async Task<List<EntUserProfile>> GetUsersData(string role)
        {
            List<EntUserProfile> userProfiles = new List<EntUserProfile>();

            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetUsersData", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@role", role);
                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await sdr.ReadAsync())
                            {
                                var userProfile = new EntUserProfile();
                                userProfile.UserName = sdr["UserName"].ToString();
                                userProfile.Role = sdr["Role"].ToString();
                                userProfile.UId = Convert.ToInt32(sdr["UId"]);
                                userProfile.FirstName = sdr["FirstName"].ToString();
                                userProfile.LastName = sdr["LastName"].ToString();
                                userProfile.EmailAddress = sdr["EmailAddress"].ToString();
                                userProfile.PhoneNumber = sdr["PhoneNumber"].ToString();
                                userProfile.Password = sdr["Password"].ToString();

                                Console.WriteLine(userProfile.Password);
                                // Handle date parsing and validation
                                if (DateTime.TryParse(sdr["DateOfBirth"].ToString(), out DateTime dateOfBirth))
                                {
                                    userProfile.DateOfBirth = dateOfBirth;
                                }
                                else
                                {
                                    // Invalid date, handle accordingly
                                    userProfile.DateOfBirth = DateTime.MinValue; // or another default value
                                }

                                userProfile.City = sdr["City"].ToString();
                                userProfile.Area = sdr["Area"].ToString();
                                userProfile.Location = sdr["Location"].ToString();
                                userProfile.IsActive = Convert.ToBoolean(sdr["IsActive"]);

                                userProfiles.Add(userProfile); // Add the user to the list
                            }
                        }
                    }
                }

                return userProfiles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Return null in case of an exception
            }
        }
    }
}
