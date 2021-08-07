using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer.Admin
{
    public class Admin_OrderdataCRUD
    {
        public static List<Admin_OrderdataDomain> Get_OrderDeialsProduct()
        {
            List<Admin_OrderdataDomain> grp = new List<Admin_OrderdataDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_OrderDetails", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Get_OrderDeialsProduct");
            cmd.Parameters.AddWithValue("@Id", "");
            cmd.Parameters.AddWithValue("@CustomerId", "");

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {

                grp.Add(
                    new Admin_OrderdataDomain
                    {
                        //CustomerId = (dr["CustomerId"]).ToString(),
                        //ProductId = (dr["ProductId"]).ToString(),
                        Item_desc = (dr["Item_desc"]).ToString(),
                        OrderId = (dr["Id"]).ToString(),
                        IsPlaced = (dr["IsPlaced"]).ToString(),
                        OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                        PaymentType = (dr["PaymentType"]).ToString(),
                        OrderAmount = dr["OrderAmount"].ToString(),
                        TransactionId = dr["TransactionId"].ToString(),
                    }); ;

            }
            return grp;
        }
    }
}
