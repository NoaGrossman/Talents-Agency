<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TalentManagement.ascx.cs" Inherits="WebApplication5.UserControls.TalentManagement" %>

<div class="talent-card">
    <h2>Talent Management</h2>
    <hr />
    <div class="field">
        <label id="idLabel">Talent ID:</label>
        <span id="talentManagementId"></span>
    </div>
    <div class="field">
        <label id="nameLabel">Name:</label>
        <input id="talentManagementName" type="text"/>
    </div>
    <div class="field">
        <label id="specializationLabel">Specialization:</label>
        <input id="talentManagementSpecialization" type="text"/>
    </div>
    <div class="field">
        <label id="emailLabel">Email:</label>
        <input id="talentManagementEmail" type="text"/>
    </div>
    <div class="field">
        <label id="DOBLabel">Date of Birth:</label>
        <input id="talentManagementDOB" type="date"/>
    </div>
</div>
<div>
    <button id="updateBtn" onclick="updateClicked(event)" style="display: none">Update Talent</button>
    <button id="addTalentBtn" onclick="onAddBtnClicked(event)" style="display: none">Add Talent</button>
</div>
