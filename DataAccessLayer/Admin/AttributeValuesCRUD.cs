using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class AttributeValuesCRUD
    {
        public static void AddAttributeValue(AttributevaluesDomain model)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("AddAttributeValues", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@attId", model.AttributeId);
            cmd.Parameters.AddWithValue("@AttributeValue", model.AttributeValue);

            cmd.ExecuteNonQuery();
            sqlconn.Close();
        }
        public static List<AttributevaluesDomain> GetAttributeValue(int AttributeValueId)
        {
            List<AttributevaluesDomain> grp = new List<AttributevaluesDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetAttributeValuesById", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", AttributeValueId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new AttributevaluesDomain
                    {

                        AttributeId = Convert.ToInt16(dr["Attid"]),
                        AttributeValue = dr["AttValue"].ToString(),
                        Id = Convert.ToInt16(dr["Id"])
                        //AttributeValue = dr["AttributeValue"].ToString(),
                    });

            }
            return grp;
        }
        public static List<AttributevaluesDomain> GetAttributeValueAll()
        {
            List<AttributevaluesDomain> grp = new List<AttributevaluesDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetAllAttributeValues", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new AttributevaluesDomain
                    {

                        AttributeValue = dr["AttValue"].ToString(),
                        AttributeName = dr["AttributeName"].ToString(),
                        Id = Convert.ToInt16(dr["Id"])
                    });

            }
            return grp;
        }
        public static bool DeleteAttributeValue(int Id)
        {
            bool result = true;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);

                SqlCommand cmd = new SqlCommand("DeleteAttributeValue", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@attvalueId", Id);

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

        public static bool UpdateAttributeValue(AttributevaluesDomain smodel)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateAttributeValues", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@attvalueId", smodel.Id);
            cmd.Parameters.AddWithValue("@attId", smodel.AttributeId);
            cmd.Parameters.AddWithValue("@AttributeValue", smodel.AttributeValue);
            sqlconn.Open();
            int i = cmd.ExecuteNonQuery();
            sqlconn.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }


        public static List<AttributevaluesDomain> GetAttValyByAttId(int AttributeId)
        {
            List<AttributevaluesDomain> grp = new List<AttributevaluesDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetAttributeValuesByAttId", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@attId", AttributeId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new AttributevaluesDomain
                    {

                        AttributeValue = dr["AttValue"].ToString(),
                        Id = Convert.ToInt16(dr["Id"])
                        //AttributeValue = dr["AttributeValue"].ToString(),
                    });

            }
            return grp;
        }

    }
}
