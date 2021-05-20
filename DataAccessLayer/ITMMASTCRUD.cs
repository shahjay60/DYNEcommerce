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
                        Sale_Price = Convert.ToDouble( dr["Sale_Price"])
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

                        Sale_Price = Convert.ToDouble(dr["Sale_Price"])
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

    }
}
