var GigDetailsController = function (followingsService) {

    var buttonFollow;

    var init = function () {
        $('.js-toggle-follow').click(toggleFollowing);
    };

    var toggleFollowing = function (e) {

        buttonFollow = $(e.target);
        var artistId = buttonFollow.attr("data-artist-id");

        buttonFollow.hasClass("btn-default")
            ? followingsService.follow(artistId, done, fail)
            : FollowingsService.unfollow(artistId, done, fail);
    };

    var done = function () {

        var text = buttonFollow.text() === "Follow" ? "Following" : "Follow";

        buttonFollow
            .toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(text);
    };

    var fail = function () {
        alert("Something Failed!!!");
    };

    return {
        init: init
    };

}(FollowingsService);