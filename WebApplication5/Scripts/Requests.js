var currentTalentId = 0; // A variable that keeps the curent id that clicked last
var rowsNumToShow = 2; // Can be changed due to talents amount we want to show at the same page
var currentPage = 1; // A variable that keeps the current table page
var curTalentsCount = 0; // A variable that will help on pagination check of last page

$(document).ready(function () {
    console.log("Noa:", "onReady()");
    getTalentsCount(); 
    showTalents();
});

function rowClicked(event, id) {

    let classNameToRemove = 'selected_talent';

    // Find all elements with the specified class
    let elements = document.querySelectorAll('.' + classNameToRemove);

    // Loop through the elements and remove the class
    elements.forEach(function (element) {
        element.classList.remove(classNameToRemove);
    });
    document.getElementById('managementButtons').style.display = 'block';
    let trElement = document.querySelector('[data-id="' + id + '"]');
    trElement.classList.add('selected_talent');
    currentTalentId = id;

    showTalentCard(event);

}

function nextPageClicked(e) {
    e.preventDefault();
    currentPage++;
    showTalents();

}

function prevPageClicked(e) {
    e.preventDefault();
    //console.log("Noa:", "PrevClicked()");
    currentPage--;
    showTalents();

}

function applyPagination() {
    document.getElementById('curPage').textContent = currentPage;
    document.getElementById('nextPage').disabled = (curTalentsCount <= currentPage * rowsNumToShow) ? true : false;
    document.getElementById('prevPage').disabled = (currentPage <= 1) ? true : false;
}

function getTalentsCount() {
    $.ajax({
        async: false,
        url: "Default.aspx/GetTalentsCount",
        type: "GET", 
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            curTalentsCount = data.d;
        },
        error: function (error) {
            console.error("Error get talents count:", error);
            alert("Error get talents count: " + error);
        }
    });
}

function showTalents() {
    console.log("showTalents - page:", currentPage);

    $.ajax({
        url: "Default.aspx/ShowTalents",
        type: "GET", 
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: { k: rowsNumToShow, curPage: currentPage }, 
        success: function (data) {
            updateTalentTable(data.d);
            applyPagination();
        },
        error: function (error) {
            console.error("Error showing talents:", error);
            alert("Error showing talents: " + error);
        }
    });
}

function removeClicked(e) {
    e.preventDefault();
    $.ajax({
        async: false,
        url: "Default.aspx/RemoveButton_Click",
        cache: false,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({ 'id': currentTalentId }),
        success: function (data) {
            alert('Talent remove succesfuly');
            window.location.reload(); 
        },
        error: function (error) {
            console.error("Error removing talent:", error);
            alert("Error removing talent: " + error);
        }
    });
}

function searchClicked(e) {
    e.preventDefault();
    let searchText = document.getElementById('searchTextBox');
    let searchBtn = document.getElementById('searchButton');
    if (searchBtn.textContent == 'Clear') {
        searchBtn.textContent = 'Search';
        searchText.value = '';
        getTalentsCount();
        showTalents();
    }
    else {
        currentPage = 1;
        searchBtn.textContent = 'Clear';
        $.ajax({
            async: false,
            url: "Default.aspx/SearchClicked",
            type: "POST", //A POST request instead of GET due to: 
            //Data Size: POST requests do not have URL length limitations, which allows sending larger amounts of data.
            //Security: POST data does not get stored in browser history.
            //Structure: Allows a structured data format(JSON) to be sent in the request body, which is more suitable for complex data.
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: JSON.stringify({ 'inputText': searchText.value }),
            success: function (data) {
                updateTalentTable(data.d);
                curTalentsCount = data.d.length;
                applyPagination();
            },
            error: function (error) {
                console.error("Error showing talents by search:", error);
                alert("Error showing talents by search: " + error);
            }
        });
    }
}

function updateTalentTable(talents) {
    var table = document.getElementById('talentList_talentTable');
    var rows = table.getElementsByTagName('tr');
    for (var i = rows.length - 1; i > 0; i--) {
        table.deleteRow(i);
    }

    talents.forEach(function (talent) {
        var row = table.insertRow();

        row.setAttribute('data-id', talent.ID); 
        row.setAttribute('onclick', 'rowClicked(event, ' + talent.ID + ')'); // Add onclick attribute

        row.insertCell(0).innerHTML = talent.ID;
        row.insertCell(1).innerHTML = talent.Name;
        row.insertCell(2).innerHTML = formatCSharpDateToJS(talent.DOB); // Format date as needed
        row.insertCell(3).innerHTML = talent.Email;
        row.insertCell(4).innerHTML = talent.Specialization;
        row.insertCell(5).innerHTML = talent.Age;
    });
}


function addClicked(e) {
    e.preventDefault();

    // Display the talent management
    var talentManagementDiv = document.getElementById('talentManagementDiv');
    talentManagementDiv.style.display = 'block';
    document.getElementById('addTalentBtn').style.display = 'block';

    $.ajax({
        async: false,
        url: "Default.aspx/GetNextId",
        type: "GET",
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            // Ensure data is in expected format
            if (data && data.d) {
                var nextId = data.d;
                document.getElementById('talentManagementId').textContent = nextId;
            } else {
                console.error("Invalid data format:", data);
            }
        },
        error: function (error) {
            console.error("Error show add new talent view:", error);
            alert("Error show add new talent view:" + error);
        }
    });
}

