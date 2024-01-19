using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
    public class DALWordDictionary
    {
        public static async Task<string> GetWordMeaning(string word)
        {
            string meaning = string.Empty;

            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("sp_GetWordDictionary", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@word", word.ToString());

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                meaning = rdr["DMeaning"].ToString();
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(meaning))
                {
                    return meaning;
                }
                else
                {
                    return "Meaning Not Found";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error IN DICTIONARY : {ex.ToString()}");
                return "Meaning Not Found";
            }
        }
    }

}
