
$(document).ready(function () {


    $(window).scroll(function () {
      let header = $(".header");
      scroll = $(window).scrollTop();
      if (scroll >= 66) {
          header.addClass("animate__backOutRight");
        header.addClass("active");
      } else {
          header.removeClass("animate__backOutRight");
        header.removeClass("active");
      }
    });
})