using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACA_WebApplication
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
           if (txt_uname.Text == "Medcom" && txt_pwd.Text == "medcom2016")

            {
                lb_status.Text = "";
                Response.Redirect("Home.aspx");
                
            }
            else
            {
                lb_status.Text = "Invalid id or password";
            }

        }
    }
}