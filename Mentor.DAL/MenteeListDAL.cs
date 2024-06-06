using Mentor.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.DAL
{
    public class MenteeListDAL
    {
        string conString = DataUtil.connectionString;
        public List<MenteeListBE> getMenteeList = new List<MenteeListBE>();

        private object read;

        public object Response { get; private set; }

        public List<MenteeListBE> Menteelist(int id/*,List<SelectedList> listOne*/)
        {
            //public List<StatusList> getStatusList = new List<StatusList>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MenteeList.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", id);
            con.Open();
            SqlDataReader sqr = cmd.ExecuteReader();

            while (sqr.Read())
            {
                string Id = sqr["MemberId"].ToString();
                string name = sqr["Name"].ToString();
                string perHourRate = sqr["PerHourRate"].ToString();
                string phoneNum = sqr["PhoneNo"].ToString();
                string status = sqr["Status"].ToString();
                string catName = sqr["CategoryName"].ToString();
                string subCatName = sqr["SubCategoryName"].ToString();

                getMenteeList.Add(new MenteeListBE(Id, name, perHourRate, phoneNum, catName, subCatName, status /*,listOne*/));
            }

            con.Close();
            return getMenteeList;

        }
        /*public List<SelectedList> getStatusList()
        {
            List<SelectedList> FollowStatusList = new List<SelectedList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_FollowStatus.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            FollowStatusList.Add(new SelectedList
            {
                Value = "0",
                Text = "Change Status"
            });
            while (read.Read())
            {
                FollowStatusList.Add(new SelectedList
                {
                    Value = read["FollowStatusId"].ToString(),
                    Text = read["Status"].ToString()
                });
            }
            con.Close();
            return FollowStatusList;
        }*/

        public bool DeleteMentee(int MenteeID, int MentorID)
        {
            bool flag = false;

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Mark_Mentee_FollowStatus_UnfollowByMentor.ToString(), con);
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

        public bool AcceptReqOfMentee(int MenteeID, int MentorID)
        {
            bool flag = false;

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Mark_Mentee_FollowStatus_Following.ToString(), con);
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

        public bool RejectReqOfMentee(int MenteeID, int MentorID)
        {
            bool flag = false;

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Mark_Mentee_FollowStatus_Reject.ToString(), con);
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
    }
}
