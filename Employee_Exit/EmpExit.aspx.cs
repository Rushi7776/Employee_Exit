using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.DynamicData;
using System.Drawing;

namespace Employee_Exit
{
    public partial class EmpExit : System.Web.UI.Page
    {
        string connectionString = @"Data Source=GZN00001028\MSSQLSERVER01;Initial Catalog=EmployeeExit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string,int> map = new Dictionary<string,int>();
            map.Add("a",1);
            map.Add("b",2);

            if (!IsPostBack)
            {


                // Create a DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Description", typeof(string));

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT Id,Description FROM ETMS_Master_LeavingOptions WHERE isdeleted=0 and OptionType = 1";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(reader.GetInt32(0), reader.GetString(1));
                            }
                        }
                    }
                }

                // Bind the DataTable to the GridView
                gvLeavingOptions.DataSource = dt;
                gvLeavingOptions.DataBind();

                BindSupervisorPointsFromDatabase(connectionString);
                BindCooperationData(connectionString);


                //GetEmployeeExitData();
                GetAllEmpExitData();



            }
        }

        private void GetAllEmpExitData()
        {
            DataTable dataTable = new DataTable();
            // Replace this with your actual database connection logic
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //using (SqlCommand command = new SqlCommand("SELECT UserId as 'User Id', ReasonForLeaving as 'Reason For Leaving Organization', ReasonForLeavingAnyOther as 'Other Reason' FROM ETMS_EmployeeExit", connection))
                using (SqlCommand command = new SqlCommand(
    "SELECT " +
    "   UserId, " +
    "   (SELECT STUFF(" +
    "           (SELECT ',' + m.Description " +
    "            FROM ETMS_Master_LeavingOptions m " +
    "            JOIN dbo.SplitString(EE.ReasonForLeaving, ',') s ON m.Id = s.Item " +
    "            FOR XML PATH('')), 1, 1, '') AS LeavingReason) AS LeavingReason, " +
    "   ReasonForLeavingAnyOther, " +
    "   CompanyLike, " +
    "   CompanyDislike, " +
    "   SupervisorPoints, " +
    "   YourRating, " +
    "   RejoinAgain, " +
    "   SuggestChanges, " +
    "   NJ_ComapnyName, " +
    "   NJ_Designation, " +
    "   NJ_Function, " +
    "   NJ_CostToCompany " +
    "FROM ETMS_EmployeeExit EE", connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {

                        adapter.Fill(dataTable);

                    }
                }
            }
            gvGetEmpExitList.DataSource = dataTable;
            gvGetEmpExitList.DataBind();
        }

        private void BindSupervisorPointsFromDatabase(string conn)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string query = "SELECT Description FROM ETMS_Master_LeavingOptions WHERE isdeleted=0 and OptionType = 2";

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
        private void BindCooperationData(string conn)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string query = "SELECT Description FROM ETMS_Master_LeavingOptions WHERE isdeleted=0 and OptionType = 3";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        gvCooperation.DataSource = dataTable;
                        gvCooperation.DataBind();
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                LeaveFeedbackModel leaveFeedbackModel = new LeaveFeedbackModel();
                //leaveFeedbackModel.Id = 101;
                leaveFeedbackModel.UserId = 101;

                StringBuilder sbReasonForLeaving = new StringBuilder();
                foreach (GridViewRow row in gvLeavingOptions.Rows)
                {
                    // Find the CheckBox in the current row
                    CheckBox checkBox = (CheckBox)row.FindControl("CheckBox1");

                    // Check if the CheckBox is checked
                    if (checkBox != null && checkBox.Checked)
                    {
                        // Find the Id value in the data source (assuming it's a bound field)
                        DataKey dataKey = gvLeavingOptions.DataKeys[row.RowIndex];
                        if (dataKey != null)
                        {
                            string id = dataKey["Id"].ToString();

                            // Append the Id to the StringBuilder with a comma
                            sbReasonForLeaving.Append(id).Append(",");
                        }
                    }
                }

                if (sbReasonForLeaving.Length > 0)
                {
                    sbReasonForLeaving.Remove(sbReasonForLeaving.Length - 1, 1); // Remove the last character (comma)
                }
                                
                leaveFeedbackModel.ReasonForLeaving = sbReasonForLeaving.ToString();



                leaveFeedbackModel.ReasonForLeavingAnyOther = txtAnyothReasonForLeaving.Value;
                leaveFeedbackModel.CompanyLike = txtLikeAbtCompany.Value;
                leaveFeedbackModel.CompanyDislike = txtDisLikeAbtCompany.Value;


                StringBuilder sbSupervisorPoints = new StringBuilder();
                for (int rowIndex = 0; rowIndex < gvSupervisorPoints.Rows.Count; rowIndex++)
                {
                    for (int columnIndex = 1; columnIndex <= 4; columnIndex++) // Assuming four rating columns
                    {
                        // Find the CheckBox in the current column and row
                        CheckBox checkBox = (CheckBox)gvSupervisorPoints.Rows[rowIndex].FindControl($"RadioButtonSupervisor{columnIndex}");

                        // Check if the CheckBox is checked
                        if (checkBox != null && checkBox.Checked)
                        {
                            int rowNumber = rowIndex + 10;

                            // Append the information to the StringBuilder with a comma
                            sbSupervisorPoints.Append($"{rowNumber}~{columnIndex},");
                        }
                    }
                }
                if (sbSupervisorPoints.Length > 0)
                {
                    sbSupervisorPoints.Remove(sbSupervisorPoints.Length - 1, 1); // Remove the last character (comma)
                }
                leaveFeedbackModel.SupervisorPoints = sbSupervisorPoints.ToString();

                StringBuilder sbCooperation = new StringBuilder();
                for (int rowIndex = 0; rowIndex < gvCooperation.Rows.Count; rowIndex++)
                {
                    for (int columnIndex = 1; columnIndex <= 4; columnIndex++) // Assuming four rating columns
                    {
                        // Find the CheckBox in the current column and row
                        RadioButton checkBox = (RadioButton)gvCooperation.Rows[rowIndex].FindControl($"RadioButtonCooperation{columnIndex}");

                        // Check if the CheckBox is checked
                        if (checkBox != null && checkBox.Checked)
                        {
                            int rowNumber = rowIndex + 1;

                            // Append the information to the StringBuilder with a comma
                            sbCooperation.Append($"{rowNumber}~{columnIndex},");
                        }
                    }
                }
                leaveFeedbackModel.YourRating = sbCooperation.ToString();
                if (rblWillYouRejoin.SelectedValue.ToString() == "Yes")
                    leaveFeedbackModel.RejoinAgain = 1;
                else
                    leaveFeedbackModel.RejoinAgain = 0;
                leaveFeedbackModel.SuggestChanges = txtSuggestedChanges1.Value;
                leaveFeedbackModel.NJ_CompanyName = txtNJ_ComapnyName.Text;
                leaveFeedbackModel.NJ_Designation = txtNJ_Designation.Text;
                leaveFeedbackModel.NJ_Function = txtNJ_Function.Text;

                decimal costToCompany;

                if (decimal.TryParse(txtNJ_CostToCompany.Text, out costToCompany))
                {
                    leaveFeedbackModel.NJ_CostToCompany = costToCompany;
                }
                else
                {
                    leaveFeedbackModel.NJ_CostToCompany = 0;
                }


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;

                        // Check if the record already exists
                        command.CommandText = "SELECT COUNT(*) FROM ETMS_EmployeeExit WHERE UserId = @UserId";
                        command.Parameters.AddWithValue("@Id", leaveFeedbackModel.Id);
                        command.Parameters.AddWithValue("@UserId", leaveFeedbackModel.UserId);

                        int existingRecordsCount = (int)command.ExecuteScalar();

                        if (existingRecordsCount > 0)
                        {
                            // Update the existing record
                            command.CommandText = "UPDATE ETMS_EmployeeExit SET ReasonForLeaving = @ReasonForLeaving, " +
                                                  "ReasonForLeavingAnyOther = @ReasonForLeavingAnyOther, " +
                                                  "CompanyLike = @CompanyLike, " +
                                                  "CompanyDislike = @CompanyDislike, " +
                                                  "SupervisorPoints = @SupervisorPoints, " +
                                                  "YourRating = @YourRating, " +
                                                  "RejoinAgain = @RejoinAgain, " +
                                                  "SuggestChanges = @SuggestChanges, " +
                                                  "NJ_ComapnyName = @NJ_ComapnyName, " +
                                                  "NJ_Designation = @NJ_Designation, " +
                                                  "NJ_Function = @NJ_Function, " +
                                                  "NJ_CostToCompany = @NJ_CostToCompany " +
                                                  "WHERE  UserId = @UserId";
                        }
                        else
                        {
                            // Insert a new record
                            command.CommandText = "INSERT INTO ETMS_EmployeeExit (UserId, ReasonForLeaving, ReasonForLeavingAnyOther, " +
                                                  "CompanyLike, CompanyDislike, SupervisorPoints, YourRating, RejoinAgain, " +
                                                  "SuggestChanges, NJ_ComapnyName, NJ_Designation, NJ_Function, NJ_CostToCompany) " +
                                                  "VALUES (@UserId, @ReasonForLeaving, @ReasonForLeavingAnyOther, " +
                                                  "@CompanyLike, @CompanyDislike, @SupervisorPoints, @YourRating, @RejoinAgain, " +
                                                  "@SuggestChanges, @NJ_ComapnyName, @NJ_Designation, @NJ_Function, " +
                                                  "@NJ_CostToCompany)";
                        }

                        // Add parameters

                        command.Parameters.AddWithValue("@ReasonForLeaving", leaveFeedbackModel.ReasonForLeaving);
                        command.Parameters.AddWithValue("@ReasonForLeavingAnyOther", leaveFeedbackModel.ReasonForLeavingAnyOther);
                        command.Parameters.AddWithValue("@CompanyLike", leaveFeedbackModel.CompanyLike);
                        command.Parameters.AddWithValue("@CompanyDislike", leaveFeedbackModel.CompanyDislike);
                        command.Parameters.AddWithValue("@SupervisorPoints", leaveFeedbackModel.SupervisorPoints);
                        command.Parameters.AddWithValue("@YourRating", leaveFeedbackModel.YourRating);
                        command.Parameters.AddWithValue("@RejoinAgain", leaveFeedbackModel.RejoinAgain);
                        command.Parameters.AddWithValue("@SuggestChanges", leaveFeedbackModel.SuggestChanges);
                        command.Parameters.AddWithValue("@NJ_ComapnyName", leaveFeedbackModel.NJ_CompanyName);
                        command.Parameters.AddWithValue("@NJ_Designation", leaveFeedbackModel.NJ_Designation);
                        command.Parameters.AddWithValue("@NJ_Function", leaveFeedbackModel.NJ_Function);
                        command.Parameters.AddWithValue("@NJ_CostToCompany", leaveFeedbackModel.NJ_CostToCompany);

                        // Execute the query
                        command.ExecuteNonQuery();
                    }
                }
                GetAllEmpExitData();
                string customMessage = "The exit form entry has been successfully added / updated.";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", $"showMessage('{customMessage}');", true);
            }
        }
        public void GetEmployeeExitData()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM [dbo].[ETMS_EmployeeExit]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            List<int> checkedRows = new List<int>();

            if (dataTable.Rows.Count > 0)
            {

                string reasonForLeavingString = dataTable.Rows[0]["ReasonForLeaving"].ToString();

                // Split the string into an array of strings using the comma as a separator
                string[] rowNumbers = reasonForLeavingString.Split(',');

                // Convert each string to an integer and add it to the List
                foreach (string rowNumber in rowNumbers)
                {
                    if (int.TryParse(rowNumber, out int parsedRowNumber))
                    {
                        checkedRows.Add(parsedRowNumber);
                    }
                }


                foreach (GridViewRow row in gvLeavingOptions.Rows)
                {
                    // Find the CheckBox in the current row
                    CheckBox checkBox = (CheckBox)row.FindControl("CheckBox1");

                    // Check if the corresponding row number is in the List of checked rows
                    int rowNumber = row.RowIndex + 1;
                    if (checkedRows.Contains(rowNumber))
                    {
                        checkBox.Checked = true;
                    }
                    else
                    {
                        checkBox.Checked = false;
                    }
                }

                //---------------------------------------------------------------------------------------------------

                // Assume you have retrieved the data from the database and stored it in a string
                string dataFromDatabase = dataTable.Rows[0]["SupervisorPoints"].ToString();

                // Split the string into an array of values

                string[] checkedCellsArray = dataFromDatabase.Split(',')
                                           .Select(cell => cell.Trim())
                                           .Where(cell => !string.IsNullOrEmpty(cell))
                                           .ToArray();

                // Create a HashSet to efficiently check if a cell is checked
                HashSet<string> checkedCellsSet = new HashSet<string>(checkedCellsArray);


                for (int rowIndex = 0; rowIndex < gvSupervisorPoints.Rows.Count; rowIndex++)
                {
                    for (int columnIndex = 1; columnIndex <= 4; columnIndex++) // Assuming four rating columns
                    {
                        // Find the CheckBox in the current column and row
                        CheckBox checkBox = (CheckBox)gvSupervisorPoints.Rows[rowIndex].FindControl($"RadioButtonSupervisor{columnIndex}");

                        // Check if the corresponding cell is checked
                        string cellKey = $"{rowIndex + 1}~{columnIndex}";
                        bool isChecked = checkedCellsSet.Contains(cellKey);

                        // Check or uncheck the checkbox based on the data from the database
                        checkBox.Checked = isChecked;
                    }
                }

                //--------------------------------------------------------------------------------------------------------

                // Assume you have retrieved the data for gvCooperation from the database and stored it in a string
                string dataForCooperationFromDatabase = dataTable.Rows[0]["YourRating"].ToString();

                // Split the string into an array of values

                string[] checkedCellsForCooperationArray = dataForCooperationFromDatabase.Split(',')
                                           .Select(cell => cell.Trim())
                                           .Where(cell => !string.IsNullOrEmpty(cell))
                                           .ToArray();

                // Create a HashSet to efficiently check if a cell is checked
                HashSet<string> checkedCellsForCooperationSet = new HashSet<string>(checkedCellsForCooperationArray);

                for (int rowIndex = 0; rowIndex < gvCooperation.Rows.Count; rowIndex++)
                {
                    for (int columnIndex = 1; columnIndex <= 4; columnIndex++) // Assuming four rating columns
                    {
                        // Find the CheckBox in the current column and row
                        CheckBox checkBox = (CheckBox)gvCooperation.Rows[rowIndex].FindControl($"RadioButtonCooperation{columnIndex}");

                        // Check if the corresponding cell is checked
                        string cellKey = $"{rowIndex + 1}~{columnIndex}";
                        bool isChecked = checkedCellsForCooperationSet.Contains(cellKey);

                        // Check or uncheck the checkbox based on the data from the database
                        checkBox.Checked = isChecked;
                    }
                }

                //---------------------------------------------------

                txtAnyothReasonForLeaving.Value = dataTable.Rows[0]["ReasonForLeavingAnyOther"].ToString();
                txtLikeAbtCompany.Value = dataTable.Rows[0]["CompanyLike"].ToString();
                txtDisLikeAbtCompany.Value = dataTable.Rows[0]["CompanyDislike"].ToString();

                if (dataTable.Rows[0]["CompanyLike"].ToString() == "1")
                    rblWillYouRejoin.SelectedValue = "Yes";
                else
                    rblWillYouRejoin.SelectedValue = "No";
                txtSuggestedChanges1.Value = dataTable.Rows[0]["SuggestChanges"].ToString();

                txtNJ_ComapnyName.Text = dataTable.Rows[0]["NJ_ComapnyName"].ToString();
                txtNJ_Designation.Text = dataTable.Rows[0]["NJ_Designation"].ToString();
                txtNJ_Function.Text = dataTable.Rows[0]["NJ_Function"].ToString();
                txtNJ_CostToCompany.Text = dataTable.Rows[0]["NJ_CostToCompany"].ToString();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageMaster.aspx");
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false; // Assume validation failure by default

            // Find the GridView by its ID
            GridView gvLeavingOptions = (GridView)FindControl("gvLeavingOptions");

            // Iterate through each row in the GridView
            foreach (GridViewRow row in gvLeavingOptions.Rows)
            {
                // Find the CheckBox in the current row
                CheckBox checkBox = (CheckBox)row.FindControl("CheckBox1");

                // Check if the CheckBox is checked
                if (checkBox != null && checkBox.Checked)
                {
                    args.IsValid = true; // Validation success if at least one CheckBox is checked
                    return; // Exit the loop since validation has passed for this condition
                }
            }
        }
        protected void CustomValidatorSupervisor_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false; // Assume validation failure by default

            // Find the GridView by its ID
            GridView gvSupervisorPoints = (GridView)FindControl("gvSupervisorPoints");

            // Iterate through each row in the GridView
            foreach (GridViewRow row in gvSupervisorPoints.Rows)
            {
                // Flag to track whether at least one RadioButton in the current row is checked
                bool isRowValid = false;

                // Loop through all radio buttons in the current row
                for (int i = 1; i <= 4; i++) // Assuming there are four radio buttons
                {
                    // Construct the ID of the RadioButton dynamically
                    string radioButtonID = "RadioButtonSupervisor" + i;

                    // Find the RadioButton in the current row
                    RadioButton radioButton = (RadioButton)row.FindControl(radioButtonID);

                    // Check if the RadioButton is checked
                    if (radioButton != null && radioButton.Checked)
                    {
                        isRowValid = true; // At least one RadioButton in the current row is checked
                        break; // Exit the loop since validation has passed for this condition
                    }
                }

                // If at least one RadioButton is not checked in the current row, set validation failure
                if (!isRowValid)
                {
                    args.IsValid = false;
                    return;
                }
            }

            // If the loop completes for all rows without setting validation failure, set validation success
            args.IsValid = true;
        }
        protected void CustomValidatorCooperation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false; // Assume validation failure by default

            // Find the GridView by its ID
            GridView gvCooperation = (GridView)FindControl("gvCooperation");

            // Iterate through each row in the GridView
            foreach (GridViewRow row in gvCooperation.Rows)
            {
                // Flag to track whether at least one RadioButton in the current row is checked
                bool isRowValid = false;

                // Loop through all radio buttons in the current row
                for (int i = 1; i <= 4; i++) // Assuming there are four radio buttons
                {
                    // Construct the ID of the RadioButton dynamically
                    string radioButtonID = "RadioButtonCooperation" + i;

                    // Find the RadioButton in the current row
                    RadioButton radioButton = (RadioButton)row.FindControl(radioButtonID);

                    // Check if the RadioButton is checked
                    if (radioButton != null && radioButton.Checked)
                    {
                        isRowValid = true; // At least one RadioButton in the current row is checked
                        break; // Exit the loop since validation has passed for this condition
                    }
                }

                // If at least one RadioButton is not checked in the current row, set validation failure
                if (!isRowValid)
                {
                    args.IsValid = false;
                    return;
                }
            }

            // If the loop completes for all rows without setting validation failure, set validation success
            args.IsValid = true;
        }




    }
}