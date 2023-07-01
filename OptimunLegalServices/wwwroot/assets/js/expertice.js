$(".htc_title").click(function () {
    $(this).toggleClass("active");
    $(this).next().slideToggle();
    $(".htc_info").not($(this).next()).slideUp();
  });