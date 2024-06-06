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
    public class MemberDAL
    {
        string conString = DataUtil.connectionString;

        /* public void Insert_Update_Member(MemberBE m)
         {
             SqlConnection con = new SqlConnection(conString);
             SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Member_Insert_Update.ToString(), con);
             cmd.CommandType = CommandType.StoredProcedure;

             if (m.MemberMenteeCareerLevelList != null)
             {
                 var table = new DataTable();
                 table.Columns.Add("MemberMenteeCareerLevelId", typeof(int));
                 foreach (var level in m.MemberMenteeCareerLevelList)
                 {
                     var row = table.NewRow();

                     row["MemberMenteeCareerLevelId"] = level.MemberCareerLevelId_fk;

                     table.Rows.Add(row);
                 }
                 var parameter = cmd.CreateParameter();
                 parameter.TypeName = "dbo.MenteeTableType";
                 parameter.Value = table;
                 parameter.ParameterName = "@MTT";
                 cmd.Parameters.Add(parameter);
             }
             cmd.Parameters.AddWithValue("@FirstName", m.FirstName);
             cmd.Parameters.AddWithValue("@LastName", m.LastName);
             cmd.Parameters.AddWithValue("@Email", m.Email);
             cmd.Parameters.AddWithValue("@PhoneNo", m.PhoneNo);
             cmd.Parameters.AddWithValue("@Password", m.Password);
             cmd.Parameters.AddWithValue("@MemberCareerLevelID", Convert.ToInt32(m.MemberLevel));
             cmd.Parameters.AddWithValue("@MemberDomainId", Convert.ToInt32(m.MemberDomain));
             cmd.Parameters.AddWithValue("@IsMentor", m.IsMentor);
             cmd.Parameters.AddWithValue("@MemberRate", m.MemberCurrentRate);
             cmd.Parameters.AddWithValue("@PhotoURL", "/Images/MemberProfilePhotos/user.png");
             cmd.Parameters.AddWithValue("@Message",null);

             // cmd.Parameters.AddWithValue("@MTT", table);

             //var ResponseId = cmd.Parameters.Add("@Message", SqlDbType.VarChar);
              //ResponseId.Direction = ParameterDirection.ReturnValue;



             try
             {
                 con.Open();
                 cmd.ExecuteNonQuery();
                //string message = (string)cmd.Parameters["@Message"].Value;
                // mymsg(ResponseId.ToString());
                 //int aa = 20;
             }
             catch (Exception exp)
             {
                 mymsg((string)cmd.Parameters["@Message"].Value);
                 throw exp;
             }
             finally
             {
                 con.Close();
             }

         }*/

        public void Insert_Update_Member(MemberBE m)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Member_Insert_Update.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            if (m.MemberLevel != "1" && m.IsMentor==true)
            {
                var table = new DataTable();
                table.Columns.Add("MemberMenteeCareerLevelId", typeof(int));
                foreach (var level in m.MemberMenteeCareerLevelList)
                {
                    var row = table.NewRow();

                    row["MemberMenteeCareerLevelId"] = level.MemberCareerLevelId_fk;

                    table.Rows.Add(row);
                }
                var parameter = cmd.CreateParameter();
                parameter.TypeName = "dbo.MenteeTableType";
                parameter.Value = table;
                parameter.ParameterName = "@MTT";
                cmd.Parameters.Add(parameter);
            }
            cmd.Parameters.AddWithValue("@FirstName", m.FirstName);
            cmd.Parameters.AddWithValue("@LastName", m.LastName);
            cmd.Parameters.AddWithValue("@Email", m.Email);
            cmd.Parameters.AddWithValue("@PhoneNo", m.PhoneNo);
            cmd.Parameters.AddWithValue("@Password", m.Password);
            cmd.Parameters.AddWithValue("@MemberCareerLevelID", Convert.ToInt32(m.MemberLevel));
            cmd.Parameters.AddWithValue("@MemberDomainId", Convert.ToInt32(m.MemberDomain));
            cmd.Parameters.AddWithValue("@IsMentor", m.IsMentor);
            cmd.Parameters.AddWithValue("@MemberRate", m.MemberCurrentRate);
            cmd.Parameters.AddWithValue("@PhotoURL", "/Images/MemberProfilePhotos/user.png");
            // cmd.Parameters.AddWithValue("@MTT", table);

            var ResponseId = cmd.Parameters.Add("@Message", SqlDbType.Int);
            ResponseId.Direction = ParameterDirection.ReturnValue;
            var Response = cmd.Parameters.Add("@status", SqlDbType.VarChar);
            Response.Direction = ParameterDirection.ReturnValue;

            try
            {
                con.Open();
                // cmd.ExecuteNonQuery();
                SqlDataReader read = cmd.ExecuteReader();
                read.Read();
                if (read.HasRows)
                {
                    string message = read["Message"].ToString();
                    mymsg(message);
                    int a = 0;
                }
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

        string msg = "";
        public string getmsg() {

            return msg;
        }
        public void mymsg(string m)
        {
            msg = m;
            
        }
        public MemberBE GetMemberById(int MemberId)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_Member.ToString(), con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberId", MemberId);
            MemberBE member = new MemberBE();
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            read.Read();
            if (read.HasRows)
            {
                member.MemberId = Convert.ToInt32(read["MemberId"]);
                member.FirstName = read["FirstName"].ToString();
                member.LastName = read["LastName"].ToString();
                member.Email = read["Email"].ToString();
                member.Gender = read["Gender"].ToString();
                if (read["DateOfBirth"] != DBNull.Value)
                {
                    member.DateOfBirth = Convert.ToDateTime(read["DateOfBirth"]);
                }
                member.PhoneNo = read["PhoneNo"].ToString();
                if (read["SecondaryPhoneNo"] != DBNull.Value)
                {
                    member.SecondaryPhoneNo = read["SecondaryPhoneNo"].ToString();
                }
                if (read["MemberDomainId_fk"] != DBNull.Value)
                {
                    member.MemberDomainID = Convert.ToInt32(read["MemberDomainId_fk"]);
                }
                if (read["MemberCareerLevelId_fk"] != DBNull.Value)
                {
                    member.MemberLevelID = Convert.ToInt32(read["MemberCareerLevelId_fk"]);
                }
                member.IsMentor = Convert.ToBoolean(read["IsMentor"]);
                if (read["MenteeCareerLevel"] != DBNull.Value)
                {
                    member.MemberPossibleMentee = read["MenteeCareerLevel"].ToString();
                }
                member.MemberCurrentRate = read["PerHourRate"].ToString();
                member.IsFirstTimeLogin = Convert.ToBoolean(read["IsFirstTimeLogin"]);
                if (read["PhotoURL"] != DBNull.Value)
                {
                    member.PhotoURL = read["PhotoURL"].ToString();
                }
                if (read["AboutYourSelf"] != DBNull.Value)
                {
                    member.AboutYourSelf = read["AboutYourSelf"].ToString();
                }
                //member.ModifiedDateTime = Convert.ToDateTime(read["ModifiedDateTime"]);
                con.Close();
                
            }
            return member;

        }
        public void UpdateMember(MemberBE m)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Member_Insert_Update.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (m.MemberLevelID != 1 && m.IsMentor==true)
            {
                if (m.MemberMenteeCareerLevelList != null)
                {
                    var table = new DataTable();
                    table.Columns.Add("MemberMenteeCareerLevelId", typeof(int));
                    foreach (var level in m.MemberMenteeCareerLevelList)
                    {
                        var row = table.NewRow();

                        row["MemberMenteeCareerLevelId"] = level.MemberCareerLevelId_fk;

                        table.Rows.Add(row);
                    }
                    var parameter = cmd.CreateParameter();
                    parameter.TypeName = "dbo.MenteeTableType";
                    parameter.Value = table;
                    parameter.ParameterName = "@MTT";
                    cmd.Parameters.Add(parameter);
                }
            }
           
      /*      var Domaintable = new DataTable();
            Domaintable.Columns.Add("MemberDomainId", typeof(int));
            foreach (var level in m.MemberSelectedDomainList)
            {
                var row = Domaintable.NewRow();

                row["MemberDomainId"] = level.MemberDomainID;

                Domaintable.Rows.Add(row);
            }*/
            cmd.Parameters.AddWithValue("@MemberID", m.MemberId);
            cmd.Parameters.AddWithValue("@FirstName", m.FirstName);
            cmd.Parameters.AddWithValue("@LastName", m.LastName);
            cmd.Parameters.AddWithValue("@Gender", m.Gender);
            cmd.Parameters.AddWithValue("@DateOfBirth", m.DateOfBirth);
            cmd.Parameters.AddWithValue("@PhoneNo", m.PhoneNo);
            cmd.Parameters.AddWithValue("@PhotoURL", m.PhotoURL);
            cmd.Parameters.AddWithValue("@SecondaryPhoneNo", m.SecondaryPhoneNo);
            cmd.Parameters.AddWithValue("@AboutYourSelf", m.AboutYourSelf);
            cmd.Parameters.AddWithValue("@MemberCareerLevelID", m.MemberLevelID);
            cmd.Parameters.AddWithValue("@MemberDomainId", m.MemberDomainID);
            if (m.IsMentor==true){
                cmd.Parameters.AddWithValue("@IsMentor", 1);
            }
            else if (m.IsMentor == false)
            {
                cmd.Parameters.AddWithValue("@IsMentor", 0);
            }
            cmd.Parameters.AddWithValue("@MemberRate", m.MemberCurrentRate);
           
      /*      var parameter1 = cmd.CreateParameter();
            parameter1.TypeName = "dbo.DomainTableType";
            parameter1.Value = Domaintable;
            parameter1.ParameterName = "@MDTT";
            cmd.Parameters.Add(parameter1);*/
            con.Open();
            cmd.ExecuteNonQuery();

        }
        public List<SelectedList> GetCareerLevelList()
        {
            List<SelectedList> levels = new List<SelectedList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberCareerLevel.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            levels.Add(new SelectedList
            {
                Value = "0",
                Text = "Select Career Level"
            });
            while (read.Read())
            {
                levels.Add(new SelectedList
                {
                    Value = read["MemberCareerLevelId"].ToString(),
                    Text = read["MemberCareerLevel"].ToString()
                });
            }
            con.Close();
            return levels;
        }
        
        public List<SelectedList> GetDomainList(int MembercareerlevelId)
        {
            List<SelectedList> domain = new List<SelectedList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberDomain.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MembercareerlevelId", MembercareerlevelId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            domain.Add(new SelectedList
            {
                Value = "0",
                Text = "Select Domain"
            });
            while (read.Read())
            {
                domain.Add(new SelectedList
                {
                    Value = read["MemberDomainId"].ToString(),
                    Text = read["MemberDomainName"].ToString()
                });
            }
            con.Close();
            return domain;
        }
        public List<SelectedList> GetCareerLevelForMenteeList()
        {
            List<SelectedList> levels = new List<SelectedList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberCareerLevel.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            levels.Add(new SelectedList
            {
                Value = "0",
                Text = "Select Mentee CareerLevel"
            });
            while (read.Read())
            {
                levels.Add(new SelectedList
                {
                    Value = read["MemberCareerLevelId"].ToString(),
                    Text = read["MemberCareerLevel"].ToString()
                });
            }
            con.Close();
            return levels;
        }
        public List<SelectedList> GetGenderList()
        {
            List<SelectedList> gender = new List<SelectedList>();

            gender.Add(new SelectedList
            {
                Value = "0",
                Text = "Select Gender"
            });
            gender.Add(new SelectedList
            {
                Value = "1",
                Text = "Male"
            });
            gender.Add(new SelectedList
            {
                Value = "2",
                Text = "Female"
            });
            return gender;
        }
        public bool DeleteMentor(int MentorID, int MenteeID)
        {
            bool flag = false;



            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Mark_Mentee_FollowStatus_UnfollowByMentee.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MentorId", MentorID);
            cmd.Parameters.AddWithValue("@MenteeId", MenteeID);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();

            if (read.HasRows)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public List<BookingListBE> getMentorBookingList(int getLogedInId)     //getMentoreList
        {
            List<BookingListBE> getBookingsOfMentors = new List<BookingListBE>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MentorBooking.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MenteeId", getLogedInId);
            con.Open();
            SqlDataReader sqr = cmd.ExecuteReader();
            while (sqr.Read())
            {
                string Id= sqr["MemberBookingId"].ToString();
                string email = sqr["Email"].ToString();
                string name = sqr["Name"].ToString();
                string day = sqr["Day"].ToString();
                string url = sqr["PhotoURL"].ToString();
                DateTime getDate = Convert.ToDateTime(sqr["MeetingDate"]);
                string date = getDate.ToShortDateString();
                //DateTime getTime =Convert.ToDateTime( sqr["StartTime"]);
                //string StartTime = getTime.ToString("HH:mm");

                string StartTime = sqr["StartTime"].ToString();

                string EndTime = sqr["EndTime"].ToString();
                string duration = sqr["Duration"].ToString();
                string amount = sqr["TotalAmount"].ToString();
                getBookingsOfMentors.Add(new BookingListBE(Id, name, email, day, date, StartTime, EndTime, url,amount));
            }

            con.Close();
            return getBookingsOfMentors;
        }

        public List<BookingListBE> getMenteeBookingList(int getLogedInId)     //getMentoreList
        {
            List<BookingListBE> getBookingsOfMentors = new List<BookingListBE>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MentorBooking.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MentorId", getLogedInId);
            con.Open();
            SqlDataReader sqr = cmd.ExecuteReader();
            while (sqr.Read())
            {
                string Id = sqr["MemberBookingId"].ToString();
                string name = sqr["Name"].ToString();
                string email = sqr["Email"].ToString();
                string day = sqr["Day"].ToString();
                string url = sqr["PhotoURL"].ToString();
                DateTime getDate =Convert.ToDateTime( sqr["MeetingDate"]);
                string date = getDate.ToShortDateString();
                //DateTime getTime = Convert.ToDateTime(sqr["StartTime"]);
                //string StartTime = getTime.ToString("HH:mm");
                string StartTime = sqr["StartTime"].ToString();
                string EndTime = sqr["EndTime"].ToString();
                string duration = sqr["Duration"].ToString();
                string amount = sqr["TotalAmount"].ToString();

                //DateTime getDate = new DateTime(date);
                //getDate = getDate.ToShortDateString();

                getBookingsOfMentors.Add(new BookingListBE(Id, name, email, day, date, StartTime, EndTime, url,amount));
            }

            con.Close();
            return getBookingsOfMentors;
        }

        public bool DeleteBookingsByMentor(int id)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Mark_BookingStatus_CancelledbyMentee.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberBookingId", id);
            con.Open();
            SqlDataReader sqr = cmd.ExecuteReader();

            return true;
        }

        public bool DeleteBookingsByMentee(int id)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Mark_BookingStatus_CancelledbyMentor.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberBookingId", id);
            con.Open();
            SqlDataReader sqr = cmd.ExecuteReader();

            return true;
        }
    }
}
