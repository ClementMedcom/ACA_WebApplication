using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace ACA_WebApplication
{
    public partial class Test2 : System.Web.UI.Page
    {
        Master_BLL objMaster = new Master_BLL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            txte1.Text = objMaster.Encrypt(txto1.Text.Trim());
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            txto2.Text = objMaster.Decrypt(txte2.Text);
        }
    }
}