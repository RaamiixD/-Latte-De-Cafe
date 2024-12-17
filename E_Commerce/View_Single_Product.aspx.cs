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
    public partial class Viwe_single_product : System.Web.UI.Page
    {
        Concls ob = new Concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = "select * from Product where Product_Id='" + Session["Product_Id"] + "'";
            SqlDataReader dr = ob.fn_reader(s);
            while (dr.Read())
            {
                Label1.Text = dr["Product_Name"].ToString();
                Label2.Text = dr["Product_Details"].ToString();
                Label3.Text = dr["Product_Price"].ToString();
                Image1.ImageUrl = dr["Product_Photo"].ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = "select max(Cart_Id) from Cart";
            string cartid = ob.fn_exescalar(s);
            int cart_id = 0;
            if (cartid == "")
            {
                cart_id = 1;
            }
            else
            {
                int cart = Convert.ToInt32(cartid);
                cart_id = cart + 1;
            }
            string h = "select product_price from Product where Product_Id='" + Session["Product_Id"] + "'";
            string u = ob.fn_exescalar(h);
            int q = Convert.ToInt32(TextBox1.Text);
            int p = Convert.ToInt32(u);
            int t_price = q * p;
            string d = "insert into Cart values(" + cart_id + "," + TextBox1.Text + "," + t_price + ",'available'," + Session["userid"] + "," + Session["Product_Id"] + ")";
            int i = ob.fn_nonquery(d);
            if (i == 1)
            {
                Label4.Visible = true;
                Label4.Text = "Added to Cart";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
             Response.Redirect("User_Index.aspx");
        }
    }
}