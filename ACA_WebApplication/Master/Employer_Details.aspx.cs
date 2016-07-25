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
    public partial class Employer_Details : System.Web.UI.Page
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
                    list_Employer(hdn_companytax_id.Value, 1, "", 10);
                    load_dropdown();
                    clearEmployerForm();
                }
            }
        }
        public void list_Employer(string companyTaxId, int pageIndex, string search, int PageSize)
        {
            try
            {
                DataSet ds = objMaster.list_Employer(companyTaxId, pageIndex, search, PageSize);
                DataTable dt = ds.Tables[0];
                DataTable dt1 = ds.Tables[1];
                rptEmployer.DataSource = dt;
                rptEmployer.DataBind();
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
        protected void rptEmployer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                HiddenField hdn_EmpTax_Id = (HiddenField)e.Item.FindControl("hdn_EmpTax_Id");
                HiddenField hdn_CompanyTax_Id = (HiddenField)e.Item.FindControl("hdn_CompanyTax_Id");
                DataTable dt = objMaster.getEmployerDetails(hdn_EmpTax_Id.Value, hdn_CompanyTax_Id.Value);
                DataRow dr = objMaster.getEmployerDetails(hdn_EmpTax_Id.Value, hdn_CompanyTax_Id.Value).Rows[0];
                string filig = dr["filingYear"].ToString();
                txt_employerName.Text = dr["name"].ToString();
                txt_ein.Text = dr["EmployerTaxID"].ToString().PadLeft(9, '0');
                if (filig != "0") { drp_fillingyear.Text = dr["filingYear"].ToString(); }
                txt_address1.Text = dr["address"].ToString();
                txt_address2.Text = dr["address2"].ToString();
                txt_city.Text = dr["city"].ToString();
                drp_state.Text = dr["state"].ToString();
                txt_zipcode.Text = dr["zip"].ToString();
                drp_country.Text = dr["country"].ToString();
                txt_contactname.Text = dr["contactName"].ToString();
                txt_contactphone.Text = dr["phoneNumber"].ToString();
                txt_title.Text = dr["signTitle"].ToString();
                drp_formtype.Text = dr["formType"].ToString();
                drp_origincode.Text = dr["originCode"].ToString();
                txt_shop.Text = dr["SHOPIdentifier"].ToString();
                hdn_isCompany.Value = dr["isCompany"].ToString();
                hdn_id.Value= dr["Id"].ToString().PadLeft(9, '0');
                OfferMethod.Checked = Convert.ToBoolean(string.IsNullOrWhiteSpace(dr["eligibility_A"].ToString()) ? 0 : dr["eligibility_A"]);
                OfferMethodRelief.Checked = Convert.ToBoolean(string.IsNullOrWhiteSpace(dr["eligibility_B"].ToString()) ? 0 : dr["eligibility_B"]);
                Section4980H.Checked = Convert.ToBoolean(string.IsNullOrWhiteSpace(dr["eligibility_C"].ToString()) ? 0 : dr["eligibility_C"]);
                OfferMethod98.Checked = Convert.ToBoolean(string.IsNullOrWhiteSpace(dr["eligibility_D"].ToString()) ? 0 : dr["eligibility_D"]);

                //create the data table for the datagrid. 
                DataTable part3Table = createDataTable();
                DataRow row;

                //create and add rows
                for (int i = 0; i < 13; i++)
                {
                    row = part3Table.NewRow();
                    if (i != 0)
                    {
                        row["month"] = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i);
                    }
                    else
                    {
                        row["month"] = "All 12 Months";
                    }

                    row["minimum"] = string.IsNullOrEmpty(dr[string.Join("_", "minimum", i.ToString())].ToString());
                    row["full"] = dr[string.Join("_", "fullTime", i.ToString())];
                    row["total"] = dr[string.Join("_", "total", i.ToString())];
                    row["aggregate"] = string.IsNullOrEmpty(dr[string.Join("_", "group", i.ToString())].ToString());
                    row["section"] = dr[string.Join("_", "S4980H", i.ToString())];
                    part3Table.Rows.Add(row);
                }
                rpt_montable.DataSource = part3Table;
                rpt_montable.DataBind();
                btn_delete.Visible = true;
            }
        }
        protected void rpt_montable_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CheckBox chk_minimum = (CheckBox)e.Item.FindControl("chk_minimum");
            CheckBox chk_aggregate = (CheckBox)e.Item.FindControl("chk_minimum");
            HiddenField hdn_chk_minimum = (HiddenField)e.Item.FindControl("hdn_chk_minimum");
            HiddenField hdn_chk_aggregate = (HiddenField)e.Item.FindControl("hdn_chk_aggregate");
            chk_minimum.Checked = hdn_chk_minimum.Value == "0" ? false : true;
            chk_aggregate.Checked = hdn_chk_aggregate.Value == "0" ? false : true;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            DataTable aleMonthly = dt_monthtable();

            int foreignAddress;
            if (!string.IsNullOrEmpty(drp_country.Text))
            {
                if (drp_country.Text.Equals("US"))
                {
                    foreignAddress = 0;
                }
                else
                {
                    foreignAddress = 1;
                }
            }
            else
            {
                foreignAddress = 1;
            }

            int result = objMaster.Insert_Update_Employer(hdn_id.Value, getEmployerParams(foreignAddress));
            if (result == 1)
            {
                if(hdn_id.Value=="0")
                    lbl_msg.Text = "Employer Added Successfully";
                else
                    lbl_msg.Text = "Employer Updated Successfully";
                lightDiv.Visible = true;
                fadeDiv.Visible = true;
                list_Employer(hdn_companytax_id.Value, 1, "", 10);
                clearEmployerForm();
            }
            else
            {
                lbl_msg.Text = "Sorry! Faild to Process";
                lightDiv.Visible = true;
                fadeDiv.Visible = true;
            }
        }
        protected void btn_reset_Click(object sender, EventArgs e)
        {
            clearEmployerForm();
        }
        protected void btn_delete_Click(object sender, EventArgs e)
        {
            int result = objMaster.Delete_Employer(txt_ein.Text);
            if (result > 0)
            {
                lbl_msg.Text = "Employer Delete Successfully";
                lightDiv.Visible = true;
                fadeDiv.Visible = true;
                list_Employer(hdn_companytax_id.Value, 1, "", 10);
                clearEmployerForm();
            }
            else
            {
                lbl_msg.Text = "Sorry! Faild to Process";
                lightDiv.Visible = true;
                fadeDiv.Visible = true;
            }
        }
        private void clearEmployerForm()
        {
            //this method sets the value of every textbox, combobox, and checkbox to null
            //in the employer tab
            hdn_id.Value = "0";
            hdn_isCompany.Value = "0";
            txt_employerName.Text = null;
            txt_ein.Text = null;
            drp_fillingyear.Text = null;
            txt_address1.Text = null;
            txt_address2.Text = null;
            txt_city.Text = null;
            drp_state.Text = null;
            txt_zipcode.Text = null;
            drp_country.Text = null;
            txt_contactname.Text = null;
            txt_contactphone.Text = null;
            txt_title.Text = null;
            drp_formtype.Text = null;
            drp_origincode.Text = null;
            txt_shop.Text = null;
            OfferMethod.Checked = false;
            OfferMethodRelief.Checked = false;
            Section4980H.Checked = false;
            OfferMethod98.Checked = false;
            DataTable part3Table = createDataTable();
            DataRow row;

            //create and add rows
            for (int i = 0; i < 13; i++)
            {
                row = part3Table.NewRow();
                if (i != 0)
                {
                    row["month"] = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i);
                }
                else
                {
                    row["month"] = "All 12 Months";
                }

                row["minimum"] = "0";
                row["full"] = DBNull.Value;
                row["total"] = DBNull.Value;
                row["aggregate"] = "0";
                row["section"] = DBNull.Value;
                part3Table.Rows.Add(row);
            }
            rpt_montable.DataSource = part3Table.DefaultView;
            rpt_montable.DataBind();
            btn_delete.Visible = false;
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            list_Employer(hdn_companytax_id.Value, Convert.ToInt32(lbl_pagenum.Text), txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        #region Navigation
        protected void drp_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_Employer(hdn_companytax_id.Value, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            int next_page = Convert.ToInt32(lbl_pagenum.Text) + 1;
            if (pageCount >= next_page)
                list_Employer(hdn_companytax_id.Value, Convert.ToInt32(lbl_pagenum.Text) + 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_previous_Click(object sender, EventArgs e)
        {
            int prev_page = Convert.ToInt32(lbl_pagenum.Text) - 1;
            if (prev_page != 0)
                list_Employer(hdn_companytax_id.Value, Convert.ToInt32(lbl_pagenum.Text) - 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }
        protected void btn_first_Click(object sender, EventArgs e)
        {
            list_Employer(hdn_companytax_id.Value, 1, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }

        protected void btn_last_Click(object sender, EventArgs e)
        {
            double dblPageCount = (double)((decimal)Convert.ToInt32(hid_rowcount.Value) / Convert.ToDecimal(drp_count.Text));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            list_Employer(hdn_companytax_id.Value, pageCount, txtsearch.Text, Convert.ToInt32(drp_count.Text));
        }
        #endregion

        #region load Dropdown

        public void load_dropdown()
        {
            //filling year
            drp_fillingyear.DataSource = objDrop.Getfilingyear();
            drp_fillingyear.DataBind();
            drp_fillingyear.Items.Insert(0, new ListItem("--Select--", ""));

            //State
            drp_state.DataSource = objDrop.Getstates();
            drp_state.DataBind();
            drp_state.Items.Insert(0, new ListItem("--Select--", ""));

            //Country
            drp_country.DataSource = objDrop.Getcountry();
            drp_country.DataBind();
            drp_country.Items.Insert(0, new ListItem("--Select--", ""));

            //form type
            drp_formtype.DataSource = objDrop.Getformtype();
            drp_formtype.DataBind();
            drp_formtype.Items.Insert(0, new ListItem("--Select--", ""));

            //Origin Code
            drp_origincode.DataSource = objDrop.Getorigincode();
            drp_origincode.DataBind();
            drp_origincode.Items.Insert(0, new ListItem("--Select--", ""));

        }

        #endregion

        protected void lbl_close_Click(object sender, EventArgs e)
        {
            lightDiv.Visible = false;
            fadeDiv.Visible = false;
        }
        private DataTable createDataTable()
        {
            //create the data table for the datagrid. 
            DataTable part3Table = new DataTable();
            DataColumn column;

            //create columns
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "month";
            part3Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.AllowDBNull = true;
            column.ColumnName = "minimum";
            part3Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.AllowDBNull = true;
            column.ColumnName = "full";
            part3Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.AllowDBNull = true;
            column.ColumnName = "total";
            part3Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.AllowDBNull = true;
            column.ColumnName = "aggregate";
            part3Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "section";
            part3Table.Columns.Add(column);

            return part3Table;
        }
        public DataTable dt_monthtable()
        {
            DataTable dt_month = new DataTable();
            dt_month.Columns.AddRange(new DataColumn[6] {
                            new DataColumn("month", typeof(string)),
                            new DataColumn("minimum", typeof(int)),
                            new DataColumn("full",typeof(int)),
                            new DataColumn("total",typeof(int)),
                            new DataColumn("aggregate",typeof(int)),
                            new DataColumn("section",typeof(string))});
            foreach (RepeaterItem item in rpt_montable.Items)
            {
                Label lbl_month = (Label)item.FindControl("lbl_month");
                CheckBox chk_minimum = (CheckBox)item.FindControl("chk_minimum");
                TextBox txt_full = (TextBox)item.FindControl("txt_full");
                TextBox txt_total = (TextBox)item.FindControl("txt_total");
                CheckBox chk_aggregate = (CheckBox)item.FindControl("chk_minimum");
                TextBox txt_section = (TextBox)item.FindControl("txt_section");
                int minimum = chk_minimum.Checked == true ? 1 : 0;
                int aggregate = chk_aggregate.Checked == true ? 1 : 0;
                int full = txt_full.Text == "" ? 0 : Convert.ToInt32(txt_full.Text);
                int total = txt_full.Text == "" ? 0 : Convert.ToInt32(txt_total.Text);
                dt_month.Rows.Add(lbl_month.Text, minimum, full, total, aggregate,txt_section.Text);
            }
            return dt_month;
        }
        private SqlParameter[] getEmployerParams(int foreignAddress)
        {
            DataTable aleMonthly = dt_monthtable();
            SqlParameter[] param = new SqlParameter[97];
            #region(params)
            param[0] = new SqlParameter("@CompanyTaxID",Session["Tax_Id"]);
            param[1] = new SqlParameter("@EmployerTaxID", txt_ein.Text);
            param[2] = new SqlParameter("@name", txt_employerName.Text);
            param[3] = new SqlParameter("@isForeignAddress", foreignAddress);
            param[4] = new SqlParameter("@address", txt_address1.Text);
            param[5] = new SqlParameter("@address2", txt_address2.Text);
            param[6] = new SqlParameter("@city", txt_city.Text);
            param[7] = new SqlParameter("@state", drp_state.Text);
            param[8] = new SqlParameter("@zip", txt_zipcode.Text);
            param[9] = new SqlParameter("@country", drp_country.Text);
            param[10] = new SqlParameter("@phoneNumber", txt_contactphone.Text);
            param[11] = new SqlParameter("@contactName", txt_contactname.Text);
            param[12] = new SqlParameter("@signTitle", txt_title.Text);
            param[13] = new SqlParameter("@signDate", null);
            param[14] = new SqlParameter("@originCode", drp_origincode.Text);
            param[15] = new SqlParameter("@SHOPIdentifier", txt_shop.Text);
            param[16] = new SqlParameter("@totalNumberEE", 0);
            param[17] = new SqlParameter("@totalNumberForms", 0);
            param[18] = new SqlParameter("@eligibility_A", OfferMethod.Checked == true ? 1 : 0);
            param[19] = new SqlParameter("@eligibility_B", OfferMethodRelief.Checked == true ? 1 : 0);
            param[20] = new SqlParameter("@eligibility_C", Section4980H.Checked == true ? 1 : 0);
            param[21] = new SqlParameter("@eligibility_D", OfferMethod98.Checked == true ? 1 : 0);
            param[22] = new SqlParameter("@lockEmployer", 0);
            param[23] = new SqlParameter("@formType", drp_formtype.Text);
            param[24] = new SqlParameter("@isCompany", hdn_isCompany.Value);
            param[25] = new SqlParameter("@status", null);
            param[26] = new SqlParameter("@teamId", null);
            param[27] = new SqlParameter("@lockCompany", 0);
            param[28] = new SqlParameter("@lastEdit", null);
            param[29] = new SqlParameter("@filingYear", drp_fillingyear.Text);
            param[30] = new SqlParameter("@lastUploadDate", null);
            param[31] = new SqlParameter("@minimum_0", aleMonthly.Rows[0][1]);
            param[32] = new SqlParameter("@minimum_1", aleMonthly.Rows[1][1]);
            param[33] = new SqlParameter("@minimum_2", aleMonthly.Rows[2][1]);
            param[34] = new SqlParameter("@minimum_3", aleMonthly.Rows[3][1]);
            param[35] = new SqlParameter("@minimum_4", aleMonthly.Rows[4][1]);
            param[36] = new SqlParameter("@minimum_5", aleMonthly.Rows[5][1]);
            param[37] = new SqlParameter("@minimum_6", aleMonthly.Rows[6][1]);
            param[38] = new SqlParameter("@minimum_7", aleMonthly.Rows[7][1]);
            param[39] = new SqlParameter("@minimum_8", aleMonthly.Rows[8][1]);
            param[40] = new SqlParameter("@minimum_9", aleMonthly.Rows[9][1]);
            param[41] = new SqlParameter("@minimum_10", aleMonthly.Rows[10][1]);
            param[42] = new SqlParameter("@minimum_11", aleMonthly.Rows[11][1]);
            param[43] = new SqlParameter("@minimum_12", aleMonthly.Rows[12][1]);
            param[44] = new SqlParameter("@fullTime_0", aleMonthly.Rows[0][2]);
            param[45] = new SqlParameter("@fullTime_1", aleMonthly.Rows[1][2]);
            param[46] = new SqlParameter("@fullTime_2", aleMonthly.Rows[2][2]);
            param[47] = new SqlParameter("@fullTime_3", aleMonthly.Rows[3][2]);
            param[48] = new SqlParameter("@fullTime_4", aleMonthly.Rows[4][2]);
            param[49] = new SqlParameter("@fullTime_5", aleMonthly.Rows[5][2]);
            param[50] = new SqlParameter("@fullTime_6", aleMonthly.Rows[6][2]);
            param[51] = new SqlParameter("@fullTime_7", aleMonthly.Rows[7][2]);
            param[52] = new SqlParameter("@fullTime_8", aleMonthly.Rows[8][2]);
            param[53] = new SqlParameter("@fullTime_9", aleMonthly.Rows[9][2]);
            param[54] = new SqlParameter("@fullTime_10", aleMonthly.Rows[10][2]);
            param[55] = new SqlParameter("@fullTime_11", aleMonthly.Rows[11][2]);
            param[56] = new SqlParameter("@fullTime_12", aleMonthly.Rows[12][2]);
            param[57] = new SqlParameter("@total_0", aleMonthly.Rows[0][3]);
            param[58] = new SqlParameter("@total_1", aleMonthly.Rows[1][3]);
            param[59] = new SqlParameter("@total_2", aleMonthly.Rows[2][3]);
            param[60] = new SqlParameter("@total_3", aleMonthly.Rows[3][3]);
            param[61] = new SqlParameter("@total_4", aleMonthly.Rows[4][3]);
            param[62] = new SqlParameter("@total_5", aleMonthly.Rows[5][3]);
            param[63] = new SqlParameter("@total_6", aleMonthly.Rows[6][3]);
            param[64] = new SqlParameter("@total_7", aleMonthly.Rows[7][3]);
            param[65] = new SqlParameter("@total_8", aleMonthly.Rows[8][3]);
            param[66] = new SqlParameter("@total_9", aleMonthly.Rows[9][3]);
            param[67] = new SqlParameter("@total_10", aleMonthly.Rows[10][3]);
            param[68] = new SqlParameter("@total_11", aleMonthly.Rows[11][3]);
            param[69] = new SqlParameter("@total_12", aleMonthly.Rows[12][3]);
            param[70] = new SqlParameter("@group_0", aleMonthly.Rows[0][4]);
            param[71] = new SqlParameter("@group_1", aleMonthly.Rows[1][4]);
            param[72] = new SqlParameter("@group_2", aleMonthly.Rows[2][4]);
            param[73] = new SqlParameter("@group_3", aleMonthly.Rows[3][4]);
            param[74] = new SqlParameter("@group_4", aleMonthly.Rows[4][4]);
            param[75] = new SqlParameter("@group_5", aleMonthly.Rows[5][4]);
            param[76] = new SqlParameter("@group_6", aleMonthly.Rows[6][4]);
            param[77] = new SqlParameter("@group_7", aleMonthly.Rows[7][4]);
            param[78] = new SqlParameter("@group_8", aleMonthly.Rows[8][4]);
            param[79] = new SqlParameter("@group_9", aleMonthly.Rows[9][4]);
            param[80] = new SqlParameter("@group_10", aleMonthly.Rows[10][4]);
            param[81] = new SqlParameter("@group_11", aleMonthly.Rows[11][4]);
            param[82] = new SqlParameter("@group_12", aleMonthly.Rows[12][4]);
            param[83] = new SqlParameter("@S4980H_0", aleMonthly.Rows[0][5]);
            param[84] = new SqlParameter("@S4980H_1", aleMonthly.Rows[1][5]);
            param[85] = new SqlParameter("@S4980H_2", aleMonthly.Rows[2][5]);
            param[86] = new SqlParameter("@S4980H_3", aleMonthly.Rows[3][5]);
            param[87] = new SqlParameter("@S4980H_4", aleMonthly.Rows[4][5]);
            param[88] = new SqlParameter("@S4980H_5", aleMonthly.Rows[5][5]);
            param[89] = new SqlParameter("@S4980H_6", aleMonthly.Rows[6][5]);
            param[90] = new SqlParameter("@S4980H_7", aleMonthly.Rows[7][5]);
            param[91] = new SqlParameter("@S4980H_8", aleMonthly.Rows[8][5]);
            param[92] = new SqlParameter("@S4980H_9", aleMonthly.Rows[9][5]);
            param[93] = new SqlParameter("@S4980H_10", aleMonthly.Rows[10][5]);
            param[94] = new SqlParameter("@S4980H_11", aleMonthly.Rows[11][5]);
            param[95] = new SqlParameter("@S4980H_12", aleMonthly.Rows[12][5]);
            param[96] = new SqlParameter("@disableChanges", chk_disable.Checked == true ? 1 : 0);
            #endregion

            return param;
        }
        //protected void rpt_montable_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "UP")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);

        //        DataTable aleMonthly = ((DataView)rpt_montable.DataSource).ToTable();
        //        for (int i = index - 1; i >= 0; i--)
        //        {
        //            for (int j = 1; j < 6; j++)
        //            {
        //                aleMonthly.Rows[i][j] = aleMonthly.Rows[index][j];
        //            }
        //        }

        //        rpt_montable.DataSource = new DataView(aleMonthly);
        //        rpt_montable.DataBind();
        //    }
        //    if (e.CommandName == "DOWN")
        //    {

        //    }
        //}
    }
}