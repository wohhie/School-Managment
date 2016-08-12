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
    /// Interaction logic for Admin_Enroll_Teacher.xaml
    /// </summary>
    public partial class Admin_Enroll_Teacher : Window
    {
        public Admin_Enroll_Teacher()
        {
            InitializeComponent();
            DetailsAboutCourse();
            SelectTeacher();
        }

        /*====================================================================
                                    APPLY EFFECT
        ======================================================================*/


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




        /*=====================================================================
                                ASSIGN COURSE TO TEACHER
        ======================================================================*/
        private void Assign_Course_To_Teacher(object sender, RoutedEventArgs e)
        {
            Admin_Enroll_Teacher csec = new Admin_Enroll_Teacher();
            csec.Show();
            this.Close();

        }


        /*=====================================================================
                                CREATE COURSE CLICK
        ======================================================================*/
        private void create_course_Click(object sender, RoutedEventArgs e)
        {
            Admin_CreateCourse csec = new Admin_CreateCourse();
            csec.Owner = this;
            ApplyEffect(this);
            csec.ShowDialog();
            ClearEffect(this);
        }


        /*=====================================================================
                                CREATE SECTION CLICK
        ======================================================================*/
        private void create_section_Click(object sender, RoutedEventArgs e)
        {
            CreateSection csec = new CreateSection();
            csec.Owner = this;
            ApplyEffect(this);
            csec.ShowDialog();
            ClearEffect(this);
        }





        /*=====================================================================
                                SELECT TEACHER 'SHOW TEACHER'
        ======================================================================*/


        private void SelectTeacher()
        {


            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");

                //open
                connection.Open();

                string sql = "select * from cce_teacher";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    //add values
                    string teacherFullName = dr.GetString(2).ToString() + " " + dr.GetString(3).ToString();


                    //values
                    select_teacher.Items.Add(teacherFullName);
                }


                //close
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }

        }



        /*=====================================================================
                                SHOW DATA SECTION
        ======================================================================*/

        private void DetailsAboutCourse()
        {
            
            
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");

                //open
                connection.Open();

                string sql = "select * from cce_courselist";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    //add values
                    string courseName = dr.GetString(1).ToString();
                    string sectionName = dr.GetString(2).ToString();
                    string courseStartTime = dr.GetTimeSpan(3).ToString(@"hh\:mm");
                    string courseEndTime = dr.GetTimeSpan(4).ToString(@"hh\:mm");


                    //values
                    subject_name.Items.Add(courseName);
                    select_section.Items.Add(sectionName);
                    starttime.Items.Add(courseStartTime);
                    endtime.Items.Add(courseEndTime);
                    
                }


                //close
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
            
        }





        /*=====================================================================
                                DEPENDING ON THE SELECT SUBJECT
        ======================================================================*/


        private void subject_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string courseName = subject_name.SelectionBoxItem.ToString();
            string courseName = subject_name.SelectedValue.ToString();
            string courseIndex = subject_name.SelectedIndex.ToString();
            //MessageBox.Show(courseIndex);
            //MessageBox.Show(courseName);    //English




            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");

                //open
                connection.Open();

                string sql = "select * from cce_courselist where courseName='" + courseName  + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    //add values
                    string sectionName = dr.GetString(2).ToString();
                    string courseStartTime = dr.GetTimeSpan(3).ToString(@"hh\:mm");
                    string courseEndTime = dr.GetTimeSpan(4).ToString(@"hh\:mm");

                    starttime.SelectedIndex = Int32.Parse(courseIndex);
                    endtime.SelectedIndex = Int32.Parse(courseIndex);
                    select_section.SelectedIndex = Int32.Parse(courseIndex);
                    //MessageBox.Show(courseStartTime);

                }

                

                //close
                connection.Close();
                //MessageBox.Show("Successfully Inserted.");

            }


            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }
        }



        /*=====================================================================
                                ASSIGN COURSE TO TEACHER
        ======================================================================*/


        private void save_change_Click(object sender, RoutedEventArgs e)
        {
            //selected values
            string courseName = subject_name.SelectedValue.ToString();
            int courseID = 0;
            //MessageBox.Show(courseName);
            //=======================================retrieve the courseID;

            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");

                //open
                connection.Open();

                string sql = "select id from cce_courselist where courseName='" + courseName + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //save to courseID
                    courseID = Int32.Parse(dr.GetValue(0).ToString());
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }





            string teacherName = select_teacher.SelectedValue.ToString();
            //MessageBox.Show(teacherName).ToString();

            int teacherID = 0;
            var names = teacherName.Split(' ');
            string firstname = names[0];
            string lastname = names[1];
            //MessageBox.Show(username);
            //=======================================retrieve the TeacherID;



            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");

                //open
                connection.Open();

                string sql = "select id from cce_teacher where firstname='" + firstname + "' and lastname='" + lastname + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //save to teacherID
                    teacherID = Int32.Parse(dr.GetValue(0).ToString());
                    //MessageBox.Show(" " + teacherID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }



            //=======================================insert to teacherCourseList account.
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True";

                string sql = "insert into cce_teacherCourseList values(" +  teacherID  +  ","  + courseID  + ")";

                //MessageBox.Show(sql);

                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("suceed");

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }
        }



        private void Assign_Course_To_Student(object sender, RoutedEventArgs e)
        {
            Admin_Enroll_Student admin_enroll = new Admin_Enroll_Student();
            admin_enroll.Show();
            this.Close();
        }
    }
}
