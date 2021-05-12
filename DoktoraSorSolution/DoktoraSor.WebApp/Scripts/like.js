$(function () {

    var noteids = [];
    $("div[data-note-id]").each(function (i, e) {

        noteids.push($(e).data("note-id"));
    });

    $.ajax({

        method: "POST",
        url: "/Notes/GetLiked",
        data: { ids: noteids }
    }).done(function (data) {

        if (data.result != null && data.result.length > 0) {

            for (var i = 0; i < data.result.length; i++) {
                var id = data.result[i];
                var likedNote = $("div[data-note-id=" + id + "]");
                var btn = likedNote.find("button[data-liked]");
                var span = btn.find("span.like-star");
                btn.data("liked", true);
                span.removeClass("la-star-o");
                span.addClass("la-star");
            }
        }



    }).fail(function () {


    });


    $("button[data-liked]").click(function () {

        var btn = $(this);
        var liked = btn.data("liked");
        var noteid = btn.data("note-id");
        var spanStar = btn.find("span.like-star");
        var spanCount = btn.find("span.like-count");

        $.ajax({

            method: "POST",
            url: "Notes/SetLikeState",
            data: { "noteid": noteid, "liked": !liked }

        }).done(function (data) {

            if (data.hasError) {
                alert(data.errorMessage);
            }
            else {

                liked = !liked;
                btn.data("liked", liked);
                spanCount.text(data.result);

                spanStar.removeClass("la-star-o");
                spanStar.removeClass("la-star");

                if (liked) {
                    spanStar.addClass("la-star");
                }
                else {

                    spanStar.addClass("la-star-o");
                }

            }

        }).fail(function () {
            alert("Lütfen giriş yapınız.");
        })

    });

});