class Request {
  constructor(cmdId, StrData, IntData) {    
    this.cmdId = cmdId;
    this.StrData = StrData;
    this.IntData = IntData;
  }
}

class Search {
  constructor(txt) {
    this.Input = txt;
  }
}

/* Function that make a request to the server and returns
   its response in a form of an object

   Use reference:
           var response = ServerResponseTo(request);
*/
function ServerResponseTo(request) {
    var req = new XMLHttpRequest();
    req.open("POST", "", false);
    req.send(JSON.stringify(request));
    return JSON.parse(req.responseText);
}

/* Function that make a request to the server and performs
   an action (second param) with server response after receiving it.
   Action format is: Func(response), where response is object
   
   Use reference:
           ServerResponseAsyncTo(request, function (response) {
                alert(response.IntData);
           });
*/
function ServerResponseAsyncTo(request, onRespond) {
    var req = new XMLHttpRequest();
    req.open("POST", "", true);
    req.send(JSON.stringify(request));
    req.onreadystatechange = function () {
        if (req.readyState != 4) return;
        if (req.status == 200) {
            onRespond(req.responseText);
            req.abort();
        }
    };
}

function changeBackground(color) {
   document.body.style.background = color;
}

function onSearch() {
  var search = new Search(document.getElementById("searchbar").value);
  var request = new Request(0, JSON.stringify(search), 0);

  ServerResponseAsyncTo(request, function (response) {
      var results = document.getElementById("results");
      results.innerHTML = "";
      results.style.display = "none";
      var responseText = JSON.parse(response).Answer;
      if (responseText != "") {
        results.style.display = "block";
        var elem = document.createElement("span");        
        elem.innerText = "Результаты:";
        elem.style.marginBottom  = "100px";
        results.appendChild(elem);

        elem = document.createElement("div");
        elem.classList.add('result');
        elem.innerText = "Услуги: " + responseText;
        elem.onclick = function() {
          document.getElementById("searchbar").value = "";
          results.innerHTML = "";
          results.style.display = "none";
        }
        results.appendChild(elem);

        elem = document.createElement("div");
        elem.classList.add('result');
        elem.innerText = "Организации: " + responseText;
        elem.onclick = function() {
          document.getElementById("searchbar").value = "";
          results.innerHTML = "";
          results.style.display = "none";
        }
        results.appendChild(elem);

        elem = document.createElement("div");
        elem.classList.add('result');
        elem.innerText = "Адреса: " + responseText;
        elem.onclick = function() {
          document.getElementById("searchbar").value = "";
          results.innerHTML = "";
          results.style.display = "none";
        }
        results.appendChild(elem);
      }
  });
}