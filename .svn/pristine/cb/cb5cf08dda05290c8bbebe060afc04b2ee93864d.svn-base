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
   public class BannerImageCRUD
    {
        public static void AddToBannerImage(BannerImageDomain mBannerImage)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);

            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_Banerimage", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Insert");

            cmd.Parameters.AddWithValue("@ImageName", mBannerImage.ImageName);
            cmd.Parameters.AddWithValue("@text1", mBannerImage.text1);
            cmd.Parameters.AddWithValue("@text2", mBannerImage.text2);
            cmd.Parameters.AddWithValue("@text3", mBannerImage.text3);
            cmd.Parameters.AddWithValue("@IsDeleted", mBannerImage.IsDeleted);

            cmd.ExecuteNonQuery();
            sqlconn.Close();


        }


        public static List<BannerImageDomain> GetBannerImage(int Id)
        {
            List<BannerImageDomain> grp = new List<BannerImageDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_Banerimage", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Get_BanerimageById");
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@ImageName", "");
            cmd.Parameters.AddWithValue("@text1", "");
            cmd.Parameters.AddWithValue("@text2", "");
            cmd.Parameters.AddWithValue("@text3", "");
            cmd.Parameters.AddWithValue("@IsDeleted", "");

           

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new BannerImageDomain
                    {
                        //Id = Convert.ToInt16(dr["Id"]),
                        ImageName = (dr["ImageName"]).ToString(),
                        text1 = dr["text1"].ToString(),
                        text2 = dr["text2"].ToString(),
                        text3 = dr["text3"].ToString(),
                        IsDeleted = Convert.ToBoolean(dr["IsDeleted"]),
                    }); ;

            }
            return grp;
        }


        public static List<BannerImageDomain> GetBannerImageAll()
        {
            List<BannerImageDomain> grp = new List<BannerImageDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_BanerimageAll", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
         

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new BannerImageDomain
                    {

                        Id = Convert.ToInt16(dr["Id"]),

                        ImageName = dr["ImageName"].ToString(),
                        text1 = dr["text1"].ToString(),
                        text2 = dr["text2"].ToString(),
                        text3 = dr["text3"].ToString(),
                        IsDeleted =Convert.ToBoolean( dr["IsDeleted"]),
                    });

            }
            return grp;
        }
        public static bool DeleteBannerImage(int Id)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("Get_Banerimage", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteBanerimage");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@ImageName", "");
                cmd.Parameters.AddWithValue("@text1", "");
                cmd.Parameters.AddWithValue("@text2", "");
                cmd.Parameters.AddWithValue("@text3", "");
                cmd.Parameters.AddWithValue("@IsDeleted", "");
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
        public static bool UpdateBanerimage(BannerImageDomain smodel)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateBanerimage", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", smodel.Id);
            cmd.Parameters.AddWithValue("@ImageName", smodel.ImageName);
            cmd.Parameters.AddWithValue("@text1", smodel.text1);
            cmd.Parameters.AddWithValue("@text2", smodel.text2);
            cmd.Parameters.AddWithValue("@text3", smodel.text3);
            cmd.Parameters.AddWithValue("@IsDeleted", smodel.IsDeleted);
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
