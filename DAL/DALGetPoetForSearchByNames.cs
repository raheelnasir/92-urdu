using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
    public class DALPoetsSearch
    {
        public static async Task<List<EntPoetsInSearchCategory>> GetPoetsBySearch(string name, string gender)
        {
            List<EntPoetsInSearchCategory> poetsList = new List<EntPoetsInSearchCategory>();
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllPoets", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@gender", gender);

                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await sdr.ReadAsync())
                            {
                                EntPoetsInSearchCategory poet = new EntPoetsInSearchCategory();
                                poet.FullName = sdr["FullName"].ToString();
                                poet.City = sdr["City"].ToString();
                                poet.UId = sdr["UId"].ToString();
                                poet.ProfileImage = sdr["ProfileImg"].ToString();


                                poetsList.Add(poet);
                            }
                        }
                    }
                    await conn.CloseAsync();
                }
                return poetsList;
            }
            catch (Exception ex)
            {
                return new List<EntPoetsInSearchCategory>();
            }
        }
    }
}
