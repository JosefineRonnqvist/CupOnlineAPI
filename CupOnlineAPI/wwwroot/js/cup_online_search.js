const url = 'https://localhost:7172'
const urlSports = url + '/api/SearchParam/Sports'
const urlYears = url + '/api/SearchParam/Years'
const urlAges = url + '/api/SearchParam/Ages'
const urlSearched = new URL(url + '/api/Cup/Search?noOfCups=200');
//&name=cup&year=2005&organizer=mo&place=vik&sport=is&age=u
function SearchForm() {

    CupNameForm();
    OrganizerForm();
    CityForm();
    GetSports();
    GetYears();   
    GetAges();
    StatusForm();
    GetSearchButton();
   /* GetStatus();*/

    //SearchParams();
}

function SearchParams() {
    var cupname = document.getElementById("search_cupname_field").value;
    urlSearched.searchParams.set("name", cupname);

    var organizer = document.getElementById("search_organizer_field").value;
    urlSearched.searchParams.set("organizer",organizer);

    var city = document.getElementById("search_city_field").value;
    urlSearched.searchParams.set("place", city);

    var sport = document.getElementById("search_sport_select");
    var selected_sport = sport.options[sport.selectedIndex].text;
    urlSearched.searchParams.set("sport", selected_sport);

    var year = document.getElementById("search_year_select");
    var selected_year = year.options[year.selectedIndex].text;
    urlSearched.searchParams.set("year", selected_year);

    var age = document.getElementById("search_age_select");
    var selected_age = age.options[age.selectedIndex].value;
    urlSearched.searchParams.set("age", selected_age);

    var status = document.getElementById("search_status_select");
    var selected_status = status.options[status.selectedIndex].value;
    urlSearched.searchParams.set("status", selected_status);

    GetSearchedCups();
}

function GetSearchButton() {
    var container = document.getElementById("search_button");
    container.onclick = SearchParams;
}

function CupNameForm() {
    var container = document.getElementById("search_cupname");
    var input = document.createElement("input");
    input.id = "search_cupname_field";
    input.placeholder = "Cupnamn";
    container.appendChild(input);
}

function OrganizerForm() {
    var container = document.getElementById("search_organizer");
    var input = document.createElement("input");
    input.id = "search_organizer_field";
    input.placeholder = "Arrangör";
    container.appendChild(input);
}

function CityForm() {
    var container = document.getElementById("search_city");
    var input = document.createElement("input");
    input.id = "search_city_field";
    input.placeholder = "Ort";
    container.appendChild(input);
}

function GetSports() {
    fetch(urlSports)
        .then(response => response.json())
        .then(data => sportForm(data))
        .catch(error => console.error("Unable to get sport.", error));
}

function sportForm(data) {
    var container = document.getElementById("search_sport");
    var select = document.createElement("select");
    select.id = "search_sport_select";
    var option = document.createElement("option");
    option.textContent = "";
    select.appendChild(option);
    for (var i = 0; i < data.length; i++) {
        var option = document.createElement("option");
        option.textContent = data[i].sport_name;
        select.appendChild(option);
    }   
    container.appendChild(select);
}

function GetYears() {
    fetch(urlYears)
        .then(response => response.json())
        .then(data => yearForm(data))
        .catch(error => console.error("Unable to get year.", error));
}

function yearForm(data) {
    var container = document.getElementById("search_year");
    var select = document.createElement("select");
    select.id = "search_year_select";
    var option = document.createElement("option");
    option.textContent = "";
    select.appendChild(option);
    for (var i = 0; i < data.length; i++) {
        var option = document.createElement("option");
        option.textContent = data[i].year;
        select.appendChild(option);
    }
    container.appendChild(select);
}

function GetAges() {
    fetch(urlAges)
        .then(response => response.json())
        .then(data => ageForm(data))
        .catch(error => console.error("Unable to get age.", error));
}

function ageForm(data) {
    var container = document.getElementById("search_age");
    var select = document.createElement("select")
    select.id = "search_age_select";
    var option = document.createElement("option");
    option.textContent = "";
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

function StatusForm() {
    var container = document.getElementById("search_status");
    var select = document.createElement("select")
    select.id = "search_status_select";
    var option = document.createElement("option");
    option.textContent = "";
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
    var container = document.getElementById("searchedCups");
    var table = document.createElement("table");
    table.setAttribute("class", "searchedTable");
    //table.border = '1';

    for (var i = 0; i < data.length; i++) {

        var tr = document.createElement("tr");
        table.appendChild(tr);

        if (i % 2 === 1) {
            tr.setAttribute("class", "odd");
        }

        else {
            tr.setAttribute("class", "even");
        }

        var tdName = document.createElement("td");
        tdName.id = "searchedCupName";
        var link = document.createElement("a");
        var name = document.createTextNode(data[i].name);
        link.href = data[i].cup_url;
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

        var tdClubName = document.createElement("td");
        tdClubName.id = "searchedCupClubName";
        var clubLink = document.createElement("a");
        var clubName = document.createTextNode(data[i].club_name);
        clubLink.href = data[i].club_url;
        clubLink.className = 'clubLink';
        clubLink.appendChild(clubName);
        tdClubName.appendChild(clubLink);
        tr.appendChild(tdClubName);

        var tdSportName = document.createElement("td");
        tdSportName.textContent = data[i].sport_name;
        tdSportName.id = "searchedSportName";
        tr.appendChild(tdSportName);

        var tdPlace = document.createElement("td");
        tdPlace.textContent = data[i].place;
        tdPlace.id = "searchedCupPlace";
        tr.appendChild(tdPlace);
    }
    container.appendChild(table);
}