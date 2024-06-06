using Mentor.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.DAL
{
    public class AddBookingDAL
    {
        string conString = DataUtil.connectionString;

        public void Insert_Member_Booking(AddBookingBE bookingBE)
        {
           // var date = DateTime.ParseExact(bookingBE.MeetingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_AddMemberBooking.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberMeetingScheduleId", bookingBE.MemberMeetingScheduleId);
            cmd.Parameters.AddWithValue("@MemberMeetingSlotId", bookingBE.MemberMeetingSlotId);
            cmd.Parameters.AddWithValue("@MenteeId", bookingBE.Menteeid);
            cmd.Parameters.AddWithValue("@MentorId", bookingBE.Mentorid);
            cmd.Parameters.AddWithValue("@PaymentTypeId", 1);
            cmd.Parameters.AddWithValue("@PaymentMethodId", 1);
            cmd.Parameters.AddWithValue("@TotalAmount", bookingBE.TotalAmount);
            cmd.Parameters.AddWithValue("@MeetingDate", bookingBE.MeetingDate);




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
        public List<Booking_Detail> Gett_Booking_Detail(int sc, int sl, int men)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_Member_Booking_Detail.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            List<Booking_Detail> book_detail = new List<Booking_Detail>();
            cmd.Parameters.AddWithValue("@MemberMeetingScheduleId", sc);
            cmd.Parameters.AddWithValue("@MemberMeetingSlotId", sl);
            cmd.Parameters.AddWithValue("@MentorId", men);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            try
            {
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        book_detail.Add(new Booking_Detail
                        {
                            Name = read["Name"].ToString(),
                            Day = read["Day"].ToString(),
                            StartTime = read["StartTime"].ToString(),
                            Duration = read["Duration"].ToString(),
                            TotalAmount = read["TotalAmount"].ToString()
                        });
                    }

                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();

            }
            return book_detail;
        }





    }
}
