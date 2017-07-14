// If JavaScript is enabled remove 'no-js' class and give 'js' class
jQuery('html').removeClass('no-js').addClass('js');

// Add .osx class to html if on Os/x
if ( navigator.appVersion.indexOf("Mac")!=-1 ) 
	jQuery('html').addClass('osx');

// When DOM is fully loaded
jQuery(document).ready(function($) {
  "use strict";


/* --------------------------------------------------------	
	 External Links
   --------------------------------------------------------	*/	

	  $(window).load(function() {
			$('a[rel=external]').attr('target','_blank');	
		});

/* --------------------------------------------------------	
	 Tooltips
   --------------------------------------------------------	*/	

    $('body').tooltip({
        delay: { show: 300, hide: 0 },
        selector: '[data-toggle=tooltip]:not([disabled])'
    });
    
/* --------------------------------------------------------	
	 Back To Top
   --------------------------------------------------------	*/	
    
		$('.back-to-top').click(function () {
			$('body,html').animate({
				scrollTop: 0
			}, 800);
			return false;
		}); 
          
/* --------------------------------------------------------	
	 Dynamic Progress Bar
   --------------------------------------------------------	*/

    $(window).load(function(){    
      $('.progress-bar').css('width',  function(){ return ($(this).attr('data-percentage')+'%')});
    });

/* --------------------------------------------------------	
	 Back To Top Button
   --------------------------------------------------------	*/	

	(function() {   
  		$('<a id="back-to-top"></a>').appendTo($('body'));

			$(window).scroll(function() {
				if($(this).scrollTop() != 0) {
					$('#back-to-top').fadeIn();	
				} else {
					$('#back-to-top').fadeOut();
				}
			});
			
			$('#back-to-top').click(function() {
				$('body,html').animate({scrollTop:0},800);
			});	        
	})();  

/* --------------------------------------------------------	
	 Fixed Menu
   --------------------------------------------------------	*/	

  $('.navbar').sticky({topSpacing:0});

/* --------------------------------------------------------	
	 One Page Navigation
   --------------------------------------------------------	*/	

  $('.navbar').onePageNav({
     currentClass: 'active',
  	 changeHash: false,
  	 scrollSpeed: 750,
  	 scrollOffset: 80,
  	 scrollThreshold: 0.1,
  	 filter: ':not(.ext)',
  	 easing: 'swing',
  });

/* --------------------------------------------------------	
	 Select navigation for small screens 
   --------------------------------------------------------	*/

	$("<option />", {
	   "selected": "selected",
	   "value"   : "",
	   "text"    : "Navigation"
	}).appendTo(".select-menu");


	$(".nav a").each(function() {
	 var select = $(this);
	 $("<option />", {
	     "value"   : select.attr("href"),
	     "text"    : select.attr("title")
	 }).appendTo(".select-menu");
	});


  $(".select-menu").change(function() {        
    $('html, body').animate({
      scrollTop: $($(this).find("option:selected").val()).offset().top -60 }, 750);
  });

/* --------------------------------------------------------	
	 Move Nav
   --------------------------------------------------------	*/

  $(window).scroll(function(){ 
    if ($(this).scrollTop() > 50){ 
      $('.navbar').addClass("navbar-move");
    } 
    else{
      $('.navbar').removeClass("navbar-move");
    }
  });

/* --------------------------------------------------------	
	 Magnific Popup
   --------------------------------------------------------	*/ 

  $('.image-link').magnificPopup({type:'image'});

	$('.popup-gallery').magnificPopup({
		delegate: 'a',
		type: 'image',
		tLoading: 'Loading image #%curr%...',
		mainClass: 'mfp-img-mobile',
		gallery: {
			enabled: true,
			navigateByImgClick: true,
			preload: [0,1] // Will preload 0 - before current, and 1 after the current image
		},
		image: {
			tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',
			titleSrc: function(item) {
				return item.el.attr('title') + '<small>by Marsel Van Oosten</small>';
			}
		}
	});
          
/* --------------------------------------------------------	
	 Portfolio 
   --------------------------------------------------------	*/	
  
  (function() {
   
    $(window).load(function(){
    	// container
    	var $container = $('#portfolio-items');
    	function filter_projects(tag)
    	{
    	  // filter projects
    	  $container.isotope({ filter: tag });
    	  // clear active class
    	  $('li.act').removeClass('act');
    	  // add active class to filter selector
    	  $('#portfolio-filter').find("[data-filter='" + tag + "']").parent().addClass('act');
    	}
    	if ($container.length) {
    		// conver data-tags to classes
    		$('.project').each(function(){
    			var $this = $(this);
    			var tags = $this.data('tags');
    			if (tags) {
    				var classes = tags.split(',');
    				for (var i = classes.length - 1; i >= 0; i--) {
    					$this.addClass(classes[i]);
    				};
    			}
    		})
    		// initialize isotope
    		$container.isotope({
    		  // options...
    		  itemSelector : '.project',
    		  layoutMode   : 'fitRows'
    		});
    		// filter items
    		$('#portfolio-filter li a').click(function(){
    			var selector = $(this).attr('data-filter');
    			filter_projects(selector);
    			return false;
    		});
    		// filter tags if location.has is available. e.g. http://example.com/work.html#design will filter projects within this category
    		if (window.location.hash!='')
    		{
    			filter_projects( '.' + window.location.hash.replace('#','') );
    		}
    	}
      
    })

	})();     
  
/* --------------------------------------------------------	
	 Parallax
   --------------------------------------------------------	*/	

  var detectmob = false;	
  if(navigator.userAgent.match(/Android/i)
    || navigator.userAgent.match(/webOS/i)
    || navigator.userAgent.match(/iPhone/i)
    || navigator.userAgent.match(/iPad/i)
    || navigator.userAgent.match(/iPod/i)
    || navigator.userAgent.match(/BlackBerry/i)
    || navigator.userAgent.match(/Windows Phone/i)) {							
      detectmob = true;
  }
  
  if (detectmob === true) {
    $( '.parallax' ).each(function(){
  			$(this).addClass('parallax-mobile');
  	});
  }
  else {
      $( '#parallax-one' ).parallax();
      $( '#parallax-two' ).parallax();
      $( '#parallax-three' ).parallax();
      $( '#parallax-four' ).parallax();
      $( '#parallax-five' ).parallax();
  }  

/* --------------------------------------------------------	
	 Flex Initialize
   --------------------------------------------------------	*/	

  $(window).load(function() {
  
    $('.flex-1').flexslider({
      animation: "slide",
      slideshow: false,
      useCSS : false,
      animationLoop: true 	
    });
   
    jQuery('.flex-1 .flex-direction-nav .flex-next').html('<i class="fa fa-angle-right"></i>');
    jQuery('.flex-1 .flex-direction-nav .flex-prev').html('<i class="fa fa-angle-left"></i>'); 
   
    $('.flex-2').flexslider({
      animationLoop: false,
  		animation: 'slide',
      useCSS : false
  	 });     
  
    $('.flex-3').flexslider({
      animation: "slide",
      slideshow: false,
      useCSS : false,
      animationLoop: false 	
    });
   
    jQuery('.flex-3 .flex-direction-nav .flex-next').html('<i class="fa fa-angle-right"></i>');
    jQuery('.flex-3 .flex-direction-nav .flex-prev').html('<i class="fa fa-angle-left"></i>'); 
  
    $('.flex-4').flexslider({
      animationLoop: false,
  		animation: 'slide',
      slideshow: false,
      useCSS : false
  	 }); 
  
  }); 

/* --------------------------------------------------------	
	 Fitvids
   --------------------------------------------------------	*/	

  $(window).load(function() {
    $("body").fitVids();
  });

/* --------------------------------------------------------	
	 Color Picker New
   --------------------------------------------------------	*/

    $(function() {
      var $div = $('<div />').appendTo('body');
      $div.attr('id', 'cp');
    });  

    $(function() {
    
      $("#cp").load("picker.html",function(){
    		$(this).clone().appendTo("body").remove();

        $('.color-picker').animate({right: '-190px'});
          		
        $('.color-picker a.handle').click(function(e){
        	e.preventDefault();
        	var div = $('.color-picker');
        	if (div.css('right') === '-190px') {
        		$('.color-picker').animate({right: '0px'}); 
        	} 
          else {
            $('.color-picker').animate({right: '-190px'});
        	}

          $(".blue" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles.css" );
          	return false;
          });
        
          $(".blue-2" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-blue-2.css" );
          	return false;
          });
    
          $(".blue-3" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-blue-3.css" );
          	return false;
          });
              
          $(".blue-4" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-blue-4.css" );
          	return false;
          });
              
          $(".green" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-green.css" );
          	return false;
          });
              
          $(".green-2" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-green-2.css" );
          	return false;
          });
              
          $(".green-3" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-green-3.css" );
          	return false;
          });
              
          $(".green-4" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-green-4.css" );
          	return false;
          });
              
          $(".yellow" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-yellow.css" );
          	return false;
          });
              
          $(".yellow-2" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-yellow-2.css" );
          	return false;
          });
              
          $(".orange" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-orange.css" );
          	return false;
          });
              
          $(".orange-2" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-orange-2.css" );
          	return false;
          });
              
          $(".red" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-red.css" );
          	return false;
          });
              
          $(".red-2" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-red-2.css" );
          	return false;
          });
              
          $(".violet" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-violet.css" );
          	return false;
          });
              
          $(".purple" ).click(function(){
          	$("#color-style" ).attr("href", "css/styles-purple.css" );
          	return false;
          });

      	});   
        
    	});   
           
    });   

/* --------------------------------------------------------	
	 LayerSlider
   --------------------------------------------------------	*/

  // Running the code when the document is ready
  $(document).ready(function(){
  
    // Calling LayerSlider on the target element
    $('#layerslider').layerSlider({

            firstLayer: 1,
            skin: 'v5',
            skinsPath: 'layerslider/skins/'

    });
  });


});
