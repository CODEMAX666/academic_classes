using Mentor.BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Mentor.DAL
{
    public class AddJobDAL
    {

        public void addjob(AddJobBE m)
        {

            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MarketJobs_Insert_Update_Delete.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@MarketJobsId", m.JobID);
            cmd.Parameters.AddWithValue("@MemberId", m.MemberId);
            cmd.Parameters.AddWithValue("@MemberCareerLevelId", m.MemberCareerLevel);
            cmd.Parameters.AddWithValue("@MemberDomainId", m.MemberDomain);
            cmd.Parameters.AddWithValue("@CategoryId", m.Category);
            cmd.Parameters.AddWithValue("@SubCategoryId", m.SubCategory);
            cmd.Parameters.AddWithValue("@Title", m.Title);
            cmd.Parameters.AddWithValue("@Company", m.Company);
            cmd.Parameters.AddWithValue("@CompanyEmail", m.ComEmail);
            cmd.Parameters.AddWithValue("@City", m.City);
            cmd.Parameters.AddWithValue("@RequiredExperienceId", m.RequiredExperience);
            cmd.Parameters.AddWithValue("@Job_KPIS", m.JobKPIS);
            cmd.Parameters.AddWithValue("@ApplicationClosingDate", m.AppClosingDate);
            cmd.Parameters.AddWithValue("@IsPublic", m.IsPublic);

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
        public void Updatejob(AddJobBE m)
        {

            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MarketJobs_Insert_Update_Delete.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MarketJobsId", m.JobID);
            cmd.Parameters.AddWithValue("@MemberId", m.MemberId);
            cmd.Parameters.AddWithValue("@MemberCareerLevelId", m.MemberCareerLevel);
            cmd.Parameters.AddWithValue("@MemberDomainId", m.MemberDomain);
            cmd.Parameters.AddWithValue("@CategoryId", m.Category);
            cmd.Parameters.AddWithValue("@SubCategoryId", m.SubCategory);
            cmd.Parameters.AddWithValue("@Title", m.Title);
            cmd.Parameters.AddWithValue("@Company", m.Company);
            cmd.Parameters.AddWithValue("@CompanyEmail", m.ComEmail);
            cmd.Parameters.AddWithValue("@City", m.City);
            cmd.Parameters.AddWithValue("@RequiredExperienceId", m.RequiredExperience);
            cmd.Parameters.AddWithValue("@Job_KPIS", m.JobKPIS);
            cmd.Parameters.AddWithValue("@ApplicationClosingDate", m.AppClosingDate);
            cmd.Parameters.AddWithValue("@IsPublic", m.IsPublic);
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


    }
}
    
