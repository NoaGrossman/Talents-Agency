<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TalentList.ascx.cs" Inherits="WebApplication5.UserControls.TalentsList" %>

<table id="talentTable" class="table table-striped" runat="server">
    <thead>
        <tr>
            <th>Talent ID</th>
            <th>Name</th>
            <th>Date of Birth</th>
            <th>Email</th>
            <th>Specialization</th>
            <th>Age</th>
        </tr>
    </thead>
    <tbody>
        <!-- Talent rows will be added dynamically here -->
    </tbody>
</table>

<div>
    <asp:TextBox ID="searchTextBox" runat="server" placeholder="Search by Name, Email, or Specialization"></asp:TextBox>
    <asp:Button ID="searchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
</div>
