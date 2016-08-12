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
using System.Data;

namespace CCEmanagement
{
    /// <summary>
    /// Interaction logic for Admin_CreateCourse.xaml
    /// </summary>
    public partial class Admin_CreateCourse : Window
    {
        public string sectionName;

        public Admin_CreateCourse()
        {
            InitializeComponent();
            validSection();
        }

        /*==================================================================
                                SHOW AVAILABLE SECTION
        ====================================================================*/
        public void validSection()
        {
            try
            {

                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");
                //open
                connection.Open();

                string sql = "select * from cce_sectionlist";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    /*string section = dr.GetString(1);
                    string startTime = dr.GetString(2);
                    string endTime = dr.GetString(3);*/

                    //add values
                    string sectionName = dr.GetString(1).ToString();
                    string section_starttime = dr.GetTimeSpan(2).ToString(@"hh\:mm");
                    string section_endtime = dr.GetTimeSpan(3).ToString(@"hh\:mm");


                    //values
                    select_section.Items.Add(sectionName);
                    starttime.Items.Add(section_starttime);
                    endtime.Items.Add(section_endtime);


                }

                //close
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }





        //close window
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        //create sbuject
        private void create_button_Click(object sender, RoutedEventArgs e)
        {
            
            

            /*if(selectedSection_valid(select_section.Text))
            {

            }*/
            
            try
            {


                string courseName = course_name.Text;

                string courseStartTime = " ",
                        courseEndTime = " ";

                string avlSection = " ";
                int valid = 1;


                //select starttime
                if (starttime.SelectedIndex >= 0)
                    courseStartTime = starttime.SelectionBoxItem.ToString();


                //select endtime
                if (endtime.SelectedIndex >= 0)
                    courseEndTime = endtime.SelectionBoxItem.ToString();


                //select capacity
                if (select_section.SelectedIndex >= 0)
                    avlSection = select_section.SelectionBoxItem.ToString();



                /*===================STORE SECTION NAME================================*/

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True";

                string sql = "insert into cce_courselist values('" + courseName + "', '" + avlSection + "','" + courseStartTime + "','" + courseEndTime + "','" +  valid + "')";


                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();


                MessageBox.Show("Successfully Inserted.");

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }

        }

        /*private void starttime_Copy1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }*/

        private void select_section_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
            string selectSection = select_section.SelectedValue.ToString();
            string selectSectionID = select_section.SelectedIndex.ToString();
            //MessageBox.Show(selectSection);

            admin_createCourse createCourse = new admin_createCourse();
            bool valid = createCourse.validSection(selectSection);
            
            if( valid == true)
            {
                //if valid ( section is clear 
                message.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#147E69"));
                message.Content = "'" + selectSection + "' is not assigned now.";
                create_button.IsEnabled = true;

            }
            else
            {
                message.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EC1A41"));
                message.Content ="Error : '" + selectSection + "' already assigned.";
                create_button.IsEnabled = false;

            }


            starttime.SelectedIndex = Int32.Parse(selectSectionID);
            endtime.SelectedIndex = Int32.Parse(selectSectionID);
        }




        /*==============================================================
                       CHECK SECTION IS AVAILABLE OR NOT
        ==============================================================*/
        public void selectedSection_valid(string selectedSection)
        {
            /*myConnection conn = new myConnection();
            //conn.exQuery();

            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = myConnection.conn;
            cmd.CommandText = "select lastname FROM cce_student where firstname='wohhie'";


            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                //add values
                string studentLastName = dr.GetString(0).ToString();
                MessageBox.Show(studentLastName);

            }*/
        }



    }
}
