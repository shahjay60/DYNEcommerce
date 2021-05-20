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
    public class AttributeCRUD
    {
        public static void AddAttribute(AttributeDomain mAttribute)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("sp_Attribute", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "PostAttribute");

            cmd.Parameters.AddWithValue("@AttributeName", mAttribute.AttributeName);


            cmd.ExecuteNonQuery();


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
                    });

            }
            return grp;
        }

        public static List<AttributeDomain> GetAllAttribute()
        {
            List<AttributeDomain> grp = new List<AttributeDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("GetAttributesData", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

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
                    });

            }
            return grp;
        }
    }
}
