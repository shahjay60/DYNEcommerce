using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace DataAccessLayer.Admin
{
    public class AdminLoginCRUD
    {
        public static void AddToAdminLogin(AdminLoginDomain mAdminLogin)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);

            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_AdminLogin", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Insert");


            cmd.Parameters.AddWithValue("@UserName", mAdminLogin.UserName);
            cmd.Parameters.AddWithValue("@Password", mAdminLogin.Password);

            cmd.Parameters.AddWithValue("@EmailId", mAdminLogin.EmailId);
            cmd.Parameters.AddWithValue("@phoneNo", mAdminLogin.phoneNo);



            cmd.ExecuteNonQuery();
            sqlconn.Close();


        }

        public static List<AdminLoginDomain> GetAdminLogin(int AdminId)
        {
            List<AdminLoginDomain> grp = new List<AdminLoginDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_AdminLogin", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Get_AdminLogin");

            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new AdminLoginDomain
                    {

                        AdminId = Convert.ToInt16(dr["AdminId"]),

                        UserName = dr["UserName"].ToString(),

                        Password = dr["Password"].ToString(),

                        EmailId = dr["EmailId"].ToString(),
                        phoneNo = dr["phoneNo"].ToString(),

                    });

            }
            return grp;
        }
        public static List<AdminLoginDomain> GetAdminLoginAll()
        {
            List<AdminLoginDomain> grp = new List<AdminLoginDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_AdminLoginAll", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new AdminLoginDomain
                    {

                        AdminId = Convert.ToInt16(dr["AdminId"]),
                        UserName = dr["UserName"].ToString(),
                        Password = dr["Password"].ToString(),
                        EmailId = dr["EmailId"].ToString(),
                        phoneNo = dr["phoneNo"].ToString(),

                    });

            }
            return grp;
        }
        public static bool DeleteAdminLogin(int AdminId)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("Get_AdminLogin", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteAdminLogin");
                cmd.Parameters.AddWithValue("@AdminId", AdminId);

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
        public bool UpdateDetails(AdminLoginDomain smodel)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateBanerimage", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AdminId", smodel.AdminId);

            cmd.Parameters.AddWithValue("@UserName", smodel.UserName);
            cmd.Parameters.AddWithValue("@Password", smodel.Password);


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
