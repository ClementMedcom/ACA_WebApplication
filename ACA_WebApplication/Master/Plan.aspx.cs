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
                    ClearEmployerPlan();
                }
            }

        }

        public void list_Plan(string companyTaxId, int pageIndex, string search, int PageSize)
        {
            try
            {
                DataSet ds = objMaster.list_Plan(companyTaxId, pageIndex, search, PageSize);
                DataTable dt = ds.Tables[0];
               
                IEnumerable<DataRow> query1 = from all_data in dt.AsEnumerable()
                                              where all_data.Field<string>("name").ToLower().StartsWith(search.ToLower())
                                              orderby all_data.Field<string>("name")
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
                                         orderby all_data.Field<string>("name")
                                         select all_data).Skip((pageIndex - 1) * Convert.ToInt32(drp_count.Text)).Take(PageSize).CopyToDataTable<DataRow>();
                    int total_rows = dt1.Rows.Count;
                    rptPlan.DataSource = dt_temp;
                    rptPlan.DataBind();
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
                    rptPlan.DataSource = null;
                    rptPlan.DataBind();
                    hid_rowcount.Value = "0";
                    lbl_pagenum.Text = "1";
                    lbl_result.Text = "Showing " + 0 + "-" + 0 + " Out of " + 0 + " Records";
                }


                //rptPlan.DataSource = dt;
                //rptPlan.DataBind();
                //if (dt1.Rows.Count > 0)
                //{
                //    hid_rowcount.Value = dt1.Rows[0]["RowCnt"].ToString();
                //    lbl_pagenum.Text = dt1.Rows[0]["page_num"].ToString();
                //    lbl_result.Text = "Showing Results " + dt1.Rows[0]["Start"] + "-" + dt1.Rows[0]["Endpage"] + " Out of " + dt1.Rows[0]["RowCnt"] + " Records";
                //}
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
                DataSet ds1 = objMaster.getPremiumDetails(hdn_CompanyTax_Id.Value);
                DataTable dt1 = ds.Tables[0];
               // DataTable dt2 = ds.Tables[1];


              
                IEnumerable<DataRow> temp1 = from id in dt1.AsEnumerable()
                                             where id.Field<int>("ID") == Convert.ToInt32(hdn_Plan_Id.Value)
                                             select id;

                // Create a table from the query.
                DataTable dt3 = temp1.CopyToDataTable<DataRow>();

                hdn_id.Value = dt3.Rows[0]["ID"].ToString();
                txt_planName.Text = dt3.Rows[0]["name"].ToString();

                drp_plantype.Text = TextManipulation.toPlanType(dt3.Rows[0]["medicalPlan"].ToString());

                if (dt3.Rows[0]["fundingType"].ToString() == "1" || dt3.Rows[0]["fundingType"].ToString() == "Fully-Insured") drp_fundingtype.Text = "Fully-Insured";
                else if (dt3.Rows[0]["fundingType"].ToString() == "0" || dt3.Rows[0]["fundingType"].ToString() == "Self-Funded") drp_fundingtype.Text = "Self-Funded";
                else drp_fundingtype.Text = "";
                txt_days.Text = dt3.Rows[0]["eligibile1stOfMonth"].ToString();
                drp_waitingperiod.Text = TextManipulation.toWaitingPeriod(dt3.Rows[0]["waitingDays"].ToString());
                drp_spouse.Text = TextManipulation.toYesNo(dt3.Rows[0]["offeredSpouse"].ToString());
                drp_dependence.Text = TextManipulation.toYesNo(dt3.Rows[0]["offeredDependents"].ToString());
                drp_termination.Text = TextManipulation.toYesNo(dt3.Rows[0]["planTermTermination"].ToString());
                string planre = dt3.Rows[0]["planRenewal"].ToString();
                if (planre != "")
                    drp_renewalmonth.Text =dt3.Rows[0]["planRenewal"].ToString();
                drp_minvalue.Text = TextManipulation.toYesNo(dt3.Rows[0]["minimumValue"].ToString());

                if (dt3.Rows[0]["bandingType"].ToString() != "NULL")
                    drp_bandingtype.Text = dt3.Rows[0]["bandingType"].ToString();
                else drp_bandingtype.Text = "";

                //bind code table

                rpt_codetbl.DataSource = dt3;
                rpt_codetbl.DataBind();

                //bind banding table
                DataTable dt5 = ds1.Tables[0];
                IEnumerable<DataRow> temp2 = from id in dt5.AsEnumerable()
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

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            DataSet ds = objMaster.Insert_Update_EmployerPlan(hdn_id.Value,getPlanParams());
            //to delete the premium details
            if (hdn_id.Value != "0")
                objMaster.PremiumDelete(hdn_companytax_id.Value, txt_planName.Text.Trim());
            //premium banding update
            foreach (RepeaterItem item in rpttable.Items)
            {
                TextBox bandingValueStart = (TextBox)item.FindControl("tb1");
                TextBox bandingValueEnd = (TextBox)item.FindControl("tb2");
                TextBox bandingStartDate = (TextBox)item.FindControl("tb3");
                TextBox bandingEndDate = (TextBox)item.FindControl("tb4");
                TextBox amount = (TextBox)item.FindControl("tb5");
                if (bandingValueStart.Text != "" && bandingValueEnd.Text != "")
                    objMaster.insert_Update_Premium(hdn_id.Value, getPremiumParams("Banding"+item.ItemIndex, bandingValueStart.Text, bandingValueEnd.Text, bandingStartDate.Text, bandingEndDate.Text, amount.Text));
            }
            if (ds.Tables.Count > 0)
            {
                lbl_msg.Text = ds.Tables[0].Rows[0][0].ToString();
                lightDiv.Visible = true;
                fadeDiv.Visible = true;
            }
            else
            {
                if (hdn_id.Value == "0")
                    lbl_msg.Text = "Plan Added Successfully";
                else
                    lbl_msg.Text = "Plan Updated Successfully";
                lightDiv.Visible = true;
                fadeDiv.Visible = true;
                list_Plan(hdn_companytax_id.Value, 1, "", 10);
                ClearEmployerPlan();
            }
        }
        private SqlParameter[] getPremiumParams(string bandingName, string bandingValueStart, string bandingValueEnd, string bandingStartDate, string bandingEndDate, string amount)
        {
            SqlParameter[] param = null;

            param = new SqlParameter[8];
            param[0] = new SqlParameter("@CompanyTaxID", hdn_companytax_id.Value);
            param[1] = new SqlParameter("@name", txt_planName.Text);
            param[2] = new SqlParameter("@bandingName", bandingName);
            param[3] = new SqlParameter("@bandingValueStart", bandingValueStart);
            param[4] = new SqlParameter("@bandingValueEnd", bandingValueEnd);
            param[5] = new SqlParameter("@bandingStartDate", FixDateFormat(bandingStartDate));
            param[6] = new SqlParameter("@bandingEndDate", FixDateFormat(bandingEndDate));
            param[7] = new SqlParameter("@amount",TextManipulation.toDBNULLfromEmpty(amount));
            return param;
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearEmployerPlan();
        }

        public void ClearEmployerPlan()
        {
            hdn_id.Value = "0";
            txt_planName.Text = null;
            drp_fundingtype.Text = "";
            drp_plantype.Text = "";
            drp_waitingperiod.Text = "";
            txt_days.Text = "";
            drp_spouse.Text = "";
            drp_dependence.Text = "";
            drp_termination.Text = "";
            drp_renewalmonth.Text = "0";
            drp_minvalue.Text = "";
            drp_bandingtype.Text = "";

            DataTable dt_preimum = dt();
            dt_preimum.Rows.Add(null, null, null, null, null, null);
            rpttable.DataSource = dt_preimum;
            rpttable.DataBind();
            rpt_codetbl.DataSource = dt_code();
            rpt_codetbl.DataBind();
        }
        private SqlParameter[] getPlanParams()
        {
            SqlParameter[] param = null;
            param = new SqlParameter[30];
            param[0] = new SqlParameter("@CompanyTaxID", hdn_companytax_id.Value);
            param[1] = new SqlParameter("@name", txt_planName.Text.Trim());
            param[2] = new SqlParameter("@medicalPlan",TextManipulation.planTypetoNumber(drp_plantype.Text));
            param[3] = new SqlParameter("@bandingType", drp_bandingtype.Text);
            param[4] = new SqlParameter("@offeredSpouse", TextManipulation.convertYesNotoNumber(drp_spouse.Text));
            param[5] = new SqlParameter("@offeredDependents", TextManipulation.convertYesNotoNumber(drp_dependence.Text));
            param[6] = new SqlParameter("@waitingDays", TextManipulation.WaitingPeriodtoNumber(drp_waitingperiod.Text));
            param[7] = new SqlParameter("@eligibile1stOfMonth",TextManipulation.toDBNULLfromEmpty(txt_days.Text.Trim()));
            param[8] = new SqlParameter("@fundingType", drp_fundingtype.Text);
            param[9] = new SqlParameter("@planRenewal", TextManipulation.toDBNULLfromEmpty(drp_renewalmonth.Text));
            param[10] = new SqlParameter("@planTermTermination", TextManipulation.convertYesNotoNumber(drp_termination.Text));
            param[11] = new SqlParameter("@minimumValue", TextManipulation.convertYesNotoNumber(drp_minvalue.Text));
            foreach(RepeaterItem item in rpt_codetbl.Items)
            {
                CheckBox chk_code1A = (CheckBox)item.FindControl("chk_code1A");
                CheckBox chk_code1B = (CheckBox)item.FindControl("chk_code1B");
                CheckBox chk_code1C = (CheckBox)item.FindControl("chk_code1C");
                CheckBox chk_code1D = (CheckBox)item.FindControl("chk_code1D");
                CheckBox chk_code1E = (CheckBox)item.FindControl("chk_code1E");
                CheckBox chk_code1F = (CheckBox)item.FindControl("chk_code1F");
                CheckBox chk_code1G = (CheckBox)item.FindControl("chk_code1G");
                CheckBox chk_code1H = (CheckBox)item.FindControl("chk_code1H");
                CheckBox chk_code1I = (CheckBox)item.FindControl("chk_code1I");

                CheckBox chk_code2A = (CheckBox)item.FindControl("chk_code2A");
                CheckBox chk_code2B = (CheckBox)item.FindControl("chk_code2B");
                CheckBox chk_code2C = (CheckBox)item.FindControl("chk_code2C");
                CheckBox chk_code2D = (CheckBox)item.FindControl("chk_code2D");
                CheckBox chk_code2E = (CheckBox)item.FindControl("chk_code2E");
                CheckBox chk_code2F = (CheckBox)item.FindControl("chk_code2F");
                CheckBox chk_code2G = (CheckBox)item.FindControl("chk_code2G");
                CheckBox chk_code2H = (CheckBox)item.FindControl("chk_code2H");
                CheckBox chk_code2I = (CheckBox)item.FindControl("chk_code2I");

                param[12] = new SqlParameter("@code1A", (chk_code1A.Checked == true ? 1 : 0));
                param[13] = new SqlParameter("@code1B", (chk_code1B.Checked == true ? 1 : 0));
                param[14] = new SqlParameter("@code1C", (chk_code1C.Checked == true ? 1 : 0));
                param[15] = new SqlParameter("@code1D", (chk_code1D.Checked == true ? 1 : 0));
                param[16] = new SqlParameter("@code1E", (chk_code1E.Checked == true ? 1 : 0));
                param[17] = new SqlParameter("@code1F", (chk_code1F.Checked == true ? 1 : 0));
                param[18] = new SqlParameter("@code1G", (chk_code1G.Checked == true ? 1 : 0));
                param[19] = new SqlParameter("@code1H", (chk_code1H.Checked == true ? 1 : 0));
                param[20] = new SqlParameter("@code1I", (chk_code1I.Checked == true ? 1 : 0));
                param[21] = new SqlParameter("@code2A", (chk_code2A.Checked == true ? 1 : 0));
                param[22] = new SqlParameter("@code2B", (chk_code2B.Checked == true ? 1 : 0));
                param[23] = new SqlParameter("@code2C", (chk_code2C.Checked == true ? 1 : 0));
                param[24] = new SqlParameter("@code2D", (chk_code2D.Checked == true ? 1 : 0));
                param[25] = new SqlParameter("@code2E", (chk_code2E.Checked == true ? 1 : 0));
                param[26] = new SqlParameter("@code2F", (chk_code2F.Checked == true ? 1 : 0));
                param[27] = new SqlParameter("@code2G", (chk_code2G.Checked == true ? 1 : 0));
                param[28] = new SqlParameter("@code2H", (chk_code2H.Checked == true ? 1 : 0));
                param[29] = new SqlParameter("@code2I", (chk_code2I.Checked == true ? 1 : 0));
            }
            //param[30]= new SqlParameter("@dt_premimum", dt_premimum);
            //param[31] = new SqlParameter("@Id", hdn_id.Value);
            return param;
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

        #region Add Preimum

        public DataTable dt()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6]
            {
                            new DataColumn("Name",typeof(string)),
                            new DataColumn("bandingValueStart",typeof(string)),
                            new DataColumn("bandingValueEnd",typeof(string)),
                            new DataColumn("bandingStartDate",typeof(string)),
                            new DataColumn("bandingEndDate",typeof(string)),
                            new DataColumn("amount",typeof(string))
            });
            return dt;
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
                dt_plan.Rows.Add("binding" + item.ItemIndex, txt1.Text, txt2.Text, txt3.Text, txt4.Text, txt5.Text);
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
                dt_plan.Rows.Add("binding"+item.ItemIndex,txt1.Text, txt2.Text, txt3.Text, txt4.Text, txt5.Text);
            }
            dt_plan.Rows[index].Delete();
            dt_plan.AcceptChanges();
            //dt_plan.Rows.Add(null, null, null, null, null);
            rpttable.DataSource = dt_plan;
            rpttable.DataBind();
        }

        public DataTable getPremimum()
        {
            DataTable dt_plan = dt();
            foreach (RepeaterItem item in rpttable.Items)
            {
                TextBox txt1 = (TextBox)item.FindControl("tb1");
                TextBox txt2 = (TextBox)item.FindControl("tb2");
                TextBox txt3 = (TextBox)item.FindControl("tb3");
                TextBox txt4 = (TextBox)item.FindControl("tb4");
                TextBox txt5 = (TextBox)item.FindControl("tb5");
                if (txt1.Text != "" && txt2.Text != "")
                    dt_plan.Rows.Add("binding" + item.ItemIndex, TextManipulation.toDBNULLfromEmpty(txt1.Text), TextManipulation.toDBNULLfromEmpty(txt2.Text),TextManipulation.FixDateFormat(txt3.Text), TextManipulation.FixDateFormat(txt4.Text), TextManipulation.toDBNULLfromEmpty(txt5.Text));
            }
            return dt_plan;
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
            drp_renewalmonth.Items.Insert(0, new ListItem("--Select--", "0"));

            //filling min value
            drp_minvalue.DataSource = objDrop.Getyes_no();
            drp_minvalue.DataBind();
            drp_minvalue.Items.Insert(0, new ListItem("--Select--", ""));

            //filling binding type
            drp_bandingtype.DataSource = objDrop.Getbandingtype();
            drp_bandingtype.DataBind();
            drp_bandingtype.Items.Insert(0, new ListItem("--Select--", ""));
        }

        protected void lbl_close_Click(object sender, EventArgs e)
        {
            lightDiv.Visible = false;
            fadeDiv.Visible = false;
        }

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

        public DataTable dt_code()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[18]
               {
                                new DataColumn("code1A",typeof(string)),
                                new DataColumn("code1B",typeof(string)),
                                new DataColumn("code1C",typeof(string)),
                                new DataColumn("code1D",typeof(string)),
                                new DataColumn("code1E",typeof(string)),
                                new DataColumn("code1F",typeof(string)),
                                new DataColumn("code1G",typeof(string)),
                                new DataColumn("code1H",typeof(string)),
                                new DataColumn("code1I",typeof(string)),
                                new DataColumn("code2A",typeof(string)),
                                new DataColumn("code2B",typeof(string)),
                                new DataColumn("code2C",typeof(string)),
                                new DataColumn("code2D",typeof(string)),
                                new DataColumn("code2E",typeof(string)),
                                new DataColumn("code2F",typeof(string)),
                                new DataColumn("code2G",typeof(string)),
                                new DataColumn("code2H",typeof(string)),
                                new DataColumn("code2I",typeof(string))
               });
            dt.Rows.Add(0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
            return dt;
        }
        protected void Refresh(object sender, EventArgs e)
        {
            list_Plan(hdn_companytax_id.Value, 1, "", 10);
            txtsearch.Text = "";
            txtsearch.Focus();
        }
    }
}