var currentTalentId = 0;
var rowsNumToShow = 2;
var currentPage = 1;
var curTalentsCount = 0;

//TODO: disable or enable next and prev buttons on every render of page

$(document).ready(function () {
    console.log("Noa:", "onReady()");
    getTalentsCount();
    showTalents();
});

function rowClicked(event, id) {

    // Replace 'your-class' with the class you want to remove
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
    //todo: change to get request
    $.ajax({
        async: false,
        url: "Default.aspx/GetTalentsCount",
        type: "POST", // Use "POST" to send data
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({}),
        success: function (data) {
            curTalentsCount = data.d;
        },
        error: function (error) {
            console.error("Error showing talents:", error);
        }
    });
}

function showTalents() {
    console.log("Noa:", "showTalents - page:" + currentPage);

    $.ajax({
        async: false,
        url: "Default.aspx/ShowTalents",
        type: "POST", // Use "POST" to send data
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(
            {
                'k': rowsNumToShow,
                'curPage': currentPage
            }
        ),
        success: function (data) {
            updateTalentTable(data.d)
            applyPagination();
        },
        error: function (error) {
            console.error("Error showing talents:", error);
        }
    });
}

function removeClicked(e) {
    e.preventDefault();
    $.ajax({
        async: false,
        url: "Default.aspx/RemoveButton_Click",
        cache: false,
        type: "POST", // Use "POST" to send data
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({ 'id': currentTalentId }),
        success: function (data) {
            console.log("Removed:", data);
        },
        error: function (error) {
            console.error("Error removing talents:", error);
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
            type: "POST", // Use "POST" to send data
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: JSON.stringify({ 'inputText': searchText.value }),
            success: function (data) {
                updateTalentTable(data.d);
                curTalentsCount = data.d.length;
                applyPagination();
            },
            error: function (error) {
                console.error("Error:", error);
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

    // Assuming 'talents' is an array of talent objects
    talents.forEach(function (talent) {
        var row = table.insertRow();

        row.setAttribute('data-id', talent.ID); // Use data-id attribute instead of id
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
        type: "POST",
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({}),
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
            console.error("Error:", error);
        }
    });
}

function onAddBtnClicked(e) {
    //e.preventDefault();
    let nameToAdd = document.getElementById('talentManagementName').value;
    let emailToAdd = document.getElementById('talentManagementEmail').value;
    let specToAdd = document.getElementById('talentManagementSpecialization').value;
    let dobToAdd = document.getElementById('talentManagementDOB').value;
    $.ajax({
        async: false,
        url: "Default.aspx/AddNewTalent",
        type: "POST",
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(
            {
                'name': nameToAdd,
                'spec': specToAdd,
                'email': emailToAdd,
                'dob': dobToAdd
            }
        ),
        success: function (data) {
            console.log('yay');
        },
        error: function (error) {
            console.error("Error:", error);
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
        async: false, // Set to true for asynchronous call
        url: "Default.aspx/EditButton_Click",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({ 'id': currentTalentId }),
        success: function (data) {
            // Ensure data is in expected format
            if (data && data.d) {
                bindTalentDataEdit(data.d);
                document.getElementById('talentCard').style.display = 'none';
            } else {
                console.error("Invalid data format:", data);
            }
        },
        error: function (error) {
            console.error("Error:", error);
        }
    });
}

function updateClicked(e) {
    e.preventDefault();
    var talentId = document.getElementById('talentManagementId').textContent;
    var talentName = document.getElementById('talentManagementName').value;
    var talentSpecialization = document.getElementById('talentManagementSpecialization').value;
    var talentEmail = document.getElementById('talentManagementEmail').value;
    var talentDOB = document.getElementById('talentManagementDOB').value;

    $.ajax({
        async: false,
        url: "Default.aspx/UpdateButton_Click",
        type: "POST", // Use "POST" to send data
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(
            {
                'id': talentId,
                'name': talentName,
                'spec': talentSpecialization,
                'email': talentEmail,
                'dob': talentDOB
            }
        ),
        success: function () {
            // Reload the page after a successful update
            window.location.reload();
        },
        error: function (error) {
            console.error("Error:", error);
        }
    });
}

function showTalentCard(e) {
    e.preventDefault();

    var talentCardDiv = document.getElementById('talentCardDiv');
    talentCardDiv.style.display = 'block';


    // Make an asynchronous AJAX call
    $.ajax({
        async: false,
        url: "Default.aspx/ShowButton_Click",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({ 'id': currentTalentId }),
        success: function (data) {
            // Ensure data is in expected format
            if (data && data.d) {
                bindTalentDataShow(data.d); // Pass only the necessary data part
            } else {
                console.error("Invalid data format:", data);
            }
        },
        error: function (error) {
            console.error("Error in showTalentCard:", error);
        }
    });
}

function bindTalentDataShow(talentData) {
    // Ensure that the talent data is not null or undefined
    if (!talentData) {
        console.error("No talent data provided");
        return;
    }

    // Update the HTML elements with the talent data
    document.getElementById('talentId').textContent = talentData.ID;
    document.getElementById('talentName').textContent = talentData.Name;
    document.getElementById('talentSpecialization').textContent = talentData.Specialization;
    document.getElementById('talentEmail').textContent = talentData.Email;
    document.getElementById('talentDOB').textContent = formatCSharpDateToJS(talentData.DOB);
}

function bindTalentDataEdit(talentData) {
    // Ensure that the talent data is not null or undefined
    if (!talentData) {
        console.error("No talent data provided");
        return;
    }

    // Update the HTML elements with the talent data
    document.getElementById('talentManagementId').textContent = talentData.ID;
    document.getElementById('talentManagementName').value = talentData.Name;
    document.getElementById('talentManagementSpecialization').value = talentData.Specialization;
    document.getElementById('talentManagementEmail').value = talentData.Email;

    document.getElementById('talentManagementDOB').value = formatCSharpDateToJS(talentData.DOB);

}

function formatCSharpDateToJS(csharpDate) {
    // Extract the milliseconds part from the C# DateTime string
    var matches = /\/Date\((\d+)\)\//.exec(csharpDate);
    if (matches === null || matches.length !== 2) {
        console.error("Invalid C# DateTime format");
        return '';
    }

    // Convert milliseconds to a JavaScript Date object
    var date = new Date(parseInt(matches[1], 10));

    // Format the date as 'yyyy/mm/dd'
    var year = date.getFullYear();
    var month = ('0' + (date.getMonth() + 1)).slice(-2); // Months are 0-based in JS
    var day = ('0' + date.getDate()).slice(-2);

    return year + '-' + month + '-' + day;
}



