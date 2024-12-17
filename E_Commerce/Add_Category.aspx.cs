using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce
{
    public partial class Add_Category : System.Web.UI.Page
    {
        Concls ob = new Concls();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string p = "~/Photos/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(p));

            string n = "insert into Category values('" + TextBox1.Text + "','" + p + "','" + TextBox2.Text + "','Available')";
            int i = ob.fn_nonquery(n);
            if (i == 1)
            {
                Label4.Visible = true;
                Label4.Text = "Category Added";
            }
        }
    }
    
}