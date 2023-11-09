<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TalentManagement.ascx.cs" Inherits="WebApplication5.UserControls.TalentManagement" %>

<div>
    <!-- Form fields for talent management -->
    <asp:TextBox ID="txtName" runat="server" Placeholder="Name" /><br />
    <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email" /><br />
    <asp:TextBox ID="txtDob" runat="server" Placeholder="Date of Birth (dd-mm-yyyy)" /><br />
    <asp:TextBox ID="txtSpecialization" runat="server" Placeholder="Specialization" /><br />

    <!-- Add, Edit, and Delete buttons -->
    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
</div>
