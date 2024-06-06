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
    public class MemberEducationDAL
    {
        string conString = DataUtil.connectionString;
        public void Insert_Update_MemberEducation(List<MemberEducation> education, int MemberId)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MemberEducation_Insert_Update.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;


            var table = new DataTable();
            table.Columns.Add("DegreeLevelName", typeof(int));
            table.Columns.Add("DegreeTitleName", typeof(int));
            table.Columns.Add("Percentage", typeof(float));
            table.Columns.Add("YearFrom", typeof(string));
            table.Columns.Add("YearTo", typeof(string));
            table.Columns.Add("Institute", typeof(string));
            foreach (var level in education)
            {
                var row = table.NewRow();
                row["DegreeLevelName"] = level.DegreeLevelName;
                row["DegreeTitleName"] = level.DegreeTitleName;
                row["Percentage"] = level.Percentage;
                row["YearFrom"] = level.YearFrom;
                row["YearTo"] = level.YearTo;
                row["Institute"] = level.Institute;

                table.Rows.Add(row);
            }

            var parameter = cmd.CreateParameter();
            parameter.TypeName = "dbo.EducationTableType";
            parameter.Value = table;
            parameter.ParameterName = "@ETT";

            cmd.Parameters.Add(parameter);
            cmd.Parameters.AddWithValue("@MemberID", MemberId);
            // cmd.Parameters.AddWithValue("@MTT", table);

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
        public List<MemberEducation> GetMemberEducation(int MemberId)
        {
            List<MemberEducation> education = new List<MemberEducation>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberEducation.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberID", MemberId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                education.Add(new MemberEducation
                {
                    MemberEducationId = Convert.ToInt32(read["MemberEducationId"]),
                    DegreeLevelName = read["DegreeLevelId_fk"].ToString(),
                    DegreeTitleName = read["DegreeTitleId_fk"].ToString(),
                    Percentage = Convert.ToInt64(read["Percentage"]),
                    YearFrom = read["YearFrom"].ToString(),
                    YearTo = read["YearTo"].ToString(),
                    Institute = read["Institute"].ToString(),
                    DegreeLevel = read["DegreeLevelName"].ToString(),
                    DegreeTitle = read["DegreeTitleName"].ToString(),
                });
            }
            con.Close();
            return education;
        }
        public List<SelectedList> GetDegreeLevelList()
        {
            List<SelectedList> degreeLevel = new List<SelectedList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_DegreeLevel.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            degreeLevel.Add(new SelectedList
            {
                Value = "0",
                Text = "Select Degree Level"
            });
            while (read.Read())
            {
                degreeLevel.Add(new SelectedList
                {
                    Value = read["DegreeLevelId"].ToString(),
                    Text = read["DegreeLevelName"].ToString()
                });
            }
            con.Close();
            return degreeLevel;
        }
        public List<SelectedList> GetDegreeTitleList(int DegreeLevelId)
        {
            List<SelectedList> degreeTitle = new List<SelectedList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_DegreeTitle.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DegreeLevelId_fk", DegreeLevelId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            degreeTitle.Add(new SelectedList
            {
                Value = "0",
                Text = "Select Degree Title"
            });
            while (read.Read())
            {
                degreeTitle.Add(new SelectedList
                {
                    Value = read["DegreeTitleId"].ToString(),
                    Text = read["DegreeTitleName"].ToString()
                });
            }
            con.Close();
            return degreeTitle;
        }
    }
}
