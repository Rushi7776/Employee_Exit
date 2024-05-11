using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antlr.Runtime.Tree;
using Microsoft.Ajax.Utilities;
using System.Web.Optimization;

namespace Employee_Exit
{
    public partial class ManageMaster : System.Web.UI.Page
    {
        string connectionString = @"Data Source=GZN00001028\MSSQLSERVER01;Initial Catalog=EmployeeExit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Call a method to bind DropDownList options
                BindDropDownListOptions();
                
            }
            //else
            //{
            //    GridView1.DataSource = null; GridView1.DataBind();
            //}            
        }
        private void BindDropDownListOptions()
        {
            // Define the options
            //string[] options = {
            //    "Reasons for leaving the company",
            //    "What did you think of your supervisor on the following points?",
            //    "How would you rate the following?"
            //};

            Dictionary<int, string> options = new Dictionary<int, string>();

            //Adding key value pair to the dictionary
            options.Add(1, "Reasons for leaving the company");
            options.Add(2, "What did you think of your supervisor on the following points?");
            options.Add(3, "How would you rate the following?");


            // Bind options to DropDownList
            //ddlBindOPtions.DataSource = options;
            //ddlBindOPtions.DataBind();

            ddlBindOPtions.DataTextField = "Value";
            ddlBindOPtions.DataValueField = "Key";
            ddlBindOPtions.DataSource = options;
            ddlBindOPtions.DataBind();

            // Add an empty item at the beginning if needed
            ddlBindOPtions.Items.Insert(0, new ListItem("-- Select an option --", ""));
        }
        protected void ddlBindOPtions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected value and text
            string selectedValue = ddlBindOPtions.SelectedValue;
            string selectedText = ddlBindOPtions.SelectedItem.Text;

            // Perform actions based on the selected value or text
            // For example, display in a label
            
        }

        private void BindGridView()
        {
            string query = string.Empty;
            // SQL query
            if (ddlBindOPtions.SelectedItem.Text == "-- Select an option --")
            {
                query = "Select  id, description from [dbo].[ETMS_Master_LeavingOptions] where isdeleted = 0";
            }
            else 
            {
                query = "Select id, description from [dbo].[ETMS_Master_LeavingOptions] where isdeleted = 0 and optiontype = "+ ddlBindOPtions.SelectedValue;
            }

            
             

            // Create a SqlConnection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlDataAdapter
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    // Create a DataTable to store the data
                    DataTable dataTable = new DataTable();

                    // Open the connection
                    connection.Open();

                    // Fill the DataTable with data from the query
                    adapter.Fill(dataTable);

                    // Close the connection
                    connection.Close();

                    // Bind the GridView with the DataTable
                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView(); // Call the method to rebind the GridView after entering edit mode
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                // Finding the controls from GridView for the row which is going to update
                GridViewRow row = GridView1.Rows[e.RowIndex];
                TextBox txt_id = row.FindControl("txt_id") as TextBox;

                TextBox descriptionTextBox = GridView1.Rows[e.RowIndex].FindControl("txt_description") as TextBox;

                if (txt_id != null && descriptionTextBox != null)
                {
                   
                    string id = txt_id.Text;
                    string description = descriptionTextBox.Text;

                   
                    // SQL update statement
                    string updateQuery = "UPDATE [dbo].[ETMS_Master_LeavingOptions] SET Description = @Description WHERE id = @ID";

                    // Create a SqlConnection and a SqlCommand
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // Set the parameters
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@Description", description);

                        // Open the connection
                        connection.Open();

                        // Execute the update command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            // Successfully updated
                            GridView1.EditIndex = -1;
                            BindGridView(); // Rebind the GridView after updating

                            string customMessage = "The master entry has been successfully updated.";
                            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", $"showMessage('{customMessage}');", true);
                        }
                        else
                        {
                            // Update failed
                            // Handle the error or display a message to the user
                        }
                    }
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                
                string customMessage = "Something went wrong while adding master entry.";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", $"showMessage('{customMessage}');", true);
            }
        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                GridView1.EditIndex = -1;
                BindGridView(); // Rebind the GridView to cancel the edit mode
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // Log the error, display a message, etc.
            }
        }

        

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;

                // Check if the row index is valid
                if (rowIndex >= 0 && rowIndex < GridView1.Rows.Count)
                {
                    // Check if DataKeys collection is not null and has values
                    if (GridView1.DataKeys != null && GridView1.DataKeys.Count > rowIndex)
                    {
                        // Accessing the primary key value from DataKeys
                        int id = Convert.ToInt32(GridView1.DataKeys[rowIndex].Value);

                        // Perform the deletion logic
                        string deleteQuery = "UPDATE [dbo].[ETMS_Master_LeavingOptions] SET isdeleted = 1 WHERE id = @ID";

                        // Create a SqlConnection and a SqlCommand
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                        {
                            // Set the parameter
                            command.Parameters.AddWithValue("@ID", id);

                            // Open the connection
                            connection.Open();

                            // Execute the update command
                            int rowsAffected = command.ExecuteNonQuery();

                            // Close the connection
                            connection.Close();

                            if (rowsAffected > 0)
                            {
                                // Successfully updated (marked as deleted)
                                BindGridView(); // Rebind the GridView after updating

                                string customMessage = "The master entry has been successfully marked as deleted.";
                                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", $"showMessage('{customMessage}');", true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // Log the error, display a message, etc.
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ddlBindOPtions.SelectedValue.ToString() != "0")
            {
                // Assuming you have a valid SqlConnection object named 'connection'
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Assuming TextBox1 is the ID of your TextBox control
                    // and ddlBindOptions is the ID of your DropDownList control
                    string description = TextBox1.Text;
                    string optionType = ddlBindOPtions.SelectedValue;

                    // SQL insert statement
                    string insertQuery = "INSERT INTO [dbo].[ETMS_Master_LeavingOptions] (description, optiontype,IsDeleted) VALUES (@Description, @OptionType,@IsDeleted);";

                    // Create a SqlCommand
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@OptionType", optionType); 
                        command.Parameters.AddWithValue("@IsDeleted", 0);
                        // Open the connection
                        connection.Open();

                        // Execute the insert command
                        command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();
                    }
                }

                // After adding the record, you might want to refresh the GridView or perform any other necessary actions
                BindGridView(); // Call a method to bind the GridView with updated data
            }
            else
                Response.Write("<script>alert('Please select option..');window.location = 'newpage.aspx';</script>");
        }

        protected void btnAdd_click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    if (ddlBindOPtions.SelectedValue.ToString() == "")
                    {
                        Response.Write("<script>alert('Please select option..');window.location = 'ManageMaster.aspx';</script>");

                    }
                    if (string.IsNullOrEmpty(TextBox1.Text))
                    {
                        Response.Write("<script>alert('Reason is empty..');window.location = 'ManageMaster.aspx';</script>");
                    }
                    if (ddlBindOPtions.SelectedValue.ToString() != "" && !string.IsNullOrEmpty(TextBox1.Text))
                    {
                        // Assuming you have a valid SqlConnection object named 'connection'
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            // Assuming TextBox1 is the ID of your TextBox control
                            // and ddlBindOptions is the ID of your DropDownList control
                            string description = TextBox1.Text.Trim();
                            string optionType = ddlBindOPtions.SelectedValue;

                            // Check for duplicate entry
                            if (!CheckDuplicateEntry(description, optionType, connection))
                            {
                                // SQL insert statement
                                string insertQuery = "INSERT INTO [dbo].[ETMS_Master_LeavingOptions] (description, optiontype, IsDeleted) VALUES (@Description, @OptionType, @IsDeleted);";

                                // Create a SqlCommand
                                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                                {
                                    // Add parameters to prevent SQL injection
                                    command.Parameters.AddWithValue("@Description", description);
                                    command.Parameters.AddWithValue("@OptionType", optionType);
                                    command.Parameters.AddWithValue("@IsDeleted", 0);

                                    // Open the connection
                                    connection.Open();

                                    // Execute the insert command
                                    command.ExecuteNonQuery();

                                    // Close the connection
                                    connection.Close();
                                }
                            }
                            else
                            {

                                string customMessageForDuplicateEntry = "Duplicate entry found for " + TextBox1.Text.Trim();
                                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", $"showMessage('{customMessageForDuplicateEntry}');", true);
                            }
                        }
                        TextBox1.Text = string.Empty;
                        // After adding the record, you might want to refresh the GridView or perform any other necessary actions
                        BindGridView(); // Call a method to bind the GridView with updated data

                        string customMessage = "The master entry has been successfully added.";
                        ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", $"showMessage('{customMessage}');", true);
                    }
                }
                catch
                {
                    string customMessage = "Something went wrong while adding master entry.";
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", $"showMessage('{customMessage}');", true);
                }
            }

        }

        protected void btnLinktoEmpExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpExit.aspx");
        }
        // Function to check for duplicate entry
        private bool CheckDuplicateEntry(string description, string optionType, SqlConnection connection)
        {
            string selectQuery = "SELECT COUNT(*) FROM [dbo].[ETMS_Master_LeavingOptions] WHERE description = @Description AND optiontype = @OptionType AND IsDeleted = 0;";

            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@OptionType", optionType);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();

                // If count is greater than 0, a duplicate entry exists
                return count > 0;
            }
        }
    }

    
}