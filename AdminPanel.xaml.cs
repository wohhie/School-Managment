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
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel(string adminID)
        {
            InitializeComponent();
            PageLoad();
            totalStudent();
            totalTeacher();
            //TodayDate();
            totalAdmin();
            AdminInformation(adminID);
        }

        private void create_section_Click(object sender, RoutedEventArgs e)
        {
            CreateSection csec = new CreateSection();
            csec.Owner = this;
            ApplyEffect(this);
            csec.ShowDialog();
            ClearEffect(this);

        }



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

        private void Assign_Course_To_Teacher(object sender, RoutedEventArgs e)
        {
            Admin_Enroll_Teacher csec = new Admin_Enroll_Teacher();
            csec.Show();
            this.Close();

        }

        private void create_course_Click(object sender, RoutedEventArgs e)
        {
            Admin_CreateCourse csec = new Admin_CreateCourse();
            csec.Owner = this;
            ApplyEffect(this);
            csec.ShowDialog();
            ClearEffect(this);
        }

        private void Assign_Course_To_Student(object sender, RoutedEventArgs e)
        {
            Admin_Enroll_Student csec = new Admin_Enroll_Student();
            csec.Show();
            this.Close();
        }




        /*===================================================================================
                                            TOTAL ADMIN
        ===================================================================================*/
        public void totalAdmin()
        {
            try
            {

                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");


                string sql = "SELECT COUNT(*) AS countValue FROM cce_admin";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        //student_name.Content = "welcome";
                        total_admin.Content = oReader["countValue"].ToString();

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

        /*===================================================================================
                                            TOTAL STUDENT
        ===================================================================================*/
        public void totalStudent()
        {
            try
            {

                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");


                string sql = "SELECT COUNT(*) AS countValue FROM cce_student";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        //student_name.Content = "welcome";
                        student_value.Content = oReader["countValue"].ToString();
                        
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


        /*===================================================================================
                                            TOTAL TEACHER
        ===================================================================================*/
        public void totalTeacher()
        {
            try
            {

                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");


                string sql = "SELECT COUNT(*) AS countValue FROM cce_teacher";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        //student_name.Content = "welcome";
                        teacher_value.Content = oReader["countValue"].ToString();

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


        /*===================================================================================
                                            TODAY DATE
        ===================================================================================*/

        public void TodayDate()
        {
            string shohan = DateTime.Now.Month.ToString("mm");
            MessageBox.Show(shohan);
        }





        /*===================================================================================
                                            CHANGE CURRICULUM
        ===================================================================================*/

        private void change_curriculum_Click(object sender, RoutedEventArgs e)
        {
            Admin_Curriculum changeCurriculum = new Admin_Curriculum();
            changeCurriculum.Owner = this;
            ApplyEffect(this);
            changeCurriculum.ShowDialog();
            ClearEffect(this);
            base.OnInitialized(e);
        }



        /*===================================================================================
                                            CREATE NEW ADMIN
        ===================================================================================*/
        private void create_new_admin_Click(object sender, RoutedEventArgs e)
        {
            Admin_CreateAdmin createAdmin = new Admin_CreateAdmin();
            createAdmin.Owner = this;
            ApplyEffect(this);
            createAdmin.ShowDialog();
            ClearEffect(this);
            base.OnInitialized(e);

        }





        /*===================================================================================
                                            STUDENT LIST
        ===================================================================================*/
        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Admin_StudentList studentList = new Admin_StudentList();
            studentList.Owner = this;
            ApplyEffect(this);
            studentList.ShowDialog();
            ClearEffect(this);
            base.OnInitialized(e);

        }



        public void AdminInformation(string adminID)
        {

            try
            {

                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");


                string sql = "Select * from cce_admin where username=@adminID";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@adminID", adminID);
                connection.Open();

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        //student_name.Content = "welcome";
                        admin_name.Content = oReader["username"].ToString().ToUpper();
                        
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

    }
}
