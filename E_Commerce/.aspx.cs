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
    public partial class Addto_Cart : System.Web.UI.Page
    {
        Concls ob = new Concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            grid_bind();
        }
        public void grid_bind()
        {
            string s = "select t1.*,t2.* from Cart t1 join Product t2 on t1.Product_Id = t2.Product_Id where Us_Id='" + Session["userid"] + "'";
            DataSet ds = ob.fn_adapter(s);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {
            Panel1.Visible = true;
            Session["cart_id"] = Convert.ToInt32(e.CommandArgument);

        }

      
        protected void LinkButton2_Command(object sender, CommandEventArgs e)
        {
            int i = Convert.ToInt32(e.CommandArgument);
            string d = "delete from Cart where Cart_Id='" + i + "'";
            int g = ob.fn_nonquery(d);
            if (g == 1) 
            { 
            grid_bind();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            string t = "select t2.Product_Price from Product t2 join Cart t1 on t1.Product_Id = t2.Product_Id where t1.Cart_Id='" + Session["cart_id"] + "'";
            string f = ob.fn_exescalar(t);
            int q = Convert.ToInt32(TextBox1.Text);
            int p = Convert.ToInt32(f);
            int t_price = q * p;
            string m = "update Cart set Cart_Quantity='" + TextBox1.Text + "',Cart_Total='" + t_price + "'where Cart_Id='" + Session["cart_id"] + "' ";
            int i = ob.fn_nonquery(m);
            if (i == 1)
            {
                grid_bind();
                Label2.Visible = true;
                Label2.Text = "success";
            }
        }
    }
    
}