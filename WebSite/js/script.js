var request = CreateRequest(1, 'login', 'password', "message", 128);

ServerResponseAsyncTo(request, function (response) {
    alert(response.IntData);
});