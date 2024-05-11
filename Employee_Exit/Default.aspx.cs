using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace Employee_Exit
{
    public partial class _Default : Page
    {
        SqlConnection _connection= new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            _connection.Open();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into ETMS_EmployeeExit values(1,'ReasonForLeaving','ReasonForLeavingAnyOther','CompanyLike','CompanyDislike','SupervisorPoints','YourRating',1,'SuggestChanges','NJ_ComapnyName','NJ_Designation','NJ_Function',100.00)", _connection);
            cmd.ExecuteNonQuery();
            _connection.Close();
            GridView1.DataBind();
        }
    }
}