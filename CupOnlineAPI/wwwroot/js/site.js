const url = 'https://localhost:7172/api/cup/coming?nrOfCups=15'

function GetComing() {  
fetch(url)
    .then(response => response.json())
    .then(data => displayCups(data))
    .catch(error => console.error('Unable to get cup.', error));
}

function displayCups(data) {
    var container = document.getElementById("coming");
    var table = document.createElement('table');
    //table.border = '1';

    for (var i = 0; i < data.length; i++) {
        //let div1 = document.createElement("div");
        //div1.setAttribute("id","cupName")
        //div1.innerHTML = data[i].name;
        //container.appendChild(div1);
        //let div2 = document.createElement("div");
        //div2.setAttribute("id", "cupDate")
        //div2.innerHTML = data[i].date;
        //container.appendChild(div2);
        //let div3 = document.createElement("div");
        //div1.setAttribute("id", "sportName")
        //div3.innerHTML = data[i].sport_name;
        //container.appendChild(div3);   
        
        tr = document.createElement('tr');
        tr.setAttribute("class", "activeTable");
        table.appendChild(tr);

        var tdName = document.createElement('td');
        tdName.textContent = data[i].name;
        tdName.setAttribute("id", "cupName");
        tr.appendChild(tdName);

        var tdDate = document.createElement('td');
        tdDate.textContent = data[i].date;
        tdDate.setAttribute("id", "cupDate");
        tr.appendChild(tdDate);

        var tdSportName = document.createElement('td');
        tdSportName.textContent = data[i].sport_name;
        tdSportName.setAttribute("id", "cupSportName");
        tr.appendChild(tdSportName);

        if (i%2===1) {
            tdName.setAttribute("class", "odd");
            tdDate.setAttribute("class", "odd");
            tdSportName.setAttribute("class", "odd");
        }

        else {
            tdName.setAttribute("class", "even");
            tdDate.setAttribute("class", "even");
            tdSportName.setAttribute("class", "even");
        }
    }
    container.appendChild(table);
}