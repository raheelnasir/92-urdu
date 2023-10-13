using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MyCrud
    {

        public static async Task CRUD(string StoreProcedure, params SqlParameter[] sqlParameters)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(StoreProcedure, con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(sqlParameters);
                        await cmd.ExecuteNonQueryAsync();
                        await con.CloseAsync();
                    };
                };
            }
            catch (Exception ex)
            {

                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }
}
