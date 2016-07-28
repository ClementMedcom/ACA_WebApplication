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
    public partial class Plan : System.Web.UI.Page
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
                    list_Plan(hdn_companytax_id.Value, 1, "", 10);
                    load_dropdown();
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[5]
                    {
                            new DataColumn("bandingValueStart",typeof(string)),
                            new DataColumn("bandingValueEnd",typeof(string)),
                            new DataColumn("bandingStartDate",typeof(string)),
                            new DataColumn("bandingEndDate",typeof(string)),
                            new DataColumn("amount",typeof(string))
                    });
                    dt.Rows.Add(null, null, null, null, null);
                    rpttable.DataSource = dt;
                    rpttable.DataBind();
                }
            }

        }

        public DataTable dt()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5]
            {
                            new DataColumn("bandingValueStart",typeof(string)),
                            new DataColumn("bandingValueEnd",typeof(string)),
                            new DataColumn("bandingStartDate",typeof(string)),
                            new DataColumn("bandingEndDate",typeof(string)),
                            new DataColumn("amount",typeof(string))
            });
            return dt;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt_plan = dt();
            foreach (RepeaterItem item in rpttable.Items)
            {
                TextBox txt1 = (TextBox)item.FindControl("tb1");
                TextBox txt2 = (TextBox)item.FindControl("tb2");
                TextBox txt3 = (TextBox)item.FindControl("tb3");
                TextBox txt4 = (TextBox)item.FindControl("tb4");
                TextBox txt5 = (TextBox)item.FindControl("tb5");
                dt_plan.Rows.Add(txt1.Text, txt2.Text, txt3.Text, txt4.Text, txt5.Text);
            }
            dt_plan.Rows.Add(null, null, null, null, null);
            rpttable.DataSource = dt_plan;
            rpttable.DataBind();
        }
        protected void btnMinus_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            RepeaterItem citem = button.NamingContainer as RepeaterItem;
            int index = citem.ItemIndex;

            DataTable dt_plan = dt();
            foreach (RepeaterItem item in rpttable.Items)
            {
                TextBox txt1 = (TextBox)item.FindControl("tb1");
                TextBox txt2 = (TextBox)item.FindControl("tb2");
                TextBox txt3 = (TextBox)item.FindControl("tb3");
                TextBox txt4 = (TextBox)item.FindControl("tb4");
                TextBox txt5 = (TextBox)item.FindControl("tb5");
                dt_plan.Rows.Add(txt1.Text, txt2.Text, txt3.Text, txt4.Text, txt5.Text);
            }
            dt_plan.Rows[index].Delete();
            dt_plan.AcceptChanges();
            //dt_plan.Rows.Add(null, null, null, null, null);
            rpttable.DataSource = dt_plan;
            rpttable.DataBind();
        }


        public void list_Plan(string companyTaxId, int pageIndex, string search, int PageSize)
        {
            try
            {
                DataSet ds = objMaster.list_Plan(companyTaxId, pageIndex, search, PageSize);
                DataTable dt = ds.Tables[0];
                DataTable dt1 = ds.Tables[1];
                rptPlan.DataSource = dt;
                rptPlan.DataBind();
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

        protected void rptPlan_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                HiddenField hdn_Plan_Id = (HiddenField)e.Item.FindControl("hdn_Plan_Id");
                HiddenField hdn_CompanyTax_Id = (HiddenField)e.Item.FindControl("hdn_CompanyTax_Id");
                DataSet ds = objMaster.getPlanDetails(hdn_CompanyTax_Id.Value);
                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];


                //var d1 = dt1.Select("").FirstOrDefault(x => (int)x["Id"] == Convert.ToInt32(hdn_Plan_Id.Value));

                IEnumerable<DataRow> temp1 = from id in dt1.AsEnumerable()
                                             where id.Field<int>("ID") == Convert.ToInt32(hdn_Plan_Id.Value)
                                             select id;

                // Create a table from the query.
                DataTable dt3 = temp1.CopyToDataTable<DataRow>();


                txt_planName.Text = dt3.Rows[0]["name"].ToString();

                if (dt3.Rows[0]["medicalPlan"].ToString() == "1" || dt3.Rows[0]["medicalPlan"].ToString() == "Medical Plan") drp_plantype.Text = "Medical Plan";
                else if (dt3.Rows[0]["medicalPlan"].ToString() == "0" || dt3.Rows[0]["medicalPlan"].ToString() == "COBRA Plan") drp_plantype.Text = "COBRA Plan";
                else drp_plantype.Text = "";

                if (dt3.Rows[0]["fundingType"].ToString() == "1" || dt3.Rows[0]["fundingType"].ToString() == "Fully-Insured") drp_fundingtype.Text = "Fully-Insured";
                else if (dt3.Rows[0]["fundingType"].ToString() == "0" || dt3.Rows[0]["fundingType"].ToString() == "Self-Funded") drp_fundingtype.Text = "Self-Funded";
                else drp_fundingtype.Text = "";

                if (dt3.Rows[0]["eligibile1stOfMonth"].ToString() == "1" || dt3.Rows[0]["eligibile1stOfMonth"].ToString() == "First of the Month Following") drp_waitingperiod.Text = "First of the Month Following";
                else if (dt3.Rows[0]["eligibile1stOfMonth"].ToString() == "0" || dt3.Rows[0]["eligibile1stOfMonth"].ToString() == "Next Day Following") drp_waitingperiod.Text = "Next Day Following";
                else drp_waitingperiod.Text = "";

                txt_days.Text = dt3.Rows[0]["waitingDays"].ToString();

                if (dt3.Rows[0]["offeredSpouse"].ToString() == "1" || dt3.Rows[0]["offeredSpouse"].ToString() == "Yes") drp_spouse.Text = "Yes";
                else if (dt3.Rows[0]["offeredSpouse"].ToString() == "0" || dt3.Rows[0]["offeredSpouse"].ToString() == "No") drp_spouse.Text = "No";
                else drp_spouse.Text = "";

                if (dt3.Rows[0]["offeredDependents"].ToString() == "1" || dt3.Rows[0]["offeredDependents"].ToString() == "Yes") drp_dependence.Text = "Yes";
                else if (dt3.Rows[0]["offeredDependents"].ToString() == "0" || dt3.Rows[0]["offeredDependents"].ToString() == "No") drp_dependence.Text = "No";
                else drp_dependence.Text = "";

                if (dt3.Rows[0]["planTermTermination"].ToString() == "1" || dt3.Rows[0]["planTermTermination"].ToString() == "Yes") drp_termination.Text = "Yes";
                else if (dt3.Rows[0]["planTermTermination"].ToString() == "0" || dt3.Rows[0]["planTermTermination"].ToString() == "No") drp_termination.Text = "No";
                else drp_termination.Text = "";

                drp_renewalmonth.Text = dt3.Rows[0]["planRenewal"].ToString();

                if (dt3.Rows[0]["minimumValue"].ToString() == "1" || dt3.Rows[0]["minimumValue"].ToString() == "Yes") drp_minvalue.Text = "Yes";
                else if (dt3.Rows[0]["minimumValue"].ToString() == "0" || dt3.Rows[0]["minimumValue"].ToString() == "No") drp_minvalue.Text = "No";
                else drp_minvalue.Text = "";

                if (dt3.Rows[0]["bandingType"].ToString() != "NULL")
                    drp_bandingtype.Text = dt3.Rows[0]["bandingType"].ToString();
                else drp_bandingtype.Text = "";

                //bind code table

                rpt_codetbl.DataSource = dt3;
                rpt_codetbl.DataBind();

                //bind banding table
                IEnumerable<DataRow> temp2 = from id in dt2.AsEnumerable()
                                             where id.Field<int>("EmployerPlanId") == Convert.ToInt32(hdn_Plan_Id.Value)
                                             select id;
                DataTable dt4 = new DataTable();
                if (temp2.Count<DataRow>() > 0)
                {
                    dt4 = temp2.CopyToDataTable<DataRow>();
                    DataRow dr;
                    dr = dt4.NewRow();
                    dr["id"] = DBNull.Value;
                    dr["bandingName"] = null;
                    dr["EmployerPlanId"] = DBNull.Value;
                    dr["bandingValueStart"] = DBNull.Value;
                    dr["bandingValueEnd"] = DBNull.Value;
                    dr["bandingStartDate"] = DBNull.Value;
                    dr["bandingEndDate"] = DBNull.Value;
                    dr["amount"] = DBNull.Value;
                    dt4.Rows.Add(dr);
                    rpttable.DataSource = dt4;
                    rpttable.DataBind();
                }
                else
                {
                    DataTable dt_plan = dt();
                    dt_plan.Rows.Add(null, null, null, null, null);
                    rpttable.DataSource = dt_plan;
                    rpttable.DataBind();
                }




            }
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            list_Plan(hdn_companytax_id.Value, Convert.ToInt32(lbl_pagenum.Text), txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        #region Navigation
        protected void drp_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_Plan(hdn_companytax_id.Value, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            int next_page = Convert.ToInt32(lbl_pagenum.Text) + 1;
            if (pageCount >= next_page)
                list_Plan(hdn_companytax_id.Value, Convert.ToInt32(lbl_pagenum.Text) + 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_previous_Click(object sender, EventArgs e)
        {
            int prev_page = Convert.ToInt32(lbl_pagenum.Text) - 1;
            if (prev_page != 0)
                list_Plan(hdn_companytax_id.Value, Convert.ToInt32(lbl_pagenum.Text) - 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }
        protected void btn_first_Click(object sender, EventArgs e)
        {
            list_Plan(hdn_companytax_id.Value, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_last_Click(object sender, EventArgs e)
        {
            double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            list_Plan(hdn_companytax_id.Value, pageCount, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }
        #endregion

        public void load_dropdown()
        {
            //filling fund type
            drp_fundingtype.DataSource = objDrop.Getfundtype();
            drp_fundingtype.DataBind();
            drp_fundingtype.Items.Insert(0, new ListItem("--Select--", ""));

            // filling plan type
            drp_plantype.DataSource = objDrop.Getplantype();
            drp_plantype.DataBind();
            drp_plantype.Items.Insert(0, new ListItem("--Select--", ""));

            //filling waiting period
            drp_waitingperiod.DataSource = objDrop.Getwaitingperiod();
            drp_waitingperiod.DataBind();
            drp_waitingperiod.Items.Insert(0, new ListItem("--Select--", ""));

            //filling spouse
            drp_spouse.DataSource = objDrop.Getyes_no();
            drp_spouse.DataBind();
            drp_spouse.Items.Insert(0, new ListItem("--Select--", ""));

            //filling dependent
            drp_dependence.DataSource = objDrop.Getyes_no();
            drp_dependence.DataBind();
            drp_dependence.Items.Insert(0, new ListItem("--Select--", ""));

            //filling termination
            drp_termination.DataSource = objDrop.Getyes_no();
            drp_termination.DataBind();
            drp_termination.Items.Insert(0, new ListItem("--Select--", ""));

            // filling renewal
            drp_renewalmonth.DataSource = objDrop.Getrenewalmonth();
            drp_renewalmonth.DataBind();
            drp_renewalmonth.Items.Insert(0, new ListItem("--Select--", ""));

            //filling min value
            drp_minvalue.DataSource = objDrop.Getyes_no();
            drp_minvalue.DataBind();
            drp_minvalue.Items.Insert(0, new ListItem("--Select--", ""));

            //filling binding type
            drp_bandingtype.DataSource = objDrop.Getbandingtype();
            drp_bandingtype.DataBind();
            drp_bandingtype.Items.Insert(0, new ListItem("--Select--", ""));
        }

        protected void rpttable_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            TextBox tb1 = (TextBox)e.Item.FindControl("tb1");
            Button btnAdd = (Button)e.Item.FindControl("btnAdd");
            Button btnMinus = (Button)e.Item.FindControl("btnMinus");
            if (tb1.Text != "")
            {
                btnMinus.Visible = true;
                btnAdd.Visible = false;
            }
            else
            {
                btnMinus.Visible = false;
                btnAdd.Visible = true;
            }
        }
    }
}