using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DALSearchGhazalByPoetAndGhazalName
    {
        public static async Task<List<EntGhazalSearch>> GetGhazalByPoetAndGhazalName(string poetname, string ghazalname)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    List<EntGhazalSearch> GhazalList = new List<EntGhazalSearch>();

                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetGhazalByPoetAndGhazalName", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ghazalname", ghazalname);
                        cmd.Parameters.AddWithValue("@poetname", poetname);
                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())

                        {

                            while (await sdr.ReadAsync())
                            {
                                EntGhazalSearch Ghazal = new EntGhazalSearch();

                                Ghazal.PoetName = sdr["FullName"].ToString();
                                Ghazal.GhazalName = sdr["GhazalName"].ToString();
                                Ghazal.UId = sdr["UId"].ToString();
                                Ghazal.GhazalId = sdr["GhazalId"].ToString();
                                Ghazal.Misra1 = sdr["Verse1"].ToString();
                                Ghazal.Misra2 = sdr["Verse2"].ToString();
                                Ghazal.PoetImage = sdr["ProfileImg"].ToString();
                                Console.WriteLine(Ghazal.UId);

                                GhazalList.Add(Ghazal);
                            }

                        }




                    }
                    await con.CloseAsync();
                    Console.WriteLine(GhazalList[0].UId);

                    return GhazalList;

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<EntGhazalSearch>();
            }

        }
    }
}
