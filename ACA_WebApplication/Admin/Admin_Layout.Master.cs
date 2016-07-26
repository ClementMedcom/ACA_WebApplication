using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACA_WebApplication.Admin
{
    public partial class Admin_Layout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckUserSession())
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
            }
        }
        private Boolean CheckUserSession()
        {
            try
            {
                if (Session["UserSession"].ToString() == null || Session["UserSession"].ToString() == ""|| Session["UserRole"].ToString() == ""|| Session["UserRole"].ToString() == "1")
                {
                    return false;
                }
                else
                {
                    lbl_name.Text = Session["UserName"].ToString().Trim();
                    lbl_lastlogin.Text = Session["LastLogin"].ToString().Trim();
                    return true;
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