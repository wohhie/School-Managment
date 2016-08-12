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
    /// Interaction logic for CreateSection.xaml
    /// </summary>
    public partial class CreateSection : Window
    {
        public CreateSection()
        {
            InitializeComponent();
        }


        //cancel button
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        //create button
        private void create_button_Click(object sender, RoutedEventArgs e)
        {

            string sectionName = section_name.Text;
            string startTime = " ",
                   endTime = " ",
                   day = " ",
                   roomno = " ";
            int capacity = 0;


            //select starttime
            if (starttime.SelectedIndex >= 0)
                startTime = starttime.SelectionBoxItem.ToString();


            //select endtime
            if (endtime.SelectedIndex >= 0)
                endTime = endtime.SelectionBoxItem.ToString();


            //select capacity
            if (section_capacity.SelectedIndex >= 0)
                capacity = Int32.Parse(section_capacity.SelectionBoxItem.ToString());



            //select roomno
            if (section_roomno.SelectedIndex >= 0)
                roomno = section_roomno.SelectionBoxItem.ToString();


            //select day
            if (section_day.SelectedIndex >= 0)
                day = section_day.SelectionBoxItem.ToString();

            /*=== check section is already assigned ===*/


            Createsection cs = new Createsection();
            bool valid = cs.checkValidSection(sectionName, roomno, startTime, endTime, day);


            /*=== check room number and time schedule ===*/
            /*MessageBox.Show(valid).ToString();
            MessageBox.Show(starttime.Text);*/

            if (valid == true)
            {
                //error cant create;
                message.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EC1A41"));
                message.Content = "Section time schedule conflict";
                startTime = starttime.SelectionBoxItem.ToString();
                //MessageBox.Show(startTime);
            }
            else
            {
                //MessageBox.Show("welcome");
                try
                {

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = @"Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True";

                    string sql = "insert into cce_sectionlist values('" + sectionName + "', '" + startTime + "','" + endTime + "','" + capacity + "','" + roomno + "','" + day + "')";

                    //MessageBox.Show(sql);

                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    //MessageBox.Show("successfully.");
                    message.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF147E69"));
                    message.Content = "Section created successfully.";




                    /****   SET VALUE == NULL   ****/
                    section_name.Text = " ";


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex);
                }
            }

        }
    }
}
