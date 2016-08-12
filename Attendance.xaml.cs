using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace CCEmanagement
{
    /// <summary>
    /// Interaction logic for Attendance.xaml
    /// </summary>
    public partial class Attendance : Window
    {
        public Attendance(string studentName, string studentRoll, string studentID, string courseID)
        {
            InitializeComponent();
            studentInformation(studentName, studentRoll, studentID, courseID);
            totalAttendanceMark(studentID, courseID);
        }


        /*===============================================================
                            SHOW ATTENDANCE INFORMATION
        =================================================================*/

        public void totalAttendanceMark(string studentID, string courseID)
        {
            try
            {

                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");


                string sql = "select SUM(attendance) as AttendanceMark, count(*) as TotalAttendance from cce_marks where studentID =" + studentID + "  and courseID = " + courseID  + "";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        //student_name.Content = "welcome";
                        total_class.Content = oReader["AttendanceMark"].ToString();
                        total_present.Content = oReader["TotalAttendance"].ToString();
                    }


                }

                connection.Close();


                //close
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }


        /*===============================================================
                            SHOW STUDENT INFORMATION
        =================================================================*/
        public void studentInformation(string name, string roll, string studentID,string courseID)
        {
            studentid.Content = roll;
            studentname.Content = name;
            invisibleID.Content =studentID;
            invisiblecourseID.Content = courseID;

        }



        /*=============================================================
                                    TAKE PRESENT
        ===============================================================*/
        private void takePresent(object sender, RoutedEventArgs e)
        {

            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");

                //Student ID : 01-285-A
                //Teacher ID : 077-235-H
                //connection open
                connection.Open();

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True";

                string sql = "insert into cce_marks (attendance,studentID,courseID) values(1, " + invisibleID.Content + "," + invisiblecourseID.Content + ")";

                //MessageBox.Show(sql);
                


                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("suceed");



                //MessageBox.Show("student Name : " + studentname.Content  + " value : " + " 1");
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }
        }



        /*==============================================================
                                    TAKE ABSENT
        ================================================================*/
        private void takeAbsent(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");

                //Student ID : 01-285-A
                //Teacher ID : 077-235-H
                //connection open
                connection.Open();

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True";

                string sql = "insert into cce_marks (attendance,studentID,courseID) values(0, " + invisibleID.Content + "," + invisiblecourseID.Content + ")";
                
                
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
               
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }
        }

        
    }
}
