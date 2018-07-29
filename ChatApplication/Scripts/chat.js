
//setting a link after user entered login/email and clicked a button
$(document).ready(function () {

    $("#btnLogin").click(function () {
        var userLogin = $("#txtUserName").val();
        var userEmail = $("#txtUserEmail").val();

        if (userLogin && userEmail) {
            //setting a link with paramethers for request to Index through AJAX
            var href = "/Home/Logon?userLogin=" + encodeURIComponent(userLogin);
            href += "&userEmail=" + encodeURIComponent(userEmail);
            href += "&logon=true";
            //adding new query link to ajax-actionlink on login page
            $("#loginButton").attr("href", href).click();
            //adding user name to the specialazed div
            $("#loginTab").text("user: ");
            $("#login").text(userLogin);
            $("#email").text(userEmail);
        }

    });
});

//loading messenges after entering chat
function LoginOnSuccess(data) {

    ScrollDown();

    //refreshing chat history every 2 sec
    setTimeout("Refresh();", 2000);

    //'send-button' click hadler - sending a message
    $("#sendMessagebtn").click(function () {
        var message = $("#textMessage").val();

        if (message) {
            //add message parameter to Index action method. Adding users login from the above div
            var href = "/Home/SendMessages?userLogin=" + encodeURIComponent($("#login").text());
            href += "&&userEmail=" + encodeURIComponent($("#email").text());
            href += "&message=" + encodeURIComponent(message);
            $("#sendQuitLink").attr("href", href).click();
            $("#textMessage").val('');
        }
    });

    //quit-chat-button handler
    $("#logoffbtn").click(function () {

        var href = "/Home/Logoff?userLogin=" + encodeURIComponent($("#login").text());
        href += "&&userEmail=" + encodeURIComponent($("#email").text());
        href += "&logoff=true";
        $("#loginTab").empty();
        $("#sendQuitLink").attr("href", href).click();
        window.location.href = "/Home/Index";
    });
}

//refreshing chat
function Refresh() {
    var href = "/Home/SendMessages?userLogin=" + encodeURIComponent($("#login").text());
    href += "&&userEmail=" + encodeURIComponent($("#email").text());
    $("#sendQuitLink").attr("href", href).click();
    setTimeout("Refresh();", 2000);
}

//removing user login from the div and publishing an error text
function LoginOnFailure(result) {
    $("#login").text("");
    $("#loginTab").empty();
    $("#error").text(result.responseText);
    //removing this text in 2 sec after publishing
    setTimeout("$('#error').empty()", 3000);
}

function ChatOnFailure(result) {
    $("#error").text(result.responseText);
    setTimeout("$('#error').empty();", 3000);
}

//scrolling to the bottom of the chat
function ScrollDown() {
    var messages = $("#messages");
    //getting the height of all the messages
    var height = messages[0].scrollHeight;
    //scrolling chat on height we've just got
    messages.scrollTop(height);
}

//refresh chat in the case of success response
function ChatOnSuccess(result) {
    ScrollDown();
}