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
    public class MemberInterestDAL
    {
        string conString = DataUtil.connectionString;

        public void Insert_Update_MemberInterest(List<MemberInterest> interest, int MemberId)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MemberInterest_Insert_Update.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;


            var table = new DataTable();
            table.Columns.Add("Category", typeof(int));
            table.Columns.Add("SubCategory", typeof(int));

            foreach (var level in interest)
            {
                var row = table.NewRow();
                row["Category"] = level.MemberCategory;
                row["SubCategory"] = level.MemberSubCategory;

                table.Rows.Add(row);
            }

            var parameter = cmd.CreateParameter();
            parameter.TypeName = "dbo.InterestTableType";
            parameter.Value = table;
            parameter.ParameterName = "@ITT";

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
        public List<MemberInterest> GetMemberInterest(int MemberId)
        {
            List<MemberInterest> interest = new List<MemberInterest>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberInterest.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberID", MemberId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                interest.Add(new MemberInterest
                {
                    MemberInterestId = Convert.ToInt32(read["MemberInterestId"]),
                    MemberCategory = read["CategoryId_fk"].ToString(),
                    MemberSubCategory = read["SubCategoryId_fk"].ToString(),
                    CategoryName = read["CategoryName"].ToString(),
                    SubCategoryName = read["SubCategoryName"].ToString()
                });
            }
            con.Close();
            return interest;
        }


    }
}
