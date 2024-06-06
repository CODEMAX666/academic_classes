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
    public class DropDownPopulate
    {
        string conString = DataUtil.connectionString;

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

       
       
        public List<SelectedListDomain> GetDomainList()
        {
            List<SelectedListDomain> domain = new List<SelectedListDomain>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberDomain.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                domain.Add(new SelectedListDomain
                {
                    Value = "0",
                    Text = "Select Domain",
                    CareerLevelId = 0
                });
                while (read.Read())
                {
                    domain.Add(new SelectedListDomain
                    {
                        Value = read["MemberDomainId"].ToString(),
                        Text = read["MemberDomainName"].ToString(),
                        CareerLevelId = Convert.ToInt32(read["MembercareerlevelId_FK"])
                    });
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
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
        public string GetSubCategoryNames()
        {
            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_SubCategoryName.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            string SubCategoryName = "";
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {

                while (read.Read())
                {
                    SubCategoryName = read["SubCategoryName"].ToString();
                }
            }

            else
            {
                Console.WriteLine("No rows found");
            }
            con.Close();
            return SubCategoryName;
        }
        // Trainings dropdown

        public List<SelectedList> GetTrainingTitleList()
        {
            List<SelectedList> Training = new List<SelectedList>();
            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_Public_MenteeTraining.ToString(), con);
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
                Training.Add(new SelectedList
                {
                    Value = read["MenteeTrainingId"].ToString(),
                    Text = read["Title"].ToString()
                });
            }
            con.Close();
            return Training;
        }
    }
}
