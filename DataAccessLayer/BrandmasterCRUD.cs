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
    public class BrandmasterCRUD
    {
        public static void AddToBrandMaster(BrandmasterDomain mBrandMaster)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("sp_Brandmaster", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "PostBrandmaster");

         
            cmd.Parameters.AddWithValue("@GRP_CD", mBrandMaster.GRP_CD);
            cmd.Parameters.AddWithValue("@BrandName", mBrandMaster.BrandName);
            

            cmd.ExecuteNonQuery();


        }
        public static List<BrandmasterDomain> GetBrandMaster(int BrandId)
        {
            List<BrandmasterDomain> grp = new List<BrandmasterDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_Brandmaster", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetBrandmaster");
       
            cmd.Parameters.AddWithValue("@BrandId", BrandId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new BrandmasterDomain
                    {

                        BrandId = Convert.ToInt16(dr["BrandId"]),

                        GRP_CD = dr["GRP_CD"].ToString(),
                        BrandName = dr["BrandName"].ToString(),
                    });

            }
            return grp;
        }

        public static List<BrandmasterDomain> GetBrandByCatId(string CatId)
        {
            List<BrandmasterDomain> grp = new List<BrandmasterDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_BrandByCatId", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetBrandByCatId");

            cmd.Parameters.AddWithValue("@GRP_CD", CatId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new BrandmasterDomain
                    {

                        BrandId = Convert.ToInt16(dr["BrandId"]),

                        GRP_CD = dr["GRP_CD"].ToString(),
                        BrandName = dr["BrandName"].ToString(),
                    });
            }
            return grp;
        }

        public static List<BrandmasterDomain> GetBrandMaster()
        {
            List<BrandmasterDomain> grp = new List<BrandmasterDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetBrandmasterAll", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new BrandmasterDomain
                    {

                        BrandId = Convert.ToInt16(dr["BrandId"]),

                        GRP_CD = dr["GRP_CD"].ToString(),
                        BrandName = dr["BrandName"].ToString(),
                        IsOnHomePage =Convert.ToBoolean(dr["IsHomePage"]),
                        ISOnWeb = Convert.ToBoolean(dr["IsOnWeb"]),
                    });
            }
            return grp;
        }


    }
}
