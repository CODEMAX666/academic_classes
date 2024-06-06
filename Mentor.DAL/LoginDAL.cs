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
    public class LoginDAL
    {
        string conString = DataUtil.connectionString;

        public int CheckLogin(string email, string password)
        {
            int MemberID = 0;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Login.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            var ResponseId = cmd.Parameters.Add("@Message", SqlDbType.Int);
            ResponseId.Direction = ParameterDirection.ReturnValue;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            //read.Read();
            var dtWriteoffUpload = new DataTable();
            dtWriteoffUpload.Columns.Add("MemberID");
            dtWriteoffUpload.Columns.Add("Email");
            dtWriteoffUpload.Columns.Add("Password");

            while (read.Read())
            {
                if (read.HasRows)
                {
                    string message = read.GetValue(0).ToString();
                    mymsg(message);
                    int a = 0;
                }
                if (msg != "Login Failed!")
                {
                    dtWriteoffUpload.Rows.Add(read["MemberID"].ToString(), read["Email"].ToString());
                }
            }
            if (msg != "Login Failed!")
            {
                if (read.HasRows)
                {
                    int Id = Convert.ToInt32(dtWriteoffUpload.Rows[0][0].ToString());
                    //int Id = Convert.ToInt32(read["MemberID"]);
                    string e = read.ToString();
                    if (read.HasRows)
                    {
                        MemberID = Id;
                    }
                    else
                    {
                        MemberID = 0;
                    }
                }
            }

            else
            {
                MemberID = -11;
            }
            con.Close();
            return MemberID;


        }
        string msg = "";
        public string getmsg()
        {

            return msg;
        }
        public void mymsg(string m)
        {
            msg = m;

        }

        public int GetId(string email)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GetId.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", email);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            read.Read();
            int Id = Convert.ToInt32(read["MemberId"]);
            con.Close();
            return Id;
        }

        public List<MentorListBE> getMentorList(int getLogedInId)     //getMentoreList
        {
            List<MentorListBE> myList = new List<MentorListBE>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MentorList.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", getLogedInId);
            con.Open();
            SqlDataReader sqr = cmd.ExecuteReader();
            while (sqr.Read())
            {
                string name = sqr["Name"].ToString();
                string email = sqr["Email"].ToString();
                string id = sqr["MemberId"].ToString();
                string status = sqr["Status"].ToString();
                myList.Add(new MentorListBE(name, email, id, status));
            }

            con.Close();
            return myList;
        }
        public MemberBE getNameAndPicture(int id)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_Member.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberID", id);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            read.Read();

            string name = "";
            string fname = read["FirstName"].ToString();
            string lname = read["LastName"].ToString();
            name = fname + " " + lname;

            string photoUrl = read["PhotoURL"].ToString();
            bool isMentor = Convert.ToBoolean(read["IsMentor"]);

            MemberBE getValues = new MemberBE { FullName = name, PhotoURL = photoUrl, IsMentor = isMentor };

            con.Close();
            return getValues;
        }
    }
}
