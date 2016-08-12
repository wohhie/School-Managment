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

namespace CCEmanagement
{
    /// <summary>
    /// Interaction logic for Teacher_AssignGrade.xaml
    /// </summary>
    public partial class Teacher_AssignGrade : Window
    {
        private myConnection conn;

        public Teacher_AssignGrade(string fullname, string sect,string roll, string courseID)
        {
            InitializeComponent();
            studentInformation(fullname, sect, roll, courseID);

        }


        /*==============================================
                        SHOW STUDENT DETAILS
        ================================================*/
        public void studentInformation(string fullName, string section, string roll,string courseID) 
        {
            //create connection
            conn = new myConnection();

            student_fullName.Content = fullName;
            student_section.Content = section;
            student_roll.Content = roll;
            course_ID.Content = courseID;

            //close connection
            conn.con.Close();
        }


        /*==============================================
                   HANDELE MID TERM MARKS
        ================================================*/
        private void Save_Mid_Marks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string roll = student_roll.Content.ToString();
                
                Teacher_assignGrade tag = new Teacher_assignGrade();
                string currStdID = tag.findStudentID(roll);                 //1
                string atdMark = tag.RetriveAttendanceMark(currStdID);      //1.00


                double  q1 = 0.00, 
                        q2 = 0.00, 
                        q3 = 0.00, 
                        rprt = 0.00, 
                        assign = 0.00, 
                        finalTerm = 0.0;
                       
                //quiz marks
                q1 = double.Parse(quiz1.Text);
                q2 = double.Parse(quiz2.Text);
                q3 = double.Parse(quiz3.Text);



                //report performance and assignment mark
                rprt = double.Parse(report_mark.Text);
                assign = double.Parse(assignment.Text);
                finalTerm = double.Parse(term.Text);
                attendance.Text = atdMark;
                
                double fQuiz = 0;


                if (rprt >= 6.00)
                {
                    message.Content = "Report marks cannot be more than " + Subject_ReportMark.Text;
                    final.Text = "0";
                }else if (q1 > 21 || q2 > 21 || q3 > 21)
                {
                    message.Content = "Quiz marks cannot be more than 20";
                    final.Text = "0";
                }
                else
                {
                    //message box null
                    message.Content = "";


                    //insert value to marks table
                    if (q1 < q2 && q1 < q3)
                        fQuiz = q2 + q3;
                    else if (q2 < q1 && q2 < q3)
                        fQuiz = q1 + q3;
                    else if (q3 < q1 && q3 < q2)
                        fQuiz = q1 + q2;
                
                    //final marks

                    double total = fQuiz + double.Parse(atdMark) + rprt + assign + finalTerm;
                    final.Text = (total).ToString();

                    //handleGrade
                    string Grade = tag.handleMidGrade(total);  //send total marks
                    grade.Text = Grade;



                    //INSERT TO THE MARKS_DETAILS

                    int currentStudent = Int32.Parse(currStdID);

                    bool successful = false;
                    successful = tag.insertMarkDetails(currentStudent, int.Parse(course_ID.Content.ToString()) , fQuiz, assign, rprt, double.Parse(atdMark), finalTerm, total, Grade);

                }
    

            }
            catch (Exception ex)
            {
                MessageBox.Show("Errror" + ex);
            }
            

        }


        /*
            focus value = null;
            */

        private const string defaultValue = "0.00";
        private void textbox_gotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.Text = tb.Text == defaultValue ? string.Empty : tb.Text;
        }

        private void textbox_lostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = tb.Text == string.Empty ? defaultValue : tb.Text;
        }
    }
}
