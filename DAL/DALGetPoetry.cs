using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPoetry
    {
        public static async Task<List<EntPoetry>> GetPoetryData()
        {
            List<EntPoetry> contentDetailsList = new List<EntPoetry>();
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetPotry", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await sdr.ReadAsync())
                            {
                                EntPoetry contentDetails = new EntPoetry();
                                contentDetails.UId = Convert.ToInt32(sdr["UId"]);
                                contentDetails.ContentType = sdr["ContentType"].ToString();
                                contentDetails.ContentArrangement = sdr["ContentArrangement"].ToString();
                                contentDetails.ContentName = sdr["ContentName"].ToString();
                                contentDetails.Verses = sdr["Verses"].ToString();
                                contentDetails.FullName = sdr["FullName"].ToString();
                                contentDetails.UserName = sdr["UserName"].ToString();

                                contentDetailsList.Add(contentDetails);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error In DALPotry: {ex}");
            }
            return contentDetailsList;
        }
    }
}
