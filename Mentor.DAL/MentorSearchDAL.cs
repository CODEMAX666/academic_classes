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
    public class MentorSearchDAL
    {
        string conString = DataUtil.connectionString;

        public List<SelectedFilteredMentorList> GetFilteredMentorList(int ID, string gender, int careerid, int domianid, int categoryid, int subcategoryid, int min, int max)
        {
            float fmin = min;
            float fmax = max;
            if (gender.Equals(""))
            {
                gender = null;
            }

            List<SelectedFilteredMentorList> filtermentor = new List<SelectedFilteredMentorList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Search_Mentor.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberId", ID);
            cmd.Parameters.AddWithValue("@Gender", gender);
            cmd.Parameters.AddWithValue("@MemberCareerLevelId",new GenericFunctionsDAL().checknullint(careerid));
            cmd.Parameters.AddWithValue("@MemberDomainId", new GenericFunctionsDAL().checknullint(domianid));
            cmd.Parameters.AddWithValue("@CategoryId", new GenericFunctionsDAL().checknullint(categoryid));
            cmd.Parameters.AddWithValue("@SubCategoryId", new GenericFunctionsDAL().checknullint(subcategoryid));
            cmd.Parameters.AddWithValue("@MinRate", new GenericFunctionsDAL().checknullfloat(fmin));
            cmd.Parameters.AddWithValue("@MaxRate", new GenericFunctionsDAL().checknullfloat(fmax));

            /* cmd.Parameters.AddWithValue("@Gender",null);
             cmd.Parameters.AddWithValue("@MemberCareerLevelId", null);
             cmd.Parameters.AddWithValue("@MemberDomainId", null);
             cmd.Parameters.AddWithValue("@CategoryId", null);
             cmd.Parameters.AddWithValue("@SubCategoryId", null);
             cmd.Parameters.AddWithValue("@MinRate", null);
             cmd.Parameters.AddWithValue("@MaxRate", null);*/
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    filtermentor.Add(new SelectedFilteredMentorList
                    {
                        MemberId = Convert.ToInt32(read["MemberId"].ToString()),
                        MemberName = read["Name"].ToString(),
                        Membergender = read["Gender"].ToString(),
                        MemberCareerLevel = read["MemberCareerLevel"].ToString(),
                        MemberDomain = read["MemberDomainName"].ToString(),
                        MemberCategory = read["CategoryName"].ToString(),
                        MemberSubCategory = read["SubCategoryName"].ToString(),
                        // MemberRate =float.Parse(read["PerHourRate"].ToString()),
                        MemberRate = read["PerHourRate"].ToString(),
                        MemberPhotoUrl = read["PhotoURL"].ToString()

                    });
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            con.Close();
            return filtermentor;
        }

        public List<MyMember> GetSelectedMember(int mid)
        {
            List<MyMember> mymentor = new List<MyMember>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_Member.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberID", mid);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            try
            {
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        mymentor.Add(new MyMember
                        {

                            FirstName = read["FirstName"].ToString(),
                            LastName = read["LastName"].ToString(),
                            CareerLevelName = read["MemberCareerLevel"].ToString(),
                            MemberCurrentRate = read["PerHourRate"].ToString(),
                            PhotoURL = read["PhotoURL"].ToString()

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
            return mymentor;
        }



        public bool DeleteMentor(int MenteeID, int MentorID)
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
    }
}