function getcurrentTalent() {
    let talentId = document.getElementById('talentManagementId').textContent;
    let talentName = document.getElementById('talentManagementName').value;
    let talentEmail = document.getElementById('talentManagementEmail').value;
    let talentSpec = document.getElementById('talentManagementSpecialization').value;
    let talentDob = document.getElementById('talentManagementDOB').value;
    let talent = { id: talentId, name: talentName, email: talentEmail, specialization: talentSpec, dob: talentDob }
    return talent;
}

function onAddBtnClicked(e) {
    let currentTalent = getcurrentTalent();
    e.preventDefault();
    $.ajax({
        async: false,
        url: "Default.aspx/UpdateOrAdd",
        type: "POST",
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(
            {
                'id': currentTalent.id,
                'name': currentTalent.name,
                'spec': currentTalent.specialization,
                'email': currentTalent.email,
                'dob': currentTalent.dob,
                'isAdd': true
            }
        ),
        success: function (response) {
            if (response.d.IsSuccess) {
                // If validation is successful
                alert("Talent Add successfuly.");
                window.location.reload(); // Reload the page
            } else {
                // If validation fails
                alert("Validation failed: " + response.d.Message);
            }
        },
        error: function (error) {
            console.error("Error adding new talent:", error);
            alert("Error adding new talent:" + error);
        }
    });
}



function editClicked(e) {
    e.preventDefault();

    // Display the talent card
    var talentManagementDiv = document.getElementById('talentManagementDiv');
    talentManagementDiv.style.display = 'block';
    document.getElementById('updateBtn').style.display = 'block';

    $.ajax({
        async: false, 
        url: "Default.aspx/EditButton_Click",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({ 'id': currentTalentId }),
        success: function (data) {
            // Ensure data is in expected format
            if (data && data.d) {
                bindTalentData(data.d, 'edit');
                document.getElementById('talentCardDiv').style.display = 'none';
            } else {
                console.error("Invalid data format:", data);
            }
        },
        error: function (error) {
            console.error("Error show edit talent view:", error);
            alert("Error show edit talent view:" + error);
        }
    });
}

function updateClicked(e) {
    e.preventDefault();
    let currentTalent = getcurrentTalent();

    $.ajax({
        url: "Default.aspx/UpdateOrAdd",
        type: "POST",
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({
            'id': currentTalent.id,
            'name': currentTalent.name,
            'spec': currentTalent.specialization,
            'email': currentTalent.email,
            'dob': currentTalent.dob,
            'isAdd': false
        }),
        success: function (response) {
            if (response.d.IsSuccess) {
                // If validation is successful
                alert("Update successful.");
                window.location.reload(); // Reload the page or redirect as needed
            } else {
                // If validation fails
                alert("Validation failed: " + response.d.Message);
            }
        },
        error: function (error) {
            console.error("Error update talent:", error);
            alert("Error update talent:" + error);

        }
    });
}


function showTalentCard(e) {
    e.preventDefault();

    var talentCardDiv = document.getElementById('talentCardDiv');
    talentCardDiv.style.display = 'block';

    var talentCardDiv = document.getElementById('talentManagementDiv');
    talentCardDiv.style.display = 'none';

    $.ajax({
        async: false,
        url: "Default.aspx/ShowButton_Click?id=" + currentTalentId,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            // Ensure data is in expected format
            if (data && data.d) {
                bindTalentData(data.d, 'show'); 
            } else {
                console.error("Invalid data format:", data);
            }
        },
        error: function (error) {
            console.error("Error showing talent card view:", error);
            alert("Error showing talent card view:" + error);
        }
    });
}


function bindTalentData(talentData, option) {
    // Ensure that the talent data is not null or undefined
    if (!talentData) {
        console.error("No talent data provided");
        return;
    }
    // Update the HTML elements with the talent data
    switch (option) {
        case 'show':
            document.getElementById('talentId').textContent = talentData.ID;
            document.getElementById('talentName').textContent = talentData.Name;
            document.getElementById('talentSpecialization').textContent = talentData.Specialization;
            document.getElementById('talentEmail').textContent = talentData.Email;
            document.getElementById('talentDOB').textContent = formatCSharpDateToJS(talentData.DOB);

        case 'add' || 'edit':
            document.getElementById('talentManagementId').textContent = talentData.ID;
            document.getElementById('talentManagementName').value = talentData.Name;
            document.getElementById('talentManagementSpecialization').value = talentData.Specialization;
            document.getElementById('talentManagementEmail').value = talentData.Email;
            document.getElementById('talentManagementDOB').value = formatCSharpDateToJS(talentData.DOB);
    }
}

function formatCSharpDateToJS(csharpDate) {
    // Extract the milliseconds part from the C# DateTime string
    console.log("csharpDate",csharpDate);
    var csharpMatches = /\/Date\((-?\d+)\)\//.exec(csharpDate);
    console.log("csharpMatches",csharpMatches);
    
    if (csharpMatches) {
        date = new Date(parseInt(csharpMatches[1], 10));
        console.log("date -good", date)
    } else {
        // Assuming the manual input is in 'dd/mm/yyyy' format
        const parts = csharpDate.split('/');
        if (parts.length === 3) {
            date = new Date(parts[2], parts[1] - 1, parts[0]);
            console.log("date -bad", date)
        } else {
            console.error("Invalid date format");
            return '';
        }
    }
    // Format the date as 'yyyy-mm-dd'
    var year = date.getFullYear();
    var month = ('0' + (date.getMonth() + 1)).slice(-2); // Months are 0-based in JS
    var day = ('0' + date.getDate()).slice(-2);

    return year + '-' + month + '-' + day;
}