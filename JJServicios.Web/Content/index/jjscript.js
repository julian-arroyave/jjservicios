$(document).ready(function () {
    
    $("html,body").animate({ scrollTop: $("#home").offset().top }, 1000);

    $(".gohome").click(function() {

        $("html,body").animate({ scrollTop: $("#home").offset().top }, 1000);
    });

    $(".nav a").click(function () {

        var anchor = $(this).attr("value");

        $("html,body").animate({ scrollTop: $(anchor).offset().top }, 1000);
        $(".nav li").removeClass("active");
        $(this).parent().addClass("active");

    });

    $(".goto").click(function() {
        var anchor = $(this).attr("value");
        $("html,body").animate({ scrollTop: $(anchor).offset().top }, 1000);

    });

    $(".select-menu").change(function() {
        var anchor = $(".select-menu option:selected").val();
        $("html,body").animate({ scrollTop: $(anchor).offset().top }, 1000);
    });


});