$(document).ready(function () {

    $('.date-selector').datepicker();

    function RefreshServerTime(response) {
        $("#time").html(response);
    }

    // Check every three seconds if any auctions are up for opening.
    var serverTimer = setInterval(function () {
        // Update website clock with server time.
        $("#refreshtimelink").click();

        $('.opentoday').each(function () {

            // Get start time of each auction opening today.
            var startTime = $(this).find('.opentime').text();

            // Is that start time equal to the server time listed on the page?
            if (startTime == $('#time').text()) {

                // It's time to reload the page!
                window.location.reload();
            }
        });

    }, 3000);

    // Countdown timer for each timer that is currently active.
    var timer = setInterval(function () {
        $(".active-seconds").each(function () {
            var newValue = parseInt($(this).text(), 10) - 1;
            $(this).text(newValue);

            if (newValue >= 9) {
                $(this).css("color", "");
                $(this).css("color", "#4682b4");
            }

            if (newValue == 8) {
                $(this).css("color", "");
                $(this).css("color", "#f3982e");
            }

            if (newValue == 5) {
                $(this).css("color", "");
                $(this).css("color", "Red");
            }

            if (newValue == 1) {
                $(this).siblings('button').addClass('disabled');
            }

            if (newValue <= 0) {
                $(this).text("Fin!");
                // Remove class so timer doesn't touch it again.
                $(this).removeClass("active-seconds");
                // Send message so server verifies if auction actually ended.
                chat.verify($(this).siblings('p.auction-id').text());
            }
        });
    }, 1000);

    var chat = $.connection.chat;
    /* Actions when someone clicks the Bid button. */
    $(".pujar").click(function () {
        chat.receive($(this).siblings('.auction-id').text());
        $(this).parent().siblings('.seconds').text('15');
    });

    chat.updateAuction = function (message) {
        var result = $.parseJSON(message);

        var divId = "#" + result.AuctionId;
        $(divId + " .seconds").text("15");
        $(divId + " .stat .amount").html("$" + result.LanceCost);
        $(divId + " .stat .latestbidder").html(result.LatestBidder);
        $(divId + " .stat").fadeOut().fadeIn();
    };

    chat.endAuction = function (message) {
        var result = $.parseJSON(message);
        console.log(result);
        if (result.IsClosed == true) {
            var divId = "#" + result.AuctionId;
            $(divId + " .timebanner").html("<p><span class='opendate'>Terminado!</span></p>");
        } else {
            window.location.reload();
        }
    };

    $.connection.hub.start();
});