using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUserGhazalByUId
    {
        public static async Task<List<EntUserContent>> GetUserGhazalByUId(int UId)
        {
            try
            {
                Console.WriteLine("2");

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

                                EUC.CId = Convert.ToInt32(rdr["ContentId"]);
                                EUC.ContentName = rdr["ContentName"].ToString();
                                GhazalDetail.Add(EUC);
                            }
                        }

                    }
                    await con.CloseAsync();
                }
                Console.WriteLine("2");

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
