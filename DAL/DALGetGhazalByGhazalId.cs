using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DALGetGhazalByGhazalId
    {
        public static async Task<List<EntGetContent>> GetGhazalByGhazalId(string ghazalId)
        {
            try
            {
                List<EntGetContent> list = new List<EntGetContent>();
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetGhazalByGhazalId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ghazalid", ghazalId);
                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {

                            while (await sdr.ReadAsync())
                            {
                                EntGetContent content = new EntGetContent();
                                content.Verse1 = sdr["Verse1"].ToString();
                                Console.WriteLine(content.Verse1);

                                content.Verse2 = sdr["Verse2"].ToString();
                                list.Add(content);
                            }
                        }
                    }
                    await con.CloseAsync();

                }
                Console.WriteLine("Ghazal");

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new List<EntGetContent>();
            }


        }

    }
}
