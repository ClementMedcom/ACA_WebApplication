using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

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
                    lbl_companyname.Text = " : "+Session["Company_Name"].ToString()+" / ";
                    //lb_employee.Enabled = true;
                    //lb_employer.Enabled = true;
                    lb_plan.Attributes.CssStyle.Add("pointer-events", "unset");
                    lb_employer.Attributes.CssStyle.Add("pointer-events", "unset");
                    lb_employee.Attributes.CssStyle.Add("pointer-events", "unset");
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
                if (Session["UserName"].ToString() == null || Session["UserName"].ToString() == "")
                {
                    return false;
                }
                else
                {
                    lbl_name.Text = Session["name"].ToString().Trim();
                    //lbl_lastlogin.Text = Session["LastLogin"].ToString().Trim();
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