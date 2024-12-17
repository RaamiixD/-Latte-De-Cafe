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
    public partial class Edit_Category : System.Web.UI.Page
    {
        Concls ob = new Concls();
        protected void Page_Load(object sender, EventArgs e)
        {

            grid_bind();
        }

        public void grid_bind()
        {
            String d = "select * from Category";
            DataSet ds = ob.fn_adapter(d);
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }

        protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {
            Panel1.Visible = true;
            Session["Id"] = Convert.ToInt32(e.CommandArgument);
            string u = "select * from Category where Category_Id='" + Session["Id"] + "'";
            SqlDataReader dr = ob.fn_reader(u);
            while (dr.Read())
            {
                Label2.Text = dr["Category_Name"].ToString();
                TextBox1.Text = dr["Category_Description"].ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string w = "~/Photos/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(w));

            string s = "update Category set Category_Photo='" + w + "',Category_Description='" + TextBox1.Text + "' where Category_Id='" + Session["Id"] + "'";
            int i = ob.fn_nonquery(s);
            if (i == 1)
            {
                Label5.Visible = true;
                Label5.Text = "Updated";
                grid_bind();
            }
        }

        protected void LinkButton2_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string t = "select Category_Status from Category where Category_Id='" + id + "'";
            string a = ob.fn_exescalar(t);
            if (a == "Available")
            {
                string w = "update Category set Category_Status='Unavailable' where Category_Id='" + id + "'";
                int i = ob.fn_nonquery(w);
                grid_bind();
            }
            else if (a == "Unavailable")
            {
                string m = "update Category set Category_Status='Available' where Category_Id='" + id + "'";
                int h = ob.fn_nonquery(m);
                grid_bind();
            }
        }
    }
}

