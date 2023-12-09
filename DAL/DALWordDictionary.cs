using System;
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
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Word", word);

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (await rdr.ReadAsync())
                            {

                                meaning = rdr["FullName"].ToString();

                            }
                        }
                        await con.CloseAsync();
                    }
                    if (meaning != "")
                    {
                        return meaning;


                    }
                    else
                    {
                        return "Meaning Not FOUND";

                    }
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
