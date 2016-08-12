using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CCEmanagement
{
    class myConnection
    {

        public SqlConnection con;
        public SqlCommand cmd;
        private SqlDataAdapter da;
        private DataTable dt;

        public  myConnection()
        {
            con = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");
            con.Open();
        }


        public void SqlQuery(string query)
        {
            cmd = new SqlCommand(query, con);
        }


        public DataTable exQuery()
        {
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void NonQuery()
        {
            cmd.ExecuteNonQuery();
        }

    }
}
