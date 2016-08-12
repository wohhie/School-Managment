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
using System.Data;

namespace CCEmanagement
{
    /// <summary>
    /// Interaction logic for Teacher_StudentDetails.xaml
    /// </summary>
    public partial class Teacher_StudentDetails : Window
    {
        private myConnection conn;

        public Teacher_StudentDetails(string studentRoll,string sect,string courseID)
        {
            InitializeComponent();
            ShowStudentInfo(studentRoll,sect, courseID);
        }


        public void ShowStudentInfo(string stdRoll, string sect,string courseID)
        {
           
            //MessageBox.Show(stdRoll);                         //show studentID;
            conn = new myConnection();
            string sql = "select * from cce_student WHERE roll='" + stdRoll + "'";
            conn.SqlQuery(sql);
            
            foreach(DataRow dr in conn.exQuery().Rows )
            {
                studentname.Content = dr["firstname"].ToString().ToUpper() + " " + dr["lastname"].ToString().ToUpper();
                email.Content = dr["email"].ToString();
                roll.Content = dr["roll"].ToString();
                section.Content = sect;
                course_id.Content = courseID;
            }
        }






        /*==========================================
                    CLOSE WINDOW
        ==========================================*/
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        /*==========================================
                    ASSIGN GRADES
        ==========================================*/
        private void assign_grades_Click(object sender, RoutedEventArgs e)
        {
            //collect information
            string fullName = studentname.Content.ToString();
            string sect = section.Content.ToString();
            string stdRoll = roll.Content.ToString();
            string courseid = course_id.Content.ToString();


            Teacher_AssignGrade tag = new Teacher_AssignGrade(fullName, sect, stdRoll, courseid);
            tag.Show();
            this.Close();
        }
    }
}
