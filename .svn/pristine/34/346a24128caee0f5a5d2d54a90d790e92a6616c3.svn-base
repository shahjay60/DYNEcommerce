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
    public class ExceptionLogCRUD
    {
        public static void AddToExceptionLog(ExceptionLogDomain mExceptionLog)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("sp_ExceptionLog", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "PostExceptionLog");

         
            cmd.Parameters.AddWithValue("@ControllerName", mExceptionLog.ControllerName);
            cmd.Parameters.AddWithValue("@MethodName", mExceptionLog.MethodName);
            cmd.Parameters.AddWithValue("@ErrorText", mExceptionLog.ErrorText);

            cmd.Parameters.AddWithValue("@StackTrace", mExceptionLog.StackTrace);
            cmd.Parameters.AddWithValue("@Datetime", mExceptionLog.Datetime);

            if (sqlconn.State == ConnectionState.Closed)
            {
                sqlconn.Open();
            }
            cmd.ExecuteNonQuery();
            sqlconn.Close();

        }
    }
}
