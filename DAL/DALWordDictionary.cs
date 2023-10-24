using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entities;

namespace DAL
{
    public class DALWordDictionary
    {
        public static async Task<Dictionary<string, EntWordDictionary>> GetWordDictionary()
        {
            Dictionary<string, EntWordDictionary> meanings = new Dictionary<string, EntWordDictionary>();
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                   await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetWordDictionary", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await sdr.ReadAsync())
                            {
                                int DID = (int)sdr["DId"];
                                string? word = sdr["DWord"].ToString();
                                string? meaning = sdr["DMeaning"].ToString();

                                meanings[word] = new EntWordDictionary { DId = DID, DWord = word, DMeaning = meaning };

                            }
                        }

                    }
                    await con.CloseAsync();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error IN DICTIONARY : {ex.ToString()}");

            }
            return meanings;
           
        }

    }
}
