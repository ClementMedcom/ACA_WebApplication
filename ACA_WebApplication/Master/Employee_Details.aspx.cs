using BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        DataTable dt_plan = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Tax_Id"] != null)
                {
                    hdn_companytax_id.Value = Session["Tax_Id"].ToString();
                    list_Employee(hdn_companytax_id.Value, 1, "", 10,"");
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

            drp_emp.DataSource = dt;
            drp_emp.DataValueField = "name";
            drp_emp.DataTextField = "name";
            drp_emp.DataBind();
            drp_emp.Items.Insert(0, new ListItem("ALL", ""));

        }
        public void list_Employee(string companyTaxId, int pageIndex, string search, int PageSize,string employer)
        {
            try
            {
                DataSet ds = objMaster.list_Employee(companyTaxId, pageIndex, search, PageSize, Session["UserName"].ToString());
                DataTable dt = ds.Tables[0];
                
                IEnumerable<DataRow> query1 = from all_data in dt.AsEnumerable()
                                              where (employer!=""?
                                                    ((all_data.Field<string>("firstname").ToLower().StartsWith(search.ToLower()) 
                                                    || all_data.Field<string>("Lastname").ToLower().StartsWith(search.ToLower()) 
                                                    || all_data.Field<string>("ssn").ToLower().StartsWith(search.ToLower()))
                                                    && all_data.Field<string>("name") == employer) :
                                                    (all_data.Field<string>("firstname").ToLower().StartsWith(search.ToLower())
                                                    || all_data.Field<string>("Lastname").ToLower().StartsWith(search.ToLower())
                                                    || all_data.Field<string>("ssn").ToLower().StartsWith(search.ToLower())))
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
                                         where (employer != "" ?
                                                   ((all_data.Field<string>("firstname").ToLower().StartsWith(search.ToLower())
                                                   || all_data.Field<string>("Lastname").ToLower().StartsWith(search.ToLower())
                                                   || all_data.Field<string>("ssn").ToLower().StartsWith(search.ToLower()))
                                                   && all_data.Field<string>("name") == employer) :
                                                   (all_data.Field<string>("firstname").ToLower().StartsWith(search.ToLower())
                                                   || all_data.Field<string>("Lastname").ToLower().StartsWith(search.ToLower())
                                                   || all_data.Field<string>("ssn").ToLower().StartsWith(search.ToLower())))
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
                    lbl_result.Text = "Showing " + start_record + "-" + End_record + " Out of " + total_rows + " Records";
                }
                else
                {
                    rptEmployee.DataSource = null;
                    rptEmployee.DataBind();
                    hid_rowcount.Value = "0";
                    lbl_pagenum.Text = "1";
                    lbl_result.Text = "Showing " + 0 + "-" + 0 + " Out of " + 0 + " Records";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchPlan(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DataSet ds = new DataSet();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Liveconnection"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "usp_EmployerPlanSelect";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompanyTaxID", HttpContext.Current.Session["Tax_Id"].ToString());
                    cmd.Parameters.AddWithValue("@name", null);
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataAdapter objadp = new SqlDataAdapter();
                    objadp.SelectCommand = cmd;
                    objadp.Fill(ds);
                    conn.Close();
                    DataTable dt = ds.Tables[0];
                    HttpContext.Current.Session["dt_plan"] = dt;

                    IEnumerable<DataRow> query1 = from all_data in dt.AsEnumerable()
                                                  where all_data.Field<string>("name").ToLower().StartsWith(prefixText.ToLower())
                                                  orderby all_data.Field<string>("name")
                                                  select all_data;
                    List<string> planList = new List<string>();
                    if (query1.Any())
                    {
                        DataTable dt1 = query1.CopyToDataTable<DataRow>();
                        foreach (DataRow dr in dt1.Rows)
                        {
                            planList.Add(dr["name"].ToString());
                        }
                    }
                    else
                    {
                        planList.Add("");
                    }
                    return planList;
                }
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
                    drp_country.Text = dbCountry;
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

                //rpt_code.DataSource = dt4;
                //rpt_code.DataBind();
                #region EmployeCode 

                drp_1All.Text = dt4.Rows[0]["ALLM_COC"].ToString();
                drp_1Jan.Text = dt4.Rows[0]["JAN_COC"].ToString();
                drp_1Feb.Text = dt4.Rows[0]["FEB_COC"].ToString();
                drp_1Mar.Text = dt4.Rows[0]["MAR_COC"].ToString();
                drp_1Apr.Text = dt4.Rows[0]["APR_COC"].ToString();
                drp_1May.Text = dt4.Rows[0]["MAY_COC"].ToString();
                drp_1Jun.Text = dt4.Rows[0]["JUN_COC"].ToString();
                drp_1Jul.Text = dt4.Rows[0]["JUL_COC"].ToString();
                drp_1Aug.Text = dt4.Rows[0]["AUG_COC"].ToString();
                drp_1Sep.Text = dt4.Rows[0]["SEP_COC"].ToString();
                drp_1Oct.Text = dt4.Rows[0]["OCT_COC"].ToString();
                drp_1Nov.Text = dt4.Rows[0]["NOV_COC"].ToString();
                drp_1Dec.Text = dt4.Rows[0]["DEC_COC"].ToString();

                lbl_ALLM_LCMP.Text = dt4.Rows[0]["ALLM_LCMP"].ToString();
                lbl_JAN_LCMP.Text = dt4.Rows[0]["JAN_LCMP"].ToString();
                lbl_FEB_LCMP.Text = dt4.Rows[0]["FEB_LCMP"].ToString();
                lbl_MAR_LCMP.Text = dt4.Rows[0]["MAR_LCMP"].ToString();
                lbl_APR_LCMP.Text = dt4.Rows[0]["APR_LCMP"].ToString();
                lbl_MAY_LCMP.Text = dt4.Rows[0]["MAY_LCMP"].ToString();
                lbl_JUN_LCMP.Text = dt4.Rows[0]["JUN_LCMP"].ToString();
                lbl_JUL_LCMP.Text = dt4.Rows[0]["JUL_LCMP"].ToString();
                lbl_AUG_LCMP.Text = dt4.Rows[0]["AUG_LCMP"].ToString();
                lbl_SEP_LCMP.Text = dt4.Rows[0]["SEP_LCMP"].ToString();
                lbl_OCT_LCMP.Text = dt4.Rows[0]["OCT_LCMP"].ToString();
                lbl_NOV_LCMP.Text = dt4.Rows[0]["NOV_LCMP"].ToString();
                lbl_DEC_LCMP.Text = dt4.Rows[0]["DEC_LCMP"].ToString();

                drp_2All.Text = dt4.Rows[0]["ALLM_SHC"].ToString();
                drp_2Jan.Text = dt4.Rows[0]["JAN_SHC"].ToString();
                drp_2Feb.Text = dt4.Rows[0]["FEB_SHC"].ToString();
                drp_2Mar.Text = dt4.Rows[0]["MAR_SHC"].ToString();
                drp_2Apr.Text = dt4.Rows[0]["APR_SHC"].ToString();
                drp_2May.Text = dt4.Rows[0]["MAY_SHC"].ToString();
                drp_2Jun.Text = dt4.Rows[0]["JUN_SHC"].ToString();
                drp_2Jul.Text = dt4.Rows[0]["JUL_SHC"].ToString();
                drp_2Aug.Text = dt4.Rows[0]["AUG_SHC"].ToString();
                drp_2Sep.Text = dt4.Rows[0]["SEP_SHC"].ToString();
                drp_2Oct.Text = dt4.Rows[0]["OCT_SHC"].ToString();
                drp_2Nov.Text = dt4.Rows[0]["NOV_SHC"].ToString();
                drp_2Dec.Text = dt4.Rows[0]["DEC_SHC"].ToString();

                hdn_filingyear.Value= dt4.Rows[0]["filingYear"].ToString();
                hdn_dependent.Value = dt4.Rows[0]["isDependent"].ToString();
                hdn_flagEmp.Value = dt4.Rows[0]["flaggedEmployee"].ToString();
                hdn_disable.Value = dt4.Rows[0]["disableCoding"].ToString();
                #endregion
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
                list_Employee(hdn_companytax_id.Value, 1, "", 10,"");
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
                CheckBox chk_unionMember = (CheckBox)item.FindControl("chk_unionMember");
                CheckBox chk_enrolled = (CheckBox)item.FindControl("chk_enrolled");
                CheckBox chk_cobraEnrolled = (CheckBox)item.FindControl("chk_cobraEnrolled");
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
                    objMaster.insert_Update_Enrollment(hdn_id.Value, getEnrollmentParams(hdn_EmployerTaxId.Value, ssn, txt_name.Text, "coverage" + item.ItemIndex, (chk_unionMember.Checked == true ? "1" : "0"), txt_contributionStartDate.Text, txt_contributionEndDate.Text, txt_coverageOfferDate.Text,
                      (chk_enrolled.Checked == true ? "1" : "0"), txt_coverageStartDate.Text, txt_coverageEndDate.Text, (chk_cobraEnrolled.Checked == true ? "1" : "0"), txt_cobraStartDate.Text, txt_cobraEndDate.Text));
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
            
            #region Param
            param = new SqlParameter[47];
            param[0] = new SqlParameter("@EmployerTaxId", EmployerTaxId);
            param[1] = new SqlParameter("@ssn", ssn);
            param[2] = new SqlParameter("@filingYear", hdn_filingyear.Value);
            param[3] = new SqlParameter("@hourlyAmount", TextManipulation.toDBNULLfromEmpty(txt_hourly.Text));
            param[4] = new SqlParameter("@salaryAmount", TextManipulation.toDBNULLfromEmpty(txt_salary.Text));
            param[5] = new SqlParameter("@ALLM_COC", drp_1All.Text);
            param[6] = new SqlParameter("@JAN_COC", drp_1Jan.Text);
            param[7] = new SqlParameter("@FEB_COC", drp_1Feb.Text);
            param[8] = new SqlParameter("@MAR_COC", drp_1Mar.Text);
            param[9] = new SqlParameter("@APR_COC", drp_1Apr.Text);
            param[10] = new SqlParameter("@MAY_COC", drp_1May.Text);
            param[11] = new SqlParameter("@JUN_COC", drp_1Jun.Text);
            param[12] = new SqlParameter("@JUL_COC", drp_1Jul.Text);
            param[13] = new SqlParameter("@AUG_COC", drp_1Aug.Text);
            param[14] = new SqlParameter("@SEP_COC", drp_1Sep.Text);
            param[15] = new SqlParameter("@OCT_COC", drp_1Oct.Text);
            param[16] = new SqlParameter("@NOV_COC", drp_1Nov.Text);
            param[17] = new SqlParameter("@DEC_COC", drp_1Dec.Text);
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
            param[31] = new SqlParameter("@ALLM_SHC", drp_2All.Text);
            param[32] = new SqlParameter("@JAN_SHC", drp_2Jan.Text);
            param[33] = new SqlParameter("@FEB_SHC", drp_2Feb.Text);
            param[34] = new SqlParameter("@MAR_SHC", drp_2Mar.Text);
            param[35] = new SqlParameter("@APR_SHC", drp_2Apr.Text);
            param[36] = new SqlParameter("@MAY_SHC", drp_2May.Text);
            param[37] = new SqlParameter("@JUN_SHC", drp_2Jun.Text);
            param[38] = new SqlParameter("@JUL_SHC", drp_2Jul.Text);
            param[39] = new SqlParameter("@AUG_SHC", drp_2Aug.Text);
            param[40] = new SqlParameter("@SEP_SHC", drp_2Sep.Text);
            param[41] = new SqlParameter("@OCT_SHC", drp_2Oct.Text);
            param[42] = new SqlParameter("@NOV_SHC", drp_2Nov.Text);
            param[43] = new SqlParameter("@DEC_SHC", drp_2Dec.Text);
            param[44] = new SqlParameter("@isDependent", hdn_dependent.Value);
            param[45] = new SqlParameter("@flaggedEmployee", hdn_flagEmp.Value);
            param[46] = new SqlParameter("@disableCoding", hdn_disable.Value);
            #endregion

            objMaster.insert_Update_EmployeeCode(hdn_id.Value, param);
            
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

            #region EmployeCode 

            drp_1All.Text = null;
            drp_1Jan.Text = null;
            drp_1Feb.Text = null;
            drp_1Mar.Text = null;
            drp_1Apr.Text = null;
            drp_1May.Text = null;
            drp_1Jun.Text = null;
            drp_1Jul.Text = null;
            drp_1Aug.Text = null;
            drp_1Sep.Text = null;
            drp_1Oct.Text = null;
            drp_1Nov.Text = null;
            drp_1Dec.Text = null;

            lbl_ALLM_LCMP.Text = null;
            lbl_JAN_LCMP.Text = null;
            lbl_FEB_LCMP.Text = null;
            lbl_MAR_LCMP.Text = null;
            lbl_APR_LCMP.Text = null;
            lbl_MAY_LCMP.Text = null;
            lbl_JUN_LCMP.Text = null;
            lbl_JUL_LCMP.Text = null;
            lbl_AUG_LCMP.Text = null;
            lbl_SEP_LCMP.Text = null;
            lbl_OCT_LCMP.Text = null;
            lbl_NOV_LCMP.Text = null;
            lbl_DEC_LCMP.Text = null;

            drp_2All.Text = null;
            drp_2Jan.Text = null;
            drp_2Feb.Text = null;
            drp_2Mar.Text = null;
            drp_2Apr.Text = null;
            drp_2May.Text = null;
            drp_2Jun.Text = null;
            drp_2Jul.Text = null;
            drp_2Aug.Text = null;
            drp_2Sep.Text = null;
            drp_2Oct.Text = null;
            drp_2Nov.Text = null;
            drp_2Dec.Text = null;

            hdn_filingyear.Value = null;
            hdn_dependent.Value = "0";
            hdn_flagEmp.Value = "0";
            hdn_disable.Value = "0";
            #endregion
            txtsearch.Focus();
        }
        protected void btn_delete_Click(object sender, EventArgs e)
        {
        }

        #region Search

        protected void drp_emp_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_Employee(hdn_companytax_id.Value, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text),drp_emp.Text);
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            list_Employee(hdn_companytax_id.Value,1, txtsearch.Text, Convert.ToInt32(drp_count.Text), drp_emp.Text);
        }

        #endregion

        #region Navigation
        protected void drp_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_Employee(hdn_companytax_id.Value, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text), drp_emp.Text);
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            int next_page = Convert.ToInt32(lbl_pagenum.Text) + 1;
            if (pageCount >= next_page)
                list_Employee(hdn_companytax_id.Value, Convert.ToInt32(lbl_pagenum.Text) + 1, txtsearch.Text, Convert.ToInt32(drp_count.Text), drp_emp.Text);
        }

        protected void btn_previous_Click(object sender, EventArgs e)
        {
            int prev_page = Convert.ToInt32(lbl_pagenum.Text) - 1;
            if (prev_page != 0)
                list_Employee(hdn_companytax_id.Value, Convert.ToInt32(lbl_pagenum.Text) - 1, txtsearch.Text, Convert.ToInt32(drp_count.Text), drp_emp.Text);
        }
        protected void btn_first_Click(object sender, EventArgs e)
        {
            list_Employee(hdn_companytax_id.Value, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text), drp_emp.Text);
        }

        protected void btn_last_Click(object sender, EventArgs e)
        {
            double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            list_Employee(hdn_companytax_id.Value, pageCount, txtsearch.Text, Convert.ToInt32(drp_count.Text), drp_emp.Text);
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
            dt_Status.Columns.AddRange(new DataColumn[4] {
                            new DataColumn("Id", typeof(int)),
                            new DataColumn("Status", typeof(string)),
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
            DataTable dt_plan = (DataTable)HttpContext.Current.Session["dt_plan"];
            foreach (RepeaterItem item in rpt_coverage.Items)
            {
                CheckBox chk_unionMember = (CheckBox)item.FindControl("chk_unionMember");
                CheckBox chk_enrolled = (CheckBox)item.FindControl("chk_enrolled");
                CheckBox chk_cobraEnrolled = (CheckBox)item.FindControl("chk_cobraEnrolled");
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
                IEnumerable<DataRow> query1 = from all_data in dt_plan.AsEnumerable()
                                              where all_data.Field<string>("name") == txt_plan.Text.Trim()
                                              select all_data;
                if (query1.Any())
                {
                    dt_Coverage.Rows.Add(hdn_coverageId.Value, (chk_unionMember.Checked == true ? "1" : "0"), txt_contributionStartDate.Text, txt_contributionEndDate.Text,
                    txt_coverageOfferDate.Text, txt_plan.Text, (chk_enrolled.Checked == true ? "1" : "0"), txt_coverageStartDate.Text, txt_coverageEndDate.Text,
                    (chk_cobraEnrolled.Checked == true ? "1" : "0"), txt_cobraStartDate.Text, txt_cobraEndDate.Text);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invaild Plan Name : " + txt_plan.Text + "');", true);
                    txt_plan.Text = "";
                    return;
                }
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
                CheckBox chk_unionMember = (CheckBox)item.FindControl("chk_unionMember");
                CheckBox chk_enrolled = (CheckBox)item.FindControl("chk_enrolled");
                CheckBox chk_cobraEnrolled = (CheckBox)item.FindControl("chk_cobraEnrolled");
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
                dt_Coverage.Rows.Add(hdn_coverageId.Value, (chk_unionMember.Checked == true ? "1" : "0"), txt_contributionStartDate.Text, txt_contributionEndDate.Text,
                    txt_coverageOfferDate.Text, txt_plan.Text, (chk_enrolled.Checked == true ? "1" : "0"), txt_coverageStartDate.Text, txt_coverageEndDate.Text,
                    (chk_cobraEnrolled.Checked == true ? "1" : "0"), txt_cobraStartDate.Text, txt_cobraEndDate.Text);
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

        #region Add Code

        //protected void rpt_code_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    #region Hidden Fields
        //    HiddenField lbl_ALLM_COC = (HiddenField)e.Item.FindControl("lbl_ALLM_COC");
        //    HiddenField lbl_JAN_COC = (HiddenField)e.Item.FindControl("lbl_JAN_COC");
        //    HiddenField lbl_FEB_COC = (HiddenField)e.Item.FindControl("lbl_FEB_COC");
        //    HiddenField lbl_MAR_COC = (HiddenField)e.Item.FindControl("lbl_MAR_COC");
        //    HiddenField lbl_APR_COC = (HiddenField)e.Item.FindControl("lbl_APR_COC");
        //    HiddenField lbl_MAY_COC = (HiddenField)e.Item.FindControl("lbl_MAY_COC");
        //    HiddenField lbl_JUN_COC = (HiddenField)e.Item.FindControl("lbl_JUN_COC");
        //    HiddenField lbl_JUL_COC = (HiddenField)e.Item.FindControl("lbl_JUL_COC");
        //    HiddenField lbl_AUG_COC = (HiddenField)e.Item.FindControl("lbl_AUG_COC");
        //    HiddenField lbl_SEP_COC = (HiddenField)e.Item.FindControl("lbl_SEP_COC");
        //    HiddenField lbl_OCT_COC = (HiddenField)e.Item.FindControl("lbl_OCT_COC");
        //    HiddenField lbl_NOV_COC = (HiddenField)e.Item.FindControl("lbl_NOV_COC");
        //    HiddenField lbl_DEC_COC = (HiddenField)e.Item.FindControl("lbl_DEC_COC");

        //    HiddenField lbl_ALLM_SHC = (HiddenField)e.Item.FindControl("lbl_ALLM_SHC");
        //    HiddenField lbl_JAN_SHC = (HiddenField)e.Item.FindControl("lbl_JAN_SHC");
        //    HiddenField lbl_FEB_SHC = (HiddenField)e.Item.FindControl("lbl_FEB_SHC");
        //    HiddenField lbl_MAR_SHC = (HiddenField)e.Item.FindControl("lbl_MAR_SHC");
        //    HiddenField lbl_APR_SHC = (HiddenField)e.Item.FindControl("lbl_APR_SHC");
        //    HiddenField lbl_MAY_SHC = (HiddenField)e.Item.FindControl("lbl_MAY_SHC");
        //    HiddenField lbl_JUN_SHC = (HiddenField)e.Item.FindControl("lbl_JUN_SHC");
        //    HiddenField lbl_JUL_SHC = (HiddenField)e.Item.FindControl("lbl_JUL_SHC");
        //    HiddenField lbl_AUG_SHC = (HiddenField)e.Item.FindControl("lbl_AUG_SHC");
        //    HiddenField lbl_SEP_SHC = (HiddenField)e.Item.FindControl("lbl_SEP_SHC");
        //    HiddenField lbl_OCT_SHC = (HiddenField)e.Item.FindControl("lbl_OCT_SHC");
        //    HiddenField lbl_NOV_SHC = (HiddenField)e.Item.FindControl("lbl_NOV_SHC");
        //    HiddenField lbl_DEC_SHC = (HiddenField)e.Item.FindControl("lbl_DEC_SHC");
        //    #endregion

        //    #region Dropdown
        //    DropDownList drp_1All = (DropDownList)e.Item.FindControl("drp_1All");
        //    DropDownList drp_1Jan = (DropDownList)e.Item.FindControl("drp_1Jan");
        //    DropDownList drp_1Feb = (DropDownList)e.Item.FindControl("drp_1Feb");
        //    DropDownList drp_1Mar = (DropDownList)e.Item.FindControl("drp_1Mar");
        //    DropDownList drp_1Apr = (DropDownList)e.Item.FindControl("drp_1Apr");
        //    DropDownList drp_1May = (DropDownList)e.Item.FindControl("drp_1May");
        //    DropDownList drp_1Jun = (DropDownList)e.Item.FindControl("drp_1Jun");
        //    DropDownList drp_1Jul = (DropDownList)e.Item.FindControl("drp_1Jul");
        //    DropDownList drp_1Aug = (DropDownList)e.Item.FindControl("drp_1Aug");
        //    DropDownList drp_1Sep = (DropDownList)e.Item.FindControl("drp_1Sep");
        //    DropDownList drp_1Oct = (DropDownList)e.Item.FindControl("drp_1Oct");
        //    DropDownList drp_1Nov = (DropDownList)e.Item.FindControl("drp_1Nov");
        //    DropDownList drp_1Dec = (DropDownList)e.Item.FindControl("drp_1Dec");

        //    DropDownList drp_2All = (DropDownList)e.Item.FindControl("drp_2All");
        //    DropDownList drp_2Jan = (DropDownList)e.Item.FindControl("drp_2Jan");
        //    DropDownList drp_2Feb = (DropDownList)e.Item.FindControl("drp_2Feb");
        //    DropDownList drp_2Mar = (DropDownList)e.Item.FindControl("drp_2Mar");
        //    DropDownList drp_2Apr = (DropDownList)e.Item.FindControl("drp_2Apr");
        //    DropDownList drp_2May = (DropDownList)e.Item.FindControl("drp_2May");
        //    DropDownList drp_2Jun = (DropDownList)e.Item.FindControl("drp_2Jun");
        //    DropDownList drp_2Jul = (DropDownList)e.Item.FindControl("drp_2Jul");
        //    DropDownList drp_2Aug = (DropDownList)e.Item.FindControl("drp_2Aug");
        //    DropDownList drp_2Sep = (DropDownList)e.Item.FindControl("drp_2Sep");
        //    DropDownList drp_2Oct = (DropDownList)e.Item.FindControl("drp_2Oct");
        //    DropDownList drp_2Nov = (DropDownList)e.Item.FindControl("drp_2Nov");
        //    DropDownList drp_2Dec = (DropDownList)e.Item.FindControl("drp_2Dec");

        //    #endregion

        //    #region assign drp value
        //    drp_1All.Items.FindByValue(lbl_ALLM_COC.Value).Selected = true;
        //    drp_1Jan.Items.FindByValue(lbl_JAN_COC.Value).Selected = true;
        //    drp_1Feb.Items.FindByValue(lbl_FEB_COC.Value).Selected = true;
        //    drp_1Mar.Items.FindByValue(lbl_MAR_COC.Value).Selected = true;
        //    drp_1Apr.Items.FindByValue(lbl_APR_COC.Value).Selected = true;
        //    drp_1May.Items.FindByValue(lbl_MAY_COC.Value).Selected = true;
        //    drp_1Jun.Items.FindByValue(lbl_JUN_COC.Value).Selected = true;
        //    drp_1Jul.Items.FindByValue(lbl_JUL_COC.Value).Selected = true;
        //    drp_1Aug.Items.FindByValue(lbl_AUG_COC.Value).Selected = true;
        //    drp_1Sep.Items.FindByValue(lbl_SEP_COC.Value).Selected = true;
        //    drp_1Oct.Items.FindByValue(lbl_OCT_COC.Value).Selected = true;
        //    drp_1Nov.Items.FindByValue(lbl_NOV_COC.Value).Selected = true;
        //    drp_1Dec.Items.FindByValue(lbl_DEC_COC.Value).Selected = true;

        //    drp_2All.Items.FindByValue(lbl_ALLM_SHC.Value).Selected = true;
        //    drp_2Jan.Items.FindByValue(lbl_JAN_SHC.Value).Selected = true;
        //    drp_2Feb.Items.FindByValue(lbl_FEB_SHC.Value).Selected = true;
        //    drp_2Mar.Items.FindByValue(lbl_MAR_SHC.Value).Selected = true;
        //    drp_2Apr.Items.FindByValue(lbl_APR_SHC.Value).Selected = true;
        //    drp_2May.Items.FindByValue(lbl_MAY_SHC.Value).Selected = true;
        //    drp_2Jun.Items.FindByValue(lbl_JUN_SHC.Value).Selected = true;
        //    drp_2Jul.Items.FindByValue(lbl_JUL_SHC.Value).Selected = true;
        //    drp_2Aug.Items.FindByValue(lbl_AUG_SHC.Value).Selected = true;
        //    drp_2Sep.Items.FindByValue(lbl_SEP_SHC.Value).Selected = true;
        //    drp_2Oct.Items.FindByValue(lbl_OCT_SHC.Value).Selected = true;
        //    drp_2Nov.Items.FindByValue(lbl_NOV_SHC.Value).Selected = true;
        //    drp_2Dec.Items.FindByValue(lbl_DEC_SHC.Value).Selected = true;
        //    #endregion
        //}

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
            list_Employee(hdn_companytax_id.Value, 1, "", 10,"");
            txtsearch.Text = "";
            drp_emp.Text = "";
            txtsearch.Focus();
        }
        protected void lbl_close_Click(object sender, EventArgs e)
        {
            lightDiv.Visible = false;
            fadeDiv.Visible = false;
        }

        
    }
}