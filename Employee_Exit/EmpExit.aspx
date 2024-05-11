<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpExit.aspx.cs" Inherits="Employee_Exit.EmpExit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Employee Form</title>
    <style>
        body {
            font-family: Arial, sans-serif;
        }

        #form1 {
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        table {
            width: 100%;
            border-spacing: 0;
        }

            table tr {
                margin-bottom: 10px;
            }

            table td {
                padding: 10px;
                border: 1px solid #ddd;
            }

        #Select1 {
            width: calc(100% - 20px); /* Set the width to 100% minus padding and border */
            box-sizing: border-box; /* Include padding and border in the total width */
        }

        #gvLeavingOptions {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

            #gvLeavingOptions th,
            #gvLeavingOptions td {
                border: 1px solid #ddd;
                padding: 8px;
                text-align: left;
            }

        .auto-style1 {
            width: 49%; /* Adjusted to 49% to allow for spacing */
            margin-top: 10px;
        }

        .section-heading {
            font-size: 18px;
            font-weight: bold;
            margin-top: 15px;
        }
    </style>
    <script>
        function showMessage(message) {
            alert(message);
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <table>

            <tr>
                <td colspan="2" class="section-heading" style="text-align: center">Suntec Web Services Pvt. Ltd.</td>
            </tr>
            <tr>
                <td colspan="2" class="section-heading" style="text-align: center">EXIT FEEDBACK FORM</td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right;">
                    <asp:Button ID="Button2" runat="server" Text="Navigate to Manage Master Portal." OnClick="Button2_Click" />
                </td>
            </tr>
            <tr>
                <td class="section-heading" colspan="2">1. Personal Details:
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <label for="Select1">Name</label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" Width="100%"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <label for="Name">Designation</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDesignation" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <label for="Name">Department</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDept" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <label for="Name">Date of Joining</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDoj" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <label for="Name">Date of Resignation</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDor" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <label for="Name">Reporting to</label>
                </td>
                <td>
                    <asp:TextBox ID="txtRepto" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <label for="Name">Last working Day</label>
                </td>
                <td>
                    <asp:TextBox ID="txtLwd" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <label for="Name">Contact Number</label>
                </td>
                <td>
                    <asp:TextBox ID="txtContactNo" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="section-heading" colspan="2">2. Reasons for leaving the company: (multiples reasons can be selected)
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvLeavingOptions" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" Width="100%" ShowHeader="false">
                        <Columns>
                            
                            <asp:BoundField DataField="Description" HeaderText="" SortExpression="Description" />
                            <asp:BoundField DataField="Id" HeaderText="" SortExpression="Id" Visible="false" />
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
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please select at least one record."
                        OnServerValidate="CustomValidator1_ServerValidate" ForeColor="Red" ValidationGroup="EmpExit"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Any other, please specify:</td>
                <td>
                    <textarea id="txtAnyothReasonForLeaving" runat="server" cols="20" rows="2" style="width: 100%"></textarea>
                </td>
            </tr>
            <tr>
                <td class="section-heading" colspan="2">3. What did you like and dislike about the company?
                </td>
            </tr>
            <tr>
                <td>Like:</td>
                <td>

                    <textarea id="txtLikeAbtCompany" runat="server" cols="20" rows="2" style="width: 100%"></textarea>
                    <asp:RequiredFieldValidator ID="rfvLikeAbtCompany" runat="server" ControlToValidate="txtLikeAbtCompany" ValidationGroup="EmpExit"
                        Display="Dynamic" ErrorMessage="Please enter something you like about the company." ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Dislike:</td>
                <td>

                    <textarea id="txtDisLikeAbtCompany" runat="server" cols="20" rows="2" style="width: 100%"></textarea>
                    <asp:RequiredFieldValidator ID="rfvDisLikeAbtCompany" runat="server" ControlToValidate="txtDisLikeAbtCompany" ValidationGroup="EmpExit"
                        Display="Dynamic" ErrorMessage="Please enter something you dislike about the company." ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="section-heading" colspan="2">4.	What did you think of your supervisor on the following points?
                </td>
            </tr>
            <tr>
                <td colspan="2">

                    <asp:GridView ID="gvSupervisorPoints" runat="server" AutoGenerateColumns="false" Width="100%" ShowHeader="true">
                        <Columns>
                            <asp:BoundField DataField="Description" HeaderText="" SortExpression="Description" />

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>Almost Always</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButtonSupervisor1" runat="server" GroupName="supervisorGroup" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>Usually</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButtonSupervisor2" runat="server" GroupName="supervisorGroup" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>Sometimes</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButtonSupervisor3" runat="server" GroupName="supervisorGroup" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>Never</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButtonSupervisor4" runat="server" GroupName="supervisorGroup" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>


                    <asp:CustomValidator ID="CustomValidatorSupervisor" runat="server" ErrorMessage="Please select a response in each row." ValidationGroup="EmpExit"
                        OnServerValidate="CustomValidatorSupervisor_ServerValidate" ForeColor="Red"></asp:CustomValidator>

                </td>
            </tr>
            <tr>
                <td class="section-heading" colspan="2">5.	How would you rate the following?
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvCooperation" runat="server" AutoGenerateColumns="false" Width="100%" ShowHeader="true">
                        <Columns>
                            <asp:BoundField DataField="Description" HeaderText="" SortExpression="Description" />

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>Excellent</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButtonCooperation1" runat="server" GroupName="cooperationGroup" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>Good</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButtonCooperation2" runat="server" GroupName="cooperationGroup" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>Fair</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButtonCooperation3" runat="server" GroupName="cooperationGroup" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>Poor</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButtonCooperation4" runat="server" GroupName="cooperationGroup" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:CustomValidator ID="CustomValidatorCooperation" runat="server" ErrorMessage="Please select a response in each row."
                        OnServerValidate="CustomValidatorCooperation_ServerValidate" ForeColor="Red" ValidationGroup="EmpExit"></asp:CustomValidator>

                </td>
            </tr>
            <tr>
                <td>Any other, please specify:</td>
                <td>
                    <textarea id="txtAnyotherHowYouRate" runat="server" cols="20" rows="2" style="width: 100%"></textarea>
                </td>
            </tr>
            <tr>
                <td class="section-heading">6.	 Would you like to re-join Suntec? </td>
                <td>
                    <asp:RadioButtonList ID="rblWillYouRejoin" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Yes" Value="Yes" />
                        <asp:ListItem Text="No" Value="No" />
                    </asp:RadioButtonList>
                </td>



            </tr>
            <tr>
                <td class="section-heading" colspan="2">7.	What changes you would like to see in Suntec if you join us in future?</td>
            </tr>
            <tr>
                <td colspan="2">
                    <textarea id="txtSuggestedChanges1" runat="server" cols="20" rows="2" style="width: 100%"></textarea>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtSuggestedChanges1"
                        Display="Dynamic"
                        ErrorMessage="Please enter suggested changes." ValidationGroup="EmpExit"
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="section-heading" colspan="2">8.	Details about the New Job (if one wants to reveal) </td>
            </tr>
            <tr>
                <td style="width: 50%">Name of the company:</td>
                <td>
                    <asp:TextBox ID="txtNJ_ComapnyName" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 50%">Designation:</td>
                <td>
                    <asp:TextBox ID="txtNJ_Designation" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
            </tr>

            <tr>
                <td style="width: 50%">Function:</td>
                <td>
                    <asp:TextBox ID="txtNJ_Function" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
            </tr>

            <tr>
                <td style="width: 50%">Cost to the company:</td>
                <td>
                    <asp:TextBox ID="txtNJ_CostToCompany" runat="server" TextMode="MultiLine" Width="100%" ValidationGroup="EmpExit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revCostToCompany" runat="server"
                        ControlToValidate="txtNJ_CostToCompany"
                        ErrorMessage="Enter a valid decimal number."
                        ValidationExpression="^\d+(\.\d+)?$"
                        Display="Dynamic"
                        ForeColor="Red">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>


            <tr>
                <td colspan="2">
                    <div style="background-color: #ad463b82; height: 20px;"></div>
                </td>
            </tr>
            <tr>
                <td class="section-heading" colspan="2">Office use only (To be filled in by HR / Functional Head): </td>
            </tr>
            <tr>
                <td colspan="2">Feedback Category&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Voluntary&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButton1" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Non-Voluntary&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButton2" runat="server" /></td>

            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtFeedback" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">HR Representative Name: 
                    <asp:TextBox ID="txtHrname" runat="server" Width="60%"></asp:TextBox>
                </td>


            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="EmpExit" />
                </td>
                <td class="auto-style1">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />


                </td>


            </tr>
            <tr>
                <td colspan="2">
                   <div style="width: 574px; overflow-x: auto;">
                        
                        <asp:GridView ID="gvGetEmpExitList" runat="server"  AutoGenerateColumns="True"
        AllowPaging="True" PageSize="10" Width="100%">
    </asp:GridView>
                    </div>
                </td>
            </tr>

        </table>



    </form>
</body>
</html>
