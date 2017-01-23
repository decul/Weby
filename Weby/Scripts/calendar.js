$(function () {
    var calendar = $('#calendar');
    calendar.datepicker({
        weekStart: 1,
        maxViewMode: 2,
        language: "pl",
        multidate: calendar.attr("picker") == 'true' ? 2 : -1,
        //todayHighlight: true,
        beforeShowDay: function (date) {
            var result = { classes: "" };
            var weekDay = date.getUTCDay();
            if (weekDay == 5)
                result.classes += 'saturday ';
            if (weekDay == 6)
                result.classes += 'sunday ';

            for (var i = 0; i < busyDays.length; i++)
            {
                var busy = busyDays[i].split('/');
                if (parseInt(busy[0]) == date.getDate() && parseInt(busy[1]) == date.getMonth() + 1 && parseInt(busy[2]) == date.getFullYear())
                    result.classes += 'busy '
            }
            for (var i = 0; i < usersDays.length; i++) {
                var busy = usersDays[i].split('/');
                if (parseInt(busy[0]) == date.getDate() && parseInt(busy[1]) == date.getMonth() + 1 && parseInt(busy[2]) == date.getFullYear())
                    result.classes += 'booked '
            }

            //if (date.getMonth() == (new Date()).getMonth()) {
            //    switch (date.getDate()) {
            //        case 4:
            //            return {
            //                tooltip: 'Example tooltip',
            //                classes: 'active'
            //            };
            //        case 8:
            //            return false;
            //        case 12:
            //            return "green";
            //    }
            //}

            return result;
        }
    });

}).on('changeDate', function (e) {
    // Add selected days to form
    var dates = "";
    var days = $('#calendar').datepicker('getDates');
    for (var i = 0; i < days.length; i++) {
        // Convert from retarded timezoned time to UTC
        days[i] = new Date(days[i].getFullYear() + "-" + (days[i].getMonth() + 1) + "-" + days[i].getDate() + "Z");
        if (i > 0)
            dates += ",";
        var day = days[i].getDate();
        if (day < 10)
            dates += '0';
        dates += day + "/";
        var month = days[i].getMonth() + 1;
        if (month < 10)
            dates += '0';
        dates += month + "/";
        dates += days[i].getFullYear();
    }
    $('#Dates').val(dates);

    // Check if selected days can be booked
    var startDate = days[0];
    var endDate = days[days.length - 1];
    if (startDate > endDate)
    {
        startDate = days[1];
        endDate = days[0];
    }
    var canBook = true;
    for (var i = 0; i < usersDays.length && canBook; i++) {
        var busy = new Date(usersDays[i].split("/").reverse().join("-") + "Z");
        if (startDate <= busy && busy <= endDate)
            canBook = false;
    }
    for (var i = 0; i < busyDays.length && canBook; i++) {
        var busy = new Date(busyDays[i].split("/").reverse().join("-") + "Z")
        if (startDate <= busy && busy <= endDate)
            canBook = false;
    }
    if (!canBook)
    {
        $("input[type=submit]").attr('disabled', 'disabled');
        $('#busy-msg').show(100);
    }
    else
    {
        $("input[type=submit]").removeAttr('disabled');
        $('#busy-msg').hide(100);
    }

});