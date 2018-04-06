var FollowingController = function (followingsService) {

    var buttonArtistId;

    var init = function (container) {
        $(container).on('click', '.js-toggle-follow', toggleFollow);
    };

    var toggleFollow = function (e) {
        buttonArtistId = $(e.target);
        var artistId = buttonArtistId.attr("data-artist-id");

        buttonArtistId.hasClass("btn-default")
            ? followingsService.follow(artistId, done, fail)
            : FollowingsService.unfollow(artistId, done, fail);
    }

    var done = function () {
        var text = buttonArtistId.text() === "Following" ? "Follow" : "Following";

        buttonArtistId
            .toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(text);
    }

    var fail = function () {
        alert("Something Failed!!!");
    };

    return {
        init: init
    };

}(FollowingsService);