using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DALPoetry
    {
        public static async Task<EntVerse> GetPoetryByPoetID()
        {
            EntVerse verse = new EntVerse();
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetPoemByPoetId", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {

                            while (sdr.Read())
                            {
                                verse.ContentId = Convert.ToInt32(sdr["GhazalId"].ToString());
                                verse.Verse1 = sdr["Verse1"].ToString();
                                verse.Verse2 = sdr["Verse2"].ToString();
                                verse.Fullname = sdr["FullName"].ToString();
                            }


                        }
                    }
                }
                return verse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error In DALPotry: {ex}");
                return verse;
            }

        }
    }
}
