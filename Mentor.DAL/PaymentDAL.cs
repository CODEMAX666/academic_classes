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
    public class  PaymentDAL
    {
        public void addMemberPayment(PaymentBE m)
        {

            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MemberTransaction_Insert.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberId", m.MemberId);
            cmd.Parameters.AddWithValue("@PaymentMethodId", m.PayM);
            cmd.Parameters.AddWithValue("@TransactionId", m.Tran);
             cmd.Parameters.AddWithValue("@TotalAmount", m.amount);
            //cmd.Parameters.AddWithValue("@PayementTypeId", m.PaymentTypeID);
            //var ResponseId = cmd.Parameters.Add("@ResponseId", SqlDbType.Int);
            //ResponseId.Direction = ParameterDirection.ReturnValue;
            //var Response = cmd.Parameters.Add("@Response", SqlDbType.VarChar);
            //Response.Direction = ParameterDirection.ReturnValue;

            try
            {
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                //cmd.ExecuteNonQuery();
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
