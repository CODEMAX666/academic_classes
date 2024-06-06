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
  public  class SearchMentoreDropDown
    {
        string conString = DataUtil.connectionString;
        public List<SelectedList1> GetCareerLevelList()
        {
            List<SelectedList1> levels = new List<SelectedList1>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberCareerLevel.ToString(), con);
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
                levels.Add(new SelectedList1
                {
                    Value = Convert.ToInt32(read["MemberCareerLevelId"].ToString()),
                    Text = read["MemberCareerLevel"].ToString()
                });
            }
            con.Close();
            return levels;
        }


        public List<SelectedListDomain1> GetDomainList(int cid)
        {
            List<SelectedListDomain1> domain = new List<SelectedListDomain1>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberDomain.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MembercareerlevelId",cid);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                
                while (read.Read())
                {
                    domain.Add(new SelectedListDomain1
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




        public List<SelectedListCategory1> GetCategoryList(int did)
        {
            List<SelectedListCategory1> categories = new List<SelectedListCategory1>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_Category.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberDomainID", did);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            /* categories.Add(new SelectedListCategory1
             {
                 Value = "0",
                 Text = "Select Category"
             });*/
            if (read.HasRows)
            {
                while (read.Read())
                {
                    categories.Add(new SelectedListCategory1
                    {
                        Value = read["CategoryId"].ToString(),
                        Text = read["CategoryName"].ToString()
                    });
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            con.Close();
            return categories;
        }



        public List<SelectedListSubCategory1> GetSubCategoryList(int cid)
        {
            List<SelectedListSubCategory1> subcategory = new List<SelectedListSubCategory1>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_SubCategory.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CategoryID", cid);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
              /*  subcategory.Add(new SelectedListSubCategory1
                {
                    Value = "0",
                    Text = "Select Sub category",
                    CategoryId = 0
                });*/
                while (read.Read())
                {
                    subcategory.Add(new SelectedListSubCategory1
                    {
                        Value = read["SubCategoryId"].ToString(),
                        Text = read["SubCategoryName"].ToString(),
                        //CategoryId = 1
                        //Convert.ToInt32(read["MembercareerlevelId_FK"])
                    });
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            con.Close();
            return subcategory;
        }




    }
}
