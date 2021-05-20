using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

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
                        GROUP_YN = dr["GROUP_YN"].ToString()
                    });

            }
            return grp;
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
                        GRP_CD = dr["GRP_CD"].ToString(),
                        GRP_NAME = dr["GRP_NAME"].ToString(),
                        FOR_GRP_CD = dr["FOR_GRP_CD"].ToString(),
                        LEVEL_TEXT = dr["LEVEL_TEXT"].ToString(),
                        GROUP_YN = dr["GROUP_YN"].ToString(),
                        BrandName = dr["BrandName"].ToString(),
                        BrandId = dr["BrandId"].ToString(),

                    });

            }
            return grp;
        }

    }
}
