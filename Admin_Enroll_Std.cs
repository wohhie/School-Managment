using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;

namespace CCEmanagement
{
    class Admin_Enroll_Std
    {
        private myConnection conn;


        public int TotalStudenCapacity(string sectionName)
        {
            

            return 0;
        }


        /*============================
                VALID SECTION
        ==============================*/
        public bool validSection(string sectionName)
        {
            int count = 0;
            bool valid = false;

            /*==============create connection==============*/
            conn = new myConnection();
            string sql = "SELECT count(*) as TotalValue FROM cce_student cs, cce_courselist cc, cce_studentCourseList cscl WHERE cs.id = cscl.studentID AND cc.id = cscl.courseID AND cc.sectionName ='" + sectionName + "'";

            conn.SqlQuery(sql);
            foreach (DataRow dr in conn.exQuery().Rows)
            {
                count = Int32.Parse(dr["TotalValue"].ToString());
            }

            if (count == 0)
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

            /*==============check valid or not==============*/

            /*==============create connection==============*/
        }


    }
}
