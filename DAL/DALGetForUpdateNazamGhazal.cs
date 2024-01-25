using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DALGetForUpdateNazamGhazal
    {
        public static async Task<List<EntGetContent>> GetContentForpdate(string? UId, string? FuntionType)
        {
            string ProcedureType = "";
            string DBVariable = "";
            try
            {
                if (FuntionType == "Nazam")
                {
                    ProcedureType = "UpdateNazam";
                    DBVariable = "@NazamId";
                }
                else if (FuntionType == "Ghazal")
                {
                    ProcedureType = "UpdateGhazal";
                    DBVariable = "@GhazalId";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new List<EntGetContent>();
            }
            try
            {
                List<EntGetContent> list = new List<EntGetContent>();
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(ProcedureType, con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(DBVariable, UId);
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
