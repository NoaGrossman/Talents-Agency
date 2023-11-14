<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TalentManagement.ascx.cs" Inherits="WebApplication5.UserControls.TalentManagement" %>

<form class="talent-card" id="talentForm">
    <h2>Talent Management</h2>
    <hr />
    <div class="field">
        <label for="talentManagementId">Talent ID:</label>
        <span id="talentManagementId"></span>
    </div>
    <div class="field">
        <label for="talentManagementName">Name:</label>
        <input id="talentManagementName" type="text" required />
    </div>
    <div class="field">
        <label for="talentManagementSpecialization">Specialization:</label>
        <input id="talentManagementSpecialization" type="text" required minlength="5" />
    </div>
    <div class="field">
        <label for="talentManagementEmail">Email:</label>
        <input id="talentManagementEmail" type="email" required />
    </div>
    <div class="field">
        <label for="talentManagementDOB">Date of Birth:</label>
        <input id="talentManagementDOB" type="date" required />
    </div>
    <div>
        <button id="updateBtn" onclick="updateClicked(event)" style="display: none">Update Talent</button>
        <button id="addTalentBtn" type="submit" onclick="onAddBtnClicked(event)"  style="display: none">Add Talent</button>
    </div>
</form>
