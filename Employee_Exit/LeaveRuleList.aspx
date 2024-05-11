<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveRuleList.aspx.cs" Inherits="Employee_Exit.LeaveRuleList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Management</title>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/fullcalendar.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/fullcalendar.min.css" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="Multiple-Dates-Picker-for-jQuery-UI-latest/jquery-ui.multidatespicker.js"></script>
    <style>
        

        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }

        label {
            display: inline-block;
            margin-right: 10px;
        }

        .calendar-container {
            display: flex;
            flex-wrap: wrap;
        }

        .month h3 {
            text-align: center;
            margin-bottom: 10px;
            color: #333;
        }

        .month .days {
            list-style: none;
            padding: 0;
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
        }

            .month .days li:hover {
                background-color: #e0e0e0;
            }

            .month .days li {
                width: calc(14.2857% - 10px);
                box-sizing: border-box;
                text-align: center;
                padding: 10px;
                margin: 5px 0;
                border: 1px solid #ddd;
                background-color: #fff;
                transition: background-color 0.3s;
            }

        .weekday-header {
            width: calc(14.2857% - 10px);
            box-sizing: border-box;
            text-align: center;
            padding: 10px;
            margin: 5px 0;
            background-color: #f0f0f0;
            border: 1px solid #ddd;
        }

        .highlighted {
            background-color: #aaffaa !important;
        }

        #gridContainer {
            border-collapse: collapse;
            width: 100%;
        }

            #gridContainer th,
            #gridContainer td {
                border: 1px solid #dddddd;
                text-align: left;
                padding: 8px;
            }

            #gridContainer th {
                background-color: #f2f2f2;
            }
    </style>
   

    <script type="text/javascript">  
        function alertMessage() {
            alert('Data already exists. Not performing the insert.');
        }

        

    </script>

   
</head>
<body>
     
    <form id="form1" runat="server">
        <h1>Leave Management</h1>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <label>Leave Type:</label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="radLeaveType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Full Day" Value="1" />
                                <asp:ListItem Text="Half Day" Value="2" />
                                <asp:ListItem Text="Short Day" Value="3" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Weekdays:</label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkWeekdays" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Sunday" Value="Sunday" />
                                <asp:ListItem Text="Monday" Value="Monday" />
                                <asp:ListItem Text="Tuesday" Value="Tuesday" />
                                <asp:ListItem Text="Wednesday" Value="Wednesday" />
                                <asp:ListItem Text="Thursday" Value="Thursday" />
                                <asp:ListItem Text="Friday" Value="Friday" />
                                <asp:ListItem Text="Saturday" Value="Saturday" />
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Weeks:</label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkWeeks" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Week 1" Value="Week1" />
                                <asp:ListItem Text="Week 2" Value="Week2" />
                                <asp:ListItem Text="Week 3" Value="Week3" />
                                <asp:ListItem Text="Week 4" Value="Week4" />
                                <asp:ListItem Text="Week 5" Value="Week5" />
                                <asp:ListItem Text="Week 6" Value="Week6" />
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Year:</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server">
                                <asp:ListItem Value="2022">2022</asp:ListItem>
                                <asp:ListItem Value="2023">2023</asp:ListItem>
                                <asp:ListItem Value="2024">2024</asp:ListItem>
                                <asp:ListItem Value="2025">2025</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAddRule" runat="server" Text="Add Rule" OnClick="btnAddRule_Click" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="min-width: 30%">
                            <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView_RowCommand" OnRowDeleting="gridView_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="Options" HeaderText="Options" ItemStyle-CssClass="optionsCell" Visible="true" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:Button runat="server" CommandName="Apply" Text="Apply" CssClass="applyButton" CommandArgument='<%# Container.DataItemIndex %>' />
                                            <asp:Button runat="server" CommandName="Delete" Text="Delete" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Formatted Dates" ItemStyle-CssClass="formattedDatesCell" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblFormattedDates" Text='<%# Eval("FormattedDates") %>' Visible="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnApplyAll" runat="server" Text="Apply All" OnClick="btnApplyAll_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- FullCalendar container -->
       
        <div id="mdp-demo"></div>
    </form>
</body>
    
<script type="text/javascript">
    $(document).ready(function () {
        console.log('Document is ready.');
        // Initialize the MultiDatesPicker
        initMultiDatesPicker();
    });

    function initMultiDatesPicker() {
        console.log(getFormattedDatesArray());
        var y = $('#<%=ddlYear.ClientID%>').val();
        $('#mdp-demo').multiDatesPicker('destroy');
        console.log('inside initMultiDatesPicker');
        $('#mdp-demo').multiDatesPicker({
            
            //addDates: ['10/14/' + y, '02/19/' + y, '01/14/' + y, '11/16/' + y], 
            addDates: getFormattedDatesArray(),
            numberOfMonths: [3, 4],
            defaultDate: '1/1/' + y
        });
    }

    function getFormattedDatesArray() {
        var datesArray = [];

        // Iterate through each GridView row and get the lblFormattedDates value
        $('#<%=gridView.ClientID%> tr').each(function () {
            var formattedDates = $(this).find('.formattedDatesCell').text();

            if (formattedDates) {
                // Split the formattedDates string and add each date to the array
                var dates = formattedDates.split(', ');

                // Format each date and add to the array
                dates.forEach(function (date) {
                    // Parse the date string to a JavaScript Date object
                    var parsedDate = new Date(date);

                    // Format the date as MM/DD/YYYY and add to the array
                    datesArray.push((parsedDate.getMonth() + 1) + '/' + parsedDate.getDate() + '/' + parsedDate.getFullYear());
                });
            }
        });

        // Log the formatted dates array for debugging
        console.log(datesArray);

        return datesArray;
    }


    // Handle DropDownList change event
    $('#<%=ddlYear.ClientID%>').change(function () {
        // Reinitialize MultiDatesPicker when the year changes
        initMultiDatesPicker();
    });

    // Handle Apply button click event     
    $(document).on('click', '#<%=gridView.ClientID%> .applyButton', function () {
        // Reinitialize MultiDatesPicker when Apply button is clicked
        console.log('Apply button clicked.');
        initMultiDatesPicker();
    });


</script>


</html>
