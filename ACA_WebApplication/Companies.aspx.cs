using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACA_WebApplication
{
    public partial class Companies : System.Web.UI.Page
    {
        Master_BLL objMaster = new Master_BLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Company_list(null, 1, "", 10);
            }
        }
        public void Company_list(string companyTaxId,int pageIndex, string search, int PageSize)
        {
            //try
            //{
            //    DataSet ds = objMaster.list_Company("",pageIndex, search, PageSize);
            //    DataTable dt = ds.Tables[0];
            //    DataTable dt1 = ds.Tables[1];
            //    rptCompany.DataSource = dt;
            //    rptCompany.DataBind();
            //    if (dt1.Rows.Count > 0)
            //    {
            //        hid_rowcount.Value = dt1.Rows[0]["RowCnt"].ToString();
            //        lbl_pagenum.Text = dt1.Rows[0]["page_num"].ToString();
            //        lbl_result.Text = "Showing Results " + dt1.Rows[0]["Start"] +"-"+ dt1.Rows[0]["Endpage"] + " Out of " + dt1.Rows[0]["RowCnt"] + " Records";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        protected void rptCompany_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string tax_id = e.CommandArgument.ToString();
                Session["Tax_Id"] = tax_id;
                Response.Redirect("~/Master/Employer_Details.aspx");
            }
        }
        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            //Company_list(null, Convert.ToInt32(lbl_pagenum.Text), txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        #region Navigation
        protected void drp_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Company_list(null, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            //double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            //int pageCount = (int)Math.Ceiling(dblPageCount);
            //int next_page = Convert.ToInt32(lbl_pagenum.Text) + 1;
            //if (pageCount >= next_page)
            //    Company_list(null, Convert.ToInt32(lbl_pagenum.Text) + 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_previous_Click(object sender, EventArgs e)
        {
            //int prev_page = Convert.ToInt32(lbl_pagenum.Text) - 1;
            //if (prev_page != 0)
            //    Company_list(null, Convert.ToInt32(lbl_pagenum.Text) - 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }
        protected void btn_first_Click(object sender, EventArgs e)
        {
            //Company_list(null, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_last_Click(object sender, EventArgs e)
        {
            //double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            //int pageCount = (int)Math.Ceiling(dblPageCount);
            //Company_list(null, pageCount, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }
        #endregion

    }
}