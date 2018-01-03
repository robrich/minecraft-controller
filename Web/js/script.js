$(document).ready(function(){
  // smooth scroll to anchors
  $('a[href^="#"]').on('click',function (e) {
    if (e.button !== 0) {
      return; // not left-click
    }
    var target = this.hash;
    if (!target) {
      return; // it's <a href="#" ... so let's assume JavaScript will handle it
    }
    e.preventDefault();
    var $target = $(target);
    $('html, body').stop().animate(
      {
      'scrollTop': $target.offset().top - 150
      },
      1100,
     'swing',
      function () {
        history.pushState({}, '', target);
      }
    );
  });

  // transparency on navbar
  function setNavTransparency() {
    if ($(window).scrollTop() >= 50) {
      $('nav').css('background','#212121');
    } else {
      $('nav').css('background','transparent');
    }
  }
  $(window).scroll(setNavTransparency);
  setNavTransparency(); // do it once on page load too

  // animate-on-scroll init
  AOS.init({
    duration: 800,
    easing: 'ease-in-sine',
    disable: 'mobile'
  });

  // mobile nav init
  $(".button-collapse").sideNav();
});

