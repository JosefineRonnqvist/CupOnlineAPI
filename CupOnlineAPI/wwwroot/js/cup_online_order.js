const url = 'https://localhost:7172'
const urlOrganizers = url + '/api/Order/GetAllOrganizers'
const urlOreganizerSearch = url + '/api/SearchParam/Organizers?clubName='
const urlCitiesSearch = url + '/api/SearchParam/Cities?city='
const urlSports = url + '/api/Order/GetAllSports'
const urlAges = url + '/api/SearchParam/Ages'
const urlCreateCity = url + '/api/Order/CreateCity'
const urlCreateOrganizer = url + '/api/Order/CreateOrganizer'
const urlCreateCup = url + '/api/Order/CreateCup'
const urlCreateCupRegistration = url + '/api/Order/CreateCupRegistration'
const urlCreateCupAdmin = url + '/api/Order/CreateCupAdmin'

function GetOptions() {
    GetSports();
    GetAges();
    cupType();
    foundCupOnline();
}


////getOrganizers from api
//function GetOrganizers() {
//    fetch(urlOrganizers)
//        .then(response => response.json())
//        .then(data => organizerOptions(data))
//        .catch(error => console.error("Unable to get organizer.", error));
//}

////Create organizer options
//function organizerOptions(data) {
//    var select = document.getElementById("order_organizer");
//    var option = document.createElement("option");
//    option.textContent = "-";
//    option.value = 0;
//    select.appendChild(option);
//    for (var i = 0; i < data.length; i++) {
//        var option = document.createElement("option");
//        option.textContent = data[i].club_name;
//        option.value = data[i].club_id;
//        select.appendChild(option);
//    }
//}


function GetOrganizers(val) {
    res = document.getElementById("getOrganizersResult");
    res.innerHTML = '';
    if (val == '') {
        return;
    }
    fetch(urlOreganizerSearch + val).then(
        function (response) {
            return response.json();
        }).then(function (data) {
            for (i = 0; i < data.length; i++) {
                var option = document.createElement("option");
                option.textContent = data[i].club_name;
                option.value = data[i].club_id;
                res.appendChild(option);
            }}).catch(function (err) {
            console.warn('Something went wrong.', err);
            return false;
        });
}

function GetCities(val) {
    res = document.getElementById("getCitiesResult");
    res.innerHTML = '';
    if (val == '') {
        return;
    }
    fetch(urlCitiesSearch + val).then(
        function (response) {
            return response.json();
        }).then(function (data) {
            for (i = 0; i < data.length; i++) {
                var option = document.createElement("option");
                option.textContent = data[i].city_name;
                option.value = data[i].city_id;
                res.appendChild(option);
            }
        }).catch(function (err) {
            console.warn('Something went wrong.', err);
            return false;
        });
}

//get ages from api
function GetAges() {
    fetch(urlAges)
        .then(response => response.json())
        .then(data => ageForm(data))
        .catch(error => console.error("Unable to get age.", error));
}

//create a select element with age options
function ageForm(data) {
    var select = document.getElementById("order_age_checkbox");
    for (var i = 0; i < data.length; i++) {
        var label = document.createElement("label");
        var input = document.createElement("input");
        input.type = "checkbox";
        label.textContent = data[i].age;
        input.value = data[i].age_id;
        label.appendChild(input);
        select.appendChild(label);
    }
}

//get sports from api
function GetSports() {
    fetch(urlSports)
        .then(response => response.json())
        .then(data => sportOptions(data))
        .catch(error => console.error("Unable to get sport.", error));
}

//creatae sport options
function sportOptions(data) {
    var select = document.getElementById("order_sport");
    var option = document.createElement("option");
    option.textContent = "-";
    option.value = 0;
    select.appendChild(option);
    for (var i = 0; i < data.length; i++) {
        var option = document.createElement("option");
        option.textContent = data[i].sport_name;
        option.value = data[i].sport_id;
        select.appendChild(option);
    }
}

//Create cup type
function cupType() {
    var select = document.getElementById("order_cup_type");
    var option = document.createElement("option");
    option.textContent = "-";
    option.value = 0;
    var option2 = document.createElement("option");
    option2.textContent = "Gratis upp t.o.m. U12*";
    option2.value = 1;
    var option3 = document.createElement("option");
    option3.textContent = "Betalande över U12";
    option3.value = 2;
    select.appendChild(option);
    select.appendChild(option2);
    select.appendChild(option3);
}

//create options for cup type
function foundCupOnline() {
    var select = document.getElementById("order_found_cuponline");
    var option = document.createElement("option");
    option.textContent = "-";
    option.value = 0;
    var option2 = document.createElement("option");
    option2.textContent = "E-postutskick";
    option2.value = 1;
    var option3 = document.createElement("option");
    option3.textContent = "Brevutskick";
    option3.value = 2;
    var option4 = document.createElement("option");
    option4.textContent = "Sökte på internet";
    option4.value = 3;
    var option5 = document.createElement("option");
    option5.textContent = "Cup som använde CupOnline";
    option5.value = 4;
    var option6 = document.createElement("option");
    option6.textContent = "Föreningen använder CupOnline";
    option6.value = 5;
    var option7 = document.createElement("option");
    option7.textContent = "Annat";
    option7.value = 6;
    select.appendChild(option);
    select.appendChild(option2);
    select.appendChild(option3);
    select.appendChild(option4);
    select.appendChild(option5);
    select.appendChild(option6);
    select.appendChild(option7);
}

//post new organizer
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

//post new city
function newCity() {

    let city = {
        city_name: document.getElementById("order_new_club_city").value,
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

//post new cup
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

//post new reigistration
function newRegistration() {
    let registration = {
        message: document.getElementById("order_message").value,
        //invoiceAddress:,
        //registrationDate:,
        //payDate:,
        //orderStatus:,
        //foundType:,
        //regIp:,
        //status:,
        //payAmount:,
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

//post new cup admin
function newCupAdmin() {
    let cupAdmin = {
        //cup_user_username:,
        //cup_user_password:,
        //cup_user_cup_id:,
        //cup_user_rights:,
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
  