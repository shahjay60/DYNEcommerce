using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class AttributeCRUD
    {
        public static void AddAttribute(string attributeName)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_Attribute", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "PostAttribute");

            cmd.Parameters.AddWithValue("@AttributeName", attributeName);
            //cmd.Parameters.AddWithValue("@AttributeValue", mAttribute.AttributeValue);

            cmd.ExecuteNonQuery();
            sqlconn.Close();
        }
        public static List<AttributeDomain> GetAttribute(int AttributeId)
        {
            List<AttributeDomain> grp = new List<AttributeDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_Attribute", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetAttribute");

            cmd.Parameters.AddWithValue("@AttributeId", AttributeId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new AttributeDomain
                    {

                        AttributeId = Convert.ToInt16(dr["AttributeId"]),

                        AttributeName = dr["AttributeName"].ToString(),
                        //AttributeValue = dr["AttributeValue"].ToString(),


                    });

            }
            return grp;
        }
        public static List<AttributeDomain> GetAttributeAll()
        {
            List<AttributeDomain> grp = new List<AttributeDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetAttributeAll", sqlconn);
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
                    new AttributeDomain
                    {

                        AttributeId = Convert.ToInt16(dr["AttributeId"]),
                        AttributeName = dr["AttributeName"].ToString(),
                        //AttributeValue = dr["AttributeValue"].ToString(),



                    });

            }
            return grp;
        }
        public static List<AttributeDomain> GetAttributesData()
        {
            List<AttributeDomain> grp = new List<AttributeDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetAttributesData", sqlconn);
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
                    new AttributeDomain
                    {

                        AttributeId = Convert.ToInt16(dr["AttributeId"]),
                        AttributeName = dr["AttributeName"].ToString().ToLower(),
                        AttributeValue = dr["AV"].ToString().ToLower(),
                    });

            }
            return grp;
        }
        public static bool DeleteAttribute(int AttributeId)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("DeleteAttributeById", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AttributeId", AttributeId);

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
        public static bool UpdateAttribute(AttributeDomain smodel)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateAttribute", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AttributeId", smodel.AttributeId);
            cmd.Parameters.AddWithValue("@AttributeName", smodel.AttributeName);

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
