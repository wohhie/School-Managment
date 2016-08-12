using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;

namespace CCEmanagement
{
    class Createsection
    {
        private myConnection conn;


        public bool checkValidSection(string secName, string roomNo,string sTime,string eTime, string day)
        {
            string _roomno      = " ",
                   _secName     = " ",
                   _startTime   = " ",
                   _endTime     = " ",
                   _day         = " ";

            bool valid = false;

            conn = new myConnection();
            string sql = "select section,startTime,endTime,day,roomno from cce_sectionlist where section= '" + secName + "' and roomno='" + roomNo + "'";
            conn.SqlQuery(sql);

            foreach (DataRow dr in conn.exQuery().Rows)
            {

                _secName = dr["section"].ToString();
                _roomno = dr["roomno"].ToString();
                _startTime = TimeSpan.Parse(dr["startTime"].ToString()).ToString(@"hh\:mm");
                _endTime = TimeSpan.Parse(dr["endTime"].ToString()).ToString(@"hh\:mm");
                _day = dr["day"].ToString();

                if( sTime == _startTime && eTime == _endTime && day == _day && roomNo == _roomno ){

                    valid = true;
                }


            }

            return valid;
        }



        /* check room no is clear on that time.*/
        public bool checkRoomNo(string sectionName)
        {
            return false;
        }

    }
}
