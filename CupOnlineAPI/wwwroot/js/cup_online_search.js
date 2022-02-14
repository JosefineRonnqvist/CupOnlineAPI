const url = 'https://localhost:7172'
const urlSports = url + '/api/SearchParam/Sports'
const urlYears = url + '/api/SearchParam/Years'
const urlAges = url + '/api/SearchParam/Ages'
var urlSearched = new URL(url + '/api/Cup/Search?noOfCups=1000&name=%25&year=%25&organizer=%25&city=%25&status=4');

function SearchForm() {

    CupNameForm();
    OrganizerForm();
    CityForm();
    GetSports();
    GetYears();   
    GetAges();
    StatusForm();
    GetSearchedCups();
    GetSearchButton();
}

//get search parameters from input and selectfield and set and send to api url
function SearchParams() {
    var cupname = document.getElementById("search_cupname_field").value;
    if (cupname == "") { urlSearched.searchParams.set("name", "%") }
    else  { urlSearched.searchParams.set("name", cupname) };

    var year = document.getElementById("search_year_select").value;
    if (year == "") { urlSearched.searchParams.set("year", "%") }
    else { urlSearched.searchParams.set("year", year)};

    var organizer = document.getElementById("search_organizer_field").value;
    if (organizer == "") { urlSearched.searchParams.set("organizer", "%") }
    else { urlSearched.searchParams.set("organizer", organizer) };

    var city = document.getElementById("search_city_field").value;
    if (city == "") { urlSearched.searchParams.set("city", "%") }
    else { urlSearched.searchParams.set("city", city) };

    var sport = document.getElementById("search_sport_select").value;
    if (sport != 0) { urlSearched.searchParams.set("sport_id", sport) };

    var age = document.getElementById("search_age_select").value;
    if (age != 0) { urlSearched.searchParams.set("age_id", age) } ;

    var status = document.getElementById("search_status_select").value;
    urlSearched.searchParams.set("status", status);

    GetSearchedCups();
}

//Connect search to div
function GetSearchButton() {
    var container = document.getElementById("search_button");
    container.onclick = SearchParams;
}

//create input element to search cupname (or city, organizer or age)
function CupNameForm() {
    var container = document.getElementById("search_cupname");
    var input = document.createElement("input");
    input.id = "search_cupname_field";
    container.appendChild(input);
}

//create input element to search organizer
function OrganizerForm() {
    var container = document.getElementById("search_organizer");
    var input = document.createElement("input");
    input.id = "search_organizer_field";
    container.appendChild(input);
}

//create input element to search city
function CityForm() {
    var container = document.getElementById("search_city");
    var input = document.createElement("input");
    input.id = "search_city_field";
    container.appendChild(input);
}

//get sports from api
function GetSports() {
    fetch(urlSports)
        .then(response => response.json())
        .then(data => sportForm(data))
        .catch(error => console.error("Unable to get sport.", error));
}

//create  a select element with sport options
function sportForm(data) {
    var container = document.getElementById("search_sport");
    var select = document.createElement("select");
    select.id = "search_sport_select";
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
    container.appendChild(select);
}

//get years from api
function GetYears() {
    fetch(urlYears)
        .then(response => response.json())
        .then(data => yearForm(data))
        .catch(error => console.error("Unable to get year.", error));
}

//create a select element with year options
function yearForm(data) {
    var container = document.getElementById("search_year");
    var select = document.createElement("select");
    select.id = "search_year_select";
    var option = document.createElement("option");
    option.textContent = "-";
    option.value = "";
    select.appendChild(option);
    for (var i = 0; i < data.length; i++) {
        var option = document.createElement("option");
        option.textContent = data[i].year;
        option.value = option.textContent;
        select.appendChild(option);
    }
    container.appendChild(select);
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
    var container = document.getElementById("search_age");
    var select = document.createElement("select")
    select.id = "search_age_select";
    var option = document.createElement("option");
    option.textContent = "-";
    option.value = 0;
    select.appendChild(option);
    for (var i = 0; i < data.length; i++) {
        var option = document.createElement("option");
        option.textContent = data[i].age;
        option.value = data[i].age_id;
        select.appendChild(option);
    }
    container.appendChild(select);
}

//create a select element with status options
function StatusForm() {
    var container = document.getElementById("search_status");
    var select = document.createElement("select")
    select.id = "search_status_select";
    var option4 = document.createElement("option");
    option4.textContent = "Aktuella";
    option4.value = 4;
    select.appendChild(option4);
    var option = document.createElement("option");
    option.textContent = "Alla";
    option.value = 0;
    select.appendChild(option);
    var option1 = document.createElement("option");
    option1.textContent = "Genomförda";
    option1.value = 1;
    select.appendChild(option1);
    var option2 = document.createElement("option");
    option2.textContent = "Pågående";
    option2.value = 2;
    select.appendChild(option2);
    var option3 = document.createElement("option");
    option3.textContent = "Kommande";
    option3.value = 3;
    select.appendChild(option3);
    container.appendChild(select);
}

//search cups with api
function GetSearchedCups() {
    fetch(urlSearched)
        .then(response => response.json())
        .then(data => displaySearchedCups(data))
        .catch(error => console.error("Unable to get cup.", error));
}

//create a table and display searchresult
function displaySearchedCups(data) {
    var container = document.getElementById("search_table");
    var existing_table = document.getElementById("searched_table");
    if (existing_table != null) { existing_table.remove() };
    var table = document.createElement("table");
    table.id = "searched_table";

    for (var i = 0; i < data.length; i++) {

        var tr = document.createElement("tr");      

        if (i % 2 === 1) { tr.setAttribute("class", "odd");}

        else {tr.setAttribute("class", "even");}

        var tdName = document.createElement("td");
        tdName.id = "searchedCupName";
        var link = document.createElement("a");
        var name = document.createTextNode(data[i].name);
        link.href = "https://www.cuponline.se/start.aspx?cupid=" + data[i].id;
        link.className = "link";
        link.appendChild(name);
        tdName.appendChild(link);
        tr.appendChild(tdName);

        var tdAge = document.createElement("td");
        tdAge.textContent = data[i].age;
        tdAge.id = "searchedCupAge";
        tr.appendChild(tdAge);

        var tdDate = document.createElement("td");
        tdDate.textContent = data[i].date;
        tdDate.id = "searchedCupDate";
        tr.appendChild(tdDate);

        var tdOrganizer = document.createElement("td");
        tdOrganizer.id = "searchedCupOrganizer";
        var organizer = document.createTextNode(data[i].organizer);
        tdOrganizer.appendChild(organizer);
        tr.appendChild(tdOrganizer);

        var tdSportName = document.createElement("td");
        tdSportName.textContent = data[i].sport_name;
        tdSportName.id = "searchedSportName";
        tr.appendChild(tdSportName);

        var tdCity = document.createElement("td");
        tdCity.textContent = data[i].city;
        tdCity.id = "searchedCupCity";
        tr.appendChild(tdCity);

        table.appendChild(tr);
    }
    container.appendChild(table);
}