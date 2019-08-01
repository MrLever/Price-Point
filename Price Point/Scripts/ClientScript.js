$(function () {
    //Connect to the hub
    var player = $.connection.pricePointHub;

    // Create a function that the hub can call to broadcast messages.
    player.client.broadcastMessage = function (name, message) {
        console.log("Incomming Message...");

        // Html encode display name and message.
        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        // Add the message to the page.
        $('#discussion').append('<li><strong>' + encodedName
            + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
    };


    player.client.prepJoin = function () {
        $('#lobby').empty();
    };

    player.client.processJoin = function (name) {
        console.log("player joining (" + name + ")...");

        //Encode player name
        var encodedName = $('<div />').text(name).html();

        //Append to lobby list
        $('#lobby').append('<li>' + encodedName + '</li>');
    };

    // Get the user name and store it to prepend to messages.
    $('#displayname').val(prompt('Enter your name:', ''));

    // Set initial focus to message input box.
    $('#message').focus();
    // Start the connection.
    $.connection.hub.start().done(function () {
        console.log("Connection open...");

        // Broadcast join event to all clients
        player.server.join($('#displayname').val());

        $('#sendmessage').click(function () {
            // Call the Send method on the hub.
            player.server.send($('#displayname').val(), $('#message').val());

            var elem = document.getElementById('log');
            elem.scrollTop = elem.scrollHeight;

            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });
    });
});