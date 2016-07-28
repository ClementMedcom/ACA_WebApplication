using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BLL;

namespace ACA_WebApplication
{
    public partial class Login : System.Web.UI.Page
    {
        Master_BLL objMaster = new Master_BLL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            lb_status.Text = string.Empty;
            DataTable dtUser = new DataTable();
            string userSession = Guid.NewGuid().ToString();
            Session["UserSession"] = userSession;
            try
            {
                dtUser = objMaster.checkUserLogin(txt_uname.Text.Trim(), txt_pwd.Text.Trim(), userSession, "LOGIN");
                //dtUser = objMaster.checkUserLogin(objMaster.Encrypt(txt_uname.Text.Trim()), objMaster.Encrypt(txt_pwd.Text.Trim()), userSession, "LOGIN");
                if (dtUser != null)
                {
                    if (dtUser.Columns.Contains("RES"))
                    {
                        lb_status.Text = dtUser.Rows[0][0].ToString();
                        ClearPage();
                    }
                    else
                    {
                        Session["UserID"] = dtUser.Rows[0]["UserID"];
                        Session["UserName"] = dtUser.Rows[0]["UserName"];
                        Session["LastLogin"] = dtUser.Rows[0]["LastLogin"];
                        Session["UserRole"]= dtUser.Rows[0]["UserRole"]; 
                        if(Session["UserRole"].ToString().Trim()=="2")
                        {
                            Response.Redirect("~/Admin/AdminHome.aspx");
                        }
                        else if (Session["UserRole"].ToString().Trim() == "1")
                        {
                            Response.Redirect("~/Master/Company_List.aspx");
                        }
                    }
                }
                else
                {
                    ClearPage();
                    lb_status.Text = "Unexpected error.";
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                dtUser.Dispose();
            }
        }
        #region " [ Private Function ] "  
        private void ClearPage()
        {
            txt_uname.Text = string.Empty;
            txt_pwd.Text = string.Empty;
            lb_status.Text = string.Empty;
        }
        #endregion
    }
}