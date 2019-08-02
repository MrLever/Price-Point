$(function () {

    // Get handles for the controls
    var fixerControls = document.getElementById('fixerControls');
    var bidderControls = document.getElementById('bidderControls');
    var bidderActiveControls = document.getElementById('bidderActive');
    var fixerActiveControls = document.getElementById('fixerActive');
    var bidderInactiveControls = document.getElementById('bidderInactive');
    var fixerInactiveControls = document.getElementById('fixerInactive');
    var winReadout = document.getElementById('winReadout');

    console.log("Hiding shit...");
    fixerControls.hidden = true;
    bidderControls.hidden = true;
    winReadout.style.display = 'none';

    // Connect to the hub
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
        winReadout.hidden = true;
    };

    player.client.startGame = function () {
        var elem = document.getElementById('gameStatus');
        elem.innerHTML = "<h1 id='gameStatus'> The Game has Begun! </h1>";

        document.getElementById('startGame').hidden = true;
    }

    player.client.selectFixer = function (name) {
       console.log("Evaluating fixer selection: " + name);

        if ($('#displayname').val() == name) {
            fixerControls.hidden = false;
            fixerActiveControls.hidden = false;
            fixerInactiveControls.hidden = true;
            bidderControls.hidden = true;
        }
        else {
            fixerControls.hidden = true;
            bidderControls.hidden = false;
            bidderActiveControls.hidden = true;
            bidderInactiveControls.hidden = false;
        }

        winReadout.style.display = 'none';
    }

    player.client.receiveItemForSale = function (url, itemTitle) {
        var box = document.getElementById('forSaleBox');
        box.style.background = "url('" + url + "')";

        var title = document.getElementById('itemTitle');
        title.innerHTML = itemTitle;
    }

    player.client.startBidding = function (name) {
        if ($('#displayname').val() == name) {
            fixerActiveControls.hidden = true;
            fixerInactiveControls.hidden = false;
        }
        else {
            bidderActiveControls.hidden = false;
            bidderInactiveControls.hidden = true;
        }
    }

    player.client.endBidding = function () {
        fixerControls.hidden = true;
        bidderControls.hidden = true;
    }

    player.client.declareWinner = function (winners) {
        winReadout.hidden = false;
        var winnersDiv = document.getElementById('winners');
        winnersDiv.innerHTML = winners;
        winReadout.style.display = 'block';
    }

    // Get the user name and store it to prepend to messages.
    $('#displayname').val(prompt('Enter your name:', ''));

    // Set initial focus to message input box.
    $('#message').focus();
    // Start the connection.
    $.connection.hub.start().done(function () {
        console.log("Connection open...");

        // Broadcast join event to all clients
        player.server.join($('#displayname').val());

        //Bind send button
        $('#sendmessage').click(function () {
            // Call the Send method on the hub.
            player.server.send($('#displayname').val(), $('#message').val());

            var elem = document.getElementById('log');
            elem.scrollTop = elem.scrollHeight;

            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });

        //Bind start game button
        $('#startGame').click(function () {
            // Call the Send method on the hub.
            player.server.startGame();
        });

        $('#endFixerTurn').click(function () {
            player.server.postItemForSale(
                $('#itemURL').val(),
                $('#itemName').val()
            );

            player.server.startBidding($('#itemPrice').val());
        });

        $('#bidButton').click(function () {
            player.server.postBid($('#displayname').val(), $('#bidValue').val());
        });

        $('#nextRound').click(function () {
            player.server.startRound();
        });
    });
});