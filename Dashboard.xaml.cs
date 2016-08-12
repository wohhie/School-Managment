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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        /*=====================================================================
                                        DASHBOARD
        =====================================================================*/
        public Dashboard(string username)
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
                    stuednt_name.Content = oReader["firstname"] + " " + oReader["lastname"] ;
                    roll_no.Content = oReader["roll"];
                    email_address.Content = oReader["email"];
                    section.Content = oReader["roll"];
                }

                
            }

            connection.Close();
        }


        //collect selected courselist.
        private void CourseList()
        {
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");

                string sql = "SELECT cl.id,cl.course_name, ct.starttime, ct.endtime, csec.sec_name FROM cce_courselist cl, cce_student cs, cce_time ct, cce_section csec where cs.username = 'wohhie' AND cl.student_id = cs.id AND cl.course_time_id = ct.id AND  cl.course_section_id = csec.id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("COURSELIST");
                sda.Fill(dt);
                courseList.ItemsSource = dt.DefaultView;

            }catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }
            
        }




        //Show_More
        private void Show_More(object sender, RoutedEventArgs e)
        {
            string courseID = "";
            DataRowView rowview = courseList.SelectedItem as DataRowView;
            courseID = rowview.Row["id"].ToString();
            //MessageBox.Show("ID : " + strid);


            /*========================SHOW THE SUBJECT DETAILS PAGE==============================*/
            subjectDetails subdetails = new subjectDetails(courseID); 
            subdetails.Show();


        }

    }
}
