using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
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
using System.IO;

namespace CCEmanagement
{
    /// <summary>
    /// Interaction logic for TeacherDasboard.xaml
    /// </summary>
    /// 

    
    public partial class TeacherDasboard : Window
    {



        private myConnection conn;

        /*=======================================================================================
                                                            CONSTRUCTOR
        =====================================================================================*/
        public TeacherDasboard(string roll_id)
        {
            InitializeComponent();
            ShowTakenSection(roll_id);
            TeacherInformation(roll_id);
        }

        /*==========================================================================================
                               TAKEN SECTION - STUDENT
        ===================================================================================*/
        private void ShowTakenSection(string teacherID)
        {
            

            try{

                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");
                //open
                connection.Open();

                string sql = "SELECT ccl.id as courseID, ccl.courseName, ccl.sectionName, ccl.cStartTime, ccl.cEndTime FROM cce_teacher ct,    cce_teacherCourseList cscl, cce_courselist ccl where ct.id = cscl.teacherID AND ccl.id = cscl.courseID AND ct.roll ='" +   teacherID  +"'";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string section = dr.GetString(2);
                    sections_list.Items.Add(section);
                }

                //close
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }
        

        /*===================================================
                    TAKE STUDENT ATTENDANCE
        =====================================================*/
        private void attendance_track_Click(object sender, RoutedEventArgs e)
        {
            string sections = sections_list.SelectedItem.ToString();
            //MessageBox.Show(sections);

            try
            {

                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");
                //open
                connection.Open();
                string sql = "select cs.id, cs.firstname, cs.lastname, cs.roll, cc.id as courseID,cc.courseName, cc.sectionName FROM cce_student cs, cce_studentCourseList cscl, cce_courselist cc WHERE cs.id = cscl.studentID AND cc.id = cscl.courseID AND cc.sectionName = '" + sections + "'";

                
                SqlCommand cmd = new SqlCommand(sql, connection);
                
                SqlDataReader queryCommandReader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();

                // Use the DataTable.Load(SqlDataReader) function to put the results of the query into a DataTable.
                dataTable.Load(queryCommandReader);

                foreach (DataRow row in dataTable.Rows)
                {

                    string student_name = row["firstname"].ToString() + " " + row["lastname"].ToString();
                    string studentRoll = row["roll"].ToString();
                    string studentID = row["id"].ToString();
                    string courseID = row["courseID"].ToString();

                    /*MessageBox.Show("Sutdent Name " + row["firstname"].ToString()  + " " + row["lastname"].ToString() + "\n" + "Student ID : " + row["roll"].ToString());*/

                    Attendance objModal = new Attendance(student_name, studentRoll, studentID, courseID);
                    objModal.Owner = this;
                    ApplyEffect(this);
                    objModal.ShowDialog();
                    ClearEffect(this);
                    /*Attendance atd = new Attendance();
                    atd.ShowDialog();*/
                    base.OnInitialized(e);

                }


                connection.Close();


                //close
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }


        /*=========================================================================================
                                             TEACHER INFORMATION
        ===========================================================================================*/
        public void TeacherInformation(string teacherID)
        {
            try
            {
                
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");
                //open
                //collect student information;
                string sql = "Select * from cce_teacher where roll=@teacherID";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@teacherID", teacherID);
                connection.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        teacher_name.Content = oReader["firstname"].ToString().ToUpper() + " " + oReader["lastname"].ToString().ToUpper();
                        teacher_ID.Content = teacherID;
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


        /*=========================================================================================
                                            PREVIOUS WINDOW EFFECT
        ===========================================================================================*/

        /* APPLY EFFECT */
        private void ApplyEffect(Window win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 6;
            win.Effect = objBlur;
        }

        /* CLEAR EFFECT */
        private void ClearEffect(Window win)
        {
            win.Effect = null;
        }

        
        private void sections_list_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string selectedSection = sections_list.SelectedValue.ToString();
            //MessageBox.Show(selectedSection);

            try
            {

                //==============a=====  
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");
                //open

                connection.Open();
                string sql = "SELECT ccl.id as courseID,ccl.courseName, cs.firstname, cs.lastname, cs.roll,ccl.id, ccl.courseName, ccl.sectionName, ccl.cStartTime, ccl.cEndTime FROM cce_student cs, cce_studentCourseList cscl,cce_courselist ccl where cs.id = cscl.studentID AND ccl.id = cscl.courseID AND ccl.sectionName = '" + selectedSection + "'";


                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("STUDENTLIST");
                sda.Fill(dt);
                studentList.ItemsSource = dt.DefaultView;


                //close
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }





        /*=========================================================================================
                                            VIEW STUDENT DETAILS
        ===========================================================================================*/
        private void view_Student_details(object sender, EventArgs e)
        {
            try
            {
                string studentRoll = "";                                            //studentID '19'
                string courseid = "";
                string sect = sections_list.SelectedValue.ToString();               //section 'A1'

                int index = Int32.Parse(studentList.SelectedIndex.ToString());      //0 index
                DataRowView roll = (DataRowView)studentList.Items[index];           // find 0 index roll
                studentRoll = roll["roll"].ToString();
                courseid = roll["courseID"].ToString();



                Teacher_StudentDetails tstd = new Teacher_StudentDetails(studentRoll, sect, courseid);  //pass student roll
                tstd.Owner = this;
                ApplyEffect(this);
                tstd.ShowDialog();
                ClearEffect(this);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error : " + ex);
            }
            
        }
        
    }
}
