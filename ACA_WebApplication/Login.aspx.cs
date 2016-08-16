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
            if (!IsPostBack)
            {
                Session.Clear();
                Session.Abandon();
            }
        }
        [System.Web.Services.WebMethod]
        public static int chk_login(string param)
        {
            string[] splitval = param.Split(',');
            string username = splitval[0].ToString();
            string password = splitval[1].ToString();
            Login log = new Login();
            int loginChk = log.btn_login_Click(username, password);
            return loginChk;
        }
        public int btn_login_Click(string username, string password)
        {
            //lb_status.Text = string.Empty;
            DataTable dtUser = new DataTable();
            int ack = 0;
            try
            {
                dtUser = objMaster.checkUserLogin(username);
                if (dtUser.Rows.Count > 0)
                {
                    if (dtUser.Rows[0]["username"].ToString() == username && dtUser.Rows[0]["password"].ToString() == password)
                    {
                        HttpContext.Current.Session["UserID"] = dtUser.Rows[0]["id"].ToString();
                        HttpContext.Current.Session["UserName"] = dtUser.Rows[0]["username"].ToString();
                        HttpContext.Current.Session["name"] = dtUser.Rows[0]["name"].ToString();
                        //Response.Redirect("~/Master/Company_List.aspx");
                        ack= 1;
                    }
                    else
                    {
                        //lb_status.Text = "Invalid Password";
                        //txt_pwd.Focus();
                        ack = 0;
                    }

                }
                else
                {
                    //lb_status.Text = "Invalid Password";
                    //txt_pwd.Focus();
                    ack = 0;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                dtUser.Dispose();
            }
            return ack;
        }
        //protected void btn_login_Click(object sender, EventArgs e)
        //{
        //    lb_status.Text = string.Empty;
        //    DataTable dtUser = new DataTable();

        //    try
        //    {
        //        dtUser = objMaster.checkUserLogin(txt_uname.Text.Trim());
        //        if (dtUser.Rows.Count>0)
        //        {
        //            if(dtUser.Rows[0]["username"].ToString() ==txt_uname.Text.Trim() && dtUser.Rows[0]["password"].ToString() == txt_pwd.Text.Trim())
        //            {
        //                Session["UserID"] = dtUser.Rows[0]["id"].ToString();
        //                Session["UserName"] = dtUser.Rows[0]["username"].ToString();
        //                Session["name"] = dtUser.Rows[0]["name"].ToString();
        //                Response.Redirect("~/Master/Company_List.aspx");

        //            }
        //            else
        //            {
        //                lb_status.Text = "Invalid Password";
        //                txt_pwd.Focus();
        //            }

        //        }
        //        else
        //        {
        //            lb_status.Text = "Invalid Password";
        //            txt_pwd.Focus();
        //        }
        //                   }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        dtUser.Dispose();
        //    }
        //}
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