var currentTalentId = 0;

function rowClicked(id) {
    document.getElementById('deleteBtn').style.display = 'inline-block';
    document.getElementById('editBtn').style.display = 'inline-block';
    document.getElementById('showBtn').style.display = 'inline-block';
    currentTalentId = id;
}

function removeClicked() {
    $.ajax({
        async: false,
        url: "Default.aspx/RemoveButton_Click",
        cache: false,
        type: "POST", // Use "POST" to send data
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({'id': currentTalentId}),
        success: function (data) {
            console.log("Removed:", data);
        },
        error: function (error) {
            console.error("Error removing talents:", error);
        }
    });
}

function editClicked() {
    $.ajax({
        async: false,
        url: "Default.aspx/EditButton_Click",
        cache: false,
        type: "POST", // Use "POST" to send data
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({ 'id': currentTalentId }),
        success: function (data) {
            console.log("Edited:", data);
        },
        error: function (error) {
            console.error("Error:", error);
        }
    });
}


function showClicked() {
    document.getElementById('talentCardDiv').style.display = 'block';
    $.ajax({
        async: false,
        url: "Default.aspx/ShowButton_Click",
        cache: false,
        type: "POST", 
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({ 'id': currentTalentId }),
        success: function (data) {
            bindTalentData(data);
        },
        error: function (error) {
            console.error("Error:", error);
        }
    });
}

function bindTalentData(data) {
    // Update the HTML elements with the returned data
    document.getElementById('talentId').innerText = data.d.ID;
    document.getElementById('talentName').innerText = data.d.Name;
    document.getElementById('talentSpecialization').innerText = data.d.Specialization;
    document.getElementById('talentEmail').innerText = data.d.Email;
    document.getElementById('talentDOB').innerText = data.d.DOB;
}
