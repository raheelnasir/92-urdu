using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBHelper
    {

        public static SqlConnection GetConnection()
        {
            // SqlConnection connection = new SqlConnection("Data Source=REARMS\\SQLEXPRESS;Initial Catalog=Project;Integrated Security=True");

            SqlConnection connection = new SqlConnection("Data Source=66.165.248.146;Initial Catalog=db_urdu;User ID=un_92urdu;Password=Mk0*5yl47"); 
            return connection;
        }

    }
}
