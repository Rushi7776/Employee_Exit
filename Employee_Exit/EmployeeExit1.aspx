<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeExit1.aspx.cs" Inherits="Employee_Exit.EmployeeExit1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <table>
    <tr>
        <td class="section-heading" colspan="2">
            1. Personal Details:
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <label for="Select1">Employee Name:</label>
        </td>
        <td>
            <select id="Select1" name="D1">
                <option value="101"></option>
            </select>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <label for="Name">Name:</label>
        </td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            (SWS00000)
        </td>
    </tr>
    <!-- ... (Other Personal Details) ... -->
    
    <tr>
        <td class="section-heading" colspan="2">
            2. Reasons for leaving the company: (multiples reasons can be selected)
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="gvLeavingOptions" runat="server" AutoGenerateColumns="false" Width="100%" ShowHeader="false">
                <Columns>
                    <asp:BoundField DataField="Description" HeaderText="" SortExpression="Description" />
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    
    <tr>
        <td class="section-heading" colspan="2">
            3. What did you like and dislike about the company?
        </td>
    </tr>
    <tr>
        <td>Like:</td>
        <td>
            <asp:TextBox ID="TextBox9" runat="server" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Dislike:</td>
        <td>
            <asp:TextBox ID="TextBox10" runat="server" Width="100%"></asp:TextBox>
        </td>
    </tr>
     <tr>
     <td class="section-heading" colspan="2">
         4.	What did you think of your supervisor on the following points?
     </td>
 </tr>
         <tr>
            <td colspan="2">
                <asp:GridView ID="gvSupervisorPoints" runat="server" AutoGenerateColumns="false" Width="100%" ShowHeader="false">
                    <Columns>
                        <asp:BoundField DataField="Description" HeaderText="" SortExpression="Description" />
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:CheckBox ID="CheckBoxSupervisor" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxSupervisor" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    <tr>
        <td class="auto-style1">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
        </td>
        <td class="auto-style1">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </td>
    </tr>
</table>
</asp:Content>
