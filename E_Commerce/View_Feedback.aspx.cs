using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;

namespace E_Commerce
{
    public partial class View_Feedback : System.Web.UI.Page
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
                string s = "select * from Feedback where Status ='pending'";
                DataSet ds = ob.fn_adapter(s);
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }

        protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {
            Panel1.Visible = true;
            int i = Convert.ToInt32(e.CommandArgument);
            Session["fid"] = i;
            string f = "select t1.*,t2.* from Feedback t1 join User_Reg t2 on t1.User_Id = t2.Us_Id where Feedback_Id='" + i + "'";
            SqlDataReader dr = ob.fn_reader(f);
            while (dr.Read())
            {
                Label5.Text = dr["Us_Name"].ToString();
                Label7.Text = dr["Us_Email"].ToString();
            }
        }


        public static void SendEmail2(string yourName, string yourGmailUserName, string yourGmailPassword, string toName, string toEmail, string subject, string body)
        {
            string to = toEmail; //To address    
            string from = yourGmailUserName; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = body;
            message.Subject = subject;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(yourGmailUserName, yourGmailPassword);
            client.EnableSsl = true;
            client.UseDefaultCredentials = true;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SendEmail2("Josna", "jk9447669330@gmail.com", "soxe awdy mjil kcwz", "'" + Label5.Text + "'", " " + Label7.Text + " ", "feedback Reply", "'" + TextBox1.Text + "'");
            string s = "update Feedback set Feedback_Reply='" + TextBox1.Text + "',Status='Sent' where Feedback_Id='" + Session["fid"] + "'";
            int u = ob.fn_nonquery(s);
            if (u == 1)
            {
                grid_bind();
                Panel1.Visible = false;

            }
        }
    }
}