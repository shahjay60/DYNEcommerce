using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class CustomerCartCRUD
    {

        public static int AddToCart(CustomerCartDomain mCustomerCart)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            SqlCommand cmd = new SqlCommand("sp_cart", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Insert");

            cmd.Parameters.AddWithValue("@CustomerId", mCustomerCart.CustomerId);
            cmd.Parameters.AddWithValue("@ProductId", mCustomerCart.ProductId);
            cmd.Parameters.AddWithValue("@CreatedDatetime", mCustomerCart.CreatedDatetime);
            cmd.Parameters.AddWithValue("@IsDeleted", mCustomerCart.IsDeleted);
            cmd.Parameters.AddWithValue("@Quantity", mCustomerCart.Quantity);
            cmd.Parameters.AddWithValue("@Amount", mCustomerCart.Amount);
            cmd.Parameters.AddWithValue("@IsPlace", mCustomerCart.IsPlace);

            if (sqlconn.State == ConnectionState.Closed)
            {
                sqlconn.Open();
            }
            //cmd.ExecuteNonQuery();
            int modified = (int)cmd.ExecuteScalar();
            sqlconn.Close();
            return modified;
        }

        public static void AddToCartAttribute(CartAttribute mCartAttribute)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            SqlCommand cmd = new SqlCommand("[sp_AddcartAttribute]", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CartId", mCartAttribute.cartId);
            cmd.Parameters.AddWithValue("@AttributeValueID", mCartAttribute.AttributeValueID);

            if (sqlconn.State == ConnectionState.Closed)
            {
                sqlconn.Open();
            }
            cmd.ExecuteNonQuery();
            sqlconn.Close();
        }

        public static List<CustomerCartDomain> GetCartByCustomerId(int CustomerId)
        {
            List<CustomerCartDomain> grp = new List<CustomerCartDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_CustomrCart", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetcartByCustID");
            cmd.Parameters.AddWithValue("@Id", "");
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {

                grp.Add(
                    new CustomerCartDomain
                    {
                        Id = Convert.ToInt16(dr["Id"]),
                        CustomerId = dr["CustomerId"].ToString(),
                        ProductId = dr["ProductId"].ToString(),
                        CreatedDatetime = Convert.ToDateTime(dr["CreatedDatetime"]),
                        //IsDeleted = dr["IsDeleted"].ToString(),
                        Quantity = Convert.ToInt16(dr["Quantity"]),
                        Amount = Convert.ToDecimal(dr["Amount"]),
                        IsPlace = Convert.ToBoolean((dr["IsPlaced"])),
                        ITEM_DESC = dr["ITEM_DESC"].ToString(),
                        AttributeValue = dr["AttributeValueId"].ToString(),
                        //AttributeType = dr["AttributeName"].ToString(),
                        Image = dr["Image"].ToString()

                    });

            }
            return grp;
        }

        public static List<CustomerCartDomain> GetCartByCartId(int cartId)
        {
            List<CustomerCartDomain> grp = new List<CustomerCartDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_CustomrCart", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetcartByCartID");
            cmd.Parameters.AddWithValue("@Id", cartId);
            cmd.Parameters.AddWithValue("@CustomerId", "");
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new CustomerCartDomain
                    {
                        Id = Convert.ToInt16(dr["Id"]),
                        CustomerId = dr["CustomerId"].ToString(),
                        ProductId = dr["ProductId"].ToString(),
                        CreatedDatetime = Convert.ToDateTime(dr["CreatedDatetime"]),
                        //IsDeleted = dr["IsDeleted"].ToString(),
                        Quantity = Convert.ToInt16(dr["Quantity"]),
                        Amount = Convert.ToDecimal(dr["Amount"]),
                        IsPlace = Convert.ToBoolean(dr["IsPlaced"]),
                        ITEM_DESC = dr["ITEM_DESC"].ToString()

                    });

            }
            return grp;
        }

        public static bool DeleteCartItem(int cartId)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("Get_CustomrCart", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteCart");
                cmd.Parameters.AddWithValue("@Id", cartId);
                cmd.Parameters.AddWithValue("@CustomerId", "");
                SqlDataAdapter sd = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                sqlconn.Open();
                sd.Fill(dt);
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public static List<CheckoutDomain> GetCartByALLCustomerId(int CustomerId)
        {
            List<CheckoutDomain> mcheckout = new List<CheckoutDomain>();

            // CheckoutDOMAIN mobj = new CheckoutDOMAIN();

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("sp_Checkout", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "getcheckout");
            // cmd.Parameters.AddWithValue("@Id", "");
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            if (sqlconn.State == ConnectionState.Closed)
            {
                sqlconn.Open();
            }
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                mcheckout.Add(
                    new CheckoutDomain
                    {
                        Id = Convert.ToInt16(dr["Id"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Country = dr["Country"].ToString(),
                        State = dr["State"].ToString(),
                        City = dr["City"].ToString(),
                        Pincode = dr["Pincode"].ToString(),
                        ShippingAddress = dr["ShippingAddress"].ToString(),
                        BillingAddress = dr["BillingAddress"].ToString(),
                        ProductName = dr["ProductName"].ToString(),
                        cartId = Convert.ToInt16(dr["cartId"]),
                        ProductId = dr["ProductId"].ToString(),
                        Quantity = Convert.ToInt16(dr["Quantity"]),
                        Amount = Convert.ToDecimal(dr["Amount"])
                        //IsPlaced =  Convert.ToBoolean(dr["IsPlaced"])

                    });

            }
            return mcheckout;
        }

        public static bool UpdateDetails(int cartID, bool IsPlace)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateCustomerCart", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", cartID);

            //cmd.Parameters.AddWithValue("@CustomerId", smodel.CustomerId);
            //cmd.Parameters.AddWithValue("@ProductId", smodel.ProductId);
            //cmd.Parameters.AddWithValue("@CreatedDatetime", smodel.CreatedDatetime);
            //cmd.Parameters.AddWithValue("@IsDeleted", smodel.IsDeleted);
            //cmd.Parameters.AddWithValue("@Quantity", smodel.Quantity);
            //cmd.Parameters.AddWithValue("@Amount", smodel.Amount);
            cmd.Parameters.AddWithValue("@IsPlace", IsPlace);
            sqlconn.Open();
            int i = cmd.ExecuteNonQuery();
            sqlconn.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public static List<CustomerModel> GetPassWordByEmailId(string emailId)
        {
            List<CustomerModel> grp = new List<CustomerModel>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_CustomrCart", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetcartByCustID");
            cmd.Parameters.AddWithValue("@Id", "");
            cmd.Parameters.AddWithValue("@CustomerId", emailId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {

                grp.Add(
                    new CustomerModel
                    {
                        Id = Convert.ToInt16(dr["Id"]),
                        Password = dr["Password"].ToString(),
                    });

            }
            return grp;
        }


    }
}
