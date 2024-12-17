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
    public partial class Viewcart : System.Web.UI.Page
    {
        Concls ob = new Concls();

        int Cid;
        int cquantity;
        int ctotal;
        string cstatus;
        int usid;
        int productid;
        int gtotal = 0;
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
            grid_bind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            string t = "select t2.Product_Price from Product t2 join Cart_Product t1 on t1.Product_Id = t2.Product_Id where t1.Cart_Id='" + Session["cart_id"] + "'";
            string f = ob.fn_exescalar(t);
            int q = Convert.ToInt32(TextBox1.Text);
            int p = Convert.ToInt32(f);
            int t_price = q * p;
            string m = "update Cart_Product set Cart_Quantity='" + TextBox1.Text + "',Cart_Total='" + t_price + "'where Cart_Id='" + Session["cart_id"] + "' ";
            int i = ob.fn_nonquery(m);
            if (i == 1)
            {
                grid_bind();
                Label2.Visible = true;
                Label2.Text = "success";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string max = "select max(Cart_Id) from Cart";
            string j = ob.fn_exescalar(max);
            int u = Convert.ToInt32(j);
            for (int i = 1; i <= u; i++)
            {
                string h = "select Us_Id from Cart where Cart_Id=" + i + "";
                string st = ob.fn_exescalar(h);
                int ju = Convert.ToInt32(st);
                int id = Convert.ToInt32(Session["userid"]);
                if (id == ju)
                {
                    string ca = "select * from Cart where Cart_Id=" + i + "";
                    SqlDataReader dr = ob.fn_reader(ca);

                    while (dr.Read())
                    {
                        Cid = Convert.ToInt32(dr["Cart_Id"].ToString());
                        cquantity = Convert.ToInt32(dr["Cart_Quantity"].ToString());
                        ctotal = Convert.ToInt32(dr["Cart_Total"].ToString());
                        cstatus = dr["Cart_Status"].ToString();
                        usid = Convert.ToInt32(dr["Us_Id"].ToString());
                        productid = Convert.ToInt32(dr["Product_Id"].ToString());

                    }
                    gtotal = gtotal + ctotal;
                    string ins = "insert into Order_Table values('Not Payed'," + Cid + "," + cquantity + "," + ctotal + ",'" + cstatus + "'," + usid + "," + productid + ")";
                    int y = ob.fn_nonquery(ins);
                }

            }
            string bill = "insert into Bill_Table values('" + DateTime.Now.ToString("yyyy-MM-dd") + "','Nill'," + usid + "," + gtotal + ")";
            int g = ob.fn_nonquery(bill);
            //if (g != 0)
            //{
            //    string del = "delete from Cart where Us_Id='" + Session["userid"] + "'";
            //    int gh = ob.fn_nonquery(del);

            //}
            Response.Redirect("ViewBill.aspx");
        }

        
    }
}
