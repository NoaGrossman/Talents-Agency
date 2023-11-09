<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TalentCard.ascx.cs" Inherits="WebApplication5.UserControls.TalentCard" %>

<div class="talent-card">
    <h2>Talent Card</h2>
    <hr />
    <div class="field">
        <label for="talentId">Talent ID:</label>
        <span><asp:Label ID="lblTalentId" runat="server"></asp:Label></span>
    </div>
    <div class="field">
        <label for="talentName">Name:</label>
        <span><asp:Label ID="lblTalentName" runat="server"></asp:Label></span>
    </div>
    <div class="field">
        <label for="talentSpecialization">Specialization:</label>
        <span><asp:Label ID="lblTalentSpecialization" runat="server"></asp:Label></span>
    </div>
    <div class="field">
        <label for="talentEmail">Email:</label>
        <span><asp:Label ID="lblTalentEmail" runat="server"></asp:Label></span>
    </div>
    <div class="field">
        <label for="talentDOB">Date of Birth:</label>
        <span><asp:Label ID="lblTalentDOB" runat="server"></asp:Label></span>
    </div>
    <div>
        <button id="editBtn" onclick="onEditClicked">Edit</button>
        <button id="deleteBtn" onclick="onDeleteClicked">Delete</button>
    </div>
</div>