const url = 'https://localhost:7172'
const urlComing = url + '/api/cup/coming?noOfCups=30'   //to change how many searchresult, change noOfCups={wanted number of searchresult}=
const urlOngoingCups = url + '/api/Cup/OngoingCups?noOfCups=30'
const urlOngoingSeries = url + '/api/Cup/OngoingSeries?noOfCups=30'
const urlFinished = url + '/api/Cup/Finished?noOfCups=30'
const urlLatest = url + '/api/Cup/Latest?noOfCups=30'

function GetComing() {  
fetch(urlComing)
    .then(response => response.json())
    .then(data => displayCups(data,"coming"))
    .catch(error => console.error("Unable to get cup.", error));
}

function GetOngoingCups() {
    fetch(urlOngoingCups)
        .then(response => response.json())
        .then(data => displayCups(data,"ongoing_cups"))
        .catch(error => console.error("Unable to get cup.", error));
}

function GetOngoingSeries() {
    fetch(urlOngoingSeries)
        .then(response => response.json())
        .then(data => displayCups(data, "ongoing_series"))
        .catch(error => console.error("Unable to get cup.", error));
}

function GetFinished() {
    fetch(urlFinished)
        .then(response => response.json())
        .then(data => displayCups(data, "finished"))
        .catch(error => console.error("Unable to get cup.", error));
}

function GetLatest() {
    fetch(urlLatest)
        .then(response => response.json())
        .then(data => displayCups(data, "latest"))
        .catch(error => console.error("Unable to get cup.", error));
}

function displayCups(data,active) {
    var container = document.getElementById(active);
    var table = document.createElement("table");
    table.setAttribute("class", "activeTable");

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
        tdDate.id= "Date";
        tr.appendChild(tdDate);

        var tdSportName = document.createElement('td');
        tdSportName.textContent = data[i].sport_name;
        tdSportName.id= "SportName";
        tr.appendChild(tdSportName);
        table.appendChild(tr);

        var tdName = document.createElement('td');
        trCup.id = 'cupName';
        var link = document.createElement('a');
        var name = document.createTextNode(data[i].name);
        link.href = "https://www.cuponline.se/start.aspx?cupid=" + data[i].id;
        link.className = 'link';
        link.appendChild(name);
        tdName.appendChild(link);
        trCup.appendChild(tdName); 
        table.appendChild(trCup);
    }
    container.appendChild(table);
}



