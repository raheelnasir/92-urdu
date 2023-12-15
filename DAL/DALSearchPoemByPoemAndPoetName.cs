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
    public static class DALSearchPoemByPoemAndPoetName
    {
        public static async Task<List<EntPoemSearch>> GetPoemByPoetAndPoemName(string poetname, string poemname)
        {
            try
            {
                List<EntPoemSearch> PoemList = new List<EntPoemSearch>();
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetPoemByPoetAndPoemName", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@poemname", poemname);
                        cmd.Parameters.AddWithValue("@poetname", poetname);
                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await sdr.ReadAsync())
                            {
                                EntPoemSearch Poem = new EntPoemSearch();
                                Poem.PoetName = sdr["FullName"].ToString();
                                Poem.PoemName = sdr["NazamName"].ToString();
                                Poem.UId = sdr["UId"].ToString();
                                Poem.PoemId = sdr["NazamId"].ToString();
                                Poem.Misra1 = sdr["Verse1"].ToString();
                                Poem.Misra2 = sdr["Verse2"].ToString();
                                Poem.PoetImage = sdr["ProfileImg"].ToString();
                                PoemList.Add(Poem);

                            }

                        }
                        await con.CloseAsync();
                        return PoemList;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<EntPoemSearch>();
            }

        }
    }
}
