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
    public class PaySumDAL
    {

        public List<MyPaymentsummary> MyPaymentSummaries(int memID)
        {
            List<MyPaymentsummary> paymentsummaries = new List<MyPaymentsummary>();
            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MemberTransaction.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@MemberId", memID);


            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    MyPaymentsummary paymentsummary = new MyPaymentsummary();
                    paymentsummary.PaymentMethod = read["PaymentMethodName"].ToString();
                    paymentsummary.TransactionID = Convert.ToInt32(read["MemberTransactionId"].ToString());
                    paymentsummary.Amount = Convert.ToInt32(read["TotalAmount"].ToString());
                    paymentsummary.Status = read["PaymentStatus"].ToString();
                    paymentsummary.PaymentType = read["PaymentTypeName"].ToString();
                    paymentsummaries.Add(paymentsummary);
  
                }
            }

            else
            {
                Console.WriteLine("No rows found");
            }
            con.Close();
            return paymentsummaries;
    
        }

    }
}



