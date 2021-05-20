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
   public class CustmorWishlistCRUD
    {
        public static void AddToWishlist(CustomerWishlistDomain mCustomerwish)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;


            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_wishlist", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Insert");

            cmd.Parameters.AddWithValue("@CustomerId", mCustomerwish.CustomerId);
            cmd.Parameters.AddWithValue("@ProductId", mCustomerwish.ProductId);
            cmd.Parameters.AddWithValue("@CreatedDatetime", mCustomerwish.CreatedDateTime);         
            cmd.Parameters.AddWithValue("@Amount", mCustomerwish.Amount);

            cmd.ExecuteNonQuery();
            sqlconn.Close();


        }

        public static List<CustomerWishlistDomain> GetWishlistByCustomerId(int CustomerId)
        {
            List<CustomerWishlistDomain> grp = new List<CustomerWishlistDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_CustomrWishlist", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetWishlistByCustID");
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
                    new CustomerWishlistDomain
                    {

                        Id=Convert.ToInt16(dr["Id"]),
                        CustomerId = Convert.ToInt16(dr["CustomerId"]),
                        ProductId = dr["ProductId"].ToString(),
                        CreatedDateTime = Convert.ToDateTime(dr["CreatedDateTime"]),
                        ITEM_DESC = dr["ITEM_DESC"].ToString(),
                        Amount = Convert.ToDecimal(dr["Amount"])
                    });

            }
            return grp;
        }

        public static List<CustomerWishlistDomain> GetWishlistByWishlistId(int Id)
        {
            List<CustomerWishlistDomain> grp = new List<CustomerWishlistDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_CustomrWishlist", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetWishlistByID");
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@CustomerId", "");
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new CustomerWishlistDomain
                    {

                        Id = Convert.ToInt16(dr["Id"]),
                        CustomerId = Convert.ToInt16(dr["CustomerId"]),
                        ProductId = dr["ProductId"].ToString(),
                        CreatedDateTime = Convert.ToDateTime(dr["CreatedDateTime"]),
                        ITEM_DESC = dr["ITEM_DESC"].ToString(),
                        Amount = Convert.ToDecimal(dr["Amount"])
                    });

            }
            return grp;
        }

        public static bool DeleteWhishListItem(int wishListId)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("[Get_CustomrWishlist]", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteWishList");
                cmd.Parameters.AddWithValue("@Id", wishListId);
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
                throw;
            }
            return result;


        }

    }
}
