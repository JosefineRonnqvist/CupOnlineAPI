const url = 'https://localhost:7172'
const urlOrganizers = url + '/api/Order/GetAllOrganizers'
const urlOreganizerSearch = url + '/api/SearchParam/Organizers'
const urlCitiesSearch = url + '/api/SearchParam/Cities'
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

function ClickedOrderCup() { 
    //newCupAdmin();
    //newRegistration();
    newCup();
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
    option2.value = 3;
    var option3 = document.createElement("option");
    option3.textContent = "Betalande över U12";
    option3.value = 17;
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
        club_name: document.getElementById("order_new_club").value,
        club_shortname: document.getElementById("order_new_club").value,
        club_url: document.getElementById("order_new_club_url").value,
        club_city_id: document.getElementById("Cities").value,
        club_sport_id: document.getElementById("order_sport").value,
    }
    fetch(urlCreateOrganizer, {
        method: "POST",
        body: JSON.stringify(organizer),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(response => response.json())
        .then(json => {
            console.log(json);
            return json.club_city_id;
        }
        )
        .catch(err => console.log(err));   
}

//post new city
function newCity(cityToAdd) {
    let city = {
        city_name:cityToAdd
    }

    fetch(urlCreateCity, {
        method: "POST",
        body: JSON.stringify(city),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(response => response.json())
        .then(json => {
            setCity(json);
            console.log(json);
        })
        .catch(err => console.log(err));
}

function setCity(city) {
    var o = $("<option/>", { value: city.city_id, text: city.city_name });
    $('#Cities').append(o);
    $('#Cities option[value="' + city.city_id + '"]').prop('selected', true);
    $('#Cities').trigger('change');
    $("#Cities").select2("close");
}


//post new cup
function newCup() {
    let organizer = $('#Organizers').select2('data')[0].club_id;
    if (organizer < 1) {
        organizer = newOrganizer();
    }
    let cup = {
        cup_club_id: organizer,
        cup_sport_id: document.getElementById("order_sport").value,
        cup_startdate: document.getElementById("order_startdate").value,
        cup_enddate: document.getElementById("order_enddate").value,
        cup_name: document.getElementById("order_cup_name").value,
        cup_players_age: document.getElementById("order_age_text").value,
        cup_play_place: document.getElementById("order_play_place").value,
        cup_date: checkCupDate(),
        cup_sponsor_logotype:checkCupTypeLogo(),
        cup_sponsor_url:checkCupTypeUrl(),
    }

    fetch(urlCreateCup, {
        method: "POST",
        body: JSON.stringify(cup),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(response => response.json())
        .then(json => {
            console.log(json);
            json.cup_id;
        })
        .catch(err => console.log(err));
}

function checkCupDate() {
    var startDate = document.getElementById("order_startdate").value.split('-');
    var endDate = document.getElementById("order_enddate").value.split('-');
    var months = ['jan', 'feb', 'mar', 'apr', 'maj', 'jun', 'jul', 'aug', 'sep', 'okt', 'nov', 'dec'];
    var endDay = endDate[2];
    var startDay = startDate[2];
    var endMonth = months[parseInt(endDate[1])-1];
    var startMonth = months[parseInt(startDate[1]) - 1] == endMonth ? '' : months[parseInt(startDate[1])-1] + ' ';
    var endYear = endDate[0];
    var startYear = startDate[0] == endYear ? '' : startDate[0] + ' ';   
    var showDate = startDay + ' ' + startMonth + startYear + '- ' + endDay + ' ' + endMonth + ' ' + endYear;
    return showDate;
}

function checkCupTypeLogo() {
    if (document.getElementById("order_cup_type").value == 3) { return "logotype_coreit.gif"; }
    else return "";
}

function checkCupTypeUrl() {
    if (document.getElementById("order_cup_type").value == 3) {
        return "http://www.coreit.se" ; }  
    else return "";
}

//post new reigistration
function newRegistration() {

    const registration = {
        cup_id: newCup(),
        message: document.getElementById("order_message").value,
        invoiceAddress:"",
        registrationDate: new Date(),
        payDate:"",
        orderStatus: document.getElementById("order_cup_type").value,
        foundType: document.getElementById("order_found_cuponline").value,
        regIp: document.getElementById("regIp"),
        status:0,
        payAmount:0,
    }

fetch(urlCreateCupRegistration, {
    method: "POST",
    body: JSON.stringify(registration),
    headers: { "Content-type": "application/json; charset=UTF-8" }
} )
        .then(response => response.json())
        .then(json => console.log(json))
        .catch(err => console.log(err));
}

//post new cup admin
function newCupAdmin() {
    let cupAdmin = {
        cup_user_username:"",
        cup_user_password:"",
        cup_user_cup_id:"",
        cup_user_rights:"",
        cup_user_name: document.getElementById("order_contact_name"),
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


$(document).ready(function () {
    $("#Cities").select2({
        language: {
            searching: function () { return '' },
            noResults: function () {
                let tempCity = $("#Cities").data('select2').dropdown.$search.val();
                var btnToReturn = document.createElement("button");
                btnToReturn.type = "button";
                btnToReturn.onclick = function ()
                {                   
                    newCity(tempCity);
                }
                btnToReturn.innerHTML = "Lägg till denna ort";
                return btnToReturn;                             
            },
            errorLoading: function () { return '' },
        },
        escapeMarkup: function (markup) { return markup; },
        multiple: false,
        selectOnClose: true,
        ajax: {
            url: urlCitiesSearch,
            dataType: 'json',
            method: 'get',
            delay: 300,
            data: function (params) {
                return {
                    city: params.term,
                };
            },
            processResults: function (response) {
                var select2Data = $.map(response, function (obj) {
                    obj.text = obj.city_name;
                    obj.id = obj.city_id;
                    return obj;
                });

                return {
                    results: select2Data
                };
            }
        },
        placeholder: "Ort",
    });
});



$(function () {
    var hidden = document.getElementById("hide_add_new_organizer");

    //$('#Cities').select2().next().hide();
    //$('#order_new_club_url').hide();
});

$(document).ready(function () {
    $("#Organizers").select2({
        language: {
            searching: function () { return '' },
            noResults: function () {
                let tempOrganizer = $("#Organizers").data('select2').dropdown.$search.val();
                var btnToReturn = document.createElement("button");
                btnToReturn.type = "button";
                btnToReturn.innerHTML = "Lägg till denna klubb"
                btnToReturn.id = "addNewOrganizer";
                var tempOption = document.createElement("option");
                tempOption.selected = "selected";
                btnToReturn.onclick = function () {
                    var organizerToAdd = document.getElementById("order_new_club");
                    organizerToAdd.value = tempOrganizer;
                    $('#Organizers').val(null).trigger('change');
                    $("#Organizers").select2("close");
                    //$('#Cities').select2().next().show();
                    //$('#order_new_club_url').show();
                }
                return btnToReturn;
            },
            errorLoading: function () { return '' }
        },
        escapeMarkup: function (markup) {
            return markup;
        },
        multiple: false,
        selectOnClose: true,
        ajax: {
            url: urlOreganizerSearch,
            dataType: 'json',
            method: 'get',
            delay: 300,
            data: function (params) {
                return {
                    clubName: params.term,
                };
            },
            processResults: function (response) {
                var select2Data = $.map(response, function (obj) {
                    obj.text = obj.club_name;
                    obj.id = obj.club_name;
                    return obj;
                });

                return {
                    results: select2Data
                };
            },
        },
        placeholder: "Förening",
    });
});

//$(function () {
//    $('#hide_add_new_organizer').children().hide();
//    $('#addNewOrganizer').click(function () {
//        $('#hide_add_new_organizer').children().show();
//    })
//});


$.getJSON("https://api.ipify.org?format=json", function (data) {
    $("#regIp").html(data.ip);
})
