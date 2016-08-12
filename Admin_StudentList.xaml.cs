using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CCEmanagement
{
    /// <summary>
    /// Interaction logic for Admin_StudentList.xaml
    /// </summary>
    public partial class Admin_StudentList : Window
    {
        public Admin_StudentList()
        {
            InitializeComponent();
            totalStudentList();
        }

        /*==================================================================
                                 ADMIN STUDENT LIST
        ====================================================================*/
        public void totalStudentList()
        {

            try
            {

                //==============a=====  
                SqlConnection connection = new SqlConnection("Data Source=Developer-Lab;Initial Catalog=ccemanagement;Integrated Security=True");
                //open

                connection.Open();
                string sql = "SELECT * FROM cce_student";


                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("STUDENTLIST");
                sda.Fill(dt);
                studentlist.ItemsSource = dt.DefaultView;


                //close
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

    }
}
