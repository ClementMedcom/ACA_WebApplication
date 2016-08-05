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
                //if (!CheckUserSession())
                //{
                //    Session.Clear();
                //    Session.Abandon();
                //    Response.Redirect("~/Login.aspx");
                //}
                //string sessionTaxID = Session["Tax_Id"] as string;
                //string sessionCompanyName = Session["Company_Name"] as string;
                //if (!string.IsNullOrEmpty(sessionTaxID))
                //{
                //    lbl_companyname.Text = "Welcome " + sessionCompanyName;
                //    lb_employee.Enabled = true;
                //    lb_employer.Enabled = true;
                //    lb_plan.Enabled = true;
                //    lb_upload.Enabled = true;
                //}
                string sessionUserSession = Session["UserSession"].ToString();
                string sessionUserName = Session["UserName"].ToString();
                //string sessionLast = Session["LastLogin"] as string;
                DateTime _loggedInTime = (DateTime)Session["LastLogin"];
                if (!string.IsNullOrEmpty(sessionUserSession))
                {
                    lbl_companyname.Text = "Inside";
                    lbl_name.Text = sessionUserName.ToString();
                    lbl_lastlogin.Text = _loggedInTime.ToString();
                    string sessionTaxID = Session["Tax_Id"].ToString();
                    string sessionCompanyName = Session["Company_Name"].ToString();
                    if (!string.IsNullOrEmpty(sessionTaxID))
                    {
                        lbl_companyname.Text = "Welcome " + sessionCompanyName;
                        lb_employee.Enabled = true;
                        lb_employer.Enabled = true;
                        lb_plan.Enabled = true;
                        lb_upload.Enabled = true;
                    }
                }
                else
                { 
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect("~/Login.aspx");
                }
                
            }
            catch (Exception ex)
            {

            }
        }
        private Boolean CheckUserSession()
        {
            try
            {
                string sessionUserSession = Session["UserSession"] as string;
                string sessionUserName = Session["UserName"] as string;
                //string sessionLast = Session["LastLogin"] as string;
                DateTime _loggedInTime = (DateTime)Session["LastLogin"];
                if (!string.IsNullOrEmpty(sessionUserSession))
                {
                    lbl_name.Text = sessionUserName;
                    lbl_lastlogin.Text = _loggedInTime.ToString();
                    return true;
                }
                else
                {
                    return false;
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