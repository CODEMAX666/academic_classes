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
    public class JobRefferDAL
    {
        public void JobReffer(ProfileBE m)
        {

            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MenteeRefferJobRequest_Insert_Update.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MarketJobsId_fk ", m.JobID);
          
            cmd.Parameters.AddWithValue("@MentorId_fk", m.MemberID);
            cmd.Parameters.AddWithValue("@MenteeId_fk", m.ViewerID);
            cmd.Parameters.AddWithValue("@JobRefferStatusId_fk", m.StatusID);

            //var ResponseId = cmd.Parameters.Add("@ResponseId", SqlDbType.Int);
            //ResponseId.Direction = ParameterDirection.ReturnValue;
            //var Response = cmd.Parameters.Add("@Response", SqlDbType.VarChar);
            //Response.Direction = ParameterDirection.ReturnValue;

            try
            {
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                //cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exp)
            {
                con.Close();
                throw exp;
            }
            //finally
            //{
            //    con.Close();
            //}
        }
        //UpdateStatus
        public ProfileBE StatusUpd(ProfileBE m)
        {
            ProfileBE updstatus = new ProfileBE();
            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MenteeRefferJobRequest_Insert_Update.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MarketJobsId_fk ", m.JobID);

            cmd.Parameters.AddWithValue("@MentorId_fk", m.MemberID);
            cmd.Parameters.AddWithValue("@MenteeId_fk", m.ViewerID);
            cmd.Parameters.AddWithValue("@JobRefferStatusId_fk", m.StatusID);

            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {

                    updstatus.StatusID = Convert.ToInt32(read["JobRefferStatusId_fk"].ToString());
                    //updstatus.MemberId = Convert.ToInt32(read["MemberId_fk"].ToString());
                    //updstatus.MemberCareerLevel = Convert.ToInt32(read["MemberCareerLevelId_fk"].ToString());
                    //updstatus.MemberDomain = Convert.ToInt32(read["MemberDomainId_fk"].ToString());
                   
                }
            }

            else
            {
                Console.WriteLine("No rows found");
            }
            con.Close();
            return updstatus;

        }
        //JobReffer Mentor List
        public List<JobRefferList> MyJobrefferLists(int memID)
        {

            List<JobRefferList> jobrefLists = new List<JobRefferList>();
            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MentorRefferJob.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@MentorId", memID);



            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    JobRefferList jobrefList = new JobRefferList();
                    jobrefList.JobID =Convert.ToInt32(read["MarketJobsId_fk"]);
                    jobrefList.StatusID =Convert.ToInt32(read["JobRefferStatusId_fk"]);
                    jobrefList.ViewerID =Convert.ToInt32(read["MenteeId_fk"]);
                    jobrefList.MemberID =Convert.ToInt32(read["MentorId_fk"]);
                    jobrefList.MenteeName = read["Name"].ToString();
                    jobrefList.JobTitle = read["Title"].ToString();
                    jobrefList.RequestDate = read["RequestDate"].ToString();

                    jobrefLists.Add(jobrefList);
                }
            }

            else
            {
                Console.WriteLine("No rows found");
            }
            con.Close();
            return jobrefLists;

        }
        //jobs requested list by mentee
        public List<JobRequestList> MyJobrequestList(int memID)
        {

            List<JobRequestList> jobreqLists = new List<JobRequestList>();
            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MentorRefferJob.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@MenteeId", memID);



            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    JobRequestList jobreqList = new JobRequestList();
                    jobreqList.JobName = read["Title"].ToString();
                    //jobreqList.RequestDate = read["Title"].ToString();
                    jobreqList.Status = read["RequestDate"].ToString();

                    jobreqLists.Add(jobreqList);
                }
            }

            else
            {
                Console.WriteLine("No rows found");
            }
            con.Close();
            return jobreqLists;

        }
        public class AddStatusDropDown
        {
            string conString = DataUtil.connectionString;
            public List<SelectedExpList> GetStatusLists()
            {
                List<SelectedExpList> StatusLists = new List<SelectedExpList>();

                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_JobRefferStatus.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                /* levels.Add(new SelectedList1
                 {
                     //Value = "0",
                     Value = 0,
                     Text = "Select Career Level"
                 }); */
                while (read.Read())
                {
                    StatusLists.Add(new SelectedExpList
                    {
                        Value = Convert.ToInt32(read["JobRefferStatusId"].ToString()),
                        Text = read["JobRefferStatus"].ToString()
                    });
                }
                con.Close();
                return StatusLists;
            }
        }
    }
}
