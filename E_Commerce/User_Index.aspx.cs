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
    public partial class User_Index : System.Web.UI.Page
    {
        Concls ob = new Concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string s = "select * from Category";
                DataSet ds = ob.fn_adapter(s);
                DataList1.DataSource = ds;
                DataList1.DataBind();
            }
        }

       

        protected void ImageButton1_Command(object sender, CommandEventArgs e)
        {
            Session["Category_Id"] = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("View_Product.aspx");
        }


    }
    
}