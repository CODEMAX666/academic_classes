
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Mentor.BE;


namespace Mentor.DAL
{
    public class MenteeMentorFollowshipDAL
    {
        string conString = DataUtil.connectionString;
        FollowUnfollowBE FUM = new FollowUnfollowBE();
        
        public void Send_Request_FollowShip(FollowUnfollowBE FUM)
        {

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MenteeMentorFollowship.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MenteeId", FUM.MenteeID);
            cmd.Parameters.AddWithValue("@MentorId", FUM.MentorID);
           


            var ResponseId = cmd.Parameters.Add("@Message", SqlDbType.VarChar);
            ResponseId.Direction = ParameterDirection.ReturnValue;
            var Response = cmd.Parameters.Add("@status", SqlDbType.Int);
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

    }
}
