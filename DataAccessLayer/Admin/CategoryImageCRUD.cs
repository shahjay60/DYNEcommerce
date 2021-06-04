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
   public class CategoryImageCRUD
    {
        public static void AddToCategoryImage(CategoryImageDomain mCategoryImage)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);

            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_CategoryImage", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Insert");

            cmd.Parameters.AddWithValue("@CatId", mCategoryImage.GRP_CD);
            cmd.Parameters.AddWithValue("@ImageName", mCategoryImage.ImageName);
          


            cmd.ExecuteNonQuery();
            sqlconn.Close();


        }
        public static List<CategoryImageDomain> GetCategoryImage(int Id)
        {
            List<CategoryImageDomain> grp = new List<CategoryImageDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_CategoryImage", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetCategoryImage");
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@CatId", "");

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new CategoryImageDomain
                    {
                        Id = Convert.ToInt16(dr["Id"]),
                        GRP_CD = (dr["CatId"]).ToString(),
                        ImageName = dr["ImageName"].ToString(),
                        CategoryName = dr["GRP_NAME"].ToString()

                    }); ;

            }
            return grp;
        }
        public static List<CategoryImageDomain> GetCategoryImageAll()
        {
            List<CategoryImageDomain> grp = new List<CategoryImageDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetCategoryImageAll", sqlconn);
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
                    new CategoryImageDomain
                    {

                        Id = Convert.ToInt16(dr["Id"]),
                        GRP_CD = (dr["CatId"]).ToString(),
                        ImageName = dr["ImageName"].ToString(),
                        CategoryName = dr["GRP_NAME"].ToString()
                    });

            }
            return grp;
        }
        public static bool DeleteCategoryImage(int Id)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("Get_CategoryImage", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteCategoryImage");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@CatId ", "");
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
        public static bool UpdateCategoryImage(CategoryImageDomain smodel)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateCategoryImage", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", smodel.Id);
            cmd.Parameters.AddWithValue("@ImageName", smodel.ImageName);
           
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
