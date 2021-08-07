using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DataAccessLayer
{
    public class CustomerOrderCRUD
    {
        public static void AddToCustomerOrder(CustomerOrderDomain mCustomerOrder)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("sp_CustomerOrder", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "PostCustomerOrder");

            cmd.Parameters.AddWithValue("@CustomerId", mCustomerOrder.CustomerId);
            cmd.Parameters.AddWithValue("@FirstName", mCustomerOrder.FirstName);
            cmd.Parameters.AddWithValue("@LastName", mCustomerOrder.LastName);
            cmd.Parameters.AddWithValue("@OrderEmail", mCustomerOrder.OrderEmail);

            cmd.Parameters.AddWithValue("@OrderPhone", mCustomerOrder.OrderPhone);
            cmd.Parameters.AddWithValue("@OrderCounty", mCustomerOrder.OrderCounty);
            cmd.Parameters.AddWithValue("@OrderState", mCustomerOrder.OrderState);
            cmd.Parameters.AddWithValue("@OrderCity", mCustomerOrder.OrderCity);

            cmd.Parameters.AddWithValue("@OrderPostCode", mCustomerOrder.OrderPostCode);
            cmd.Parameters.AddWithValue("@ShippingAddress", mCustomerOrder.ShippingAddress);
            cmd.Parameters.AddWithValue("@OrderDate", mCustomerOrder.OrderDate);
            cmd.Parameters.AddWithValue("@PaymentType", mCustomerOrder.PaymentType);

            cmd.Parameters.AddWithValue("@TransactionId", mCustomerOrder.TransactionId);
            cmd.Parameters.AddWithValue("@OrderAmount", mCustomerOrder.OrderAmount);

            cmd.ExecuteNonQuery();
            sqlconn.Close();


        }

        public static List<CustomerOrderDomain> GetCustomerOrderByCustomerId(int CustomerId)
        {
            List<CustomerOrderDomain> grp = new List<CustomerOrderDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_CustomerOrder", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new CustomerOrderDomain
                    {

                        Id = Convert.ToInt16(dr["Id"]),
                        CustomerId = Convert.ToInt16(dr["CustomerId"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        OrderEmail = dr["OrderEmail"].ToString(),
                        OrderPhone = dr["OrderPhone"].ToString(),
                        OrderCounty = dr["OrderCounty"].ToString(),
                        OrderState = dr["OrderState"].ToString(),
                        OrderCity = dr["OrderCity"].ToString(),
                        OrderPostCode = dr["OrderPostCode"].ToString(),
                        ShippingAddress = dr["ShippingAddress"].ToString(),
                        OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                        PaymentType = dr["PaymentType"].ToString(),
                        TransactionId = dr["TransactionId"].ToString(),
                        OrderAmount = dr["OrderAmount"].ToString(),
                    });

            }
            return grp;
        }

    }
}
