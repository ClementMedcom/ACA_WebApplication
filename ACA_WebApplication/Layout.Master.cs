using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACA_WebApplication
{
    public partial class Layout1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!CheckUserSession())
                {
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect("~/Login.aspx");
                }
                if (Session["Tax_Id"].ToString() == null || Session["Tax_Id"].ToString() == "")
                {
                    return;
                }
                else
                {
                    lb_employee.Enabled = true;
                    lb_employer.Enabled = true;
                    lb_plan.Enabled = true;
                    lb_upload.Enabled = true;
                }
            }
            catch (Exception)
            {

            }
        }
        private Boolean CheckUserSession()
        {
            try
            {
                if (Session["UserSession"].ToString() == null || Session["UserSession"].ToString() == "")
                {
                    return false;
                }
                else
                {
                    lbl_name.Text = Session["UserName"].ToString().Trim();
                    lbl_lastlogin.Text = Session["LastLogin"].ToString().Trim();
                    return true ;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
            }
            catch (Exception)
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}