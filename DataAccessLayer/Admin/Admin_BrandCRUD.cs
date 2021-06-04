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
    public class Admin_BrandCRUD
    {
        public static void AddToBrandMaster(BrandmasterDomain mBrandMaster)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);

            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_Brandmaster", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "PostBrandmaster");

            //cmd.Parameters.AddWithValue("@BrandId", mBrandMaster.BrandId);
            cmd.Parameters.AddWithValue("@BrandName", mBrandMaster.BrandName);
            cmd.Parameters.AddWithValue("@IsOnWeb", mBrandMaster.ISOnWeb);
            cmd.Parameters.AddWithValue("@IsHomePage", mBrandMaster.IsOnHomePage);

            cmd.ExecuteNonQuery();
            sqlconn.Close();


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
                        ISOnWeb = Convert.ToBoolean(dr["IsOnWeb"]),
                        IsOnHomePage = Convert.ToBoolean(dr["IsHomePage"]),
                    });

            }
            return grp;
        }
        public static List<BrandmasterDomain> GetBrandMasterAll(string mode)
        {
            List<BrandmasterDomain> grp = new List<BrandmasterDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetBrandmasterAll", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", mode);

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                if (mode == "SelctBrandWiseCategory")
                {
                    grp.Add(
                        new BrandmasterDomain
                        {

                            BrandId = Convert.ToInt16(dr["BrandId"]),
                            GRP_CD = dr["GRP_CD"].ToString().Trim(),
                            BrandName = dr["BrandName"].ToString(),
                            CategoryName = dr["GRP_NAME"].ToString(),
                            ISOnWeb = Convert.ToBoolean(dr["IsOnWeb"]),
                            IsOnHomePage = Convert.ToBoolean(dr["IsHomePage"]),
                        });
                }
                else
                {
                    grp.Add(
                        new BrandmasterDomain
                        {

                            BrandId = Convert.ToInt16(dr["BrandId"]),
                            BrandName = dr["BrandName"].ToString(),
                            ISOnWeb = Convert.ToBoolean(dr["IsOnWeb"]),
                            IsOnHomePage = Convert.ToBoolean(dr["IsHomePage"]),
                        });
                }

            }
            return grp;
        }
        public static List<BrandmasterDomain> GetbrandByCategoryName(int BrandId)
        {
            List<BrandmasterDomain> grp = new List<BrandmasterDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetbrandByCategoryName", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetbrandByCategoryName");
            cmd.Parameters.AddWithValue("@Id", BrandId);

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
                        ISOnWeb = Convert.ToBoolean(dr["IsOnWeb"]),
                        IsOnHomePage = Convert.ToBoolean(dr["IsHomePage"]),
                    });

            }
            return grp;
        }

        public static List<BrandmasterDomain> GetBrandCategoryByCatId(string CatId)
        {
            List<BrandmasterDomain> grp = new List<BrandmasterDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetBrandCategoryByCatId", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CatId", CatId);

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
                        CategoryName = dr["GRP_NAME"].ToString(),
                        BrandName = dr["BrandName"].ToString()
                    });

            }
            return grp;
        }
        public static bool DeleteBrandmaster(int BrandId)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("DeleteBrandmasterById", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@mode", "DeleteCategoryImage");
                cmd.Parameters.AddWithValue("@Id", BrandId);

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
        public static bool UpdateBrandmaster(BrandmasterDomain smodel)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateBrandmaster", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BrandId", smodel.BrandId);
            cmd.Parameters.AddWithValue("@BrandName", smodel.BrandName);
            cmd.Parameters.AddWithValue("@IsHomePage", smodel.IsOnHomePage);
            cmd.Parameters.AddWithValue("@IsOnWeb", smodel.ISOnWeb);


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
