using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPoetProfile 
    {
        public static async Task<EntUserProfile> GetPoetData(PoetGetParameters ent)

        {
            EntUserProfile? userProfile = new EntUserProfile();
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetPoetData", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(ent.PId));
                        cmd.Parameters.AddWithValue("@role", ent.Role);


                        Console.WriteLine($"{ent.Role} DAL {ent.PId}");

                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await sdr.ReadAsync())
                            {
                                userProfile.UserName = sdr["UserName"].ToString();
                                userProfile.FirstName = sdr["FirstName"].ToString();
                                userProfile.LastName = sdr["LastName"].ToString();
                                userProfile.EmailAddress = sdr["EmailAddress"].ToString();
                                userProfile.PhoneNumber = sdr["PhoneNumber"].ToString();
                                if (DateTime.TryParse(sdr["DateOfBirth"].ToString(), out DateTime dateOfBirth))
                                {
                                    userProfile.DateOfBirth = dateOfBirth;
                                }
                                else
                                {
                                    // Invalid date, handle accordingly
                                    userProfile.DateOfBirth = DateTime.MaxValue; // or another default value
                                }

                                userProfile.City = sdr["City"].ToString();
                                userProfile.Area = sdr["Area"].ToString();
                                userProfile.Location = sdr["Location"].ToString();
                            }
                        }
                    }

                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error In Poet Controller {ex}");
            }
            return userProfile;


        }
    }


}
