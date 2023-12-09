using Entities;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DAL
{
    public static class DALGetPoetOfDay
    {
        public static async Task<EntPoetOfDay> GetPoetOfDay(string day)
        {
            EntPoetOfDay Poet = new EntPoetOfDay();
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetPoetOfDay", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Add the @day parameter
                        cmd.Parameters.Add(new SqlParameter("@day", System.Data.SqlDbType.NVarChar, 20));
                        cmd.Parameters["@day"].Value = day;

                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await sdr.ReadAsync())
                            {
                                Poet.FullName = sdr["FullName"].ToString();
                                Poet.City = sdr["City"].ToString();
                                Poet.DateOfBirth = sdr["DateOfBirth"].ToString();
                                Poet.UId = sdr["UId"].ToString();
                                Poet.ImgUrl = sdr["UId"].ToString();
                                Poet.Verse1 = sdr["Verse1"].ToString();
                                Poet.Verse2 = sdr["Verse2"].ToString();
                            }
                        }
                    }
                    await conn.CloseAsync();
                }
                return Poet;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error In DALPotry: {ex}");
                return new EntPoetOfDay();
            }
        }
    }
}
