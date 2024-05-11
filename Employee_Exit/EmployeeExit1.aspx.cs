using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Employee_Exit
{
    public partial class EmployeeExit1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Create a DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("Description", typeof(string));

                // Retrieve data from the database and fill the DataTable
                string connectionString = @"Data Source=RUSHI\SQLEXPRESS;Initial Catalog=EmployeeExit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT Description FROM ETMS_Master_LeavingOptions WHERE OptionType = 1";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(reader.GetString(0));
                            }
                        }
                    }
                }

                // Bind the DataTable to the GridView
                gvLeavingOptions.DataSource = dt;
                gvLeavingOptions.DataBind();

                BindSupervisorPointsFromDatabase(connectionString);
            }
        }
        private void BindSupervisorPointsFromDatabase(string conn)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string query = "SELECT Description FROM ETMS_Master_LeavingOptions WHERE OptionType = 2";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Create a DataTable to store the data
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // Bind the GridView to the DataTable
                        gvSupervisorPoints.DataSource = dataTable;
                        gvSupervisorPoints.DataBind();
                    }
                }
            }
        }

    }
}