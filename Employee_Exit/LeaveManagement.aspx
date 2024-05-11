<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveManagement.aspx.cs" Inherits="Employee_Exit.LeaveManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Leave Management Application</h3>
            <div>
                <label>Leave Type:</label>
                <asp:RadioButtonList ID="rblLeaveTypes" runat="server">
                    <asp:ListItem Text="Full Day" Value="FullDay" />
                    <asp:ListItem Text="Half Day" Value="HalfDay" />
                    <asp:ListItem Text="Short Day" Value="ShortDay" />
                </asp:RadioButtonList>
            </div>

            <div>
                <label>Weekdays:</label>
                <asp:CheckBoxList ID="cblWeekdays" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Monday" Value="Monday" />
                    <asp:ListItem Text="Tuesday" Value="Tuesday" />
                    <asp:ListItem Text="Wednesday" Value="Wednesday" />
                    <asp:ListItem Text="Thursday" Value="Thursday" />
                    <asp:ListItem Text="Friday" Value="Friday" />
                    <asp:ListItem Text="Saturday" Value="Saturday" />
                    <asp:ListItem Text="Sunday" Value="Sunday" />
                </asp:CheckBoxList>
            </div>

            <div>
                <label>Weeks:</label>
                <asp:CheckBoxList ID="cblWeeks" runat="server" RepeatDirection="Horizontal">
    <asp:ListItem Text="Week 1" Value="Week1" />
    <asp:ListItem Text="Week 2" Value="Week2" />
    <asp:ListItem Text="Week 3" Value="Week3" />
    <asp:ListItem Text="Week 4" Value="Week4" />
    <asp:ListItem Text="Week 5" Value="Week5" />
    <asp:ListItem Text="Week 6" Value="Week6" />
</asp:CheckBoxList>

            </div>

            <div>
                <label>Year:</label>
                <asp:DropDownList ID="ddlYear" runat="server">
                </asp:DropDownList>
            </div>

            <div>
                <asp:Button ID="btnAddRule" runat="server" Text="Add Rule" OnClick="btnAddRule_Click" />
            </div>

            <div>
                <asp:GridView ID="gvRules" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Options" HeaderText="Options" ItemStyle-CssClass="optionsCell" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button runat="server" CommandName="Apply" Text="Apply" CssClass="applyButton" />
                                <asp:Button runat="server" CommandName="Delete" Text="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div>
                <label>Calendar for Selected Year:</label>
                <asp:Panel ID="calendarContainer" runat="server" CssClass="calendar-container">
                    <!-- Calendar controls will be added dynamically here -->
                </asp:Panel>
            </div>
        </div>

    </form>
</body>
</html>
