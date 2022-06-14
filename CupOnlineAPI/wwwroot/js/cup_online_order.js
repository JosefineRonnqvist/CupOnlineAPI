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
const urlCreateCupRegistrationValTest = url + '/api/Order/CreateCupRegistrationValTest'
const urlCreateCupAdmin = url + '/api/Order/CreateCupAdmin'

//gets options to form
function GetOptions() {
    hidePanels();
    GetSports();
    GetAges();
    cupType();
    foundCupOnline();
}

//when submit button is clicked, first get reg ip and then send new cup, new registration and new 
const form = document.querySelector('#order_form');
form.onsubmit = (e) => {
    e.preventDefault();
    getRegIp('https://api.ipify.org?format=json');
}

function sendEmail(mail) {
    Email.send({
        SecureToken: "<your generated token>",
        To: mail,
        From: "support@cuponline.se",
        Subject: "Cuponline ny cup - " + cup_name,
        Body: "<html><h3>{cup_name}</h3></html>"
    }).then(
        message => alert("mail sent successfully")
    );
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

function showPanels() {
    $('.hideNew').show();
}

function hidePanels() {
    $('.hideNew').hide();
}

//post new organizer and return id
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
        city_name: cityToAdd
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

//set the added city as the chosen city in select
function setCity(city) {
    var o = $("<option/>", { value: city.city_id, text: city.city_name });
    $('#Cities').append(o);
    $('#Cities option[value="' + city.city_id + '"]').prop('selected', true);
    $('#Cities').trigger('change');
    $("#Cities").select2("close");
}


//post new cup, get id and send it to new user and new registration
function newCup(regIp) {
    let organizer;
    try { organizer = $('#Organizers').select2('data')[0].club_id; }
    catch (e) {
        organizer = newOrganizer();
    }

    let cup = {
        cup_club_id: organizer,
        cup_sport_id: document.getElementById("order_sport").value,
        cup_startdate: new Date(document.getElementById("order_startdate").value).toISOString().split('T')[0],
        cup_enddate: new Date(document.getElementById("order_enddate").value).toISOString().split('T')[0],
        cup_name: document.getElementById("order_cup_name").value,
        cup_players_age: document.getElementById("order_age_text").value,
        cup_play_place: document.getElementById("order_play_place").value,
        cup_date: checkCupDate(),
        cup_sponsor_logotype: checkCupTypeLogo(),
        cup_sponsor_url: checkCupTypeUrl(),
    }

    fetch(urlCreateCup, {
        method: "POST",
        body: JSON.stringify(cup),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(response => response.json())
        .then(json => {
            console.log(json);
            var cupId = json.cup_id;
            newCupAdmin(cupId);
            newRegistration(cupId, regIp);
        })
        .catch(err => console.log(err));
}

//take startdate and enddate and change it to readable format
function checkCupDate() {
    var startDate = document.getElementById("order_startdate").value.split('-');
    var endDate = document.getElementById("order_enddate").value.split('-');
    var months = ['jan', 'feb', 'mar', 'apr', 'maj', 'jun', 'jul', 'aug', 'sep', 'okt', 'nov', 'dec'];
    var endDay = endDate[2];
    var startDay = startDate[2];
    var endMonth = months[parseInt(endDate[1]) - 1];
    var startMonth = months[parseInt(startDate[1]) - 1] == endMonth ? '' : months[parseInt(startDate[1]) - 1] + ' ';
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
        return "http://www.coreit.se";
    }
    else return "";
}

//post new registration
function newRegistration(cupId, regIp) {

    const reg = {
        cup_id: cupId,
        message: document.getElementById("order_message").value,
        invoiceAddress: "",
        orderStatus: document.getElementById("order_cup_type").value,
        foundType: document.getElementById("order_found_cuponline").value,
        regIp: regIp,
    }
    console.log("reg before:" + JSON.stringify(reg))
    fetch(urlCreateCupRegistration, {
        method: "POST",
        body: JSON.stringify(reg),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(response => response.json())
        .then(json => console.log(json))
        .catch(err => console.log(err));     
}

//get ip from registrator
function getRegIp(url) {
    fetch(url)
        .then(res => res.json())
        .then(json => {
            var regIp = json.ip;
            newCup(regIp);
        });
}

//post new cup admin
function newCupAdmin(cupId) {
    let admin = {
        cup_user_username: "",
        cup_user_password: "",
        cup_user_cup_id: cupId,
        cup_user_rights: 0,
        cup_user_name: document.getElementById("order_contact_name").value,
        cup_user_email: document.getElementById("order_contact_mail").value,
        cup_user_phone: document.getElementById("order_contact_number").value,
    }

    fetch(urlCreateCupAdmin, {
        method: "POST",
        body: JSON.stringify(admin),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(response => response.json())
        .then(json => console.log(json))
        .catch(err => console.log(err));
}

//create the select with search for cities, if city not exists, create new if button is pressed
$(document).ready(function () {
    $("#Cities").select2({
        language: {
            searching: function () { return '' },
            noResults: function () {
                let tempCity = $("#Cities").data('select2').dropdown.$search.val();
                var btnToReturn = document.createElement("button");
                btnToReturn.type = "button";
                btnToReturn.onclick = function () {
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

//create select with search for organizers, if not found and button is pressed, open options to create new organizer
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
                    showPanels();
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



