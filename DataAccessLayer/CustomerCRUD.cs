using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomerCRUD
    {
        #region OldCode

        //public static TMATS00012020Entities objEntity = new TMATS00012020Entities();

        //public static void Register(CustomerRegistration model)
        //{
        //    objEntity.CustomerRegistrations.Add(model);
        //    objEntity.SaveChanges();
        //}

        //public static bool Login(CustomerModel model)
        //{
        //    bool result=true;
        //    var data = objEntity.CustomerRegistrations.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
        //    if(data!=null)
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }
        //    return result;
        //}

        #endregion

        public static void AddToCustomer(CustomerDomain mCustomer)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;


            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("InsertCustomerReg", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Insert");

            cmd.Parameters.AddWithValue("@FirstName", mCustomer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", mCustomer.LastName);
            cmd.Parameters.AddWithValue("@Email", mCustomer.Email);
            cmd.Parameters.AddWithValue("@Phone", mCustomer.Phone);
            cmd.Parameters.AddWithValue("@Country", mCustomer.Country);
            cmd.Parameters.AddWithValue("@State", mCustomer.State);
            cmd.Parameters.AddWithValue("@City", mCustomer.City);
            cmd.Parameters.AddWithValue("@Pincode", mCustomer.Pincode);
            cmd.Parameters.AddWithValue("@ShippingAddress", mCustomer.ShippingAddress);
            cmd.Parameters.AddWithValue("@BillingAddress", mCustomer.ShippingAddress);
            cmd.Parameters.AddWithValue("@RegistrationDatetime",DateTime.Now);
            cmd.Parameters.AddWithValue("@Password", mCustomer.Password);

            cmd.ExecuteNonQuery();

            sqlconn.Close();
        }
        public static List<CustomerCRUDNEWDomain> CustomerLogin(string Email, string Password)
        {
            List<CustomerCRUDNEWDomain> grp = new List<CustomerCRUDNEWDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_CustomerLogin", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "Login");

            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new CustomerCRUDNEWDomain
                    {

                        //Id = Convert.ToInt16(dr["Id"]),

                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Id =Convert.ToInt32( dr["id"]),
                        //Password = dr["Password"].ToString()

                    });

            }
            sqlconn.Close();
            return grp;
        }
        public static int ChkEmailExistsOrNot(string email)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_ChkEmailExistsOrNot", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output; 

            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Close();
            string totalRecord = cmd.Parameters["@RowCount"].Value.ToString();
            return Convert.ToInt32(totalRecord);
        }

        public static int ChkPasswordExistsOrNot(string Password)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("sp_ChkPasswordExistsOrNot", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Close();
            string totalRecord = cmd.Parameters["@RowCount"].Value.ToString();
            return Convert.ToInt32(totalRecord);
        }

        public static List<CustomerDomain> GetCustomerById(int Id)
        {
            List<CustomerDomain> grp = new List<CustomerDomain>();
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("Get_CustomerById", sqlconn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", "GetCustomerRegistrationById");
            cmd.Parameters.AddWithValue("@Id", Id);

            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sqlconn.Open();
            sd.Fill(dt);
            sqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                grp.Add(
                    new CustomerDomain
                    {
                        Id = Convert.ToInt16(dr["Id"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Country = dr["Country"].ToString(),
                        State = dr["State"].ToString(),
                        City = dr["City"].ToString(),
                        Pincode = dr["Pincode"].ToString(),
                        ShippingAddress = dr["ShippingAddress"].ToString(),
                        BillingAddress = dr["BillingAddress"].ToString(),
                        RegistrationDatetime = Convert.ToDateTime(dr["RegistrationDatetime"]),
                        Password = dr["Password"].ToString(),
                    });

            }
            return grp;
        }
        public static bool UpdateCustomerPassword(int id,string password)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateCustomerPassword", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            //cmd.Parameters.AddWithValue("@CustomerId", smodel.CustomerId);
            //cmd.Parameters.AddWithValue("@ProductId", smodel.ProductId);
            //cmd.Parameters.AddWithValue("@CreatedDatetime", smodel.CreatedDatetime);
            //cmd.Parameters.AddWithValue("@IsDeleted", smodel.IsDeleted);
            //cmd.Parameters.AddWithValue("@Quantity", smodel.Quantity);
            //cmd.Parameters.AddWithValue("@Amount", smodel.Amount);
            cmd.Parameters.AddWithValue("@Password", password);
            sqlconn.Open();
            int i = cmd.ExecuteNonQuery();
            sqlconn.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }


        public static bool UpdateCustomerById(CustomerDomain smodel)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand cmd = new SqlCommand("UpdateCustomerRegistrationByID", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", smodel.Id);

            cmd.Parameters.AddWithValue("@FirstName", smodel.FirstName);
            cmd.Parameters.AddWithValue("@LastName", smodel.LastName);
            cmd.Parameters.AddWithValue("@Email", smodel.Email);
            cmd.Parameters.AddWithValue("@Phone", smodel.Phone);
            cmd.Parameters.AddWithValue("@Country", smodel.Country);
            cmd.Parameters.AddWithValue("@State", smodel.State);
            cmd.Parameters.AddWithValue("@City", smodel.City);
            cmd.Parameters.AddWithValue("@Pincode", smodel.Pincode);
            cmd.Parameters.AddWithValue("@ShippingAddress", smodel.ShippingAddress);
            cmd.Parameters.AddWithValue("@BillingAddress", smodel.BillingAddress);
            //cmd.Parameters.AddWithValue("@RegistrationDatetime", smodel.RegistrationDatetime);
            //cmd.Parameters.AddWithValue("@Password", smodel.Password);
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
