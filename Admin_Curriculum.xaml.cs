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
    /// Interaction logic for Admin_Curriculum.xaml
    /// </summary>
    public partial class Admin_Curriculum : Window
    {
        public Admin_Curriculum()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //drop all the table elements
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True";

                
                //DELETE cce_sectionlist table;
                /*================================*/
                string sql0 = "DELETE FROM cce_sectionlist";


                //DELETE cce_marks table;
                /*================================*/
                string sql1 = "delete from cce_marks";


                //DELETE courselist table;
                /*================================*/
                string sql2 = "DELETE FROM cce_studentCourseList";


                //DELETE cce_studentCourseList table;
                /*================================*/
                string sql3 = "DELETE FROM cce_teacherCourseList";



                //DELETE cce_teacherCourseList table;
                /*================================*/
                string sql4 = "DELETE FROM cce_courselist";


                //drop cce_curriculum table;
                /*================================*/

                string sql5 = "DELETE FROM cce_curriculum";


                con.Open();
                SqlCommand cmd0 = new SqlCommand(sql0, con);
                SqlCommand cmd1 = new SqlCommand(sql1, con);
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                SqlCommand cmd3 = new SqlCommand(sql3, con);
                SqlCommand cmd4 = new SqlCommand(sql4, con);
                SqlCommand cmd5 = new SqlCommand(sql5, con);


                cmd0.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();
                cmd5.ExecuteNonQuery();

                con.Close();
                successful_message.Content = "Successfully Reset.";


                //all the buttons are enable
                select_program.IsEnabled = true;
                grading_system.IsEnabled = true;
                mark_system.IsEnabled = true;
                save_change.IsEnabled = true;



                //drop teacherCourseList table;
                /*================================*/





            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }

            
        }

        private void save_change_Click(object sender, RoutedEventArgs e)
        {

            string selectProgram = " ",
                    markSystem = " ",
                    gradingSystem = " ";

            
            //select starttime
            if (select_program.SelectedIndex >= 0)
                selectProgram = select_program.SelectionBoxItem.ToString();


            //select endtime
            if (mark_system.SelectedIndex >= 0)
                markSystem = mark_system.SelectionBoxItem.ToString();


            //select capacity
            if (grading_system.SelectedIndex >= 0)
                gradingSystem = grading_system.SelectionBoxItem.ToString();




            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True";

                string sql = "insert into cce_curriculum values('" + selectProgram + "', '" + gradingSystem + "','" + markSystem + "')";

                //MessageBox.Show(sql);

                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();

                successful_message.Content = "Succesfully Selected Curriculum.";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }
        }
    }
}
