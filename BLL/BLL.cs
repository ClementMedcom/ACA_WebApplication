﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Security.Cryptography;
using System.IO;
namespace BLL
{
    public class Master_BLL
    {
        Database_operation objDB = new Database_operation();

        public int Id;
        public string Item_Name;
        public int qty;
        public decimal unitprice;

        #region Login
        public DataTable checkUserLogin(string uname, string pwd, string userSession, string mode)
        {
            SqlCommand cmd = new SqlCommand("USP_UserLogin");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@loginID", uname);
            cmd.Parameters.AddWithValue("@password", pwd);
            cmd.Parameters.AddWithValue("@sessionID", userSession);
            cmd.Parameters.AddWithValue("@mode", mode);
            return objDB.GetDataTable(cmd); ;
        }

        #endregion

        #region Item_Master

        public int insert_update_item()
        {
            SqlCommand cmd = new SqlCommand("Usp_ItemAdd");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@ItemName", Item_Name);
            cmd.Parameters.AddWithValue("@qty", qty);
            cmd.Parameters.AddWithValue("@unitPrince", unitprice);
            cmd.Parameters.Add("@Result", SqlDbType.Int, 4);
            cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            objDB.ExecuteQuery(cmd);
            return Convert.ToInt32(cmd.Parameters["@Result"].Value);
        }
        public int list_delete()
        {
            SqlCommand cmd = new SqlCommand("usp_ItemDelete");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.Add("@Result", SqlDbType.Int, 4);
            cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            objDB.ExecuteQuery(cmd);
            return Convert.ToInt32(cmd.Parameters["@Result"].Value);
        }
        public DataSet list_item(int pageIndex, string search, int PageSize, int sort)
        {
            //DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("usp_ItemSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);
            cmd.Parameters.AddWithValue("@search", search);
            cmd.Parameters.AddWithValue("@sort", sort);
            return objDB.GetDataSet(cmd); ;
        }
        public DataSet Edit_Item()
        {
            SqlCommand cmd = new SqlCommand("Usp_ItemEdit");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            return objDB.GetDataSet(cmd); ;
        }
        #endregion

        #region Company

        public DataSet list_Company(string companyTaxId, int pageIndex, string search, int PageSize)
        {
            //DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Usp_Company_list");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxID", null);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);
            cmd.Parameters.AddWithValue("@search", search);
            return objDB.GetDataSet(cmd); ;
        }

        #endregion

        #region Employer

        public DataSet list_Employer(string companyTaxId, int pageIndex, string search, int PageSize)
        {
            //DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("usp_Employer_list");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxID", companyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxID", null);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);
            cmd.Parameters.AddWithValue("@search", search);
            return objDB.GetDataSet(cmd); ;
        }

        public DataTable getEmployerDetails(string EmployeeTaxId, string companyTaxId)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployerMSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxID", companyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxID", EmployeeTaxId);
            return objDB.GetDataTable(cmd); ;
        }

        public int Insert_Update_Employer(string Id, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployerAction");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddRange(parameters);
            cmd.Parameters.Add("@Result", SqlDbType.Int, 4);
            cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            objDB.ExecuteQuery(cmd);
            return Convert.ToInt32(cmd.Parameters["@Result"].Value);
        }
        public int Delete_Employer(string tax_id)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployerDelete");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaxId", tax_id);
            cmd.Parameters.Add("@Result", SqlDbType.Int, 4);
            cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            objDB.ExecuteQuery(cmd);
            return Convert.ToInt32(cmd.Parameters["@Result"].Value);
        }
        #endregion

        #region Employee

        public DataSet list_Employee(string companyTaxId, int pageIndex, string search, int PageSize)
        {
            //DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("usp_Employee_list");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxID", companyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxID", null);
            cmd.Parameters.AddWithValue("@ssn", null);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);
            cmd.Parameters.AddWithValue("@search", search);
            return objDB.GetDataSet(cmd); ;
        }

        #endregion

        #region Cryptocraphy
       public  string Encrypt(string clearText)
        {
            string EncryptionKey = "OTNA8SPENG92016";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    public  string Decrypt(string cipherText)
        {
            string EncryptionKey = "OTNA8SPENG92016";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        #endregion

    }
}
