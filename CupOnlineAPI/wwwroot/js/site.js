const url = 'https://localhost:7172'
const urlComing = url + '/api/cup/coming?noOfCups=15&daysFromToday=30'
const urlOngoing = url +'/api/Cup/Ongoing?noOfCups=15'
const urlFinished = url + '/api/Cup/Finished?noOfCups=15&daysFromToday=30'
const urlSearched = url + '/api/Cup/Search?noOfCups=200&name=cup&year=2005&organizer=mo&place=vik&sport=is&age=u'
const urlSports = url + '/api/SearchParam/Sports'
const urlYears = url + '/api/SearchParam/Years'
const urlAges = url + '/api/SearchParam/Ages'

function GetComing() {  
fetch(urlComing)
    .then(response => response.json())
    .then(data => displayCups(data,"coming"))
    .catch(error => console.error('Unable to get cup.', error));
}

function GetOngoing() {
    fetch(urlOngoing)
        .then(response => response.json())
        .then(data => displayCups(data,"ongoing"))
        .catch(error => console.error('Unable to get cup.', error));
}

function GetFinished() {
    fetch(urlFinished)
        .then(response => response.json())
        .then(data => displayCups(data, "finished"))
        .catch(error => console.error('Unable to get cup.', error));
}

function GetSearchedCups() {
    fetch(urlSearched)
        .then(response => response.json())
        .then(data => displaySearchedCups(data))
        .catch(error => console.error('Unable to get cup.', error));
}

function GetSports() {
    fetch(urlSports)
        .then(response => response.json())
        .then(data => sportForm(data))
        .catch(error => console.error('Unable to get sport.', error));
}

function GetYears() {
    fetch(urlYears)
        .then(response => response.json())
        .then(data => yearForm(data))
        .catch(error => console.error('Unable to get year.', error));
}

function GetAges() {
    fetch(urlAges)
        .then(response => response.json())
        .then(data => AgeForm(data))
        .catch(error => console.error('Unable to get age.', error));
}

function displayCups(data,active) {
    var container = document.getElementById(active);
    var table = document.createElement('table');
    table.setAttribute("class", "activeTable");
    //table.border = '1';

    for (var i = 0; i < data.length; i++) {
       
        var tr = document.createElement('tr');
       
        var trCup = document.createElement('tr');
        

        if (i%2===1) {
            tr.className = "odd";
            trCup.className = "odd";
        }

        else {
            tr.className = "even";
            trCup.className = "even";
        }

        var tdDate = document.createElement('td');
        tdDate.textContent = data[i].date;
        tdDate.id= "cupDate";
        tr.appendChild(tdDate);

        var tdSportName = document.createElement('td');
        tdSportName.textContent = data[i].sport_name;
        tdSportName.id= "cupSportName";
        tr.appendChild(tdSportName);
        table.appendChild(tr);

        var tdName = document.createElement('td');
        trCup.id = 'cupName';
        var link = document.createElement('a');
        var name = document.createTextNode(data[i].name);
        link.href= data[i].cup_url;
        link.className = 'link';
        link.appendChild(name);
        tdName.appendChild(link);
        trCup.appendChild(tdName); 
        table.appendChild(trCup);
    }
    container.appendChild(table);
}

function displaySearchedCups(data) {
    var container = document.getElementById("searchedCups");
    var table = document.createElement('table');
    table.setAttribute("class", "searchedTable");
    //table.border = '1';

    for (var i = 0; i < data.length; i++) {

        var tr = document.createElement('tr');
        table.appendChild(tr);

        if (i % 2 === 1) {
            tr.setAttribute("class", "odd");
        }

        else {
            tr.setAttribute("class", "even");
        }

        var tdName = document.createElement('td');
        tdName.id = 'searchedCupName';
        var link = document.createElement('a');
        var name = document.createTextNode(data[i].name);
        link.href = data[i].cup_url;
        link.className = 'link';
        link.appendChild(name);
        tdName.appendChild(link);
        tr.appendChild(tdName);

        var tdAge = document.createElement('td');
        tdAge.textContent = data[i].age;
        tdAge.id= "searchedCupAge";
        tr.appendChild(tdAge);

        var tdDate = document.createElement('td');
        tdDate.textContent = data[i].date;
        tdDate.id= "searchedCupDate";
        tr.appendChild(tdDate);

        var tdClubName = document.createElement('td');
        tdClubName.id = "searchedCupClubName";
        var clubLink = document.createElement('a');
        var clubName = document.createTextNode(data[i].club_name);
        clubLink.href = data[i].club_url;
        clubLink.className = 'clubLink';
        clubLink.appendChild(clubName);
        tdClubName.appendChild(clubLink);        
        tr.appendChild(tdClubName);

        var tdSportName = document.createElement('td');
        tdSportName.textContent = data[i].sport_name;
        tdSportName.id= "cupSportName";
        tr.appendChild(tdSportName);

        var tdPlace = document.createElement('td');
        tdPlace.textContent = data[i].place;
        tdPlace.id= "searchedCupPlace";
        tr.appendChild(tdPlace);
    }
    container.appendChild(table);

    function sportForm(sport) {
        var container = document.getElementById("searchedForm")
        let select = document.createElement("select");
        for (var i = 0; i < sport.length, i++;) {
            let option = document.createElement("option");
            option.setAttribute("value", sport[i].sport_name);
            select.appendChild(option);           
        }
        container.appendChild(select);
    }

    //function searchForm() {
    //    var container = document.getElementById("searchedForm");
    //    var form = document.createElement("form")
    //    sportForm()
    //}
}