using Entities;
using System.Data.SqlClient;


namespace DAL
{
    public static class DALGetNazamByNazamId
    {
        public static async Task<List<EntGetContent>> GetNazamByNazamId(string poemid)
        {
            try
            {
                List<EntGetContent> list = new List<EntGetContent>();
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetNazamByNazamId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@poemid", poemid);
                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (sdr.Read())
                            {
                                EntGetContent phrase = new EntGetContent();
                                phrase.Verse1 = sdr["Verse1"].ToString();
                                phrase.Verse2 = sdr["Verse2"].ToString();
                                list.Add(phrase);
                            }
                        }
                    }
                    await con.CloseAsync();
                }
                Console.WriteLine("POEM");
                return list;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return new List<EntGetContent>();
            }
        }
    }
}
