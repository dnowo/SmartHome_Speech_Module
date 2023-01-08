using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHSM
{
    public partial class DatabaseDriver
    {
        public DatabaseDriver()
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;database=smarthome;uid=root;pwd=\"\";");
            try {
                connection.Open();
                System.Diagnostics.Trace.WriteLine("Connected to database: smarthome");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }

    }

}
