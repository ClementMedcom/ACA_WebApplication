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
                if (Session["UserSession"].ToString() == null || Session["UserSession"].ToString() == "")
                {
                    return false;
                }
                else
                {
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