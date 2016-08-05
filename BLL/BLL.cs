using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
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

        #region Company

        public DataSet list_Company(string companyTaxId)
        {
            //DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("usp_CompanySelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxID", null);
            return objDB.GetDataSet(cmd); ;
        }

        #endregion

        #region Employer

        public DataSet list_Employer(string companyTaxId)
        {
            //DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("usp_EmployerMSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxID", companyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxID", null);
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

        public DataSet Insert_Update_Employer(string Id, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            if (Id=="0")
                cmd = new SqlCommand("usp_EmployerAdd");
            else
                cmd = new SqlCommand("usp_EmployerModify");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            return objDB.GetDataSet(cmd); 
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
            SqlCommand cmd = new SqlCommand("usp_EmployeeASelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxID", companyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxID", null);
            cmd.Parameters.AddWithValue("@ssn", null);
            return objDB.GetDataSet(cmd); ;
        }

        public DataTable drp_Employer(string companyTaxId)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployerMSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxID", companyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxID", null);
            return objDB.GetDataTable(cmd); ;
        }

        //Get Employee
        public DataSet Edit_Employee(string CompanyTaxId, string EmployerTaxId, string ssn)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployeeASelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxId", CompanyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxId", EmployerTaxId);
            cmd.Parameters.AddWithValue("@ssn", ssn);
            return objDB.GetDataSet(cmd); ;
        }
        //Get Hire Name
        public DataTable Edit_HireName(string CompanyTaxId, string EmployerTaxId, string ssn)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployeeHireSpanSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxId", CompanyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxId", EmployerTaxId);
            cmd.Parameters.AddWithValue("@ssn", ssn);
            return objDB.GetDataTable(cmd); ;
        }

        //get Status
        public DataTable Edit_Status(string CompanyTaxId, string EmployerTaxId, string ssn)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployeeStatusSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxId", CompanyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxId", EmployerTaxId);
            cmd.Parameters.AddWithValue("@ssn", ssn);
            return objDB.GetDataTable(cmd); ;
        }
        //get Employee Enrollment
        public DataTable Edit_EmployeeEnrollment(string CompanyTaxId, string EmployerTaxId, string ssn)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployeeEnrollmentSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxId", CompanyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxId", EmployerTaxId);
            cmd.Parameters.AddWithValue("@ssn", ssn);
            return objDB.GetDataTable(cmd); ;
        }
        //get Employee code
        public DataTable Edit_EmployeeCode(string CompanyTaxId, string EmployerTaxId, string ssn)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployeeCodeSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxId", CompanyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxId", EmployerTaxId);
            cmd.Parameters.AddWithValue("@ssn", ssn);
            return objDB.GetDataTable(cmd); ;
        }
        //get employee covered individual
        public DataTable Edit_CoveredIndividual(string CompanyTaxId, string EmployerTaxId, string ssn)
        {
            SqlCommand cmd = new SqlCommand("usp_CoveredIndividualSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxId", CompanyTaxId);
            cmd.Parameters.AddWithValue("@EmployerTaxId", EmployerTaxId);
            cmd.Parameters.AddWithValue("@ssn", ssn);
            return objDB.GetDataTable(cmd); ;
        }

        public DataSet Insert_Update_Employee(string Id, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            if (Id == "0")
                cmd = new SqlCommand("usp_EmployeeAdd");
            else
                cmd = new SqlCommand("usp_EmployeeAModify");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            return objDB.GetDataSet(cmd);
        }
        //Delete Employee Hire 
        public void Delete_HireName(string EmployerTaxId, string ssn, string hireName)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployeeHireSpanDelete");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployerTaxId", EmployerTaxId);
            cmd.Parameters.AddWithValue("@ssn", ssn);
            cmd.Parameters.AddWithValue("@hireName", hireName);
            objDB.ExecuteQuery(cmd);
        }
        //Delete Employee Status
        public void Delete_Status(string EmployerTaxId, string ssn, string statusName)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployeeStatusDelete");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployerTaxId", EmployerTaxId);
            cmd.Parameters.AddWithValue("@ssn", ssn);
            cmd.Parameters.AddWithValue("@statusName", statusName);
            objDB.ExecuteQuery(cmd);
        }
        //Delete Employee Enrollment
        public void Delete_Enrollment(string EmployerTaxId, string ssn, string enrollmentName)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployeeEnrollmentDelete");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployerTaxId", EmployerTaxId);
            cmd.Parameters.AddWithValue("@ssn", ssn);
            cmd.Parameters.AddWithValue("@enrollmentName", enrollmentName);
            objDB.ExecuteQuery(cmd);
        }

        //Delete Employee Enrollment
        public void Delete_CoveredIndividual(string EmployerTaxId, string ssnEmployee, string ssn)
        {
            SqlCommand cmd = new SqlCommand("usp_CoveredIndividualDelete");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployerTaxId", EmployerTaxId);
            cmd.Parameters.AddWithValue("@ssnEmployee", ssnEmployee);
            cmd.Parameters.AddWithValue("@ssn", ssn);
            objDB.ExecuteQuery(cmd);
        }

        //update Employee Hire
        public void insert_Update_Hire(string id, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            if (id == "0")
                cmd = new SqlCommand("usp_EmployeeHireSpanAdd");
            else
                cmd = new SqlCommand("usp_EmployeeHireSpanModify");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            objDB.ExecuteQuery(cmd);
        }
        // update Employee Status
        public void insert_Update_Status(string id, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            if (id == "0")
                cmd = new SqlCommand("usp_EmployeeStatusAdd");
            else
                cmd = new SqlCommand("usp_EmployeeStatusModify");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            objDB.ExecuteQuery(cmd);
        }
        //update Employee Enrollment
        public void insert_Update_Enrollment(string id, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            if (id == "0")
                cmd = new SqlCommand("usp_EmployeeEnrollmentAdd");
            else
                cmd = new SqlCommand("usp_EmployeeEnrollmentModify");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            objDB.ExecuteQuery(cmd);
        }
        //update Employee Covered Individual
        public void insert_Update_Covered_Individual(string id, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            if (id == "0")
                cmd = new SqlCommand("usp_CoveredIndividualAdd");
            else
                cmd = new SqlCommand("usp_CoveredIndividualModify");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            objDB.ExecuteQuery(cmd);
        }

        //update Employee Covered Individual
        public void insert_Update_EmployeeCode(string id, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            if (id == "0")
                cmd = new SqlCommand("usp_EmployeeCodeAdd");
            else
                cmd = new SqlCommand("usp_EmployeeCodeModify");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            objDB.ExecuteQuery(cmd);
        }

        #endregion

        #region Plan

        public DataSet list_Plan(string companyTaxId, int pageIndex, string search, int PageSize)
        {
            //DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("usp_EmployerPlanSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxID", companyTaxId);
            cmd.Parameters.AddWithValue("@name", null);
            return objDB.GetDataSet(cmd); ;
        }
        public DataSet getPlanDetails(string companyTaxId)
        {
            SqlCommand cmd = new SqlCommand("usp_EmployerPlanSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxID", companyTaxId);
            return objDB.GetDataSet(cmd); ;
        }
        public DataSet getPremiumDetails(string companyTaxId)
        {
            SqlCommand cmd = new SqlCommand("usp_PremiumSelect");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxID", companyTaxId);
            return objDB.GetDataSet(cmd); 
        }

        

        public DataSet Insert_Update_EmployerPlan(string id,params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            if (id == "0")
                cmd = new SqlCommand("usp_EmployerPlanAdd");
            else
                cmd = new SqlCommand("usp_EmployerPlanModify");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            return objDB.GetDataSet(cmd);
        }
        public void insert_Update_Premium(string id, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            if (id == "0")
                cmd = new SqlCommand("usp_PremiumAdd");
            else
                cmd = new SqlCommand("usp_PremiumModify");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            objDB.ExecuteQuery(cmd);
        }
        public void PremiumDelete(string companyTaxId, string name)
        {
            SqlCommand cmd = new SqlCommand("usp_PremiumDelete");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyTaxId", companyTaxId);
            cmd.Parameters.AddWithValue("@name", name);
            objDB.ExecuteQuery(cmd);
        }

        #endregion

    }
}
