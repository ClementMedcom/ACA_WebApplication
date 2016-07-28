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
                DataTable dt1 = ds.Tables[1];
                rptEmployee.DataSource = dt;
                rptEmployee.DataBind();
                if (dt1.Rows.Count > 0)
                {
                    hid_rowcount.Value = dt1.Rows[0]["RowCnt"].ToString();
                    lbl_pagenum.Text = dt1.Rows[0]["page_num"].ToString();
                    lbl_result.Text = "Showing Results " + dt1.Rows[0]["Start"] + "-" + dt1.Rows[0]["Endpage"] + " Out of " + dt1.Rows[0]["RowCnt"] + " Records";
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
                HiddenField hdn_Id = (HiddenField)e.Item.FindControl("hdn_Id");
                HiddenField hdn_employerTaxId = (HiddenField)e.Item.FindControl("hdn_employerTaxId");
                Label lbl_ssn = (Label)e.Item.FindControl("lbl_ssn");
                DataSet ds = objMaster.Edit_Employee(hdn_employerTaxId.Value, lbl_ssn.Text, hdn_Id.Value);
                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];
                DataTable dt3 = ds.Tables[2];
                DataTable dt4 = ds.Tables[3];
                DataTable dt5 = ds.Tables[4];
                DataTable dt6 = ds.Tables[5];
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
                    txt_salary.Text = dt6.Rows[0]["salaryAmount"].ToString();
                    txt_hourly.Text = dt6.Rows[0]["hourlyAmount"].ToString();
                    drp_state.Text = dt6.Rows[0]["state"].ToString();
                    drp_country.Text = dt6.Rows[0]["country"].ToString();
                    txt_email.Text= dt6.Rows[0]["email"].ToString();
                }
                dt1.Rows.Add(0, null, null);
                rptHiredata.DataSource = dt1;
                rptHiredata.DataBind();

                dt2.Rows.Add(0, null, null, null);
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
            DataTable dt_hire = Get_hireData();
            DataTable dt_status = Get_Status();
            DataTable dt_Coverage = GetCoverage();
            DataTable dt_CoveredIndividual = Get_Covered_Individual();
            int Result = objMaster.Insert_Update_Employee(hdn_id.Value, getEmployeeParams(dt_hire, dt_status, dt_Coverage, dt_CoveredIndividual));
            if (Result == 1)
            {
                if (hdn_id.Value == "0")
                    lbl_msg.Text = "Employer Added Successfully";
                else
                    lbl_msg.Text = "Employer Updated Successfully";
                lightDiv.Visible = true;
                fadeDiv.Visible = true;
                list_Employee(hdn_companytax_id.Value, 1, "", 10);
                ClearEmployeeForm();
            }
            else
            {
                lbl_msg.Text = "Sorry! Faild to Process";
                lightDiv.Visible = true;
                fadeDiv.Visible = true;
            }
        }

        #region Get DataTable Data

        private SqlParameter[] getEmployeeParams(DataTable dt_hire, DataTable dt_status, DataTable dt_Coverage, DataTable dt_CoveredIndividual)
        {
            SqlParameter[] param = null;
            param = new SqlParameter[17];
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
            param[12] = new SqlParameter("@dt_hire", dt_hire);
            param[13] = new SqlParameter("@dt_status", dt_status);
            param[14] = new SqlParameter("@dt_Coverage", dt_Coverage);
            param[15] = new SqlParameter("@dt_CoveredIndividual", dt_CoveredIndividual);
            param[16] = new SqlParameter("@EmployerTaxId", hdn_companytax_id.Value);
            return param;
        }

        public DataTable Get_hireData()
        {
            DataTable dt_hiredata = new DataTable();
            dt_hiredata.Columns.AddRange(new DataColumn[4] {
                            new DataColumn("Id", typeof(string)),
                            new DataColumn("hireName", typeof(string)),
                            new DataColumn("startDate", typeof(string)),
                            new DataColumn("endDate",typeof(string))});
            foreach (RepeaterItem item in rptHiredata.Items)
            {
                HiddenField hdn_hireId = (HiddenField)item.FindControl("hdn_hireId");
                TextBox txt_startdate = (TextBox)item.FindControl("txt_startdate");
                TextBox txt_enddate = (TextBox)item.FindControl("txt_enddate");
                if (txt_startdate.Text != "")
                    dt_hiredata.Rows.Add(hdn_hireId.Value, "hire" + item.ItemIndex, TextManipulation.toDBNULLfromEmpty(txt_startdate.Text), TextManipulation.toDBNULLfromEmpty(txt_enddate.Text));
            }
            return dt_hiredata;
        }

        public DataTable Get_Status()
        {
            DataTable dt_Status = new DataTable();
            dt_Status.Columns.AddRange(new DataColumn[5] {
                            new DataColumn("Id", typeof(int)),
                            new DataColumn("StatusName", typeof(string)),
                            new DataColumn("Status", typeof(string)),
                            new DataColumn("startDate", typeof(string)),
                            new DataColumn("endDate",typeof(string))});
            foreach (RepeaterItem item in rpt_Status.Items)
            {
                HiddenField hdn_statusId = (HiddenField)item.FindControl("hdn_statusId");
                DropDownList drp_status = (DropDownList)item.FindControl("drp_status");
                TextBox txt_startdate = (TextBox)item.FindControl("txt_startdate");
                TextBox txt_enddate = (TextBox)item.FindControl("txt_enddate");
                if (drp_status.Text != "" && txt_startdate.Text != "")
                    dt_Status.Rows.Add(hdn_statusId.Value, "Status" + item.ItemIndex, drp_status.Text, TextManipulation.toDBNULLfromEmpty(txt_startdate.Text), TextManipulation.toDBNULLfromEmpty(txt_enddate.Text));
            }
            return dt_Status;
        }

        public DataTable GetCoverage()
        {
            DataTable dt_Coverage = new DataTable();
            dt_Coverage.Columns.AddRange(new DataColumn[13] {
                            new DataColumn("Id", typeof(int)),
                            new DataColumn("EnrollmentName", typeof(string)),
                            new DataColumn("unionMember", typeof(int)),
                            new DataColumn("contributionStartDate", typeof(string)),
                            new DataColumn("contributionEndDate",typeof(string)),
                            new DataColumn("coverageOfferDate", typeof(string)),
                            new DataColumn("PlanName", typeof(string)),
                            new DataColumn("isEnrolled",typeof(int)),
                            new DataColumn("coverageStartDate", typeof(string)),
                            new DataColumn("coverageEndDate", typeof(string)),
                            new DataColumn("cobraEnrolled", typeof(int)),
                            new DataColumn("cobraStartDate",typeof(string)),
                            new DataColumn("cobraEndDate",typeof(string))});

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
                if (txt_contributionStartDate.Text != "")
                    dt_Coverage.Rows.Add(hdn_coverageId.Value, "Coverage" + item.ItemIndex, hdn_unionMember.Value, TextManipulation.toDBNULLfromEmpty(txt_contributionStartDate.Text), TextManipulation.toDBNULLfromEmpty(txt_contributionEndDate.Text),
                        TextManipulation.toDBNULLfromEmpty(txt_coverageOfferDate.Text), txt_name.Text, hdn_enrolled.Value, TextManipulation.toDBNULLfromEmpty(txt_coverageStartDate.Text), TextManipulation.toDBNULLfromEmpty(txt_coverageEndDate.Text),
                        hdn_cobraEnrolled.Value, TextManipulation.toDBNULLfromEmpty(txt_cobraStartDate.Text), TextManipulation.toDBNULLfromEmpty(txt_cobraEndDate.Text));
            }
            return dt_Coverage;
        }

        public DataTable Get_Covered_Individual()
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
                    dt_coveredIndividula.Rows.Add(hdn_CI_id.Value, txt_first.Text, txt_last.Text, txt_ssn.Text, TextManipulation.toDBNULLfromEmpty(txt_dob.Text), all, jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec);
            }
            return dt_coveredIndividula;
        }

        #endregion

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            ClearEmployeeForm();
        }
        public void ClearEmployeeForm()
        {
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
        protected void lbl_close_Click(object sender, EventArgs e)
        {
            lightDiv.Visible = false;
            fadeDiv.Visible = false;
        }

        #region Add hiredata

        public DataTable dt_hiredate()
        {
            DataTable dt_hiredata = new DataTable();
            dt_hiredata.Columns.AddRange(new DataColumn[3] {
                            new DataColumn("Id", typeof(string)),
                            new DataColumn("startDate", typeof(string)),
                            new DataColumn("endDate",typeof(string))});

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

        #region Add Coverage

        public DataTable dtCoverage()
        {
            DataTable dt_Coverage = new DataTable();
            dt_Coverage.Columns.AddRange(new DataColumn[12] {
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
                            new DataColumn("cobraEndDate",typeof(string))});
            return dt_Coverage;
        }
        protected void rpt_coverage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
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
            Button btn_coverageplus = (Button)e.Item.FindControl("btn_coverageplus");
            Button btn_coverageminus = (Button)e.Item.FindControl("btn_coverageminus");
            if (txt_contributionStartDate.Text != "")
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
                TextBox txt_name = (TextBox)item.FindControl("txt_name");
                HiddenField hdn_enrolled = (HiddenField)item.FindControl("hdn_enrolled");
                TextBox txt_coverageStartDate = (TextBox)item.FindControl("txt_coverageStartDate");
                TextBox txt_coverageEndDate = (TextBox)item.FindControl("txt_coverageEndDate");
                HiddenField hdn_cobraEnrolled = (HiddenField)item.FindControl("hdn_cobraEnrolled");
                TextBox txt_cobraStartDate = (TextBox)item.FindControl("txt_cobraStartDate");
                TextBox txt_cobraEndDate = (TextBox)item.FindControl("txt_cobraEndDate");
                dt_Coverage.Rows.Add(hdn_coverageId.Value, hdn_unionMember.Value, txt_contributionStartDate.Text, txt_contributionEndDate.Text,
                    txt_coverageOfferDate.Text, txt_name.Text, hdn_enrolled.Value, txt_coverageStartDate.Text, txt_coverageEndDate.Text,
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
                TextBox txt_name = (TextBox)item.FindControl("txt_name");
                HiddenField hdn_enrolled = (HiddenField)item.FindControl("hdn_enrolled");
                TextBox txt_coverageStartDate = (TextBox)item.FindControl("txt_coverageStartDate");
                TextBox txt_coverageEndDate = (TextBox)item.FindControl("txt_coverageEndDate");
                HiddenField hdn_cobraEnrolled = (HiddenField)item.FindControl("hdn_cobraEnrolled");
                TextBox txt_cobraStartDate = (TextBox)item.FindControl("txt_cobraStartDate");
                TextBox txt_cobraEndDate = (TextBox)item.FindControl("txt_cobraEndDate");
                dt_Coverage.Rows.Add(hdn_coverageId.Value, hdn_unionMember.Value, txt_contributionStartDate.Text, txt_contributionEndDate.Text,
                    txt_coverageOfferDate.Text, txt_name.Text, hdn_enrolled.Value, txt_coverageStartDate.Text, txt_coverageEndDate.Text,
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
                string all = (chk_all.Checked == true) ? "1" : "0";string jan = (chk_jan.Checked == true) ? "1" : "0";string feb = (chk_feb.Checked == true) ? "1" : "0";
                string mar = (chk_mar.Checked == true) ? "1" : "0";string apr = (chk_apr.Checked == true) ? "1" : "0";string may = (chk_may.Checked == true) ? "1" : "0";
                string jun = (chk_jun.Checked == true) ? "1" : "0";string jul = (chk_jul.Checked == true) ? "1" : "0";string aug = (chk_aug.Checked == true) ? "1" : "0";
                string sep = (chk_sep.Checked == true) ? "1" : "0";string oct = (chk_oct.Checked == true) ? "1" : "0";string nov = (chk_nov.Checked == true) ? "1" : "0";
                string dec = (chk_dec.Checked == true) ? "1" : "0";
                dtcoveredIndividula.Rows.Add(hdn_CI_id.Value, txt_first.Text, txt_last.Text, txt_ssn.Text, txt_dob.Text, all, jan, feb, mar, apr, may, jun, jul, aug,sep,oct, nov, dec);
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
    }
}