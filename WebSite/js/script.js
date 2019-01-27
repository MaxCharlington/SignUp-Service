var request = CreateRequest(0, "message", 128);

ServerResponseAsyncTo(request, function (response) {
    alert(sha256(response));
});