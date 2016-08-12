using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace CCEmanagement
{
    class Teacher_assignGrade
    {
        //create connection
        private myConnection conn;

        /*
                HANDE MIDTERM GRADE
            */

        public string std;

        /*============FIND STUDENT==================*/
        public string findStudentID(string studentRoll)
        {
            try
            {
                conn = new myConnection();
                string sql = "SELECT id FROM cce_student WHERE roll='" + studentRoll + "'";
                conn.SqlQuery(sql);
                foreach (DataRow dr in conn.exQuery().Rows)
                {
                    std = dr["id"].ToString();

                }

                return std;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        

        /*============RETRIVE ATTENDACE MARK==================*/
        public string RetriveAttendanceMark(string stdID)
        {
            double stdid = double.Parse(stdID);
            try
            {
                conn = new myConnection();
                string sql = "select sum(attendance) as totalAtdMark from cce_marks where studentID=" + stdid;
                conn.SqlQuery(sql);
                foreach (DataRow dr in conn.exQuery().Rows)
                {
                    std = dr["totalAtdMark"].ToString();

                }


                return std;


            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        

        /*============HANDGLE GRADE==================*/
        public string handleMidGrade(double totalMarks)
        {

            double marks = totalMarks;
            string Grade = "";

            if (marks > 89 && marks < 101)
            {
                Grade = "A";
            }

            else if (marks > 79 && marks < 91)
            {
                Grade = "B";
            }

            else if (marks > 69 && marks < 81)
            {
                Grade = "C";
            }

            else if (marks > 59 && marks < 71)
            {
                Grade = "D";
            }

            else if (marks > -1 && marks < 61)
            {
                Grade = "F";
            }
            else
            {
                Grade = "Z";
            }


            return Grade;

        }



        /*============INSERT ALL VALUES==================*/
        public bool insertMarkDetails(int currStdID, int course_ID,double fQuiz, double assign, double rprt, double attendance, double term,double total, string grade)
            
        {
            try
            {
                conn = new myConnection();
                string sql = "INSERT INTO cce_markDetails values(" + currStdID + "," + course_ID + "," + fQuiz + "," + assign + "," + rprt + "," + term + "," + total + ",'" + grade + "')";
                conn.SqlQuery(sql);
                conn.NonQuery();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return true;
        }



    }
}
