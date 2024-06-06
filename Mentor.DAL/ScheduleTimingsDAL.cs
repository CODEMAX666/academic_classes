using Mentor.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.DAL
{
    public class ScheduleTimingsDAL
    {
        string conString = DataUtil.connectionString;

        public void Insert_Schedule_Timing(int mid, string currentday, string sdd1, string edd1, string d1, string sdd2, string edd2, string d2, string sdd3, string edd3, string d3)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MemberMeetingSchedule_Insert_Update.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            var dt = new DataTable();
            dt.Columns.Add("STT_ID", typeof(int));
            dt.Columns.Add("StartTime", typeof(TimeSpan));
            dt.Columns.Add("EndTime", typeof(TimeSpan));
            dt.Columns.Add("Duration", typeof(int));
            if (sdd1 != "-1" && sdd1 != null && edd1 != "-1" && edd1 != null && d1 != "-1" && d1 != null)
            {
                var row1 = dt.NewRow();
                row1["STT_ID"] = 1;
                row1["StartTime"] = sdd1;
                row1["EndTime"] = edd1;
                row1["Duration"] = d1;

                dt.Rows.Add(row1);
            }
            if (sdd2 != "-1" && sdd2 != null && edd2 != "-1" && edd2 != null && d2 != "-1" && d2 != null)
            {
                var row2 = dt.NewRow();
                row2["STT_ID"] = 2;
                row2["StartTime"] = sdd2;
                row2["EndTime"] = edd2;
                row2["Duration"] = d2;

                dt.Rows.Add(row2);
            }
            if (sdd3 != "-1" && sdd3 != null && edd3 != "-1" && edd3 != null && d3 != "-1" && d3 != null)
            {
                var row3 = dt.NewRow();
                row3["STT_ID"] = 3;
                row3["StartTime"] = sdd3;
                row3["EndTime"] = edd3;
                row3["Duration"] = d3;

                dt.Rows.Add(row3);
            }
            cmd.Parameters.AddWithValue("@MemberId", mid);
            cmd.Parameters.AddWithValue("@Day", currentday);

            var parameter = cmd.CreateParameter();
            parameter.TypeName = "dbo.ScheduleTableType";
            parameter.Value = dt;
            parameter.ParameterName = "@STT";
            cmd.Parameters.Add(parameter);


            var ResponseId = cmd.Parameters.Add("@Message", SqlDbType.Int);
            ResponseId.Direction = ParameterDirection.ReturnValue;
            var Response = cmd.Parameters.Add("@status", SqlDbType.VarChar);
            Response.Direction = ParameterDirection.ReturnValue;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }

        }
        public List<SelectedScheduleList> NumberofSlots(int mid)
        {
            List<SelectedScheduleList> scheduletiming = new List<SelectedScheduleList>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberMeetingSchedule.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", mid);


            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    scheduletiming.Add(new SelectedScheduleList
                    {
                        MemberMeetingScheduleId = Convert.ToInt32(read["MemberMeetingScheduleId"].ToString()),
                        MemberId = Convert.ToInt32(read["MemberId_fk"].ToString()),
                        MeetingDay = read["Day"].ToString(),
                        MeetingStartTime = read["StartTime"].ToString(),
                        MeetingEndTime = read["EndTime"].ToString(),
                        MeetingDuration = Convert.ToInt32(read["Duration"].ToString())

                    });
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            con.Close();
            return scheduletiming;
        }

        public void Update_Schedule_Timing(int mmsid, int mid, string currentday, string sdd1, string edd1, string d1)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MemberMeetingSchedule_Insert_Update.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            var dt = new DataTable();
            dt.Columns.Add("STT_ID", typeof(int));
            dt.Columns.Add("StartTime", typeof(TimeSpan));
            dt.Columns.Add("EndTime", typeof(TimeSpan));
            dt.Columns.Add("Duration", typeof(int));
            if (sdd1 != "-1" && sdd1 != null && edd1 != "-1" && edd1 != null && d1 != "-1" && d1 != null)
            {
                var row1 = dt.NewRow();
                row1["STT_ID"] = 1;
                row1["StartTime"] = sdd1;
                row1["EndTime"] = edd1;
                row1["Duration"] = d1;

                dt.Rows.Add(row1);
            }

            cmd.Parameters.AddWithValue("@MemberMeetingScheduleID", mmsid);
            cmd.Parameters.AddWithValue("@MemberId", mid);
            cmd.Parameters.AddWithValue("@Day", currentday);

            var parameter = cmd.CreateParameter();
            parameter.TypeName = "dbo.ScheduleTableType";
            parameter.Value = dt;
            parameter.ParameterName = "@STT";
            cmd.Parameters.Add(parameter);


            var ResponseId = cmd.Parameters.Add("@Message", SqlDbType.Int);
            ResponseId.Direction = ParameterDirection.ReturnValue;
            var Response = cmd.Parameters.Add("@status", SqlDbType.VarChar);
            Response.Direction = ParameterDirection.ReturnValue;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }

        }



        public void Delete_Schedule_Timing(int mmsid)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_DeleteScheduling.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@MemberMeetingScheduleId", mmsid);

            var ResponseId = cmd.Parameters.Add("@Message", SqlDbType.Int);
            ResponseId.Direction = ParameterDirection.ReturnValue;
            var Response = cmd.Parameters.Add("@status", SqlDbType.VarChar);
            Response.Direction = ParameterDirection.ReturnValue;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }

        }




        public List<SelectedSlotsList> GetAllMeetingSlots(int mid)
        {
            List<SelectedSlotsList> slots = new List<SelectedSlotsList>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberMeetingSlot.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", mid);


            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    slots.Add(new SelectedSlotsList
                    {
                        MemberMeetingScheduleId = Convert.ToInt32(read["MemberMeetingScheduleId_fk"].ToString()),
                        MemberMeetingSlotId = Convert.ToInt32(read["MemberMeetingSlotId"].ToString()),
                        SlotDay = read["Day"].ToString(),
                        SlotStartTime = read["StartTime"].ToString(),
                        SlotEndTime = read["EndTime"].ToString(),
                        isReserved = read["IsResrved"].ToString(),

                    });
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            con.Close();
            return slots;
        }



    }
}
