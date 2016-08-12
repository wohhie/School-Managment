using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        //create Connection
        private myConnection conn;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");

                //Student ID : 01-285-A
                //Teacher ID : 077-235-H
                //connection open
                connection.Open();


                string user_id = roll_id.Text;


                //========================FOR STUDENT=====================================
                if (user_id.Length == 8)
                {
                    //send the user to the student Page;
                    SqlCommand cmd = new SqlCommand("SELECT * FROM cce_student WHERE roll='" + roll_id.Text + "' and password='" + password.Text + "'", connection);

                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    int count = 0;
                    while (dr.Read())
                    {
                        count += 1;
                    }//end while

                    if (count == 1)
                    {

                        //MessageBox.Show(roll_id.Text);
                        Student dashboard = new Student(roll_id.Text);
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username and Password is invalid.");
                    }

                }

                //=====================================FOR TEACHER=====================================
                else if (user_id.Length == 9)
                {
                    //send the user to teacher page;
                    SqlCommand cmd = new SqlCommand("SELECT * FROM cce_teacher WHERE roll='" + roll_id.Text + "' and password='" + password.Text + "'", connection);


                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    int count = 0;
                    while (dr.Read())
                    {
                        count += 1;
                    }//end while

                    if (count == 1)
                    {

                        //MessageBox.Show(roll_id.Text);
                        TeacherDasboard dashboard = new TeacherDasboard(roll_id.Text);
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username and Password is invalid.");
                    }
                }

                //else for the student
                else if(user_id.Contains("-") == false)
                {
                    //send the user to teacher page;
                    SqlCommand cmd = new SqlCommand("SELECT * FROM cce_admin WHERE username='" + roll_id.Text + "' and password='" + password.Text + "'", connection);


                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    int count = 0;
                    while (dr.Read())
                    {
                        count += 1;
                    }//end while

                    if (count == 1)
                    {

                        //MessageBox.Show(roll_id.Text);
                        AdminPanel dashboard = new AdminPanel(roll_id.Text);
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username and Password is invalid.");
                    }
                }
                
                

            }
            catch(Exception ex)
            {
                MessageBox.Show("Invalid." + ex);
            }            

        }



        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            uploadStudentInformation ite = new uploadStudentInformation();
            ite.Show();
            this.Close();
        }





        /*==============CONNNECT CHECK=================*/
        private void establish_connection_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                conn = new myConnection();
                string sql = "select lastname FROM cce_student where firstname='wohhie'";
                conn.SqlQuery(sql);
                

                foreach (DataRow dr in conn.exQuery().Rows)
                {
                    MessageBox.Show(dr["lastname"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }

            
        }

    }
}
