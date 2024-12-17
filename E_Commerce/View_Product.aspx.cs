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
    public partial class View_Product : System.Web.UI.Page
    {
        Concls ob = new Concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                string f = "select * from Product where Category_Id='" + Session["Category_id"] + "'";
                DataSet ds1 = ob.fn_adapter(f);
                DataList1.DataSource = ds1;
                DataList1.DataBind();
            }

        }

        protected void ImageButton1_Command(object sender, CommandEventArgs e)
        {
            Session["Product_Id"] = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("View_Single_Product.aspx");
        }
    }
}