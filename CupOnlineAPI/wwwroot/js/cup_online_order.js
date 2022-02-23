const url = 'https://localhost:7172'
const urlOrganizer = url + '/api/Organizer/GetAllOrganizers'
const urlCreateCity = url + '/api/Order/CreateCity'
const urlCreateOrganizer = url + '/api/Order/CreateOrganizer'
const urlCreateCup = url + '/api/Order/CreateCup'
const urlCreateCupRegistration = url + '/api/Order/CreateCupRegistration'
const urlCreateCupAdmin = url + '/api/Order/CreateCupAdmin'

function GetOrganizers() {
    fetch(urlOrganizer)
        .then(response => response.json())
        .then(data => organizerForm(data))
        .catch(error => console.error("Unable to get organizer.", error));
}

//let _data = {
//    title: "foo",
//    body: "bar",
//    userId: 1
//}

//fetch('https://jsonplaceholder.typicode.com/posts', {
//    method: "POST",
//    body: JSON.stringify(_data),
//    headers: { "Content-type": "application/json; charset=UTF-8" }
//})
//    .then(response => response.json())
//    .then(json => console.log(json));
//.catch (err => console.log(err));

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

function newOrganizer() {
    var clubName = document.getElementById("order_new_club_name").value;
    var clubUrl = document.getElementById("order_new_club_url").value;
    var clubCityId = document.getElementById("order_city").value;
    var clubSportId = document.getElementById("order_sport").value;
}

