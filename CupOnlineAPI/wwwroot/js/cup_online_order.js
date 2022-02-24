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

    let organizer = {
        club_name: document.getElementById("order_new_club_name").value,
        club_shortname: document.getElementById("order_new_club_name").value,
        club_url: document.getElementById("order_new_club_url").value,
        club_city_id: document.getElementById("order_new_club_city").value,
        club_sport_id: document.getElementById("order_sport").value,
    }

    fetch(urlCreateOrganizer, {
        method: "POST",
        body: JSON.stringify(organizer),
    headers: { "Content-type": "application/json; charset=UTF-8" }
    })
    .then(response => response.json())
    .then(json => console.log(json))
    .catch (err => console.log(err));
}

function newCity() {

    let city = {
        city_name: document.getElementById("order_new_club_city").value, //Add Check if city exists*************************************************************
    }

    fetch(urlCreateCity, {
        method: "POST",
        body: JSON.stringify(city),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(response => response.json())
        .then(json => console.log(json))
        .catch(err => console.log(err));
}

function newCup() {
    let cup = {
        cup_club_id: document.getElementById("order_organizer").value,
        cup_sport_id: document.getElementById("order_sport").value,
        cup_startdate: document.getElementById("order_startdate").value,
        cup_enddate: document.getElementById("order_enddate").value,
        cup_name: document.getElementById("order_name").value,
        cup_players_age: document.getElementById("order_age_text").value,
        cup_play_place: document.getElementById("order_city")
    }

    fetch(urlCreateCup, {
        method: "POST",
        body: JSON.stringify(cup),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(response => response.json())
        .then(json => console.log(json))
        .catch(err => console.log(err));
}

function newRegistration() {
    let registration = {
        message: document.getElementById("order_message").value,
        invoiceAddress:,
        registrationDate:,
        payDate:,
        orderStatus:,
        foundType:,
        regIp:,
        status:,
        payAmount:,
    }

    fetch(urlCreateCupRegistration, {
        method: "POST",
        body: JSON.stringify(registration),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(response => response.json())
        .then(json => console.log(json))
        .catch(err => console.log(err));
}

function newCupAdmin() {
    let cupAdmin = {
        cup_user_username:,
        cup_user_password:,
        cup_user_cup_id:,
        cup_user_rights:,
        cup_user_name:document.getElementById("order_contact_name"),
        cup_user_email: document.getElementById("order_contact_mail"),
        cup_user_phone: document.getElementById("order_contact_number"),
    }


    fetch(urlCreateCupAdmin, {
        method: "POST",
        body: JSON.stringify(cupAdmin),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(response => response.json())
        .then(json => console.log(json))
        .catch(err => console.log(err));
}
  