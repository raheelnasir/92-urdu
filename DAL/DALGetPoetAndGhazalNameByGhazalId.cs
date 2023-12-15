using Entities;
using System.Data.SqlClient;


namespace DAL
{
    public static class DALGetPoetAndGhazalNameByGhazalId
    {
        public static async Task<EntContentDetails> GetGhazalAndPoetNameByGhazalId(string Ghazalid)
        {
            try
            {
                EntContentDetails ContentHolder = new EntContentDetails();
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetPoetAndNazamNameByGhazalId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Ghazalid", Ghazalid);
                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (sdr.Read())
                            {
                                ContentHolder.UId = Convert.ToInt32(sdr["UId"]);
                                ContentHolder.PoetName= sdr["PoetName"].ToString();
                                ContentHolder.ContentName = sdr["GhazalName"].ToString();
                            }
                        }
                    }
                    await con.CloseAsync();
                }
                Console.WriteLine("Ghazal");
                return ContentHolder;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return new EntContentDetails();
            }
        }
    }
}
