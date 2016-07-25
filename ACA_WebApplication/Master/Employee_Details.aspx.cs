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
                }
            }
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

            }
        }
        
        protected void btn_Save_Click(object sender, EventArgs e)
        {
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
        }
        public void ClearEmployeeForm()
        {
            txt_employer_name.Text = null;
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
            DataTable dt_hiredata = new DataTable();
            dt_hiredata.Columns.AddRange(new DataColumn[3] {
                            new DataColumn("Id", typeof(int)),
                            new DataColumn("startDate", typeof(DateTime)),
                            new DataColumn("endDate",typeof(DateTime))});
            dt_hiredata.Rows.Add(0, null, null);
            rptHiredata.DataSource = dt_hiredata;
            rptHiredata.DataBind();

            DataTable dt_Status = new DataTable();
            dt_Status.Columns.AddRange(new DataColumn[4] {
                            new DataColumn("Id", typeof(int)),
                            new DataColumn("Status", typeof(string)),
                            new DataColumn("startDate", typeof(DateTime)),
                            new DataColumn("endDate",typeof(DateTime))});
            dt_Status.Rows.Add(0,null, null, null);
            rpt_Status.DataSource = dt_Status;
            rpt_Status.DataBind();

            DataTable dt_Coverage = new DataTable();
            dt_Coverage.Columns.AddRange(new DataColumn[12] {
                            new DataColumn("Id", typeof(int)),
                            new DataColumn("unionMember", typeof(int)),
                            new DataColumn("contributionStartDate", typeof(DateTime)),
                            new DataColumn("contributionEndDate",typeof(DateTime)),
                            new DataColumn("coverageOfferDate", typeof(DateTime)),
                            new DataColumn("name", typeof(string)),
                            new DataColumn("isEnrolled",typeof(int)),
                            new DataColumn("coverageStartDate", typeof(DateTime)),
                            new DataColumn("coverageEndDate", typeof(DateTime)),
                            new DataColumn("cobraEnrolled", typeof(int)),
                            new DataColumn("cobraStartDate",typeof(DateTime)),
                            new DataColumn("cobraEndDate",typeof(DateTime))});
            dt_Coverage.Rows.Add(0, 0, null, null, null, null, 0, null, null, 0, null, null);
            rpt_coverage.DataSource = dt_Coverage;
            rpt_coverage.DataBind();
            //eeCodes.ItemsSource = null;
            //eeCoveredIndividuals.ItemsSource = null;
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

        protected void btn_hireplus_Click(object sender, EventArgs e)
        {
            DataTable dt_hiredata = new DataTable();
            dt_hiredata.Columns.AddRange(new DataColumn[3] {
                            new DataColumn("Id", typeof(string)),
                            new DataColumn("startDate", typeof(string)),
                            new DataColumn("endDate",typeof(string))});

            foreach (RepeaterItem item in rptHiredata.Items)
            {
                HiddenField hdn_hireId = (HiddenField)item.FindControl("hdn_hireId");
                TextBox txt_startdate = (TextBox)item.FindControl("txt_startdate");
                TextBox txt_enddate = (TextBox)item.FindControl("txt_enddate");
                dt_hiredata.Rows.Add(hdn_hireId.Value, txt_startdate.Text, txt_enddate.Text);
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

            DataTable dt_hiredata = new DataTable();
            dt_hiredata.Columns.AddRange(new DataColumn[3] {
                            new DataColumn("Id", typeof(string)),
                            new DataColumn("startDate", typeof(string)),
                            new DataColumn("endDate",typeof(string))});
            foreach (RepeaterItem item in rptHiredata.Items)
            {
                HiddenField hdn_hireId = (HiddenField)item.FindControl("hdn_hireId");
                TextBox txt_startdate = (TextBox)item.FindControl("txt_startdate");
                TextBox txt_enddate = (TextBox)item.FindControl("txt_enddate");
                dt_hiredata.Rows.Add(hdn_hireId.Value, txt_startdate.Text, txt_enddate.Text);
            }
            dt_hiredata.Rows[index].Delete();
            dt_hiredata.AcceptChanges();
            //dt_hiredata.Rows.Add(0, null, null);
            rptHiredata.DataSource = dt_hiredata;
            rptHiredata.DataBind();
        }

        #endregion

        #region Add Status

        protected void rpt_Status_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DropDownList drp_status = (DropDownList)e.Item.FindControl("drp_status");
            //drp status
            drp_status.DataSource = objDrop.GetEmployeeStatus();
            drp_status.DataBind();
            drp_status.Items.Insert(0, new ListItem("--Select--", ""));
        }

        protected void btn_statusplus_Click(object sender, EventArgs e)
        {
            DataTable dt_Status = new DataTable();
            dt_Status.Columns.AddRange(new DataColumn[4] {
                            new DataColumn("Id", typeof(int)),
                            new DataColumn("Status", typeof(string)),
                            new DataColumn("startDate", typeof(string)),
                            new DataColumn("endDate",typeof(string))});
            foreach (RepeaterItem item in rptHiredata.Items)
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

            DataTable dt_Status = new DataTable();
            dt_Status.Columns.AddRange(new DataColumn[4] {
                            new DataColumn("Id", typeof(int)),
                            new DataColumn("Status", typeof(string)),
                            new DataColumn("startDate", typeof(string)),
                            new DataColumn("endDate",typeof(string))});

            foreach (RepeaterItem item in rptHiredata.Items)
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
        }

        protected void btn_coverageplus_Click(object sender, EventArgs e)
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
           
            foreach (RepeaterItem item in rptHiredata.Items)
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

            foreach (RepeaterItem item in rptHiredata.Items)
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
    }
}