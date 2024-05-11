<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageMaster.aspx.cs" Inherits="Employee_Exit.ManageMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        #form1 {
            max-width: 600px;
            margin: 20px auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        td {
            padding: 10px;
            border: 1px solid #ddd;
        }

        #Button1,
        #Button2,
        #Button3,
        #Button4 {
            padding: 10px;
            margin-right: 10px;
            background-color: #4caf50;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        #TextBox1,
        #ddlBindOPtions {
            width: 100%;
            padding: 10px;
            box-sizing: border-box;
        }

        /* Center-align buttons */
        td button {
            display: block;
            margin: 0 auto;
        }
    </style>

    <script>
        function showMessage(message) {
            alert(message);
        }

        function confirmDelete() {
            return confirm('Are you sure you want to delete this record?');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="DeleteConfirmed" runat="server" />
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <table>
            <tr>
                <td colspan="2">
                    <asp:DropDownList ID="ddlBindOPtions" runat="server" OnSelectedIndexChanged="ddlBindOPtions_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTextBox1" runat="server"
                        ControlToValidate="TextBox1"
                        Display="Dynamic"
                        ErrorMessage="Reason is required."
                        ForeColor="Red"
                        ValidationGroup="master">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_click"   ValidationGroup="master"/>
                    <asp:Button ID="btnList" runat="server" Text="List" OnClick="btnList_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right">
                    <asp:Button ID="btnLinktoEmpExit" runat="server" Text="Navigate to Employee Exit Portal." OnClick="btnLinktoEmpExit_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" DataKeyNames="id" EnableViewState="true"
                                AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit">
                                <Columns>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_description" runat="server" Text='<%#Eval("description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_id" runat="server" Text='<%#Eval("id") %>' Style="display: none;"></asp:TextBox>
                                            <asp:TextBox ID="txt_description" Width="100%" runat="server" Text='<%#Eval("description") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:CommandField ShowEditButton="True" ButtonType="Link" ControlStyle-CssClass="editButton"
                                        ItemStyle-HorizontalAlign="Center" HeaderText="Edit" />

                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CausesValidation="False"
                                                OnClientClick='<%# "return confirmDelete(" + Eval("id") + ");" %>'>Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
