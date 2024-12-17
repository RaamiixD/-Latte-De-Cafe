using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace E_Commerce
{
    public partial class Login : System.Web.UI.Page
    {
        Concls ob = new Concls();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "select count(Us_id) from Login where Username='" + TextBox1.Text + "' and Password='" + TextBox2.Text + "'";
            string cid = ob.fn_exescalar(str);
            int cid1 = Convert.ToInt32(cid);
            if (cid1 == 1)
            {
                string str1 = "select Us_Id from Login where  Username='" + TextBox1.Text + "' and Password='" + TextBox2.Text + "'";
                string regid = ob.fn_exescalar(str1);
                Session["userid"] = regid;
                string str2 = "select User_Type from Login where  Username='" + TextBox1.Text + "' and Password='" + TextBox2.Text + "'";
                string logtype = ob.fn_exescalar(str2);
                if (logtype == "admin")
                {
                   Response.Redirect("Admin_Index.aspx");
                    Label3.Visible = true;
                    Label3.Text = "Admin";
                }
                else if (logtype == "user")
                {

                   Response.Redirect("User_Index.aspx");
                    Label3.Visible = true;
                    Label3.Text = "User";

                }

            }
        }
    }
}
