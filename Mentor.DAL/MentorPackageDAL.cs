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
    public class MentorPackageDAL
    {
        string conString = DataUtil.connectionString;

        public void Insert_Update_MentorPackage(MentorPackageBE package,int MentorId)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MentorOwnPackage_Insert_Update.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            string temp = (package.ValidityStart).ToString("yyyy-MM-dd");
            cmd.Parameters.AddWithValue("@MentorOwnedPackageId", package.MentorOwnPackageId);
            cmd.Parameters.AddWithValue("@MenteePackageId", package.MenteePackageId);
            cmd.Parameters.AddWithValue("@MentorId", MentorId);
            cmd.Parameters.AddWithValue("@PackageRate", package.PackageRate);
                    cmd.Parameters.AddWithValue("@ValidityStart", package.ValidityStart);
            cmd.Parameters.AddWithValue("@ValidityEnd", package.ValidityEnd);

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
        //public void Delete_MentorOwnPackage(int MentorOwnedPackageId, int MentorId)
        //{
        //    SqlConnection con = new SqlConnection(conString);
        //    SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MentorOwnPackage_Delete.ToString(), con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@MentorOwnedPackageId", MentorOwnedPackageId);
        //    cmd.Parameters.AddWithValue("@MentorId", MentorId);
        //    try
        //    {
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        public List<MentorPackageBE> GET_MentorOwnedPackage(int MentorId)
        {
            List<MentorPackageBE> package = new List<MentorPackageBE>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MentorOwnedPackage.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MentorId", MentorId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                package.Add(new MentorPackageBE
                {
                    MentorOwnPackageId= read["MentorOwnedPackageId"].ToString(),
                    MenteePackageId = Convert.ToInt32(read["MenteePackageId_fk"]),
                    MenteePackageName = read["MenteePackageName"].ToString(),
                    MemberMenteeCareerLevel = read["MemberCareerLevelId_fk"].ToString(),
                    PackageRate = read["PackageRate"].ToString(),
                    MenteePackageDescription = read["MenteePackageDescription"].ToString(),
                    ValidityStart = Convert.ToDateTime(read["ValidityStart"]),
                    ValidityEnd = Convert.ToDateTime(read["ValidityEnd"])
                });
            }
            con.Close();
            return package;
        }
        public List<PackageList> GET_MenteePackageList(int MemberMenteeCareerLevelId)
        {
            List<PackageList> package = new List<PackageList>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MenteePackage.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MenteeCareerLevelId", MemberMenteeCareerLevelId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            package.Add(new PackageList
            {
                Value = "0",
                Text = "Select Package"
            });
            while (read.Read())
            {
                package.Add(new PackageList
                {
                    Value = read["MenteePackageId"].ToString(),
                    Text = read["MenteePackageName"].ToString(),
                    MenteePackageDescription = read["MenteePackageDescription"].ToString()
                });
            }
            con.Close();
            return package;
        }
        public List<SelectedList> Get_MenteeCareerLevelList(int MentorId)
        {
            List<SelectedList> levels = new List<SelectedList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberMenteeCareerLevel.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MentorId", MentorId);
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
                    Value = read["MemberCareerLevelId_fk"].ToString(),
                    Text = read["MemberCareerLevel"].ToString()
                });
            }
            con.Close();
            return levels;
        }
    }
}
