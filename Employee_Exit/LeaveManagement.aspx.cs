using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Employee_Exit
{
    public partial class LeaveManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate years dynamically or set a range
                int startYear = DateTime.Now.Year;
                int endYear = startYear + 5; // Set the range for the next 5 years

                for (int year = startYear; year <= endYear; year++)
                {
                    ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }

                // Display calendar for the current year on initial load
                DisplayCalendar(startYear);
            }

        }
        protected void btnAddRule_Click(object sender, EventArgs e)
        {
            // Retrieve selected values and add to GridView
            string leaveType = rblLeaveTypes.SelectedValue;
            List<string> selectedWeekdays = cblWeekdays.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();
            List<string> selectedWeeks = cblWeeks.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();
            string selectedYear = ddlYear.SelectedValue;

            // Add logic to insert the selected rule into GridView
            // e.g., gvRules.Rows.Add(leaveType, selectedWeekdays, selectedWeek, selectedYear);
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display calendar for the selected year when the year dropdown changes
            int selectedYear = int.Parse(ddlYear.SelectedValue);
            DisplayCalendar(selectedYear);
        }

        private void DisplayCalendar(int year)
        {
            calendarContainer.Controls.Clear(); // Clear previous calendars

            for (int month = 1; month <= 12; month++)
            {
                System.Web.UI.WebControls.Calendar calendar = new System.Web.UI.WebControls.Calendar();
                calendar.ShowTitle = true;
                //calendar.ShowGridLines = CalendarShowGridLines.None; // Adjust grid lines as needed
                calendar.SelectionMode = CalendarSelectionMode.None;
                calendar.Caption = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

                // Set the month and year for the calendar
                calendar.VisibleDate = new DateTime(year, month, 1);

                // Add the calendar to the container
                calendarContainer.Controls.Add(calendar);
            }
        }
    }
}