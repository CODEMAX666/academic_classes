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
    public class MemberExperienceDAL
    {
        string conString = DataUtil.connectionString;
        public void Insert_Update_MemberExperience(MemberExperience experience, int MemberId)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MemberExperience_Insert_Update.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (experience.SubCategoryList != null)
            {
                var table = new DataTable();
                table.Columns.Add("CategoryId", typeof(int));
                table.Columns.Add("SubCategoryId", typeof(int));
                foreach (var level in experience.SubCategoryList)
                {
                    var row = table.NewRow();
                    row["CategoryId"] = level.CategoryId;
                    row["SubCategoryId"] = level.SubCategoryId;

                    table.Rows.Add(row);
                }
                var parameter = cmd.CreateParameter();
                parameter.TypeName = "dbo.CategoriesTableType";
                parameter.Value = table;
                parameter.ParameterName = "@CTT";

                cmd.Parameters.Add(parameter);
            }

            cmd.Parameters.AddWithValue("@MemberExperienceId", experience.MemberExperienceId);
            cmd.Parameters.AddWithValue("@MemberID", MemberId);
            cmd.Parameters.AddWithValue("@Designation", experience.Designation);
            cmd.Parameters.AddWithValue("@Company", experience.Company);
            cmd.Parameters.AddWithValue("@YearFrom", experience.YearFrom);
            cmd.Parameters.AddWithValue("@YearTo", experience.YearTo);

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
        public List<MemberExperience> GetMemberExperience(int MemberId)
        {
            List<MemberExperience> experience = new List<MemberExperience>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberExperience.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberID", MemberId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                experience.Add(new MemberExperience
                {
                    MemberExperienceId = Convert.ToInt32(read["MemberExperienceId"]),
                    MemberCategory = read["CategoryId_fk"].ToString(),
                    MemberSubCategory = read["SubCategoryId"].ToString(),
                    Designation = read["Designation"].ToString(),
                    Company = read["Company"].ToString(),
                    YearFrom = read["YearFrom"].ToString(),
                    YearTo = read["YearTo"].ToString(),
                    CategoryName = read["CategoryName"].ToString(),
                    SubCategoryName = read["SubCategoryName"].ToString()
                });
            }
            con.Close();
            return experience;
        }
        public List<SelectedList> GetCategoryList(int MemberDomainId)
        {
            List<SelectedList> category = new List<SelectedList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_Category.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberDomainID", MemberDomainId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            category.Add(new SelectedList
            {
                Value = "0",
                Text = "Select category"
            });
            while (read.Read())
            {
                category.Add(new SelectedList
                {
                    Value = read["CategoryId"].ToString(),
                    Text = read["CategoryName"].ToString()
                });
            }
            con.Close();
            return category;
        }
        public List<SelectedList> GetSubCategoryList(int MemberCategoryId)
        {
            List<SelectedList> subcategory = new List<SelectedList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_SubCategory.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CategoryID", MemberCategoryId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            subcategory.Add(new SelectedList
            {
                Value = "0",
                Text = "Select SubCategory"
            });
            while (read.Read())
            {
                subcategory.Add(new SelectedList
                {
                    Value = read["SubCategoryId"].ToString(),
                    Text = read["SubCategoryName"].ToString()
                });
            }
            con.Close();
            return subcategory;
        }
    }
}
