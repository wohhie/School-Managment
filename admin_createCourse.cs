using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace CCEmanagement
{
    class admin_createCourse
    {
        private myConnection conn;

        /*==================================
                SECTION IS AVAILABLE
        ====================================*/

        public bool validSection(string sectionName)
        {
            bool valid = false;
            int count = 0;

            conn = new myConnection();
            string sql = "SELECT count(*) as count FROM cce_courselist WHERE sectionName='" + sectionName + "'";
            conn.SqlQuery(sql);
            
            foreach(DataRow dr in conn.exQuery().Rows)
            {
                count = Int32.Parse(dr["count"].ToString());
            }

            if(count == 0)
            {
                //return false
                valid = true;
            }
            else
            {
                //return false
                valid = false;
            }

            return valid;
        }

    }
}
