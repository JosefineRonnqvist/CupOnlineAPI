const url = 'https://localhost:7172'
const urlOrganizer = url + '/api/Organizer/GetAllOrganizers'

function GetOrganizers() {
    fetch(urlOrganizer)
        .then(response => response.json())
        .then(data => organizerForm(data))
        .catch(error => console.error("Unable to get organizer.", error));
}

//function organizerForm(data) {
//    var select = document.getElementById("brow");
//    var option = document.createElement("option");
//    option.textContent = "-";
//    option.value = 0;
//    select.appendChild(option);
//    for (var i = 0; i < data.length; i++) {
//        var option = document.createElement("option");
//        //option.textContent = data[i].club_name;
//        option.value = data[i].club_name;
//        select.appendChild(option);
//    }
//}

//$(document).ready(function () {
//    //change selectboxes to selectize mode to be searchable
//    $("select").select2();
//});


function organizerForm(data) {
    var container = document.getElementById("order_organizer");
    var select = document.createElement("select");
    select.id = "order_organizer_select";
    var option = document.createElement("option");
    option.textContent = "-";
    option.value = 0;
    select.appendChild(option);
    for (var i = 0; i < data.length; i++) {
        var option = document.createElement("option");
        option.textContent = data[i].club_name;
        option.value = data[i].club_id;
        select.appendChild(option);
    }
    container.appendChild(select);
}

