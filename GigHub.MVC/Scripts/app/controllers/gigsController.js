var GigsController = function (attendancesService) {

    var buttonGigId;

    var init = function (container) {
        $(container).on('click', '.js-toggle-attendance', toggleAttendance);
    };

    var toggleAttendance = function (e) {

        buttonGigId = $(e.target);

        var gigId = buttonGigId.attr("data-gig-id");

        buttonGigId.hasClass("btn-default")
            ? attendancesService.createAttendance(gigId, done, fail)
            : attendancesService.deleteAttendance(gigId, done, fail);
    };

    var fail = function () {
        alert(`Something Failed!!!`);
    };

    var done = function () {
        var text = buttonGigId.text() === 'Going' ? "Going?" : "Going";

        buttonGigId
            .toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(text);
    };

    return {
        init: init
    };

}(AttendancesService);