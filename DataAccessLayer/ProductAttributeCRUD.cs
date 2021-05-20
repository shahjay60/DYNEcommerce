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
   public class ProductAttributeCRUD
    {
        public static void AddProductAttribute(Product_AttributeDomain mProductAttribute)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("sp_ProductAttribute", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "ProductAttribute");

            cmd.Parameters.AddWithValue("@ITEM_CD", mProductAttribute.ITEM_CD);
            cmd.Parameters.AddWithValue("@AttributeId", mProductAttribute.AttributeId);
            cmd.Parameters.AddWithValue("@AttributeValue", mProductAttribute.AttributeValue);
            cmd.ExecuteNonQuery();

        }

        public static List<Product_AttributeDomain> GetProductAttribute(int PaId)
        {
            List<Product_AttributeDomain> grp = new List<Product_AttributeDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_ProductAttribute", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetProductAttribute");

            cmd.Parameters.AddWithValue("@PaId", PaId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new Product_AttributeDomain
                    {

                        PaId = Convert.ToInt16(dr["PaId"]),

                        ITEM_CD = dr["ITEM_CD"].ToString(),

                        AttributeId = Convert.ToInt16(dr["AttributeId"]),

                        AttributeValue = dr["AttributeValue"].ToString(),
                    });

            }
            return grp;
        }
    }
}
