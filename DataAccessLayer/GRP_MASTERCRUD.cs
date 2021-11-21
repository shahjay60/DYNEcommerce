using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class GRP_MASTERCRUD
    {
        public static TMATS00012020Entities objEntity = new TMATS00012020Entities();

        public static List<GRP_MASTERDomain> GetAllMenu()
        {
            List<GRP_MASTERDomain> grp = new List<GRP_MASTERDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_grp_master", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Select");
            cmd.Parameters.AddWithValue("@GRP_CD", "");
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                string primaryCat = dr["GRP_NAME"].ToString().Trim();
                if (primaryCat == "Primary")
                    continue;
                grp.Add(
                    new GRP_MASTERDomain
                    {
                        GRP_CD = dr["GRP_CD"].ToString(),
                        GRP_NAME = dr["GRP_NAME"].ToString(),
                        FOR_GRP_CD = dr["FOR_GRP_CD"].ToString(),
                        LEVEL_TEXT = dr["LEVEL_TEXT"].ToString(),
                        GROUP_YN = dr["GROUP_YN"].ToString(),
                        Isonhomepage = dr["Isonhomepage"] == DBNull.Value ? false : Convert.ToBoolean(dr["Isonhomepage"]),
                        Isonmenu = dr["Isonmenu"] == DBNull.Value ? false : Convert.ToBoolean(dr["Isonmenu"]),
                    });

            }
            return grp;
        }

        public static bool UpdateCategoryByBrandId(BrandCategoryDomain model)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateCategoryByBrandId", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@brandId", model.brandId);
            cmd.Parameters.AddWithValue("@catId", model.GRP_CD);

            sqlconn.Open();
            int i = cmd.ExecuteNonQuery();
            sqlconn.Close();

            if (i >= 1)
                return true;
            else
                return false;
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

        public static List<GRP_MASTERDomain> GetCategoryByBrandId(int BrandId)
        {
            List<GRP_MASTERDomain> grp = new List<GRP_MASTERDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetCategoryByBrandId", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@brandId", BrandId);
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
                        GROUP_YN = dr["GROUP_YN"].ToString(),
                        BrandName = dr["BrandName"].ToString(),
                        BrandId = dr["BrandId"].ToString(),
                        ImageName = dr["ImageName"].ToString()

                    });

            }
            return grp;
        }

        public static List<GRP_MASTERDomain> GetAllCategory()
        {
            List<GRP_MASTERDomain> grp = new List<GRP_MASTERDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetAllCategory", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                    GRP_CD = dr["GRP_CD"].ToString().Trim(),
                    GRP_NAME = dr["GRP_NAME"].ToString(),
                    GRP_SNAME = dr["GRP_SNAME"].ToString(),
                    FOR_GRP_CD = dr["FOR_GRP_CD"].ToString().Trim(),
                    LEVEL_TEXT = dr["LEVEL_TEXT"].ToString(),
                    GROUP_YN = dr["GROUP_YN"].ToString(),
                    BrandName = dr["BrandName"].ToString(),
                    BrandId = dr["BrandId"].ToString(),
                    Isonhomepage = dr["Isonhomepage"] == DBNull.Value ? false : Convert.ToBoolean(dr["Isonhomepage"]),
                    Isonmenu = dr["Isonmenu"] == DBNull.Value ? false : Convert.ToBoolean(dr["Isonmenu"]),
                    ImageName = dr["ImageName"].ToString()

                });
            }
            return grp;
        }

        public static List<GRP_MASTERDomain> GetCategories()
        {
            List<GRP_MASTERDomain> grp = new List<GRP_MASTERDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetAllCategory", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                    GRP_CD = dr["GRP_CD"].ToString().Trim(),
                    GRP_NAME = dr["GRP_NAME"].ToString(),
                    GRP_SNAME = dr["GRP_SNAME"].ToString(),
                    FOR_GRP_CD = dr["FOR_GRP_CD"].ToString().Trim(),
                    LEVEL_TEXT = dr["LEVEL_TEXT"].ToString(),
                    GROUP_YN = dr["GROUP_YN"].ToString(),
                    BrandName = dr["BrandName"].ToString(),
                    BrandId = dr["BrandId"].ToString(),
                    Isonhomepage = dr["Isonhomepage"] == DBNull.Value ? false : Convert.ToBoolean(dr["Isonhomepage"]),
                    Isonmenu = dr["Isonmenu"] == DBNull.Value ? false : Convert.ToBoolean(dr["Isonmenu"]),
                    ImageName = dr["ImageName"].ToString()

                });
            }
            return grp;
        }

        public static List<GRP_MASTERDomain> GetProductByCategory()
        {
            List<GRP_MASTERDomain> grp = new List<GRP_MASTERDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetProductByCategory", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                        GRP_CD = dr["CategoryId"].ToString().Trim(),
                        GRP_NAME = dr["GRP_NAME"].ToString(),
                        ProductName = dr["ITEM_DESC"].ToString(),
                        Pid = dr["ITEM_CD"].ToString(),
                        ProductImage = dr["Image"].ToString(),
                        ProductPrice = dr["sale_Price"].ToString(),
                    });

            }
            return grp;
        }

        public static void AddToCategoryMaster(AdminCategory model)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);

            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("AddCategory", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Insert");
            cmd.Parameters.AddWithValue("@GRP_CD", model.GRP_CD);
            cmd.Parameters.AddWithValue("@GRP_SNAME", model.GRP_SNAME);
            cmd.Parameters.AddWithValue("@GROUP_YN", model.GROUP_YN);
            cmd.Parameters.AddWithValue("@GRP_NAME", model.GRP_NAME);
            cmd.Parameters.AddWithValue("@LEVEL_TEXT", model.LEVEL_TEXT);
            cmd.Parameters.AddWithValue("@FOR_GRP_CD", model.FOR_GRP_CD);
            cmd.Parameters.AddWithValue("@BrandId", model.BrandId);
            cmd.Parameters.AddWithValue("@Isonmenu", model.Isonmenu);
            cmd.Parameters.AddWithValue("@Isonhomepage", model.Isonhomepage);

            cmd.ExecuteNonQuery();
            sqlconn.Close();

        }

        public static List<AdminCategory> GetCategoryById(string catId)
        {
            List<AdminCategory> grp = new List<AdminCategory>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetCategoryById", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GRP_CD", catId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new AdminCategory
                    {
                        GRP_CD = dr["GRP_CD"].ToString(),
                        GRP_NAME = dr["GRP_NAME"].ToString(),
                        GRP_SNAME = dr["GRP_SNAME"].ToString(),
                        FOR_GRP_CD = dr["FOR_GRP_CD"].ToString(),
                        LEVEL_TEXT = dr["LEVEL_TEXT"].ToString(),
                        GROUP_YN = dr["GROUP_YN"].ToString(),
                        BrandId = dr["BrandId"].ToString() == "" ? Convert.ToInt32("0") : Convert.ToInt32(dr["BrandId"].ToString()),
                        //Isonmenu = dr["Isonmenu"].ToString() == "" ? false : Convert.ToBoolean(dr["Isonmenu"].ToString()),
                        //Isonhomepage = dr["Isonhomepage"].ToString() == "" ? false : Convert.ToBoolean(dr["Isonhomepage"].ToString())
                        Isonmenu = Convert.ToBoolean(dr["Isonmenu"]),
                        Isonhomepage = Convert.ToBoolean(dr["Isonhomepage"])
                    });

            }
            return grp;
        }
        public static string GetLastCategoryID()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            string query = "select top(1)grp_Cd from GRP_MASTER order by grp_Cd desc";
            SqlCommand cmd = new SqlCommand(query, sqlconn);
            cmd.CommandType = System.Data.CommandType.Text;

            string lastId = Convert.ToString(cmd.ExecuteScalar());
            return lastId;
        }

        public static bool UpdateCategoryMaster(AdminCategory model)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("AddCategory", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Update");
            cmd.Parameters.AddWithValue("@GRP_CD", model.GRP_CD);
            cmd.Parameters.AddWithValue("@GRP_SNAME", model.GRP_SNAME);
            cmd.Parameters.AddWithValue("@GRP_NAME", model.GRP_NAME);
            cmd.Parameters.AddWithValue("@FOR_GRP_CD", model.FOR_GRP_CD);
            cmd.Parameters.AddWithValue("@LEVEL_TEXT", model.LEVEL_TEXT);
            cmd.Parameters.AddWithValue("@BrandId", model.BrandId);
            cmd.Parameters.AddWithValue("@Isonmenu", model.Isonmenu);
            cmd.Parameters.AddWithValue("@Isonhomepage", model.Isonhomepage);
            cmd.Parameters.AddWithValue("@GROUP_YN", model.GROUP_YN);

            sqlconn.Open();
            int i = cmd.ExecuteNonQuery();
            sqlconn.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public static bool Delete(string catId)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("DeleteCategory", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GRP_CD", catId);

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

        public static bool DeleteBrandCategorymasterById(string catId)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("deleteBrandFromCategory", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@grp_cd", catId);

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
