using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInterface3.ViewModel
{
    class BaseViewModel
    {
        static string connectionString = @"Data Source=deptinfo420;Initial Catalog=BDLab5_Nico;Integrated Security=True";
        protected static SqlConnection con;

        public BaseViewModel()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }
    }
}
