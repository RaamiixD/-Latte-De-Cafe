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
    
    public partial class Add_Product : System.Web.UI.Page
    {
        Concls ob = new Concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                string u = "select * from Category";
                DataSet ds = ob.fn_adapter(u);
                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "Category_Name";
                DropDownList1.DataValueField = "Category_Id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "--select--");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string p = "~/Photos/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(p));

            string s = "insert into Product values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + p + "'," + TextBox3.Text + ",'" + TextBox4.Text + "','available'," + DropDownList1.SelectedItem.Value + ")";
            int i = ob.fn_nonquery(s);
            if (i == 1)
            {
                Label7.Visible = true;
                Label7.Text = "Added Product";
            }
        }
    }
    
}