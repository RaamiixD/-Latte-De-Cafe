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
    public partial class ViewBill : System.Web.UI.Page
    {
        Concls ob = new Concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string s = "select t1.*,t2.* from Product t1 join Order_Table t2 on t1.Product_Id=t2.Product_Id where Order_Status='Not Payed' and Us_Id='" + Session["userid"] + "'";
                DataSet ds = ob.fn_adapter(s);
                GridView1.DataSource = ds;
                GridView1.DataBind();

                string g = "select * from Bill_Table where Us_Id='" + Session["userid"] + "' and Bill_Status='Nill'";
                SqlDataReader dr = ob.fn_reader(g);
                while (dr.Read())
                {
                    Label2.Text = dr["Bill_Id"].ToString();
                    Label3.Text = dr["Bill_Dtae"].ToString();
                    Label6.Text = dr["Grand_Total"].ToString();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string j = "select Grand_Total from Bill_Table where Us_Id='" + Session["userid"] + "'";
            string y = ob.fn_exescalar(j);
            int d = Convert.ToInt32(y);

            Check_Bal.ServiceClient obj = new Check_Bal.ServiceClient();
            string bal = obj.balancecheck(TextBox1.Text);
            int w = Convert.ToInt32(bal);

            if (w > d)
            {
                string a = "select max(Order_Id) from Order_Table where Us_Id='" + Session["userid"] + "'";
                string b = ob.fn_exescalar(a);
                int count = Convert.ToInt32(b);
                if (count != 0)
                {
                    int pro_id = 0;
                    for (int ab = 1; ab <= count; ab++)
                    {
                        int pro_qty = 0, cart_qty = 0, qty = 0;
                        string stup = "select t1.*,t2.* from Product t1 join Order_Table t2 on t1.Product_Id= t2.Product_Id where Us_Id='" + Session["userid"] + "'";
                        SqlDataReader dr = ob.fn_reader(stup);
                        while (dr.Read())
                        {
                            pro_qty = Convert.ToInt32(dr["Product_Stock"]);
                            cart_qty = Convert.ToInt32(dr["Cart_Quantity"]);
                            pro_id = Convert.ToInt32(dr["Product_Id"]);
                            break;
                        }
                        qty = pro_qty - cart_qty;
                        string sup = "update Product set Product_Stock = " + qty + " where Product_id=" + pro_id + "";
                        int up = ob.fn_nonquery(sup);

                    }
                }
                string f = "update Order_Table set Order_Status='Payed'";
                int q = ob.fn_nonquery(f);

                string t = "update Bill_Table set Bill_Status='Payed'";
                int r = ob.fn_nonquery(t);

                string u = "update Account set Balance_Amount='" + (w - d) + "' where Us_Id='" + Session["userid"] + "'";
                int x = ob.fn_nonquery(u);

                string ty = "update Order_Table set Cart_Status='Unavailable'";
                int fg = ob.fn_nonquery(ty);
                Label7.Text = "Payed";
            }

        }
    }

    
}
    
