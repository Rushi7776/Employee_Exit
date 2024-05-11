<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Employee_Exit._Default" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="MainContent">
    <asp:Label ID="Label1" runat="server" Text="Employee Name: "></asp:Label>
    <select id="Select1" name="D1">
        <option></option>
    </select><br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Reasons for leaving the company: "></asp:Label>
    <asp:CheckBoxList ID="CheckBoxList1" runat="server" Width="316px">
        <asp:ListItem>Poor Salary</asp:ListItem>
        <asp:ListItem>Lack Of Recognition</asp:ListItem>
        <asp:ListItem>Interpersonal Conflicts</asp:ListItem>
        <asp:ListItem>Opportunities Abroad	</asp:ListItem>
        <asp:ListItem>Commutation To Workplace</asp:ListItem>
        <asp:ListItem>Marriage</asp:ListItem>
        <asp:ListItem>To Enter Business</asp:ListItem>
        <asp:ListItem>Empowerment</asp:ListItem>
        <asp:ListItem>Job Rotation  </asp:ListItem>
        <asp:ListItem>No Promotion Opportunities</asp:ListItem>
        <asp:ListItem>Infrastructure  </asp:ListItem>
        <asp:ListItem>To Pursue Studies</asp:ListItem>
        <asp:ListItem>Health Issue</asp:ListItem>
        <asp:ListItem>Family Problems</asp:ListItem>
        <asp:ListItem>Quality Of Work</asp:ListItem>
        <asp:ListItem>Role Clarity</asp:ListItem>
    </asp:CheckBoxList>
    <br />
    <asp:Label ID="Label3" runat="server" Text="What did you like and dislike about the company? "></asp:Label>
    <textarea id="TextArea1" cols="20" name="S1" rows="2"></textarea><br />
    <br />
    <asp:Label ID="Label4" runat="server" Text="What did you think of your supervisor on the following points?"></asp:Label>
    <asp:Label ID="Label5" runat="server" Text="Was consistently fair"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="242px">
        <asp:ListItem>Almost Always</asp:ListItem>
        <asp:ListItem>Usually</asp:ListItem>
        <asp:ListItem>Sometimes</asp:ListItem>
        <asp:ListItem>Never</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label6" runat="server" Text="Provided recognition"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList2" runat="server" Width="242px">
        <asp:ListItem>Almost Always</asp:ListItem>
        <asp:ListItem>Usually</asp:ListItem>
        <asp:ListItem>Sometimes</asp:ListItem>
        <asp:ListItem>Never</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label7" runat="server" Text="Resolved complaints"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList3" runat="server" Width="242px">
        <asp:ListItem>Almost Always</asp:ListItem>
        <asp:ListItem>Usually</asp:ListItem>
        <asp:ListItem>Sometimes</asp:ListItem>
        <asp:ListItem>Never</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label8" runat="server" Text="Was sensitive to employees’ needs"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList4" runat="server" Width="242px">
        <asp:ListItem>Almost Always</asp:ListItem>
        <asp:ListItem>Usually</asp:ListItem>
        <asp:ListItem>Sometimes</asp:ListItem>
        <asp:ListItem>Never</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label9" runat="server" Text="Provided feedback on performance"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList5" runat="server" Width="242px">
        <asp:ListItem>Almost Always</asp:ListItem>
        <asp:ListItem>Usually</asp:ListItem>
        <asp:ListItem>Sometimes</asp:ListItem>
        <asp:ListItem>Never</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label10" runat="server" Text="Was receptive to open communication"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList6" runat="server" Width="242px">
        <asp:ListItem>Almost Always</asp:ListItem>
        <asp:ListItem>Usually</asp:ListItem>
        <asp:ListItem>Sometimes</asp:ListItem>
        <asp:ListItem>Never</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <asp:Label ID="Label11" runat="server" Text="How would you rate the following?"></asp:Label>
    <asp:Label ID="Label12" runat="server" Text="Cooperation within your division/program"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList7" runat="server" Width="242px">
        <asp:ListItem>Excellent</asp:ListItem>
        <asp:ListItem>Good</asp:ListItem>
        <asp:ListItem>Fair</asp:ListItem>
        <asp:ListItem>Poor</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label13" runat="server" Text="Cooperation with other divisions"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList8" runat="server" Width="242px">
        <asp:ListItem>Excellent</asp:ListItem>
        <asp:ListItem>Good</asp:ListItem>
        <asp:ListItem>Fair</asp:ListItem>
        <asp:ListItem>Poor</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label14" runat="server" Text="Personal job training"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList9" runat="server" Width="242px">
        <asp:ListItem>Excellent</asp:ListItem>
        <asp:ListItem>Good</asp:ListItem>
        <asp:ListItem>Fair</asp:ListItem>
        <asp:ListItem>Poor</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label15" runat="server" Text="Equipment provided (materials, resources, facilities)"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList10" runat="server" Width="242px">
        <asp:ListItem>Excellent</asp:ListItem>
        <asp:ListItem>Good</asp:ListItem>
        <asp:ListItem>Fair</asp:ListItem>
        <asp:ListItem>Poor</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label16" runat="server" Text="Company’s performance review system"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList11" runat="server" Width="242px">
        <asp:ListItem>Excellent</asp:ListItem>
        <asp:ListItem>Good</asp:ListItem>
        <asp:ListItem>Fair</asp:ListItem>
        <asp:ListItem>Poor</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label17" runat="server" Text="Company’s new employee orientation program"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList12" runat="server" Width="242px">
        <asp:ListItem>Excellent</asp:ListItem>
        <asp:ListItem>Good</asp:ListItem>
        <asp:ListItem>Fair</asp:ListItem>
        <asp:ListItem>Poor</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label18" runat="server" Text="Rate of pay for your job"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList13" runat="server" Width="242px">
        <asp:ListItem>Excellent</asp:ListItem>
        <asp:ListItem>Good</asp:ListItem>
        <asp:ListItem>Fair</asp:ListItem>
        <asp:ListItem>Poor</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label19" runat="server" Text="Career development/Advancement opportunities"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList14" runat="server" Width="242px">
        <asp:ListItem>Excellent</asp:ListItem>
        <asp:ListItem>Good</asp:ListItem>
        <asp:ListItem>Fair</asp:ListItem>
        <asp:ListItem>Poor</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label20" runat="server" Text="Physical working conditions"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList15" runat="server" Width="242px">
        <asp:ListItem>Excellent</asp:ListItem>
        <asp:ListItem>Good</asp:ListItem>
        <asp:ListItem>Fair</asp:ListItem>
        <asp:ListItem>Poor</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <asp:Label ID="Label21" runat="server" Text="Would you like to re-join Suntec? "></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList16" runat="server" Width="242px">
        <asp:ListItem>Yes</asp:ListItem>
        <asp:ListItem>No</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label22" runat="server" Text="What changes you would like to see in Suntec if you join us in future?"></asp:Label>
    <br />
    <textarea id="TextArea2" name="S2" rows="2"></textarea><br />
    <asp:Label ID="Label23" runat="server" Text="Details about the New Job (if one wants to reveal) "></asp:Label>
    <br />
    <textarea id="TextArea3" name="S3" rows="2"></textarea><br />
    <asp:Label ID="Label24" runat="server" Text="Feedback Category"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList17" runat="server" Width="242px">
        <asp:ListItem>Voluntary  </asp:ListItem>
        <asp:ListItem>Non-Voluntary</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label25" runat="server" Text="HR Feedback Comments:"></asp:Label>
    <br />
    <textarea id="TextArea4" cols="20" name="S4" rows="2"></textarea><br />
    <asp:Label ID="Label26" runat="server" Text="HR Representative Name:"></asp:Label>
    <br />
    <textarea id="TextArea5" cols="20" name="S5" rows="2"></textarea><br />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save" />
    <asp:Button ID="Button2" runat="server" Text="Cancel" />
    <br />
    <br />
    <asp:Label ID="Label27" runat="server" Text="Data"></asp:Label>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [ETMS_EmployeeExit]"></asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />
            <asp:BoundField DataField="ReasonForLeaving" HeaderText="ReasonForLeaving" SortExpression="ReasonForLeaving" />
            <asp:BoundField DataField="ReasonForLeavingAnyOther" HeaderText="ReasonForLeavingAnyOther" SortExpression="ReasonForLeavingAnyOther" />
            <asp:BoundField DataField="CompanyLike" HeaderText="CompanyLike" SortExpression="CompanyLike" />
            <asp:BoundField DataField="CompanyDislike" HeaderText="CompanyDislike" SortExpression="CompanyDislike" />
            <asp:BoundField DataField="SupervisorPoints" HeaderText="SupervisorPoints" SortExpression="SupervisorPoints" />
            <asp:BoundField DataField="YourRating" HeaderText="YourRating" SortExpression="YourRating" />
            <asp:BoundField DataField="RejoinAgain" HeaderText="RejoinAgain" SortExpression="RejoinAgain" />
            <asp:BoundField DataField="SuggestChanges" HeaderText="SuggestChanges" SortExpression="SuggestChanges" />
            <asp:BoundField DataField="NJ_ComapnyName" HeaderText="NJ_ComapnyName" SortExpression="NJ_ComapnyName" />
            <asp:BoundField DataField="NJ_Designation" HeaderText="NJ_Designation" SortExpression="NJ_Designation" />
            <asp:BoundField DataField="NJ_Function" HeaderText="NJ_Function" SortExpression="NJ_Function" />
            <asp:BoundField DataField="NJ_CostToCompany" HeaderText="NJ_CostToCompany" SortExpression="NJ_CostToCompany" />
        </Columns>
    </asp:GridView>
</asp:Content>

