using Entities;
using System.Data.SqlClient;

namespace DAL
{
    public class DALUserPoemByUId
    {
        public static async Task<List<EntUserContent>> GetUserPoemByUId(int UId)
        {
            try
            {

                List<Entities.EntUserContent> PoemDetail = new List<Entities.EntUserContent>();


                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetPoemByUId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(UId));
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (await rdr.ReadAsync())
                            {
                                EntUserContent EUC = new EntUserContent();

                                EUC.CId = Convert.ToInt32(rdr["NazamId"]);
                                EUC.ContentName = rdr["NazamName"].ToString();
                                PoemDetail.Add(EUC);
                            }
                        }

                    }
                    await con.CloseAsync();
                }
                return PoemDetail;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new List<EntUserContent>();
            }
        }
    }
}
