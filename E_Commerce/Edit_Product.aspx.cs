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
    public partial class Edit_Product : System.Web.UI.Page
    {
        Concls ob = new Concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                grid_bind();
            }
        }
        public void grid_bind()
        {
            string w = "select * from Product";
            DataSet ds = ob.fn_adapter(w);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {
            Panel1.Visible = true;
            Session["Id"] = Convert.ToInt32(e.CommandArgument);
            string s = "select * from Product where Product_Id='" + Session["Id"] + "'";
            SqlDataReader dr = ob.fn_reader(s);
            while (dr.Read())
            {
                Label2.Text = dr["Product_Name"].ToString();
                TextBox1.Text = dr["Product_Details"].ToString();
                TextBox2.Text = dr["Product_Price"].ToString();
                TextBox3.Text = dr["Product_Stock"].ToString();

            }
        }
        protected void LinkButton2_Command(object sender, CommandEventArgs e)
        {
            int i = Convert.ToInt32(e.CommandArgument);
            string h = "select Product_Status from Product where Product_Id='" + i + "'";
            string d = ob.fn_exescalar(h);
            if (d == "available")
            {
                string w = "update Product set Product_Status='Unavailable' where Product_Id='" + i + "'";
                int r = ob.fn_nonquery(w);
                grid_bind();
            }
            else if (d == "Unavailable")
            {
                string m = "update Product set Product_Status='Available' where Product_Id='" + i + "'";
                int o = ob.fn_nonquery(m);
                grid_bind();
            }

        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            string p = "~/Photos/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(p));

            string w = "update Product set Product_Details='" + TextBox1.Text + "',Product_Photo='" + p + "',Product_Price=" + TextBox2.Text + ",Product_Stock='" + TextBox3.Text + "' where Product_Id='" + Session["Id"] + "'";
            int u = ob.fn_nonquery(w);
            if (u == 1)
            {
                Label7.Visible = true;
                Label7.Text = "Updated";
                grid_bind();

            }

        }
    }
    
}
