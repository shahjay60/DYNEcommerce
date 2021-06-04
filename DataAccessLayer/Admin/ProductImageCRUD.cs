using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DataAccessLayer.Admin
{
    public class ProductImageCRUD
    {
        public static void AddToProductImage(ProductImageDomain mProductImage)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);

            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_ProductImage", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Insert");

            cmd.Parameters.AddWithValue("@Pid", mProductImage.Pid);
            cmd.Parameters.AddWithValue("@Viewon", mProductImage.Viewon);
            cmd.Parameters.AddWithValue("@Image", mProductImage.Image);
          

            cmd.ExecuteNonQuery();
            sqlconn.Close();


        }
        public static List<ProductImageDomain> GetProductImage(int Id)
        {
            List<ProductImageDomain> grp = new List<ProductImageDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_ProductImage", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetProductImage");
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Pid", "");
            cmd.Parameters.AddWithValue("@Viewon", "");
            cmd.Parameters.AddWithValue("@Image", "");
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new ProductImageDomain
                    {
                        Id = Convert.ToInt16(dr["Id"]),
                        Pid = dr["Pid"].ToString(),
                        Viewon = dr["Viewon"].ToString(),
                        Image = dr["Image"].ToString(),
                        ProductName = dr["ITEM_DESC"].ToString()
                    });

            }
            return grp;
        }
        public static List<ProductImageDomain> GetProductImageAll()
        {
            List<ProductImageDomain> grp = new List<ProductImageDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetProductImageAll", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("@mode", "GetCategoryImageAll");

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new ProductImageDomain
                    {

                        Id = Convert.ToInt16(dr["Id"]),
                        Pid = dr["Pid"].ToString(),
                        Viewon = dr["Viewon"].ToString(),
                        Image = dr["Image"].ToString(),
                        ProductName = dr["ITEM_DESC"].ToString()
                    });

            }
            return grp;
        }

        public static bool DeleteProductImage(int Id)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("Get_ProductImage", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteProductImage");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Pid", "");
                cmd.Parameters.AddWithValue("@Viewon", "");
                cmd.Parameters.AddWithValue("@Image", "");
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

        public static bool UpdateProductImage(ProductImageDomain smodel)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateProductImage", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", smodel.Id);
            cmd.Parameters.AddWithValue("@Viewon", smodel.Viewon);
            cmd.Parameters.AddWithValue("@Image", smodel.Image);

            sqlconn.Open();
            int i = cmd.ExecuteNonQuery();
            sqlconn.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}
