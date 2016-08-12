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
    /// Interaction logic for Admin_CreateAdmin.xaml
    /// </summary>
    public partial class Admin_CreateAdmin : Window
    {
        public Admin_CreateAdmin()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        /*====================================================================================
                                        CREATE A NEW ADMIN
        ====================================================================================*/

        public void create_button_Click(object sender, RoutedEventArgs e)
        {
            string userName = username.Text;
            string passWord = password.Text;
            string confirmPassword = confirm_password.Text;
            string email = email_address.Text;
            bool flag = false;

            //check username is UNIQUE.
            //check password matched.
            if ( checkValidUsername(userName) == true){
                successful_message.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EC1A41"));
                successful_message.Content = userName +" aready exists.";
                flag = true;
            }


            //check password matched.
            if (passWord != confirmPassword)
            {
                successful_message.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EC1A41"));
                successful_message.Content = "Password don't match.";
                flag = true;
            }

            if (flag == false)
            {
                try
                {


                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = @"Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True";

                    string sql = "insert into cce_admin values('" + userName + "', '" + passWord + "','" + email + "')";

                    //MessageBox.Show(sql);

                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    successful_message.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#147E69"));
                    successful_message.Content = "Admin Created Successfully.";
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex);
                }
            }
            else
            {
                successful_message.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EC1A41"));
                successful_message.Content = ("Invalid Username and Password. Try Again.");
            }
       }//end else statement.




        /*====================================================================================
                                        CHECK USERNAME IS VALID OR NOT
        ====================================================================================*/

        public bool checkValidUsername(string username){

            int validation = 0;
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");


                string sql = "SELECT COUNT(*) AS countValue FROM cce_admin WHERE username='" + username  + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        //student_name.Content = "welcome";
                        validation = Int32.Parse(oReader["countValue"].ToString());

                    }


                }

                connection.Close();


            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }

            return (validation == 1) ? true : false; 
        }


    }


}