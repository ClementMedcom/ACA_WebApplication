using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ACA_WebApplication.Master
{
    public partial class Employee_Details : System.Web.UI.Page
    {
        Master_BLL objMaster = new Master_BLL();
        StaticContent objDrop = new StaticContent();
        DataTable dt_hire = new DataTable();
        DataTable dt_Status = new DataTable();
        DataTable dt_Enrollment = new DataTable();
        DataTable dt_Covered = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Tax_Id"] != null)
                {
                    hdn_companytax_id.Value = Session["Tax_Id"].ToString();
                    list_Employee(hdn_companytax_id.Value, 1, "", 10);
                    ClearEmployeeForm();
                    load_Employer();
                    load_dropdown();
                }
            }
            txtsearch.Focus();
        }
        public void load_Employer()
        {
            DataTable dt = objMaster.drp_Employer(hdn_companytax_id.Value);
            drp_employer.DataSource = dt;
            drp_employer.DataValueField = "name";
            drp_employer.DataTextField = "name";
            drp_employer.DataBind();
            drp_employer.Items.Insert(0, new ListItem("--Select--", ""));

        }
        public void list_Employee(string companyTaxId, int pageIndex, string search, int PageSize)
        {
            try
            {
                DataSet ds = objMaster.list_Employee(companyTaxId, pageIndex, search, PageSize);
                DataTable dt = ds.Tables[0];
                
                IEnumerable<DataRow> query1 = from all_data in dt.AsEnumerable()
                                              where all_data.Field<string>("name").ToLower().StartsWith(search.ToLower()) 
                                              orderby all_data.Field<string>("firstname")
                                              select all_data;
                if (query1.Any())
                {
                    DataTable dt1 = query1.CopyToDataTable<DataRow>();
                    dt1.Columns.Add("RowNumber", typeof(System.Int32));
                    int ColumnValue = 0;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        ColumnValue = ColumnValue + 1;
                        dr["RowNumber"] = ColumnValue.ToString();
                    }

                    DataTable dt_temp = (from all_data in dt1.AsEnumerable()
                                         where all_data.Field<string>("name").ToLower().StartsWith(search.ToLower()) 
                                         orderby all_data.Field<string>("firstname")
                                         select all_data).Skip((pageIndex - 1) * Convert.ToInt32(drp_count.Text)).Take(PageSize).CopyToDataTable<DataRow>();
                    int total_rows = dt1.Rows.Count;
                    rptEmployee.DataSource = dt_temp;
                    rptEmployee.DataBind();
                    hid_rowcount.Value = total_rows.ToString();
                    lbl_pagenum.Text = pageIndex.ToString();
                    int start_record = ((pageIndex - 1) * Convert.ToInt32(drp_count.Text)) + 1;
                    int End_record = ((pageIndex - 1) * Convert.ToInt32(drp_count.Text)) + Convert.ToInt32(drp_count.Text);
                    if (End_record > total_rows)
                    {
                        End_record = total_rows;
                    }
                    lbl_result.Text = "Showing Results " + start_record + "-" + End_record + " Out of " + total_rows + " Records";
                }
                else
                {
                    rptEmployee.DataSource = null;
                    rptEmployee.DataBind();
                    hid_rowcount.Value = "0";
                    lbl_pagenum.Text = "1";
                    lbl_result.Text = "Showing Results " + 0 + "-" + 0 + " Out of " + 0 + " Records";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rptEmployee_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                TabContainer1.ActiveTabIndex= 0;
                HiddenField hdn_Id = (HiddenField)e.Item.FindControl("hdn_Id");
                HiddenField hdn_employerTaxId = (HiddenField)e.Item.FindControl("hdn_employerTaxId");
                Label lbl_ssn = (Label)e.Item.FindControl("lbl_ssn");
                DataSet ds = objMaster.Edit_Employee(hdn_companytax_id.Value,hdn_employerTaxId.Value, lbl_ssn.Text);
                DataTable dt6 = ds.Tables[0];

                DataTable dt1 = objMaster.Edit_HireName(hdn_companytax_id.Value, hdn_employerTaxId.Value, lbl_ssn.Text);
                DataTable dt2 = objMaster.Edit_Status(hdn_companytax_id.Value, hdn_employerTaxId.Value, lbl_ssn.Text);
                DataTable dt3 = objMaster.Edit_EmployeeEnrollment(hdn_companytax_id.Value, hdn_employerTaxId.Value, lbl_ssn.Text);
                Session["dt_hire"] = dt1;
                Session["dt_Status"] = dt2;
                Session["dt_Enrollment"] = dt3;

                DataTable dt4 = objMaster.Edit_EmployeeCode(hdn_companytax_id.Value, hdn_employerTaxId.Value, lbl_ssn.Text);
                DataTable dt5 = objMaster.Edit_CoveredIndividual(hdn_companytax_id.Value, hdn_employerTaxId.Value, lbl_ssn.Text);
                Session["dt_Covered"] = dt5;

                hdn_EmployerTaxId.Value = hdn_employerTaxId.Value;
                if (dt6.Rows.Count > 0)
                {
                    hdn_id.Value = dt6.Rows[0]["id"].ToString();
                    drp_employer.Text = dt6.Rows[0]["name"].ToString();
                    txt_SSN.Text = dt6.Rows[0]["ssn"].ToString();
                    txt_firstname.Text = dt6.Rows[0]["firstName"].ToString();
                    txt_middlename.Text = dt6.Rows[0]["middleName"].ToString();
                    txt_lastname.Text = dt6.Rows[0]["lastName"].ToString();
                    txt_dob.Text = TextManipulation.FixDateFormat(dt6.Rows[0]["birthday"].ToString());
                    txt_address1.Text = dt6.Rows[0]["address"].ToString();
                    txt_address2.Text = dt6.Rows[0]["address2"].ToString();
                    txt_city.Text = dt6.Rows[0]["city"].ToString();
                    txt_zipcode.Text = dt6.Rows[0]["zip"].ToString();
                    drp_state.Text = dt6.Rows[0]["state"].ToString();
                    string dbCountry = dt6.Rows[0]["country"].ToString();
                    if (dbCountry == "US")
                        dbCountry = "United States of America";
                    txt_email.Text = dt6.Rows[0]["email"].ToString();
                }
                if (dt4.Rows.Count > 0)
                {
                    txt_salary.Text = dt4.Rows[0]["salaryAmount"].ToString();
                    txt_hourly.Text = dt4.Rows[0]["hourlyAmount"].ToString();
                }
                dt1.Rows.Add(0, null, null,null);
                rptHiredata.DataSource = dt1;
                rptHiredata.DataBind();

                dt2.Rows.Add(0, null, null, null, null);
                rpt_Status.DataSource = dt2;
                rpt_Status.DataBind();
                DataRow row;
                row = dt3.NewRow();
                row["Id"] = 0;
                row["unionMember"] = 0;
                row["contributionStartDate"] = DBNull.Value;
                row["contributionEndDate"] = DBNull.Value;
                row["coverageOfferDate"] = DBNull.Value;
                row["name"] = null;
                row["isEnrolled"] = 0;
                row["coverageStartDate"] = DBNull.Value;
                row["coverageEndDate"] = DBNull.Value;
                row["cobraEnrolled"] = 0;
                row["cobraStartDate"] = DBNull.Value;
                row["cobraEndDate"] = DBNull.Value;
                row["enrollmentName"] = DBNull.Value;

                dt3.Rows.Add(row);
                rpt_coverage.DataSource = dt3;
                rpt_coverage.DataBind();

                DataRow dr;
                dr = dt5.NewRow();
                dr["Id"] = 0;
                dr["firstName"] = null;
                dr["lastName"] = null;
                dr["employeeSSN"] = null;
                dr["birthday"] = DBNull.Value;
                dr["allCoverage"] = 0;
                dr["janCoverage"] = 0;
                dr["febCoverage"] = 0;
                dr["marCoverage"] = 0;
                dr["aprCoverage"] = 0;
                dr["mayCoverage"] = 0;
                dr["junCoverage"] = 0;
                dr["julCoverage"] = 0;
                dr["augCoverage"] = 0;
                dr["sepCoverage"] = 0;
                dr["octCoverage"] = 0;
                dr["novCoverage"] = 0;
                dr["decCoverage"] = 0;
                dt5.Rows.Add(dr);
                rpt_IndividualData.DataSource = dt5;
                rpt_IndividualData.DataBind();

                rpt_code.DataSource = dt4;
                rpt_code.DataBind();
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            DataSet ds = objMaster.Insert_Update_Employee(hdn_id.Value, getEmployeeParams());
            if (hdn_id.Value != "0")
            {
                DataTable dt_hire = (DataTable)Session["dt_hire"];
                DataTable dt_Status = (DataTable)Session["dt_Status"];
                DataTable dt_Enrollment = (DataTable)Session["dt_Enrollment"];
                DataTable dt_Covered = (DataTable)Session["dt_Covered"];
                Delete_HireName(hdn_EmployerTaxId.Value, txt_SSN.Text.Trim(), dt_hire);
                Delete_Status(hdn_EmployerTaxId.Value, txt_SSN.Text.Trim(), dt_Status);
                Delete_Enrollment(hdn_EmployerTaxId.Value, txt_SSN.Text.Trim(), dt_Enrollment);
                Delete_CoveredIndividual(hdn_EmployerTaxId.Value, txt_SSN.Text.Trim(), dt_Covered);
            }

            Insert_Update_EmployeeCode(hdn_EmployerTaxId.Value, txt_SSN.Text);
            Insert_Update_hire(hdn_EmployerTaxId.Value,txt_SSN.Text);
            Insert_Update_Status(hdn_EmployerTaxId.Value, txt_SSN.Text);
            Insert_Update_Enrollment(hdn_EmployerTaxId.Value, txt_SSN.Text);
            Insert_Update_Covered_Individual(hdn_EmployerTaxId.Value, txt_SSN.Text);



            if (ds.Tables.Count > 0)
            {
                lbl_msg.Text = ds.Tables[0].Rows[0][0].ToString();
                lightDiv.Visible = true;
                fadeDiv.Visible = true;
            }
            else
            {
                if (hdn_id.Value == "0")
                    lbl_msg.Text = "Employee Added Successfully";
                else
                    lbl_msg.Text = "Employee Updated Successfully";
                lightDiv.Visible = true;
                fadeDiv.Visible = true;
                list_Employee(hdn_companytax_id.Value, 1, "", 10);
                ClearEmployeeForm();
            }
        }

        #region Insert Update Subtable in employee

        public void Insert_Update_hire(string EmployerTaxId, string ssn)
        {
            foreach (RepeaterItem item in rptHiredata.Items)
            {
                HiddenField hdn_hireId = (HiddenField)item.FindControl("hdn_hireId");
                TextBox txt_startdate = (TextBox)item.FindControl("txt_startdate");
                TextBox txt_enddate = (TextBox)item.FindControl("txt_enddate");
                if (txt_startdate.Text != "")
                        objMaster.insert_Update_Hire(hdn_id.Value, getHireParams(hdn_EmployerTaxId.Value,ssn, "hire" + item.ItemIndex, txt_startdate.Text, txt_enddate.Text));
            }
            
        }

        public void Insert_Update_Status(string EmployerTaxId, string ssn)
        {
            foreach (RepeaterItem item in rpt_Status.Items)
            {
                HiddenField hdn_statusId = (HiddenField)item.FindControl("hdn_statusId");
                DropDownList drp_status = (DropDownList)item.FindControl("drp_status");
                TextBox txt_startdate = (TextBox)item.FindControl("txt_startdate");
                TextBox txt_enddate = (TextBox)item.FindControl("txt_enddate");
                if (drp_status.Text != "" && txt_startdate.Text != "")
                    objMaster.insert_Update_Status(hdn_id.Value, getStatusParams(hdn_EmployerTaxId.Value, ssn, "status" + item.ItemIndex, drp_status.Text, txt_startdate.Text, txt_enddate.Text));
            }
        }

        public void Insert_Update_Enrollment(string EmployerTaxId, string ssn)
        {
            foreach (RepeaterItem item in rpt_coverage.Items)
            {
                HiddenField hdn_coverageId = (HiddenField)item.FindControl("hdn_coverageId");
                HiddenField hdn_unionMember = (HiddenField)item.FindControl("hdn_unionMember");
                TextBox txt_contributionStartDate = (TextBox)item.FindControl("txt_contributionStartDate");
                TextBox txt_contributionEndDate = (TextBox)item.FindControl("txt_contributionEndDate");
                TextBox txt_coverageOfferDate = (TextBox)item.FindControl("txt_coverageOfferDate");
                TextBox txt_name = (TextBox)item.FindControl("txt_name");
                HiddenField hdn_enrolled = (HiddenField)item.FindControl("hdn_enrolled");
                TextBox txt_coverageStartDate = (TextBox)item.FindControl("txt_coverageStartDate");
                TextBox txt_coverageEndDate = (TextBox)item.FindControl("txt_coverageEndDate");
                HiddenField hdn_cobraEnrolled = (HiddenField)item.FindControl("hdn_cobraEnrolled");
                TextBox txt_cobraStartDate = (TextBox)item.FindControl("txt_cobraStartDate");
                TextBox txt_cobraEndDate = (TextBox)item.FindControl("txt_cobraEndDate");
                if (txt_name.Text != "")
                    objMaster.insert_Update_Enrollment(hdn_id.Value, getEnrollmentParams(hdn_EmployerTaxId.Value, ssn, txt_name.Text, "coverage" + item.ItemIndex, hdn_unionMember.Value, txt_contributionStartDate.Text, txt_contributionEndDate.Text, txt_coverageOfferDate.Text,
                       hdn_enrolled.Value, txt_coverageStartDate.Text, txt_coverageEndDate.Text, hdn_cobraEnrolled.Value, txt_cobraStartDate.Text, txt_cobraEndDate.Text));
            }
        }

        public void Insert_Update_Covered_Individual(string EmployerTaxId, string ssn)
        {
            foreach (RepeaterItem item in rpt_IndividualData.Items)
            {
                TextBox txt_first = (TextBox)item.FindControl("txt_first");
                TextBox txt_last = (TextBox)item.FindControl("txt_last");
                TextBox txt_ssn = (TextBox)item.FindControl("txt_ssn");
                TextBox txt_dob = (TextBox)item.FindControl("txt_dob");
                CheckBox chk_all = (CheckBox)item.FindControl("chk_all");
                CheckBox chk_jan = (CheckBox)item.FindControl("chk_jan");
                CheckBox chk_feb = (CheckBox)item.FindControl("chk_feb");
                CheckBox chk_mar = (CheckBox)item.FindControl("chk_mar");
                CheckBox chk_apr = (CheckBox)item.FindControl("chk_apr");
                CheckBox chk_may = (CheckBox)item.FindControl("chk_may");
                CheckBox chk_jun = (CheckBox)item.FindControl("chk_jun");
                CheckBox chk_jul = (CheckBox)item.FindControl("chk_jul");
                CheckBox chk_aug = (CheckBox)item.FindControl("chk_aug");
                CheckBox chk_sep = (CheckBox)item.FindControl("chk_sep");
                CheckBox chk_oct = (CheckBox)item.FindControl("chk_oct");
                CheckBox chk_nov = (CheckBox)item.FindControl("chk_nov");
                CheckBox chk_dec = (CheckBox)item.FindControl("chk_dec");
                HiddenField hdn_CI_id = (HiddenField)item.FindControl("hdn_CI_id");
                string all = (chk_all.Checked == true) ? "1" : "0"; string jan = (chk_jan.Checked == true) ? "1" : "0"; string feb = (chk_feb.Checked == true) ? "1" : "0";
                string mar = (chk_mar.Checked == true) ? "1" : "0"; string apr = (chk_apr.Checked == true) ? "1" : "0"; string may = (chk_may.Checked == true) ? "1" : "0";
                string jun = (chk_jun.Checked == true) ? "1" : "0"; string jul = (chk_jul.Checked == true) ? "1" : "0"; string aug = (chk_aug.Checked == true) ? "1" : "0";
                string sep = (chk_sep.Checked == true) ? "1" : "0"; string oct = (chk_oct.Checked == true) ? "1" : "0"; string nov = (chk_nov.Checked == true) ? "1" : "0";
                string dec = (chk_dec.Checked == true) ? "1" : "0";
                if (txt_first.Text != "" && txt_ssn.Text != "")
                    objMaster.insert_Update_Covered_Individual(hdn_id.Value, getCoveredIndividualParams(hdn_EmployerTaxId.Value, ssn, txt_first.Text,txt_middlename.Text , 
                        txt_last.Text, txt_ssn.Text, txt_dob.Text, all, jan, feb, mar, apr, may, jun,jul, aug,sep,oct,nov, dec, "0"));
            }
        }

        public void Insert_Update_EmployeeCode(string EmployerTaxId, string ssn)
        {
            SqlParameter[] param = null;
            foreach (RepeaterItem item in rpt_code.Items)
            {
                #region get Repeater Data
                HiddenField hdn_filingyear = (HiddenField)item.FindControl("hdn_filingyear");
                HiddenField hdn_dependent = (HiddenField)item.FindControl("hdn_dependent");
                HiddenField hdn_flagEmp = (HiddenField)item.FindControl("hdn_flagEmp");
                HiddenField hdn_disable = (HiddenField)item.FindControl("hdn_disable");

                Label lbl_ALLM_COC = (Label)item.FindControl("lbl_ALLM_COC");
                Label lbl_JAN_COC = (Label)item.FindControl("lbl_JAN_COC");
                Label lbl_FEB_COC = (Label)item.FindControl("lbl_FEB_COC");
                Label lbl_MAR_COC = (Label)item.FindControl("lbl_MAR_COC");
                Label lbl_APR_COC = (Label)item.FindControl("lbl_APR_COC");
                Label lbl_MAY_COC = (Label)item.FindControl("lbl_MAY_COC");
                Label lbl_JUN_COC = (Label)item.FindControl("lbl_JUN_COC");
                Label lbl_JUL_COC = (Label)item.FindControl("lbl_JUL_COC");
                Label lbl_AUG_COC = (Label)item.FindControl("lbl_AUG_COC");
                Label lbl_SEP_COC = (Label)item.FindControl("lbl_SEP_COC");
                Label lbl_OCT_COC = (Label)item.FindControl("lbl_OCT_COC");
                Label lbl_NOV_COC = (Label)item.FindControl("lbl_NOV_COC");
                Label lbl_DEC_COC = (Label)item.FindControl("lbl_DEC_COC");

                Label lbl_ALLM_LCMP = (Label)item.FindControl("lbl_ALLM_LCMP");
                Label lbl_JAN_LCMP = (Label)item.FindControl("lbl_JAN_LCMP");
                Label lbl_FEB_LCMP = (Label)item.FindControl("lbl_FEB_LCMP");
                Label lbl_MAR_LCMP = (Label)item.FindControl("lbl_MAR_LCMP");
                Label lbl_APR_LCMP = (Label)item.FindControl("lbl_APR_LCMP");
                Label lbl_MAY_LCMP = (Label)item.FindControl("lbl_MAY_LCMP");
                Label lbl_JUN_LCMP = (Label)item.FindControl("lbl_JUN_LCMP");
                Label lbl_JUL_LCMP = (Label)item.FindControl("lbl_JUL_LCMP");
                Label lbl_AUG_LCMP = (Label)item.FindControl("lbl_AUG_LCMP");
                Label lbl_SEP_LCMP = (Label)item.FindControl("lbl_SEP_LCMP");
                Label lbl_OCT_LCMP = (Label)item.FindControl("lbl_OCT_LCMP");
                Label lbl_NOV_LCMP = (Label)item.FindControl("lbl_NOV_LCMP");
                Label lbl_DEC_LCMP = (Label)item.FindControl("lbl_DEC_LCMP");

                Label lbl_ALLM_SHC = (Label)item.FindControl("lbl_ALLM_SHC");
                Label lbl_JAN_SHC = (Label)item.FindControl("lbl_JAN_SHC");
                Label lbl_FEB_SHC = (Label)item.FindControl("lbl_FEB_SHC");
                Label lbl_MAR_SHC = (Label)item.FindControl("lbl_MAR_SHC");
                Label lbl_APR_SHC = (Label)item.FindControl("lbl_APR_SHC");
                Label lbl_MAY_SHC = (Label)item.FindControl("lbl_MAY_SHC");
                Label lbl_JUN_SHC = (Label)item.FindControl("lbl_JUN_SHC");
                Label lbl_JUL_SHC = (Label)item.FindControl("lbl_JUL_SHC");
                Label lbl_AUG_SHC = (Label)item.FindControl("lbl_AUG_SHC");
                Label lbl_SEP_SHC = (Label)item.FindControl("lbl_SEP_SHC");
                Label lbl_OCT_SHC = (Label)item.FindControl("lbl_OCT_SHC");
                Label lbl_NOV_SHC = (Label)item.FindControl("lbl_NOV_SHC");
                Label lbl_DEC_SHC = (Label)item.FindControl("lbl_DEC_SHC");
                #endregion

                #region Param
                param = new SqlParameter[47];
                param[0] = new SqlParameter("@EmployerTaxId", EmployerTaxId);
                param[1] = new SqlParameter("@ssn", ssn);
                param[2] = new SqlParameter("@filingYear", hdn_filingyear.Value);
                param[3] = new SqlParameter("@hourlyAmount", TextManipulation.toDBNULLfromEmpty(txt_hourly.Text));
                param[4] = new SqlParameter("@salaryAmount", TextManipulation.toDBNULLfromEmpty(txt_salary.Text));
                param[5] = new SqlParameter("@ALLM_COC", lbl_ALLM_COC.Text);
                param[6] = new SqlParameter("@JAN_COC", lbl_JAN_COC.Text);
                param[7] = new SqlParameter("@FEB_COC", lbl_FEB_COC.Text);
                param[8] = new SqlParameter("@MAR_COC", lbl_MAR_COC.Text);
                param[9] = new SqlParameter("@APR_COC", lbl_APR_COC.Text);
                param[10] = new SqlParameter("@MAY_COC", lbl_MAY_COC.Text);
                param[11] = new SqlParameter("@JUN_COC", lbl_JUN_COC.Text);
                param[12] = new SqlParameter("@JUL_COC", lbl_JUL_COC.Text);
                param[13] = new SqlParameter("@AUG_COC", lbl_AUG_COC.Text);
                param[14] = new SqlParameter("@SEP_COC", lbl_SEP_COC.Text);
                param[15] = new SqlParameter("@OCT_COC", lbl_OCT_COC.Text);
                param[16] = new SqlParameter("@NOV_COC", lbl_NOV_COC.Text);
                param[17] = new SqlParameter("@DEC_COC", lbl_DEC_COC.Text);
                param[18] = new SqlParameter("@ALLM_LCMP", lbl_ALLM_LCMP.Text);
                param[19] = new SqlParameter("@JAN_LCMP", lbl_JAN_LCMP.Text);
                param[20] = new SqlParameter("@FEB_LCMP", lbl_FEB_LCMP.Text);
                param[21] = new SqlParameter("@MAR_LCMP", lbl_MAR_LCMP.Text);
                param[22] = new SqlParameter("@APR_LCMP", lbl_APR_LCMP.Text);
                param[23] = new SqlParameter("@MAY_LCMP", lbl_MAY_LCMP.Text);
                param[24] = new SqlParameter("@JUN_LCMP", lbl_JUN_LCMP.Text);
                param[25] = new SqlParameter("@JUL_LCMP", lbl_JUL_LCMP.Text);
                param[26] = new SqlParameter("@AUG_LCMP", lbl_AUG_LCMP.Text);
                param[27] = new SqlParameter("@SEP_LCMP", lbl_SEP_LCMP.Text);
                param[28] = new SqlParameter("@OCT_LCMP", lbl_OCT_LCMP.Text);
                param[29] = new SqlParameter("@NOV_LCMP", lbl_NOV_LCMP.Text);
                param[30] = new SqlParameter("@DEC_LCMP", lbl_DEC_LCMP.Text);
                param[31] = new SqlParameter("@ALLM_SHC", lbl_ALLM_SHC.Text);
                param[32] = new SqlParameter("@JAN_SHC", lbl_JAN_SHC.Text);
                param[33] = new SqlParameter("@FEB_SHC", lbl_FEB_SHC.Text);
                param[34] = new SqlParameter("@MAR_SHC", lbl_MAR_SHC.Text);
                param[35] = new SqlParameter("@APR_SHC", lbl_APR_SHC.Text);
                param[36] = new SqlParameter("@MAY_SHC", lbl_MAY_SHC.Text);
                param[37] = new SqlParameter("@JUN_SHC", lbl_JUN_SHC.Text);
                param[38] = new SqlParameter("@JUL_SHC", lbl_JUL_SHC.Text);
                param[39] = new SqlParameter("@AUG_SHC", lbl_AUG_SHC.Text);
                param[40] = new SqlParameter("@SEP_SHC", lbl_SEP_SHC.Text);
                param[41] = new SqlParameter("@OCT_SHC", lbl_OCT_SHC.Text);
                param[42] = new SqlParameter("@NOV_SHC", lbl_NOV_SHC.Text);
                param[43] = new SqlParameter("@DEC_SHC", lbl_DEC_SHC.Text);
                param[44] = new SqlParameter("@isDependent", hdn_dependent.Value);
                param[45] = new SqlParameter("@flaggedEmployee", hdn_flagEmp.Value);
                param[46] = new SqlParameter("@disableCoding", hdn_disable.Value);
                #endregion

                objMaster.insert_Update_EmployeeCode(hdn_id.Value, param);
            }
        }

        #endregion

        #region Delete subtable records
        public void Delete_HireName(string EmployerTaxId, string ssn,DataTable dthire)
        {
            foreach (DataRow row in dthire.Rows)
            {
                objMaster.Delete_HireName(hdn_EmployerTaxId.Value, ssn, row["hirename"].ToString());
            }
        }

        public void Delete_Status(string EmployerTaxId, string ssn,DataTable dtstatus)
        {
            foreach (DataRow row in dtstatus.Rows)
            {
                objMaster.Delete_Status(hdn_EmployerTaxId.Value, ssn, row["statusName"].ToString());
            }
        }
        public void Delete_Enrollment(string EmployerTaxId, string ssn,DataTable dtEnrollment)
        {
            foreach (DataRow row in dtEnrollment.Rows)
            {
                objMaster.Delete_Enrollment(hdn_EmployerTaxId.Value, ssn, row["enrollmentName"].ToString());
            }
        }
        public void Delete_CoveredIndividual(string EmployerTaxId, string ssn,DataTable dtCovered)
        {
            foreach (DataRow row in dtCovered.Rows)
            {
                objMaster.Delete_CoveredIndividual(hdn_EmployerTaxId.Value, ssn, row["ssn"].ToString());
            }
        }
        


        #endregion

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            ClearEmployeeForm();
        }
        public void ClearEmployeeForm()
        {
            TabContainer1.ActiveTabIndex = 0;
            drp_employer.Text = null;
            txt_firstname.Text = null;
            txt_middlename.Text = null;
            txt_lastname.Text = null;
            txt_SSN.Text = null;
            txt_dob.Text = null;
            txt_salary.Text = null;
            txt_hourly.Text = null;
            txt_email.Text = null;
            txt_address1.Text = null;
            txt_address2.Text = null;
            txt_city.Text = null;
            drp_state.Text = null;
            txt_zipcode.Text = null;
            drp_country.Text = null;

            //set the data source to null for all the data tables
            //hire data
            DataTable dt_hiredata = dt_hiredate();
            dt_hiredata.Rows.Add(0, null, null);
            rptHiredata.DataSource = dt_hiredata;
            rptHiredata.DataBind();

            DataTable dt_Status = dtStatus();
            dt_Status.Rows.Add(0, null, null, null);
            rpt_Status.DataSource = dt_Status;
            rpt_Status.DataBind();

            DataTable dt_Coverage = dtCoverage();
            dt_Coverage.Rows.Add(0, 0, null, null, null, null, 0, null, null, 0, null, null);
            rpt_coverage.DataSource = dt_Coverage;
            rpt_coverage.DataBind();

            DataTable dtcoveredIndividula = dt_coveredIndividula();
            dtcoveredIndividula.Rows.Add(0, null, null, null, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            rpt_IndividualData.DataSource = dtcoveredIndividula;
            rpt_IndividualData.DataBind();

            rpt_code.DataSource = null;
            rpt_code.DataBind();
            txtsearch.Focus();
        }
        protected void btn_delete_Click(object sender, EventArgs e)
        {
        }
        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            list_Employee(hdn_companytax_id.Value, Convert.ToInt32(lbl_pagenum.Text), txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        #region Navigation
        protected void drp_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_Employee(hdn_companytax_id.Value, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            int next_page = Convert.ToInt32(lbl_pagenum.Text) + 1;
            if (pageCount >= next_page)
                list_Employee(hdn_companytax_id.Value, Convert.ToInt32(lbl_pagenum.Text) + 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_previous_Click(object sender, EventArgs e)
        {
            int prev_page = Convert.ToInt32(lbl_pagenum.Text) - 1;
            if (prev_page != 0)
                list_Employee(hdn_companytax_id.Value, Convert.ToInt32(lbl_pagenum.Text) - 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }
        protected void btn_first_Click(object sender, EventArgs e)
        {
            list_Employee(hdn_companytax_id.Value, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_last_Click(object sender, EventArgs e)
        {
            double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            list_Employee(hdn_companytax_id.Value, pageCount, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }
        #endregion

        #region Parameter

        private SqlParameter[] getEmployeeParams()
        {
            SqlParameter[] param = null;
            param = new SqlParameter[12];
            param[0] = new SqlParameter("@firstName", txt_firstname.Text);
            param[1] = new SqlParameter("@middleName", txt_middlename.Text);
            param[2] = new SqlParameter("@lastName", txt_lastname.Text);
            param[3] = new SqlParameter("@ssn", txt_SSN.Text);
            param[4] = new SqlParameter("@birthday", TextManipulation.toDBNULLfromEmpty(txt_dob.Text));
            param[5] = new SqlParameter("@address", txt_address1.Text);
            param[6] = new SqlParameter("@address2", txt_address2.Text);
            param[7] = new SqlParameter("@city", txt_city.Text);
            param[8] = new SqlParameter("@state", drp_state.Text);
            param[9] = new SqlParameter("@zip", txt_zipcode.Text);
            param[10] = new SqlParameter("@country", drp_country.Text);
            param[11] = new SqlParameter("@email", txt_email.Text);
            return param;
        }


        private SqlParameter[] getHireParams(string employerTaxId, string ssn, string hireName, string startDate, string endDate)
        {
            SqlParameter[] param = null;

            param = new SqlParameter[5];
            param[0] = new SqlParameter("@EmployerTaxId", employerTaxId);
            param[1] = new SqlParameter("@ssn", txt_SSN.Text.Trim());
            param[2] = new SqlParameter("@hireName", hireName);
            param[3] = new SqlParameter("@startDate",TextManipulation.toDBNULLfromEmpty(startDate));
            param[4] = new SqlParameter("@endDate", TextManipulation.toDBNULLfromEmpty(endDate));
            return param;
        }

        private SqlParameter[] getStatusParams(string employerTaxId, string employeeSSN, string statusName,string status, string startDate,string endDate)
            {
            SqlParameter[] param = null;

            param = new SqlParameter[6];
            param[0] = new SqlParameter("@EmployerTaxId", employerTaxId);
            param[1] = new SqlParameter("@ssn", employeeSSN);
            param[2] = new SqlParameter("@statusName", statusName);
            param[3] = new SqlParameter("@status", status);
            param[4] = new SqlParameter("@startDate", TextManipulation.toDBNULLfromEmpty(startDate));
            param[5] = new SqlParameter("@endDate", TextManipulation.toDBNULLfromEmpty(endDate));
            return param;
        }

        private SqlParameter[] getEnrollmentParams(string employerTaxId, string employeeSSN, string planName, string enrollmentName, string unionMember, string contributionStartDate, string contributionEndDate, string coverageOfferDate, string isEnrolled, 
            string coverageStartDate,  string coverageEndDate, string cobraEnrolled, string cobraStartDate, string cobraEndDate)
        {
            SqlParameter[] param = null;
            param = new SqlParameter[14];
            param[0] = new SqlParameter("@EmployerTaxId", employerTaxId);
            param[1] = new SqlParameter("@ssn", employeeSSN);
            param[2] = new SqlParameter("@planName", planName.toDBNULLfromEmpty());
            param[3] = new SqlParameter("@enrollmentName", enrollmentName);
            param[4] = new SqlParameter("@unionMember", unionMember);
            param[5] = new SqlParameter("@contributionStartDate", TextManipulation.toDBNULLfromEmpty(contributionStartDate));
            param[6] = new SqlParameter("@contributionEndDate", TextManipulation.toDBNULLfromEmpty(contributionEndDate));
            param[7] = new SqlParameter("@coverageOfferDate", TextManipulation.toDBNULLfromEmpty(coverageOfferDate));
            param[8] = new SqlParameter("@isEnrolled", isEnrolled);
            param[9] = new SqlParameter("@CoverageStartDate", TextManipulation.toDBNULLfromEmpty(coverageStartDate));
            param[10] = new SqlParameter("@coverageEndDate", TextManipulation.toDBNULLfromEmpty(coverageEndDate));
            param[11] = new SqlParameter("@COBRAEnrolled", cobraEnrolled);
            param[12] = new SqlParameter("@COBRAStartDate", TextManipulation.toDBNULLfromEmpty(cobraStartDate));
            param[13] = new SqlParameter("@COBRAEndDate", TextManipulation.toDBNULLfromEmpty(cobraEndDate));
            return param;
        }

        private SqlParameter[] getCoveredIndividualParams(string employerTaxId, string employeeSSN, string firstName, string middleName,
            string lastName, string ssn, string birthday, string allCoverage, string janCoverage, string febCoverage, string marCoverage,
            string aprCoverage, string mayCoverage, string junCoverage, string julCoverage, string augCoverage, string sepCoverage, 
            string octCoverage, string novCoverage, string decCoverage, string disableCoding)
        {
            SqlParameter[] param = null;
            param = new SqlParameter[21];
            param[0] = new SqlParameter("@EmployerTaxId", employerTaxId);
            param[1] = new SqlParameter("@ssnEmployee", employeeSSN);
            param[2] = new SqlParameter("@firstName", firstName);
            param[3] = new SqlParameter("@middleName", middleName);
            param[4] = new SqlParameter("@lastName", lastName);
            param[5] = new SqlParameter("@ssn", ssn);
            param[6] = new SqlParameter("@birthday", TextManipulation.toDBNULLfromEmpty(birthday));
            param[7] = new SqlParameter("@allCoverage", allCoverage);
            param[8] = new SqlParameter("@janCoverage", janCoverage);
            param[9] = new SqlParameter("@febCoverage", febCoverage);
            param[10] = new SqlParameter("@marCoverage", marCoverage);
            param[11] = new SqlParameter("@aprCoverage", aprCoverage);
            param[12] = new SqlParameter("@mayCoverage", mayCoverage);
            param[13] = new SqlParameter("@junCoverage", junCoverage);
            param[14] = new SqlParameter("@julCoverage", julCoverage);
            param[15] = new SqlParameter("@augCoverage", augCoverage);
            param[16] = new SqlParameter("@sepCoverage", sepCoverage);
            param[17] = new SqlParameter("@octCoverage", octCoverage);
            param[18] = new SqlParameter("@novCoverage", novCoverage);
            param[19] = new SqlParameter("@decCoverage", decCoverage);
            param[20] = new SqlParameter("@disableCoding", disableCoding);
            return param;
        }
        
        #endregion

        #region Add hiredata

        public DataTable dt_hiredate()
        {
            DataTable dt_hiredata = new DataTable();
            dt_hiredata.Columns.AddRange(new DataColumn[4] {
                            new DataColumn("Id", typeof(string)),
                            new DataColumn("startDate", typeof(string)),
                            new DataColumn("endDate",typeof(string)),
                            new DataColumn("hirename",typeof(string))});

            return dt_hiredata;
        }

        protected void rptHiredata_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            TextBox txt_startdate = (TextBox)e.Item.FindControl("txt_startdate");
            Button btn_hireplus = (Button)e.Item.FindControl("btn_hireplus");
            Button btn_hireminus = (Button)e.Item.FindControl("btn_hireminus");
            if (txt_startdate.Text != "")
            {
                btn_hireminus.Visible = true;
                btn_hireplus.Visible = false;
            }
            else
            {
                btn_hireplus.Visible = true;
                btn_hireminus.Visible = false;
            }
        }
        protected void btn_hireplus_Click(object sender, EventArgs e)
        {
            DataTable dt_hiredata = dt_hiredate();
            foreach (RepeaterItem item in rptHiredata.Items)
            {
                HiddenField hdn_hireId = (HiddenField)item.FindControl("hdn_hireId");
                TextBox txt_startdate = (TextBox)item.FindControl("txt_startdate");
                TextBox txt_enddate = (TextBox)item.FindControl("txt_enddate");
                dt_hiredata.Rows.Add(hdn_hireId.Value, TextManipulation.FixDateFormat(txt_startdate.Text), TextManipulation.FixDateFormat(txt_enddate.Text));
            }
            dt_hiredata.Rows.Add(0, null, null);

            rptHiredata.DataSource = dt_hiredata;
            rptHiredata.DataBind();
        }
        protected void btn_hireminus_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            RepeaterItem citem = button.NamingContainer as RepeaterItem;
            int index = citem.ItemIndex;

            DataTable dt_hiredata = dt_hiredate();
            foreach (RepeaterItem item in rptHiredata.Items)
            {
                HiddenField hdn_hireId = (HiddenField)item.FindControl("hdn_hireId");
                TextBox txt_startdate = (TextBox)item.FindControl("txt_startdate");
                TextBox txt_enddate = (TextBox)item.FindControl("txt_enddate");
                dt_hiredata.Rows.Add(hdn_hireId.Value, TextManipulation.FixDateFormat(txt_startdate.Text), TextManipulation.FixDateFormat(txt_enddate.Text));
            }
            dt_hiredata.Rows[index].Delete();
            dt_hiredata.AcceptChanges();
            //dt_hiredata.Rows.Add(0, null, null);
            rptHiredata.DataSource = dt_hiredata;
            rptHiredata.DataBind();
        }

        #endregion

        #region Add Status

        public DataTable dtStatus()
        {
            DataTable dt_Status = new DataTable();
            dt_Status.Columns.AddRange(new DataColumn[5] {
                            new DataColumn("Id", typeof(int)),
                            new DataColumn("Status", typeof(string)),
                            new DataColumn("statusName", typeof(string)),
                            new DataColumn("startDate", typeof(string)),
                            new DataColumn("endDate",typeof(string))});
            return dt_Status;
        }

        protected void rpt_Status_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                //drp status
                DropDownList drp_status = (DropDownList)e.Item.FindControl("drp_status");
                drp_status.DataSource = objDrop.GetEmployeeStatus();
                drp_status.DataBind();
                drp_status.Items.Insert(0, new ListItem("--Select--", ""));

                HiddenField hdn_status = (HiddenField)e.Item.FindControl("hdn_status");
                TextBox txt_startdate = (TextBox)e.Item.FindControl("txt_startdate");
                Button btn_statusplus = (Button)e.Item.FindControl("btn_statusplus");
                Button btn_statusminus = (Button)e.Item.FindControl("btn_statusminus");
                if (hdn_status.Value != "")
                {
                    drp_status.Items.FindByValue(hdn_status.Value).Selected = true;
                }

                if (drp_status.Text != "" || txt_startdate.Text != "")
                {
                    btn_statusminus.Visible = true;
                    btn_statusplus.Visible = false;
                }
                else
                {
                    btn_statusplus.Visible = true;
                    btn_statusminus.Visible = false;
                }
            }
        }

        protected void btn_statusplus_Click(object sender, EventArgs e)
        {
            DataTable dt_Status = dtStatus();
            foreach (RepeaterItem item in rpt_Status.Items)
            {
                HiddenField hdn_statusId = (HiddenField)item.FindControl("hdn_statusId");
                DropDownList drp_status = (DropDownList)item.FindControl("drp_status");
                TextBox txt_startdate = (TextBox)item.FindControl("txt_startdate");
                TextBox txt_enddate = (TextBox)item.FindControl("txt_enddate");
                dt_Status.Rows.Add(hdn_statusId.Value, drp_status.Text, txt_startdate.Text, txt_enddate.Text);
            }
            dt_Status.Rows.Add(0, null, null, null);
            rpt_Status.DataSource = dt_Status;
            rpt_Status.DataBind();
        }
        protected void btn_statusminus_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            RepeaterItem citem = button.NamingContainer as RepeaterItem;
            int index = citem.ItemIndex;

            DataTable dt_Status = dtStatus();

            foreach (RepeaterItem item in rpt_Status.Items)
            {
                HiddenField hdn_statusId = (HiddenField)item.FindControl("hdn_statusId");
                DropDownList drp_status = (DropDownList)item.FindControl("drp_status");
                TextBox txt_startdate = (TextBox)item.FindControl("txt_startdate");
                TextBox txt_enddate = (TextBox)item.FindControl("txt_enddate");
                dt_Status.Rows.Add(hdn_statusId.Value, drp_status.Text, txt_startdate.Text, txt_enddate.Text);
            }
            //dt_Status.Rows.Add(0, null, null, null);
            dt_Status.Rows[index].Delete();
            dt_Status.AcceptChanges();
            rpt_Status.DataSource = dt_Status;
            rpt_Status.DataBind();
        }

        #endregion

        #region Add Employee Enrollment

        public DataTable dtCoverage()
        {
            DataTable dt_Coverage = new DataTable();
            dt_Coverage.Columns.AddRange(new DataColumn[13] {
                            new DataColumn("Id", typeof(int)),
                            new DataColumn("unionMember", typeof(int)),
                            new DataColumn("contributionStartDate", typeof(string)),
                            new DataColumn("contributionEndDate",typeof(string)),
                            new DataColumn("coverageOfferDate", typeof(string)),
                            new DataColumn("name", typeof(string)),
                            new DataColumn("isEnrolled",typeof(int)),
                            new DataColumn("coverageStartDate", typeof(string)),
                            new DataColumn("coverageEndDate", typeof(string)),
                            new DataColumn("cobraEnrolled", typeof(int)),
                            new DataColumn("cobraStartDate",typeof(string)),
                            new DataColumn("cobraEndDate",typeof(string)),
                            new DataColumn("enrollmentName", typeof(string))});
            return dt_Coverage;
        }
        protected void rpt_coverage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DropDownList drp_plan = (DropDownList)e.Item.FindControl("drp_plan");
            HiddenField hdn_planId = (HiddenField)e.Item.FindControl("hdn_planId");

            CheckBox chk_unionMember = (CheckBox)e.Item.FindControl("chk_unionMember");
            CheckBox chk_enrolled = (CheckBox)e.Item.FindControl("chk_enrolled");
            CheckBox chk_cobraEnrolled = (CheckBox)e.Item.FindControl("chk_cobraEnrolled");
            HiddenField hdn_unionMember = (HiddenField)e.Item.FindControl("hdn_unionMember");
            HiddenField hdn_enrolled = (HiddenField)e.Item.FindControl("hdn_enrolled");
            HiddenField hdn_cobraEnrolled = (HiddenField)e.Item.FindControl("hdn_cobraEnrolled");
            chk_unionMember.Checked = hdn_unionMember.Value == "0" ? false : true;
            chk_enrolled.Checked = hdn_enrolled.Value == "0" ? false : true;
            chk_cobraEnrolled.Checked = hdn_cobraEnrolled.Value == "0" ? false : true;

            TextBox txt_contributionStartDate = (TextBox)e.Item.FindControl("txt_contributionStartDate");
            TextBox txt_name = (TextBox)e.Item.FindControl("txt_name");
            Button btn_coverageplus = (Button)e.Item.FindControl("btn_coverageplus");
            Button btn_coverageminus = (Button)e.Item.FindControl("btn_coverageminus");

            if (txt_name.Text != "")
            {
                btn_coverageminus.Visible = true;
                btn_coverageplus.Visible = false;
            }
            else
            {
                btn_coverageplus.Visible = true;
                btn_coverageminus.Visible = false;
            }
        }

        protected void btn_coverageplus_Click(object sender, EventArgs e)
        {
            DataTable dt_Coverage = dtCoverage();

            foreach (RepeaterItem item in rpt_coverage.Items)
            {
                HiddenField hdn_coverageId = (HiddenField)item.FindControl("hdn_coverageId");
                HiddenField hdn_unionMember = (HiddenField)item.FindControl("hdn_unionMember");
                TextBox txt_contributionStartDate = (TextBox)item.FindControl("txt_contributionStartDate");
                TextBox txt_contributionEndDate = (TextBox)item.FindControl("txt_contributionEndDate");
                TextBox txt_coverageOfferDate = (TextBox)item.FindControl("txt_coverageOfferDate");
                TextBox txt_plan = (TextBox)item.FindControl("txt_name");
                HiddenField hdn_enrolled = (HiddenField)item.FindControl("hdn_enrolled");
                TextBox txt_coverageStartDate = (TextBox)item.FindControl("txt_coverageStartDate");
                TextBox txt_coverageEndDate = (TextBox)item.FindControl("txt_coverageEndDate");
                HiddenField hdn_cobraEnrolled = (HiddenField)item.FindControl("hdn_cobraEnrolled");
                TextBox txt_cobraStartDate = (TextBox)item.FindControl("txt_cobraStartDate");
                TextBox txt_cobraEndDate = (TextBox)item.FindControl("txt_cobraEndDate");
                dt_Coverage.Rows.Add(hdn_coverageId.Value, hdn_unionMember.Value, txt_contributionStartDate.Text, txt_contributionEndDate.Text,
                    txt_coverageOfferDate.Text, txt_plan.Text, hdn_enrolled.Value, txt_coverageStartDate.Text, txt_coverageEndDate.Text,
                    hdn_cobraEnrolled.Value, txt_cobraStartDate.Text, txt_cobraEndDate.Text);
            }
            dt_Coverage.Rows.Add(0, 0, null, null, null, null, 0, null, null, 0, null, null);
            rpt_coverage.DataSource = dt_Coverage;
            rpt_coverage.DataBind();
        }
        protected void btn_coverageminus_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            RepeaterItem citem = button.NamingContainer as RepeaterItem;
            int index = citem.ItemIndex;

            DataTable dt_Coverage = dtCoverage();

            foreach (RepeaterItem item in rpt_coverage.Items)
            {
                HiddenField hdn_coverageId = (HiddenField)item.FindControl("hdn_coverageId");
                HiddenField hdn_unionMember = (HiddenField)item.FindControl("hdn_unionMember");
                TextBox txt_contributionStartDate = (TextBox)item.FindControl("txt_contributionStartDate");
                TextBox txt_contributionEndDate = (TextBox)item.FindControl("txt_contributionEndDate");
                TextBox txt_coverageOfferDate = (TextBox)item.FindControl("txt_coverageOfferDate");
                TextBox txt_plan = (TextBox)item.FindControl("txt_name");
                HiddenField hdn_enrolled = (HiddenField)item.FindControl("hdn_enrolled");
                TextBox txt_coverageStartDate = (TextBox)item.FindControl("txt_coverageStartDate");
                TextBox txt_coverageEndDate = (TextBox)item.FindControl("txt_coverageEndDate");
                HiddenField hdn_cobraEnrolled = (HiddenField)item.FindControl("hdn_cobraEnrolled");
                TextBox txt_cobraStartDate = (TextBox)item.FindControl("txt_cobraStartDate");
                TextBox txt_cobraEndDate = (TextBox)item.FindControl("txt_cobraEndDate");
                dt_Coverage.Rows.Add(hdn_coverageId.Value, hdn_unionMember.Value, txt_contributionStartDate.Text, txt_contributionEndDate.Text,
                    txt_coverageOfferDate.Text, txt_plan.Text, hdn_enrolled.Value, txt_coverageStartDate.Text, txt_coverageEndDate.Text,
                    hdn_cobraEnrolled.Value, txt_cobraStartDate.Text, txt_cobraEndDate.Text);
            }
            //dt_Coverage.Rows.Add(0, 0, null, null, null, null, 0, null, null, 0, null, null);
            dt_Coverage.Rows[index].Delete();
            dt_Coverage.AcceptChanges();
            rpt_coverage.DataSource = dt_Coverage;
            rpt_coverage.DataBind();
        }

        #endregion

        #region Add Covered Individual

        public DataTable dt_coveredIndividula()
        {
            DataTable dt_coveredIndividula = new DataTable();
            dt_coveredIndividula.Columns.AddRange(new DataColumn[18] {
                            new DataColumn("Id", typeof(string)),
                            new DataColumn("firstName", typeof(string)),
                            new DataColumn("lastName",typeof(string)),
                            new DataColumn("employeeSSN", typeof(string)),
                            new DataColumn("birthday", typeof(string)),
                            new DataColumn("allCoverage",typeof(string)),
                            new DataColumn("janCoverage", typeof(string)),
                            new DataColumn("febCoverage", typeof(string)),
                            new DataColumn("marCoverage",typeof(string)),
                            new DataColumn("aprCoverage", typeof(string)),
                            new DataColumn("mayCoverage", typeof(string)),
                            new DataColumn("junCoverage",typeof(string)),
                            new DataColumn("julCoverage", typeof(string)),
                            new DataColumn("augCoverage", typeof(string)),
                            new DataColumn("sepCoverage",typeof(string)),
                            new DataColumn("octCoverage", typeof(string)),
                            new DataColumn("novCoverage", typeof(string)),
                            new DataColumn("decCoverage",typeof(string))});

            return dt_coveredIndividula;
        }

        protected void rpt_IndividualData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            TextBox txt_first = (TextBox)e.Item.FindControl("txt_first");
            Button btn_CIplus = (Button)e.Item.FindControl("btn_CIplus");
            Button btn_CIminus = (Button)e.Item.FindControl("btn_CIminus");
            if (txt_first.Text != "")
            {
                btn_CIminus.Visible = true;
                btn_CIplus.Visible = false;
            }
            else
            {
                btn_CIplus.Visible = true;
                btn_CIminus.Visible = false;
            }
        }

        protected void btn_CIplus_Click(object sender, EventArgs e)
        {
            DataTable dtcoveredIndividula = dt_coveredIndividula();
            foreach (RepeaterItem item in rpt_IndividualData.Items)
            {
                TextBox txt_first = (TextBox)item.FindControl("txt_first");
                TextBox txt_last = (TextBox)item.FindControl("txt_last");
                TextBox txt_ssn = (TextBox)item.FindControl("txt_ssn");
                TextBox txt_dob = (TextBox)item.FindControl("txt_dob");
                CheckBox chk_all = (CheckBox)item.FindControl("chk_all");
                CheckBox chk_jan = (CheckBox)item.FindControl("chk_jan");
                CheckBox chk_feb = (CheckBox)item.FindControl("chk_feb");
                CheckBox chk_mar = (CheckBox)item.FindControl("chk_mar");
                CheckBox chk_apr = (CheckBox)item.FindControl("chk_apr");
                CheckBox chk_may = (CheckBox)item.FindControl("chk_may");
                CheckBox chk_jun = (CheckBox)item.FindControl("chk_jun");
                CheckBox chk_jul = (CheckBox)item.FindControl("chk_jul");
                CheckBox chk_aug = (CheckBox)item.FindControl("chk_aug");
                CheckBox chk_sep = (CheckBox)item.FindControl("chk_sep");
                CheckBox chk_oct = (CheckBox)item.FindControl("chk_oct");
                CheckBox chk_nov = (CheckBox)item.FindControl("chk_nov");
                CheckBox chk_dec = (CheckBox)item.FindControl("chk_dec");
                HiddenField hdn_CI_id = (HiddenField)item.FindControl("hdn_CI_id");
                string all = (chk_all.Checked == true) ? "1" : "0"; string jan = (chk_jan.Checked == true) ? "1" : "0"; string feb = (chk_feb.Checked == true) ? "1" : "0";
                string mar = (chk_mar.Checked == true) ? "1" : "0"; string apr = (chk_apr.Checked == true) ? "1" : "0"; string may = (chk_may.Checked == true) ? "1" : "0";
                string jun = (chk_jun.Checked == true) ? "1" : "0"; string jul = (chk_jul.Checked == true) ? "1" : "0"; string aug = (chk_aug.Checked == true) ? "1" : "0";
                string sep = (chk_sep.Checked == true) ? "1" : "0"; string oct = (chk_oct.Checked == true) ? "1" : "0"; string nov = (chk_nov.Checked == true) ? "1" : "0";
                string dec = (chk_dec.Checked == true) ? "1" : "0";
                dtcoveredIndividula.Rows.Add(hdn_CI_id.Value, txt_first.Text, txt_last.Text, txt_ssn.Text, txt_dob.Text, all, jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec);
            }
            dtcoveredIndividula.Rows.Add(0, null, null, null, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            rpt_IndividualData.DataSource = dtcoveredIndividula;
            rpt_IndividualData.DataBind();
        }
        protected void btn_CIminus_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            RepeaterItem citem = button.NamingContainer as RepeaterItem;
            int index = citem.ItemIndex;

            DataTable dtcoveredIndividula = dt_coveredIndividula();
            foreach (RepeaterItem item in rpt_IndividualData.Items)
            {
                TextBox txt_first = (TextBox)item.FindControl("txt_first");
                TextBox txt_last = (TextBox)item.FindControl("txt_last");
                TextBox txt_ssn = (TextBox)item.FindControl("txt_ssn");
                TextBox txt_dob = (TextBox)item.FindControl("txt_dob");
                CheckBox chk_all = (CheckBox)item.FindControl("chk_all");
                CheckBox chk_jan = (CheckBox)item.FindControl("chk_jan");
                CheckBox chk_feb = (CheckBox)item.FindControl("chk_feb");
                CheckBox chk_mar = (CheckBox)item.FindControl("chk_mar");
                CheckBox chk_apr = (CheckBox)item.FindControl("chk_apr");
                CheckBox chk_may = (CheckBox)item.FindControl("chk_may");
                CheckBox chk_jun = (CheckBox)item.FindControl("chk_jun");
                CheckBox chk_jul = (CheckBox)item.FindControl("chk_jul");
                CheckBox chk_aug = (CheckBox)item.FindControl("chk_aug");
                CheckBox chk_sep = (CheckBox)item.FindControl("chk_sep");
                CheckBox chk_oct = (CheckBox)item.FindControl("chk_oct");
                CheckBox chk_nov = (CheckBox)item.FindControl("chk_nov");
                CheckBox chk_dec = (CheckBox)item.FindControl("chk_dec");
                HiddenField hdn_CI_id = (HiddenField)item.FindControl("hdn_CI_id");
                string all = (chk_all.Checked == true) ? "1" : "0"; string jan = (chk_jan.Checked == true) ? "1" : "0"; string feb = (chk_feb.Checked == true) ? "1" : "0";
                string mar = (chk_mar.Checked == true) ? "1" : "0"; string apr = (chk_apr.Checked == true) ? "1" : "0"; string may = (chk_may.Checked == true) ? "1" : "0";
                string jun = (chk_jun.Checked == true) ? "1" : "0"; string jul = (chk_jul.Checked == true) ? "1" : "0"; string aug = (chk_aug.Checked == true) ? "1" : "0";
                string sep = (chk_sep.Checked == true) ? "1" : "0"; string oct = (chk_oct.Checked == true) ? "1" : "0"; string nov = (chk_nov.Checked == true) ? "1" : "0";
                string dec = (chk_dec.Checked == true) ? "1" : "0";
                dtcoveredIndividula.Rows.Add(hdn_CI_id.Value, txt_first.Text, txt_last.Text, txt_ssn.Text, txt_dob.Text, all, jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec);
            }
            dtcoveredIndividula.Rows[index].Delete();
            dtcoveredIndividula.AcceptChanges();
            rpt_IndividualData.DataSource = dtcoveredIndividula;
            rpt_IndividualData.DataBind();
        }
        #endregion

        #region load Dropdown

        public void load_dropdown()
        {
            //State
            drp_state.DataSource = objDrop.Getstates();
            drp_state.DataBind();
            drp_state.Items.Insert(0, new ListItem("--Select--", ""));

            //Country
            drp_country.DataSource = objDrop.Getcountry();
            drp_country.DataBind();
            drp_country.Items.Insert(0, new ListItem("--Select--", ""));
        }

        #endregion

        #region DateFormat

        protected string FixDateFormat(string str)
        {
            DateTime date;
            if (DateTime.TryParse(str, out date))
            {
                return date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                return null;
            }
        }

        #endregion

        protected void Refresh(object sender, EventArgs e)
        {
            list_Employee(hdn_companytax_id.Value, 1, "", 10);
            txtsearch.Text = "";
            txtsearch.Focus();
        }
        protected void lbl_close_Click(object sender, EventArgs e)
        {
            lightDiv.Visible = false;
            fadeDiv.Visible = false;
        }

    }
}