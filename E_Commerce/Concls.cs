using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace E_Commerce
{
    public class Concls
    {
        SqlConnection con;
        SqlCommand cmd;

        public Concls()
        {
            con = new SqlConnection(@"server=RAMI\SQLEXPRESS01;database=Project;Integrated security=true");
        }
        public int fn_nonquery(string s)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(s, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            return i;
        }
        public string fn_exescalar(string s)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(s, con);
            con.Open();
            string q = cmd.ExecuteScalar().ToString();
            con.Close();
            return q;
        }
        public SqlDataReader fn_reader(string s)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(s, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public DataSet fn_adapter(string s)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            SqlDataAdapter da = new SqlDataAdapter(s, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

    }
}
    