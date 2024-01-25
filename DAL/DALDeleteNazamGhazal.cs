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
    public static class DALDeleteNazamGhazal
    {
        public static async void Delete(string? CId, string? FuntionType)
        {
            string ProcedureType = "";
            string DBVariable = "";
            try
            {
                if (FuntionType == "Nazam")
                {
                    ProcedureType = "DeleteNazam";
                    DBVariable = "@NazamId";
                }
                else if (FuntionType == "Ghazal")
                {
                    ProcedureType = "DeleteGhazal";
                    DBVariable = "@GhazalId";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(ProcedureType, con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(DBVariable, CId);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    await con.CloseAsync();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }


        }

    }
}
