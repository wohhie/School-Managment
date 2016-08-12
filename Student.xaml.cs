using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace CCEmanagement
{
    /// <summary>
    /// Interaction logic for Student.xaml
    /// </summary>
    public partial class Student : Window
    {

        
        public Student(string username)
        {
            
            InitializeComponent();
            PageLoad();
            CourseList(username);
            StudentInformation(username);
        }

        /*public Student(string username)
        {
            SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");

            //InitializeComponent
            InitializeComponent();
            CourseList();


            //show usermail address.

            //collect student information;
            string sql = "Select * from cce_student where username=@username";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@username", username);
            connection.Open();
            using (SqlDataReader oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    stuednt_name.Content = oReader["firstname"] + " " + oReader["lastname"];
                    roll_no.Content = oReader["roll"];
                    email_address.Content = oReader["email"];
                    section.Content = oReader["roll"];
                }

                connection.Close();
            }
        }*/


        /*=========================================================================================
                                      PAGE LOAD ELEMENTS
  ===========================================================================================*/
        public void PageLoad()
        {
            try
            {

                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");


                string sql = "SELECT * FROM cce_curriculum";

                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        //student_name.Content = "welcome";
                        curriculum_system.Content = oReader["program"].ToString();
                        grading_system.Content = oReader["gradingSystem"].ToString();
                        mark_system.Content = oReader["markSystem"].ToString();

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



        /*================================================================
                         COLLECT SELECTED COURSE
        ==================================================================*/
        private void CourseList(string studentID)
        {
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");
                connection.Open();


                string sql = "SELECT ccl.id, ccl.courseName,ccl.sectionName, ccl.cStartTime,ccl.cEndTime FROM cce_student cs, cce_studentCourseList cscl, cce_courselist ccl where cs.id = cscl.studentID AND ccl.id = cscl.courseID AND cs.roll='"+ studentID + "'";

                //MessageBox.Show(sql);
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("COURSELIST");
                sda.Fill(dt);
                courseList.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }

        }




        /*================================================================================
                                    STUDENT INFORMATION
        =================================================================================*/
        public void StudentInformation(string studentID)
        {
            try
            {

                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");
                

                string sql = "Select * from cce_student where roll=@studentID";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@studentID", studentID);
                connection.Open();

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        //student_name.Content = "welcome";
                        student_name.Content = oReader["firstname"].ToString().ToUpper() + " " + oReader["lastname"].ToString().ToUpper();
                        student_ID.Content = studentID;
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





        /*================================================================================
                                    RESULT SHOW
        =================================================================================*/
        private void ResultShow(object sender, RoutedEventArgs e)
        {
            StudentGradingSystem sgs = new StudentGradingSystem();
            sgs.Show();
            this.Close();
        
        }
    }
}
