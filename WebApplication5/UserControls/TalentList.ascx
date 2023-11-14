<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TalentList.ascx.cs" Inherits="WebApplication5.UserControls.TalentsList" %>
<header>
</header>
<body>
    <div>
        <input id="searchTextBox" placeholder="Search by Name, Email, or Specialization" />
        <button id="searchButton" onclick="searchClicked(event)">Search</button>
    </div>
    <br />

    <table runat="server" id="talentTable" data-id="talentsTable" class="table table-striped">
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
        <button id="prevPage" onclick="prevPageClicked(event)" disabled="disabled"><<</button>
        <span id="curPage"></span>
        <button id="nextPage" onclick="nextPageClicked(event)">>></button>
    </div>
    <br />

    <button id="addBtn" onclick="addClicked(event)">Add Talent</button>

    <div id="managementButtons" style="display: none">
        <button id="editBtn" onclick="editClicked(event)">Edit</button>
        <button id="deleteBtn" onclick="removeClicked(event)">Remove</button>
    </div>
</body>


