using Mentor.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mentor.DAL
{
    public class AddExpDropDown
    {
        string conString = DataUtil.connectionString;
        public List<SelectedExpList> GetSelectedExpLists()
        {
            List<SelectedExpList> expLists = new List<SelectedExpList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_RequiredExperience.ToString(), con);
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
                expLists.Add(new SelectedExpList
                {
                    Value = Convert.ToInt32(read["RequiredExperienceId"].ToString()),
                    Text = read["RequiredExperience"].ToString()
                });
            }
            con.Close();
            return expLists;
        }
    }
}