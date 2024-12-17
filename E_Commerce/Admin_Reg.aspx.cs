using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce
{
    public partial class Admin_Reg : System.Web.UI.Page
    {
        Concls ob = new Concls();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string s = "select max(Us_Id) from Login";
            string regid = ob.fn_exescalar(s);
            int reg = 0;
            if (regid == "")
            {
                reg = 1;
            }
            else
            {
                int re = Convert.ToInt32(regid);
                reg = re + 1;
            }
            string r = "insert into Admin_Regi values(" + reg + ",'" + TextBox1.Text + "'," + TextBox2.Text + ",'" + TextBox3.Text + "'," + TextBox4.Text + ",'" + TextBox5.Text + "')";
            int i = ob.fn_nonquery(r);
            if (i != 0)
            {
                string str = "insert into Login values('" + TextBox6.Text + "','" + TextBox7.Text + "','admin','active'," + reg + ")";
                int q = ob.fn_nonquery(str);
                if (q == 1)
                {
                    Label8.Visible = true;
                    Label8.Text = "Registered";
                }
            }
        }
    }
}