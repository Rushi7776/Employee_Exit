using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Employee_Exit
{
    public partial class LeaveRuleList : System.Web.UI.Page
    {
        string connectionString = @"Data Source=GZN00001028\MSSQLSERVER01;Initial Catalog=EmployeeExit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        DataTable SelectedDates;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ViewState["GridData"] = null;
                //InitializeGridView();
                //radLeaveType.Items[0].Selected = true;

                ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
                Session["SessionId"] = ViewState["ViewStateId"].ToString();

                ViewState["GridData"] = null;
                InitializeGridView();
                radLeaveType.Items[0].Selected = true;

                // Clear GridView data
                gridView.DataSource = null;
                gridView.DataBind();

                // Disable ViewState for the GridView
                gridView.EnableViewState = false;
            }
            else
            {
                if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
                {
                    ViewState["GridData"] = null;
                    gridView.DataSource = null;
                    gridView.DataBind();
                    radLeaveType.Items[0].Selected = true;
                }

                Session["SessionId"] = System.Guid.NewGuid().ToString();
                ViewState["ViewStateId"] = Session["SessionId"].ToString();
            }

        }
        
        private void InitializeGridView()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Options");
            dt.Columns.Add("FormattedDates");
            gridView.DataSource = dt;
            ViewState["GridData"] = dt;
            gridView.DataBind();
        }

        private void AddSelectedDatesToList(int year, string week, string weekday)
        {
            // Retrieve the list from the session or create a new one
            List<string> selectedDates = Session["SelectedDates"] as List<string> ?? new List<string>();

            // Get the first day of the selected year
            DateTime jan1 = new DateTime(year, 1, 1);

            for (int month = 1; month <= 12; month++)
            {
                // Calculate the target day in the target week
                int daysToFirstDay = (int)Enum.Parse(typeof(DayOfWeek), weekday) - (int)jan1.DayOfWeek;
                int daysToTargetWeek = (7 * (int)Enum.Parse(typeof(DayOfWeek), week.Substring(4))) + daysToFirstDay;

                // Adjust for negative values
                if (daysToTargetWeek < 0)
                {
                    daysToTargetWeek += 7;
                }

                DateTime targetDate = jan1.AddDays(daysToTargetWeek);

                // Ensure the date belongs to the correct ISO week and is a Sunday
                if (GetISOWeek(targetDate) == 1 && targetDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    // Add the formatted date to the list
                    selectedDates.Add(targetDate.ToString("MMMM d, yyyy"));
                }

                // Move to the next month
                jan1 = jan1.AddMonths(1);
            }

            // Update the session with the modified list
            Session["SelectedDates"] = selectedDates;


        }


        protected void btnAddRule_Click(object sender, EventArgs e)
        {
            // Get the values
            string leaveType = GetSelectedRadioButtonValue("radLeaveType");
            string selectedDay = GetSelectedCheckboxesValuesAsString("chkWeekdays");
            string selectedWeek = GetSelectedCheckboxesValuesAsString("chkWeeks");

            List<string> selectedDays = GetSelectedCheckboxesValuesAsList("chkWeekdays");
            List<string> selectedWeeks = GetSelectedCheckboxesValuesAsList("chkWeeks");

            // Combine the values into a single string
            string resultString = $"{leaveType} {selectedDay} {selectedWeek}";


            DataTable dt = (DataTable)(ViewState["GridData"] ?? CreateDataTable());
            List<DateTime> selectedDates = GetFormattedSelectedDates(int.Parse(ddlYear.SelectedValue), selectedWeeks, selectedDays);

            // Concatenate selected dates into a string
            string formattedDates = string.Join(", ", selectedDates.Select(date => date.ToString("MM/dd/yyyy")));
            // Add a new row to the DataTable
            DataRow newRow = dt.NewRow();
            newRow["Options"] = resultString;
            newRow["FormattedDates"] = formattedDates; // Add the new column and set its value
            dt.Rows.Add(newRow);

            // Save the DataTable to ViewState
            ViewState["GridData"] = dt;

            // Bind the data to the GridView
            gridView.DataSource = dt;
            gridView.DataBind();
        }
        private List<string> GetSelectedCheckboxesValuesAsList(string checkboxListID)
        {
            List<string> selectedValues = new List<string>();

            CheckBoxList checkBoxList = FindControl(checkboxListID) as CheckBoxList;

            if (checkBoxList != null)
            {
                foreach (ListItem item in checkBoxList.Items)
                {
                    if (item.Selected)
                    {
                        selectedValues.Add(item.Value);
                    }
                }
            }

            return selectedValues;
        }




        //private List<DateTime> GetFormattedSelectedDates(int year, string week, string weekday)
        //{
        //    List<DateTime> formattedDates = new List<DateTime>();

        //    // Get the first day of the selected year
        //    DateTime jan1 = new DateTime(year, 1, 1);

        //    for (int month = 1; month <= 12; month++)
        //    {
        //        // Calculate the target day in the target week
        //        int daysToFirstDay = ((int)Enum.Parse(typeof(DayOfWeek), weekday) - (int)jan1.DayOfWeek + 7) % 7;
        //        int daysToTargetWeek = (7 * (int)Enum.Parse(typeof(DayOfWeek), week.Substring(4))) + daysToFirstDay;

        //        DateTime targetDate = jan1.AddDays(daysToTargetWeek);

        //        // Ensure the date belongs to the correct ISO week
        //        if (GetISOWeek(targetDate) == int.Parse(week.Substring(4)))
        //        {
        //            formattedDates.Add(targetDate);
        //        }

        //        // Move to the next month
        //        jan1 = jan1.AddMonths(1);
        //    }

        //    return formattedDates;
        //}
        private List<DateTime> GetFormattedSelectedDates(int year, List<string> weeks, List<string> weekdays)
        {
            List<DateTime> formattedDates = new List<DateTime>();

            foreach (string week in weeks)
            {
                int targetWeek = int.Parse(week.Substring(4));

                for (int month = 1; month <= 12; month++)
                {
                    DateTime firstDayOfMonth = new DateTime(year, month, 1);
                    DateTime firstWeekdayOfMonth = GetFirstWeekdayOfMonth(firstDayOfMonth, weekdays);

                    DateTime targetDate = firstWeekdayOfMonth.AddDays((targetWeek - 1) * 7);

                    // Ensure that the target date is within the current month
                    if (targetDate.Month == month && targetDate.Year == year)
                    {
                        formattedDates.Add(targetDate);
                    }
                }
            }

            return formattedDates;
        }

        private DateTime GetFirstWeekdayOfMonth(DateTime date, List<string> weekdays)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            while (!weekdays.Contains(firstDayOfMonth.DayOfWeek.ToString()))
            {
                firstDayOfMonth = firstDayOfMonth.AddDays(1);
            }

            return firstDayOfMonth;
        }









        //private List<DateTime> GetFormattedSelectedDates(int year, string week, string weekday)
        //{
        //    List<DateTime> formattedDates = new List<DateTime>();

        //    // Get the first day of the selected year
        //    DateTime jan1 = new DateTime(year, 1, 1);

        //    for (int month = 1; month <= 12; month++)
        //    {
        //        // Calculate the target day in the target week
        //        int daysToFirstDay = (int)Enum.Parse(typeof(DayOfWeek), weekday) - (int)jan1.DayOfWeek;
        //        int daysToTargetWeek = (7 * (int)Enum.Parse(typeof(DayOfWeek), week.Substring(4))) + daysToFirstDay;

        //        // Adjust for negative values
        //        if (daysToTargetWeek < 0)
        //        {
        //            daysToTargetWeek += 7;
        //        }

        //        DateTime targetDate = jan1.AddDays(daysToTargetWeek);

        //        // Ensure the date belongs to the correct ISO week and is a Sunday
        //        if (GetISOWeek(targetDate) == 1 && targetDate.DayOfWeek == DayOfWeek.Sunday)
        //        {
        //            formattedDates.Add(targetDate);
        //        }

        //        // Move to the next month
        //        jan1 = jan1.AddMonths(1);
        //    }

        //    return formattedDates;
        //}

        private int GetISOWeek(DateTime date)
        {
            int jan1 = new DateTime(date.Year, 1, 1).DayOfWeek - DayOfWeek.Monday;
            int daysOffset = (jan1 + 7) % 7;

            DateTime firstMonday = date.AddDays(-daysOffset);
            TimeSpan ts = date - firstMonday;

            int weekNumber = ts.Days / 7 + 1;
            return weekNumber;
        }

        private DataTable CreateDataTable()
        {
            // Create a DataTable with a column named "Options"
            DataTable dt = new DataTable();
            dt.Columns.Add("Options");
            dt.Columns.Add("FormattedDates");
            return dt;
        }
        private DataTable CreateDataTable1()
        {
            // Create a DataTable with a column named "Options"
            DataTable dt = new DataTable();
            dt.Columns.Add("SelectedDate");
            return dt;
        }

        // Helper method to get ISO week number

        private string GetSelectedRadioButtonValue(string groupName)
        {
            // Implement this method to retrieve the selected value from radio buttons
            // For simplicity, let's assume you have a single RadioButtonList with the specified group name
            RadioButtonList radioButtonList = FindControlRecursive(form1, groupName) as RadioButtonList;
            return radioButtonList != null ? radioButtonList.SelectedItem.Text : null;
        }

        private string GetSelectedCheckboxesValuesAsString(string groupName)
        {
            // Find the CheckBoxList with the specified group name
            CheckBoxList checkBoxList = FindControlRecursive(form1, groupName) as CheckBoxList;

            if (checkBoxList != null)
            {
                // Get the selected values and concatenate them into a comma-separated string
                string selectedValues = string.Join(", ", checkBoxList.Items.Cast<ListItem>()
                    .Where(li => li.Selected)
                    .Select(li => li.Value));

                return selectedValues;
            }
            else
            {
                return string.Empty; // or null, depending on your preference
            }
        }


        // A helper method to recursively find a control by ID
        private Control FindControlRecursive(Control parent, string id)
        {
            if (parent.ID == id)
            {
                return parent;
            }

            foreach (Control control in parent.Controls)
            {
                Control foundControl = FindControlRecursive(control, id);
                if (foundControl != null)
                {
                    return foundControl;
                }
            }

            return null;
        }
        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Empty handler for RowDeleting event
            // This can be left blank
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Apply")
            {
                // Get the row index from the CommandArgument
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Access the GridView row based on the index
                GridViewRow row = gridView.Rows[rowIndex];

                // Find the control within the row that holds the FormattedDates value
                Label lblFormattedDates = (Label)row.FindControl("lblFormattedDates");

                // Get the FormattedDates value
                string formattedDates = lblFormattedDates.Text;

                // Now, you can save the FormattedDates value to the database or perform any other necessary actions
               // SaveToDatabase(formattedDates, Convert.ToInt32(radLeaveType.SelectedValue));

                
                //string script = "alert('Rule added successfully!');";
                //ClientScript.RegisterStartupScript(this.GetType(), "RuleAddedScript", script, true);
            }
            else if (e.CommandName == "Delete")
            {
                int rowIndex1 = Convert.ToInt32(e.CommandArgument);
                //if (int.TryParse(e.CommandArgument.ToString(), out int rowIndex) && rowIndex >= 0 && rowIndex < gridView.Rows.Count)
                //{
                // Retrieve the DataKey value using the DataKeyNames property
                // string selectedRule = gridView.DataKeys[rowIndex]["Options"].ToString();

                // Perform the necessary action based on the selected rule
                // For example, delete the data from your data source

                // Assuming your data source is a DataTable, you might do something like this:
                DataTable dataTable = (DataTable)ViewState["GridData"];

                if (dataTable != null && dataTable.Rows.Count > rowIndex1)
                {
                    DataRow rowToDelete = dataTable.Rows[rowIndex1];
                    rowToDelete.Delete();
                    dataTable.AcceptChanges();
                    ViewState["GridData"] = dataTable;
                    gridView.DataSource = dataTable;
                }

                // Refresh GridView to display the updated rules
                gridView.DataBind();
                // }


            }
        }

        private int GetColumnIndexByName(string columnName)
        {
            // Loop through the columns to find the index by name
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                if (gridView.Columns[i] is BoundField && ((BoundField)gridView.Columns[i]).DataField.Equals(columnName))
                {
                    return i;
                }
            }
            // If not found, return -1 or handle it according to your logic
            return -1;
        }

        private void SaveToDatabase(string formattedDates, int holidayType)
        {
            // Assuming you have a SqlConnection already established
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Split the original string into an array of date components
                string[] dateArray = formattedDates.Split(',').Select(date => date.Trim()).ToArray();

                // Format each date component with single quotes and join them with commas
                string result = string.Join(", ", dateArray.Select(date => $"'{date}'"));

                using (SqlCommand checkCmd = new SqlCommand($"SELECT COUNT(*) FROM LeaveManagement WHERE HolidayType = @HolidayType AND Holiday_Date IN ({result})", connection))
                {
                    checkCmd.Parameters.AddWithValue("@HolidayType", holidayType);

                    try
                    {
                        connection.Open();
                        int count = (int)checkCmd.ExecuteScalar();

                        // Check if the data already exists
                        if (count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
                            
                        }
                        else
                        {
                            // Data doesn't exist, proceed with the insert
                            using (SqlCommand cmd = new SqlCommand("InsertLeaveManagement", connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                // Assuming the parameters for your stored procedure are as follows
                                cmd.Parameters.AddWithValue("@HolidayType", holidayType);
                                cmd.Parameters.AddWithValue("@CompanyClient_Id", 1);
                                cmd.Parameters.AddWithValue("@Is_Active", 1);
                                cmd.Parameters.AddWithValue("@CreatedBy", 1);
                                cmd.Parameters.AddWithValue("@SelectedDates", formattedDates);

                                cmd.ExecuteNonQuery();

                                string formattedDatesArray = "[" + formattedDates + "]";
                                ClientScript.RegisterStartupScript(this.GetType(), "SetFormattedDatesArray", $"var formattedDatesArray = {formattedDatesArray};", true);

                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle the exception, log, or display an error message
                        Console.WriteLine($"Database Error: {ex.Message}");
                    }
                }
            }

        }

        protected void btnApplyAll_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (GridViewRow row in gridView.Rows)
            {
                // Find the label control within the row
                Label lblFormattedDates = (Label)row.FindControl("lblFormattedDates");

                stringBuilder.Append(lblFormattedDates.Text.ToString());

                // Now, you can use the 'formattedDates' value as needed

            }
            SaveToDatabase(stringBuilder.ToString(), Convert.ToInt32(radLeaveType.SelectedValue));
            string script = "alert('Rules added successfully!');";
            ClientScript.RegisterStartupScript(this.GetType(), "RuleAddedScript", script, true);
        }


    }
}