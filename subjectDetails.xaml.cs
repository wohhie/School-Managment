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
    /// Interaction logic for subjectDetails.xaml
    /// </summary>


    /*=====================================================================
                            SUBJECT DETAILS
    =====================================================================*/

    public partial class subjectDetails : Window
    {

        public subjectDetails(string courseID)
        {

            InitializeComponent();


            //connecting to database;

            //show usermail address.
            
            try
            {
                course_id.Content = courseID;
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");

                //collect student information;
                string sql = "SELECT cl.course_name, ct.starttime, ct.endtime, csec.sec_name FROM cce_courselist cl, cce_student cs, cce_time ct, cce_section csec where cl.student_id = cs.id AND cl.course_time_id = ct.id AND cl.course_section_id = csec.id AND cl.id= @courseid";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@courseid", courseID);
                connection.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        course_name.Content = oReader["course_name"];
                        course_section.Content = oReader["course_name"];
                        course_section.Content = oReader["sec_name"];
                        course_time.Content = oReader["starttime"] + " - " + oReader["endtime"];

                    }

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }


        }




    }
}
