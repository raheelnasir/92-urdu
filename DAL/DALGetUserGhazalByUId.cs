using Entities;
using System.Data.SqlClient;

namespace DAL
{
    public class DALUserGhazalByUId
    {
        public static async Task<List<EntUserContent>> GetUserGhazalByUId(int UId)
        {
            try
            {
                List<Entities.EntUserContent> GhazalDetail = new List<Entities.EntUserContent>();


                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetGhazalByUId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(UId));
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (await rdr.ReadAsync())
                            {
                                EntUserContent EUC = new EntUserContent();

                                EUC.CId = Convert.ToInt32(rdr["GhazalId"]);
                                EUC.ContentName = rdr["GhazalName"].ToString();
                                GhazalDetail.Add(EUC);
                            }
                        }

                    }
                    await con.CloseAsync();
                }
                
                return GhazalDetail;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new List<EntUserContent>();
            }
        }
    }
}
