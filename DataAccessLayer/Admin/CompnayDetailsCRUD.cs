using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace DataAccessLayer.Admin
{
    public class CompnayDetailsCRUD
    {
        public static void AddToCompnayDetails(CompnayDetailsDomain mCompnayDetails)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);

            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_CompnayDetails", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Insert");

            cmd.Parameters.AddWithValue("@Name", mCompnayDetails.Name);
            cmd.Parameters.AddWithValue("@Address", mCompnayDetails.Address);

            cmd.Parameters.AddWithValue("@PhoneNumber", mCompnayDetails.PhoneNumber);
            cmd.Parameters.AddWithValue("@PhoneNumber2", mCompnayDetails.PhoneNumber2);
            cmd.Parameters.AddWithValue("@EmailAddress", mCompnayDetails.EmailAddress);
            cmd.Parameters.AddWithValue("@Logo", mCompnayDetails.Logo);
            cmd.Parameters.AddWithValue("@FacebookLink", mCompnayDetails.FacebookLink);
            cmd.Parameters.AddWithValue("@InstagramLink", mCompnayDetails.InstagramLink);
            cmd.Parameters.AddWithValue("@FaxNo", mCompnayDetails.FaxNo);

            cmd.ExecuteNonQuery();
            sqlconn.Close();

        }
        public static List<CompnayDetailsDomain> GetCompnayDetails(int Id)
        {
            List<CompnayDetailsDomain> grp = new List<CompnayDetailsDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_CompnayDetails", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "getbyCompnayDetailsId");
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Name", "");
            cmd.Parameters.AddWithValue("@Address", "");
            cmd.Parameters.AddWithValue("@PhoneNumber", "");
            cmd.Parameters.AddWithValue("@PhoneNumber2", "");
            cmd.Parameters.AddWithValue("@EmailAddress", "");

            cmd.Parameters.AddWithValue("@Logo", "");

            cmd.Parameters.AddWithValue("@FacebookLink", "");
            cmd.Parameters.AddWithValue("@InstagramLink", "");
            cmd.Parameters.AddWithValue("@FaxNo", "");

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new CompnayDetailsDomain
                    {
                        Id = Convert.ToInt16(dr["Id"]),
                        Name = (dr["Name"]).ToString(),
                        Address = dr["Address"].ToString(),
                        PhoneNumber = dr["PhoneNumber"].ToString(),
                        PhoneNumber2 = dr["PhoneNumber2"].ToString(),
                        EmailAddress = dr["EmailAddress"].ToString(),
                        Logo = (dr["Logo"]).ToString(),
                        FacebookLink = dr["FacebookLink"].ToString(),
                        InstagramLink = dr["InstagramLink"].ToString(),
                        FaxNo = (dr["FaxNo"]).ToString()

                    }); ;

            }
            return grp;
        }
        public static List<CompnayDetailsDomain> GetCompnayDetailsAll()
        {
            List<CompnayDetailsDomain> grp = new List<CompnayDetailsDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_CompnayDetailsAll", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new CompnayDetailsDomain
                    {

                        Id = Convert.ToInt16(dr["Id"]),
                        Name = (dr["Name"]).ToString(),
                        Address = dr["Address"].ToString(),
                        PhoneNumber = dr["PhoneNumber"].ToString(),
                        PhoneNumber2 = dr["PhoneNumber2"].ToString(),
                        EmailAddress = dr["EmailAddress"].ToString(),
                        Logo = (dr["Logo"]).ToString(),
                        FacebookLink = dr["FacebookLink"].ToString(),
                        InstagramLink = dr["InstagramLink"].ToString(),
                        FaxNo = (dr["FaxNo"]).ToString()


                    });

            }
            return grp;
        }
        public static bool DeleteCompnayDetails(int Id)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("Get_CompnayDetails", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteCompnayDetails");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Address", "");
                cmd.Parameters.AddWithValue("@PhoneNumber", "");
                cmd.Parameters.AddWithValue("@PhoneNumber2", "");
                cmd.Parameters.AddWithValue("@EmailAddress", "");
                cmd.Parameters.AddWithValue("@Logo", "");

                cmd.Parameters.AddWithValue("@FacebookLink", "");
                cmd.Parameters.AddWithValue("@InstagramLink", "");
                cmd.Parameters.AddWithValue("@FaxNo", "");



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
        public static bool UpdateCompnayDetails(CompnayDetailsDomain smodel)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Update_CompnayDetails", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", smodel.Id);
            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@Address", smodel.Address);
            cmd.Parameters.AddWithValue("@PhoneNumber", smodel.PhoneNumber);
            cmd.Parameters.AddWithValue("@PhoneNumber2", smodel.PhoneNumber2);
            cmd.Parameters.AddWithValue("@EmailAddress", smodel.EmailAddress);
            cmd.Parameters.AddWithValue("@Logo", smodel.Logo);
            cmd.Parameters.AddWithValue("@FacebookLink", smodel.FacebookLink);
            cmd.Parameters.AddWithValue("@InstagramLink", smodel.InstagramLink);
            cmd.Parameters.AddWithValue("@FaxNo", smodel.FaxNo);

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
