//Request ctor
function CreateRequest(cmdId, Login, Password, StrData, IntData) {
    var Request = {
        cmdId: null,
        Login: null,
        Password: null,
        StrData: null,
        IntData: null,
    };
    Request.cmdId = cmdId;
    Request.Login = Login;
    Request.Password = Password;
    Request.StrData = StrData;
    Request.IntData = IntData;
    return Request;
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
function ServerResponseAsyncTo(request, func) {
    var req = new XMLHttpRequest();
    req.open("POST", "", true);
    req.send(JSON.stringify(request));
    req.onreadystatechange = function () {
        if (req.readyState != 4) return;
        if (req.status == 200) {
            func(JSON.parse(req.responseText));
            req.abort();
        }
    };
}