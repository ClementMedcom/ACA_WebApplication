using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class Database_operation
    {
        private string constring = "Data Source=JOSEPH\\SQLEXPRESS;Initial Catalog=ACA2016;User ID=sa;password=P@ssw0rd";
        public int ExecuteQuery(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                cmd.Connection = con;
                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        public DataTable GetDataTable(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                DataSet ds = new DataSet();
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter objadp = new SqlDataAdapter();
                objadp.SelectCommand = cmd;
                objadp.Fill(ds);
                con.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        public DataSet GetDataSet(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                DataSet ds = new DataSet();
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter objadp = new SqlDataAdapter();
                objadp.SelectCommand = cmd;
                objadp.Fill(ds);
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        public string ExecuteScalar(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                string result = null;
                cmd.Connection = con;
                con.Open();
                object value = cmd.ExecuteScalar();
                if (value != null)
                    result = value.ToString();
                con.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
        }
    }
}
