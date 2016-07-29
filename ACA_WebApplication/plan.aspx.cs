using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACA_WebApplication
{
    public partial class plan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3]
                {
                    new DataColumn("Start",typeof(string)),
                    new DataColumn("End",typeof(string)),
                    new DataColumn("Amount",typeof(string))
                });
                dt.Rows.Add(null, null, null);
                rpttable.DataSource = dt;
                rpttable.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3]
            {
                    new DataColumn("Start",typeof(string)),
                    new DataColumn("End",typeof(string)),
                    new DataColumn("Amount",typeof(string))
            });
            foreach(RepeaterItem item in rpttable.Items)
            {
                TextBox txt1 = (TextBox)item.FindControl("tb1");
                TextBox txt2 = (TextBox)item.FindControl("tb2");
                TextBox txt3 = (TextBox)item.FindControl("tb3");
                dt.Rows.Add(txt1.Text, txt2.Text, txt3.Text);
            }
            dt.Rows.Add(null, null, null);
            rpttable.DataSource = dt;
            rpttable.DataBind();
        }
    }
}