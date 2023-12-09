using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUserByID
    {
        public static async Task<EntUserAbout> GetUserById(int UId)
        {
            try
            {
                Console.WriteLine("2");

                Entities.EntUserAbout UserAbout = new Entities.EntUserAbout();


                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetUserByUId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(UId));
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (await rdr.ReadAsync())
                            {

                                UserAbout.FullName = rdr["FullName"].ToString();
                                UserAbout.City = rdr["City"].ToString();
                                UserAbout.Biography = rdr["UserBio"].ToString();
                                if(rdr["DateOfBirth"].ToString()=="")
                                {
                                    UserAbout.DateOfBirth = "NIL";

                                }
                                else
                                {
                                    UserAbout.DateOfBirth = rdr["DateOfBirth"].ToString();

                                }
                            }
                        }

                    }
                    await con.CloseAsync();
                }
                Console.WriteLine("2");

                return UserAbout;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new EntUserAbout();
            }
        }
    }
}
