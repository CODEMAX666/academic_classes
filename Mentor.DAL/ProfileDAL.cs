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
    public class ProfileDAL
    {
          public List<ProfileBE>  Profile (int MemID, int ViewerID)
        {
            List<ProfileBE> profiles = new List<ProfileBE>();

            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberProfile_insert.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@MemberId", MemID);
            cmd.Parameters.AddWithValue("@ViewerId", ViewerID);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    profiles.Add(new ProfileBE
                    {
                        FullName = read["Name"].ToString(),
                        Gender = read["Gender"].ToString(),
                        DateofJoin = read["JoiningDate"].ToString(),
                        AboutYourself = read["AboutMe"].ToString(),
                        CareerLevelName = read["MemberCareerLevel"].ToString(),
                        DomainName = read["MemberDomainName"].ToString(),
                        PerHourRate = read["PerHourRate"].ToString(),
                        Photo = read["PhotoURL"].ToString(),
                        //Institute = read["Institute"].ToString(),
                        //DegreeLevel = read["DegreeLevelName"].ToString(),
                        //DegreeTitle = read["DegreeTitleName"].ToString(),
                        //Designation = read["Designation"].ToString(),
                        //Company = read["Company"].ToString(),
                        //TotalExperience = read["TotalExperience"].ToString(),
                        //ExpCategory = read["CategoryName"].ToString(),
                        //ExpSubCat = read["SubCategoryName"].ToString(),
                       
                    });

                 
                   // profile.RequiredExperience = read["RequiredExperienceId_fk"]),
                   //profile.JobKPIS = read["Job_KPIS"],
                   //profile.AppClosingDate = read["ApplicationClosingDate"],
                   //profile.IsPublic = read["IsPublic"]),
                };
            } 

            else
            {
                Console.WriteLine("No rows found");
            }
            con.Close();
            return profiles;


        }
        public List<MyTrainingList> TraininglistbyID(int memID, int menteeid)
        {

            List<MyTrainingList> TrainingLists = new List<MyTrainingList>();
            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MenteeTrainingAvailable.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@MentorId", memID);
            cmd.Parameters.AddWithValue("@MenteeId", menteeid);


            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    MyTrainingList TrainingList = new MyTrainingList();
                    TrainingList.TaskTitle= read["Title"].ToString() ;
                    TrainingList.StartingDate = read["TrainingStartDate"].ToString();
                    //jobList.MarketJobID = Convert.ToInt32(read["MarketJobsId"]);
                    TrainingList.TrainingHour = read["TrainingHour"].ToString();
                   
                    TrainingLists.Add(TrainingList);
                }
            }

            else
            {
                Console.WriteLine("No rows found");
            }
            con.Close();
            return TrainingLists;

        }
    }
}
