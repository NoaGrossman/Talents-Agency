<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TalentCard.ascx.cs" Inherits="WebApplication5.UserControls.TalentCard" %>

<div id="talentCard" class="talent-card">
    <h2>Talent Card</h2>
    <hr />
    <div class="field">
        <label id="idLabel">Talent ID:</label>
        <span id="talentId"></span>
    </div>
    <div class="field">
        <label id="nameLabel">Name:</label>
        <span id="talentName"></span>
    </div>
    <div class="field">
        <label id="specializationLabel">Specialization:</label>
        <span id="talentSpecialization"></span>
    </div>
    <div class="field">
        <label id="emailLabel">Email:</label>
        <span id="talentEmail"></span>
    </div>
    <div class="field">
        <label id="DOBLabel">Date of Birth:</label>
        <span id="talentDOB"></span>
    </div>
</div>