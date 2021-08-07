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
    public class ProductAttributeCRUD
    {
        public static void AddProductAttribute(Product_Attribute mProductAttribute)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_ProductAttribute", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Insert");
            cmd.Parameters.AddWithValue("@ITEM_CD", mProductAttribute.ITEM_CD);
            cmd.Parameters.AddWithValue("@AttributeId", mProductAttribute.AttributeId);
            cmd.Parameters.AddWithValue("@AttributeValue", mProductAttribute.AttributeValue);
            cmd.ExecuteNonQuery();
            sqlconn.Close();

        }
        public static List<Product_Attribute> GetProductAttribute(int PaId)
        {
            List<Product_Attribute> grp = new List<Product_Attribute>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_ProductAttribute", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "SelectById");
            cmd.Parameters.AddWithValue("@PaId", PaId);
            cmd.Parameters.AddWithValue("@AttributeId", "");
            cmd.Parameters.AddWithValue("@ITEM_CD", "");
            cmd.Parameters.AddWithValue("@AttributeValue", "");
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new Product_Attribute
                    {
                        //PaId = Convert.ToInt16(dr["PaId"]),
                        ITEM_CD = (dr["ITEM_CD"]).ToString(),
                        AttributeId = Convert.ToInt16(dr["AttributeId"]),
                        //AttributeValue = dr["AttributeValue"].ToString()
                        

                    }); ;

            }
            return grp;
        }
        public static List<Product_Attribute> GetProduct_AttributeAll()
        {
            List<Product_Attribute> grp = new List<Product_Attribute>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetProductAttributeAll", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
           
           
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new Product_Attribute
                    {

                        PaId = Convert.ToInt16(dr["PaId"]),
                        ITEM_CD = (dr["ITEM_CD"]).ToString(),
                        AttributeId = Convert.ToInt16(dr["AttributeId"]),
                        ProductName= (dr["ITEM_DESC"]).ToString(),
                        AttributeValue = dr["AttributeValue"].ToString(),
                        AttributeName = dr["AttributeName"].ToString()
                    });

            }
            return grp;
        }
        public static bool DeleteProduct_Attribute(int PaId)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("DeleteProductAttributeById", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@mode", "DeleteById");
                cmd.Parameters.AddWithValue("@PaId", PaId);
               // cmd.Parameters.AddWithValue("@AttributeId ", "");
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
        public static bool UpdateProduct_Attribute(Product_Attribute smodel)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateProductAttribute", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PaId", smodel.PaId);
            cmd.Parameters.AddWithValue("@ITEM_CD", smodel.ITEM_CD);
            cmd.Parameters.AddWithValue("@AttributeId", smodel.AttributeId);
            cmd.Parameters.AddWithValue("@AttributeValue", smodel.AttributeValue);
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
