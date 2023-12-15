using Entities;
using System.Data.SqlClient;


namespace DAL
{
    public static class DALGetPoetAndPoemNameByNazamId
    {
        public static async Task<EntContentDetails>GetNazamAndPoetNameByNazamId(string poemid)
        {
            try
            {
                EntContentDetails ContentHolder = new EntContentDetails();
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetPoetAndNazamNameByPoemId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@poemid", poemid);
                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (sdr.Read())
                            {
                                ContentHolder.UId= Convert.ToInt32( sdr["UId"]);
                                ContentHolder.PoetName = sdr["PoetName"].ToString();
                                ContentHolder.ContentName = sdr["NazamName"].ToString();
                            }
                        }
                    }
                    await con.CloseAsync();
                }
                Console.WriteLine("POEM");
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
