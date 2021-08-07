using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ITMMASTCRUD
    {
        public static List<ITMMASTDomain> GetProductByCatId(string GRP_CD)
        {
            List<ITMMASTDomain> itmmast = new List<ITMMASTDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_ITMMAST", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "getbyCatid");
            cmd.Parameters.AddWithValue("@GRP_CD", GRP_CD);
            cmd.Parameters.AddWithValue("@ITEM_CD", "");

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                itmmast.Add(
                    new ITMMASTDomain
                    {
                        GRP_CD = dr["GRP_CD"].ToString(),
                        Item_CD = dr["Item_CD"].ToString(),
                        Item_ID = dr["Item_ID"].ToString(),
                        Item_Desc = dr["Item_Desc"].ToString(),
                        BrandId = dr["BrandId"].ToString(),
                        Sale_Price = Convert.ToDouble(dr["Sale_Price"]),
                        Product_Image = dr["Image"].ToString(),
                        viewon = dr["Viewon"].ToString(),
                        AttributeValue = dr["AttValue"].ToString()
                    });

            }
            return itmmast;
        }
        public static List<ITMMASTDomain> GetProductById(string ITEM_CD)
        {
            List<ITMMASTDomain> itmmast = new List<ITMMASTDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_ITMMAST", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "getbyid");
            cmd.Parameters.AddWithValue("@GRP_CD", "");
            cmd.Parameters.AddWithValue("@ITEM_CD", ITEM_CD);

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                itmmast.Add(
                    new ITMMASTDomain
                    {
                        GRP_CD = dr["GRP_CD"].ToString(),
                        Item_CD = dr["Item_CD"].ToString(),
                        Item_ID = dr["Item_ID"].ToString(),
                        Item_Desc = dr["Item_Desc"].ToString(),
                        Qty = dr["Quantity"].ToString(),
                        Sale_Price = Convert.ToDouble(dr["Sale_Price"]),
                        Isnewarriaval = dr["Isnewarriaval"] == DBNull.Value ? false : Convert.ToBoolean(dr["Isnewarriaval"]),
                        Bar_Code = Convert.ToString(dr["Bar_Code"]),
                        DetailDesc = Convert.ToString(dr["Remark_Memo_Itmmast"]),
                        Offer_Price = dr["offer_price"] == DBNull.Value ? 0 : Convert.ToDouble(dr["offer_price"]),
                    });

            }
            return itmmast;
        }
        public static List<ITMMASTDomain> GetAllProducts()
        {
            List<ITMMASTDomain> itmmast = new List<ITMMASTDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_ITMMAST", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Select");
            cmd.Parameters.AddWithValue("@GRP_CD", "");
            cmd.Parameters.AddWithValue("@ITEM_CD", "");

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                itmmast.Add(
                    new ITMMASTDomain
                    {
                        GRP_CD = dr["GRP_CD"].ToString(),
                        Item_CD = dr["Item_CD"].ToString(),
                        Item_ID = dr["Item_ID"].ToString(),
                        Item_Desc = dr["Item_Desc"].ToString(),
                        Sale_Price = Convert.ToDouble(dr["Sale_Price"]),
                        Offer_Price = dr["offer_price"] == DBNull.Value ? 0 : Convert.ToDouble(dr["offer_price"]),
                        Bar_Code = dr["Bar_Code"].ToString(),
                        GRP_NAME = dr["GRP_NAME"].ToString(),
                        Isnewarriaval = dr["Isnewarriaval"] == DBNull.Value ? false : Convert.ToBoolean(dr["Isnewarriaval"]),
                    });

            }
            return itmmast;
        }
        public static List<GRP_MASTERDomain> GetMenuById(string GRP_CDs)
        {
            List<GRP_MASTERDomain> grp = new List<GRP_MASTERDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_grp_master", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "getbyid");
            cmd.Parameters.AddWithValue("@GRP_CD", GRP_CDs);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new GRP_MASTERDomain
                    {
                        GRP_CD = dr["GRP_CD"].ToString(),
                        GRP_NAME = dr["GRP_NAME"].ToString(),
                        FOR_GRP_CD = dr["FOR_GRP_CD"].ToString(),
                        LEVEL_TEXT = dr["LEVEL_TEXT"].ToString(),
                        GROUP_YN = dr["GROUP_YN"].ToString()
                    });

            }
            return grp;
        }
        public static string GetLastProductID()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            string query = "select top(1) item_cd from itmmast where ITEM_CD like'%Z0%' order by item_cd desc";
            SqlCommand cmd = new SqlCommand(query, sqlconn);
            cmd.CommandType = System.Data.CommandType.Text;

            string lastId = Convert.ToString(cmd.ExecuteScalar());
            return lastId;
        }
        public static void AddProduct(ITMMASTDomain model)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("AddProduct", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_CD", model.Item_CD);
            cmd.Parameters.AddWithValue("@Item_Desc", model.Item_Desc);
            cmd.Parameters.AddWithValue("@Sale_Price", model.Sale_Price);
            cmd.Parameters.AddWithValue("@offer_price", model.Offer_Price);
            cmd.Parameters.AddWithValue("@BarCode", model.Bar_Code);
            cmd.Parameters.AddWithValue("@GRP_CD", model.GRP_CD);
            cmd.Parameters.AddWithValue("@Isnewarriaval", model.Isnewarriaval);
            cmd.Parameters.AddWithValue("@Remark_Memo_Itmmast", model.DetailDesc);

            cmd.ExecuteNonQuery();
            sqlconn.Close();
        }


        public static bool UpdateProduct(ITMMASTDomain model)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateProduct", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_CD", model.Item_CD);
            cmd.Parameters.AddWithValue("@Item_Desc", model.Item_Desc);
            cmd.Parameters.AddWithValue("@Sale_Price", model.Sale_Price);
            cmd.Parameters.AddWithValue("@offer_price", model.Offer_Price);
            cmd.Parameters.AddWithValue("@BarCode", model.Bar_Code);
            cmd.Parameters.AddWithValue("@GRP_CD", model.GRP_CD);
            cmd.Parameters.AddWithValue("@Isnewarriaval", model.Isnewarriaval);
            cmd.Parameters.AddWithValue("@Remark_Memo_Itmmast", model.DetailDesc);
            sqlconn.Open();
            int i = cmd.ExecuteNonQuery();
            sqlconn.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public static bool Delete(string pId)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("DeleteProduct", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Item_CD", pId);

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

        public static object GetColorWiseProductImage(string productId, string selectedColor)
        {
            List<ProductImageDomain> ProdImages = new List<ProductImageDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetColorWiseProductImage", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@productId", productId);
            cmd.Parameters.AddWithValue("@attValue", selectedColor);

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                ProdImages.Add(
                    new ProductImageDomain
                    {
                        Image = dr["Image"].ToString(),
                        Id = Convert.ToInt32(dr["Id"].ToString()),
                    });

            }
            return ProdImages;
        }

        public static List<search> GETPRODUCTNAME_ID(string pName)
        {
            List<search> itmmast = new List<search>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GETPRODUCTNAME_ID", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                itmmast.Add(
                    new search
                    {
                        Id = dr["Item_CD"].ToString(),
                        Name = dr["Item_Desc"].ToString(),
                    });

            }
            return itmmast;
        }

    }
}

public class search
{
    public string Id { get; set; }
    public string Name { get; set; }
}