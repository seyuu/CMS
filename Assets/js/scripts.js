(function ($) {
	"use strict";

	// Page Loaded...
	$(document).ready(function () {

		/*==========  Responsive Navigation  ==========*/
		$('.main-nav').children().clone().appendTo('.responsive-nav');
		$('.responsive-menu-open').on('click', function(event) {
			event.preventDefault();
			$('body').addClass('no-scroll');
			$('.responsive-menu').addClass('open');
			return false;
		});
		$('.responsive-menu-close').on('click', function(event) {
			event.preventDefault();
			$('body').removeClass('no-scroll');
			$('.responsive-menu').removeClass('open');
			return false;
		});

		/*==========  Clients Slider  ==========*/
		$('.clients-slider').owlCarousel({
			loop: true,
			autoplay: true,
			margin: 18,
			nav: true,
			dots: false,
			navText: ['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
			responsive: {
				0: {
					items: 1
				},
				768: {
					items: 4
				}
			}
		});

		/*==========  Portfolio  ==========*/
		var $portfolioContainer = $('#portfolio').imagesLoaded(function() {
			$portfolioContainer.isotope({
				itemSelector: '.item',
				layoutMode: 'masonry',
				percentPosition: true,
				masonry: {
					columnWidth: $portfolioContainer.find('.portfolio-sizer')[0]
				}
			});
			return false;
		});
		$('#portfolio-filters').on('click', 'button', function() {
			$('#portfolio-filters button').removeClass('active');
			$(this).addClass('active');
			var filterValue = $(this).attr('data-filter');
			$portfolioContainer.isotope({filter: filterValue});
		});

		/*==========  Blog Masonry  ==========*/
		var $blogContainer = $('#blog-masonry').imagesLoaded(function() {
			$blogContainer.isotope({
				itemSelector: '.blog-post',
				layoutMode: 'masonry',
				percentPosition: true,
				masonry: {
					columnWidth: $blogContainer.find('.blog-post-sizer')[0]
				}
			});
			return false;
		});

		/*==========  Welcome Slider  ==========*/
		$('#welcome-slider').flexslider({
			selector: '.slides > .slide',
			controlNav: false,
			pauseOnHover: false
		});

		/*==========  Team Slider  ==========*/
		$('#team-slider').flexslider({
			selector: '.slides > .slide',
			controlNav: 'thumbnails'
		});

		/*==========  Testimonial Slider  ==========*/
		$('.testimonial-slider').owlCarousel({
			loop: true,
			autoplay: true,
			items: 1,
			nav: true,
			dots: false,
			navText: ['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>']
		});

		/*==========  Counter  ==========*/
		$('.counter').counterUp();

		/*==========  Video Popup  ==========*/
		$('.video-popup').nivoLightbox();
        $('.nivo-popup').nivoLightbox({
            effect: 'fade',                               // The effect to use when showing the lightbox 
            theme: 'default',                             // The lightbox theme to use 
            keyboardNav: true,                            // Enable/Disable keyboard navigation (left/right/escape) 
            clickOverlayToClose: true,                    // If false clicking the "close" button will be the only way to close the lightbox 
            errorMessage: 'The requested content cannot be loaded. Please try again later.' // Error message when content can't be loaded 
        });

		/*==========  Blog Post Slider  ==========*/
		$('.blog-post-slider').owlCarousel({
			loop: true,
			autoplay: true,
			items: 1,
			nav: true,
			dots: false,
			navText: ['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>']
		});

		/*==========  Popular and Latest Post Slider  ==========*/
		$('.popular-latest-posts-slider').flexslider({
			selector: '.slides > .post',
			directionNav: false,
		});

	});
	
	/*==========  Validate Email  ==========*/
	function validateEmail($validate_email) {
		var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
		if( !emailReg.test( $validate_email ) ) {
			return false;
		} else {
			return true;
		}
		return false;
	}
	
	/*==========  Contact Form  ==========*/
	$('#contact-form').on('submit', function() {
		$('#contact-error').fadeOut();
		$('#contact-success').fadeOut();
		$('#contact-loading').fadeOut();
		$('#contact-loading').fadeIn();
		if (validateEmail($('#contact-email').val()) && $('#contact-email').val().length !== 0 && $('#contact-name').val().length !== 0 && $('#contact-message').val().length !== 0) {
			var action = $(this).attr('action');
			$.ajax({
				type: "POST",
				url : action,
				data: {
					contact_name: $('#contact-name').val(),
					contact_email: $('#contact-email').val(),
					contact_phone: $('#contact-phone').val(),
					contact_message: $('#contact-message').val()
				},
				success: function() {
					$('#contact-error').fadeOut();
					$('#contact-success').fadeOut();
					$('#contact-loading').fadeOut();
					$('#contact-success .message').html('Success! Thanks for contacting us!');
					$('#contact-success').fadeIn();
				},
				error: function() {
					$('#contact-error').fadeOut();
					$('#contact-success').fadeOut();
					$('#contact-loading').fadeOut();
					$('#contact-error .message').html('Sorry, an error occurred.');
					$('#contact-error').fadeIn();
				}
			});
		} else if (!validateEmail($('#contact-email').val()) && $('#contact-email').val().length !== 0 && $('#contact-name').val().length !== 0 && $('#contact-message').val().length !== 0) {
			$('#contact-error').fadeOut();
			$('#contact-success').fadeOut();
			$('#contact-loading').fadeOut();
			$('#contact-error .message').html('Uygun bir E-Posta Giriniz.');
			$('#contact-error').fadeIn();
		} else {
			$('#contact-error').fadeOut();
			$('#contact-success').fadeOut();
			$('#contact-loading').fadeOut();
			$('#contact-error .message').html('Boþ Geçilemez');
			$('#contact-error').fadeIn();
		}
		return false;
	});

	/*==========  Newsletter Form  ==========*/
	var $form = $('#mc-embedded-subscribe-form');
	$form.submit(function() {
		$('#newsletter-error').fadeOut();
		$('#newsletter-success').fadeOut();
		$('#newsletter-loading').fadeOut();
		$('#newsletter-info').fadeOut();
		$('#newsletter-loading').fadeIn();
		if (validateEmail($('#mce-EMAIL').val()) && $('#mce-EMAIL').val().length !== 0) {
			$.ajax({
				type: $form.attr('method'),
				url: $form.attr('action'),
				data: $form.serialize(),
				cache: false,
				dataType: 'json',
				contentType: 'application/json; charset=utf-8',
				error: function(err) {
					$('#newsletter-error').fadeOut();
					$('#newsletter-success').fadeOut();
					$('#newsletter-loading').fadeOut();
					$('#newsletter-info').fadeOut();
					$('#newsletter-error .message').html(err.msg);
					$('#newsletter-error').fadeIn();
				},
				success: function(data) {
					if (data.result !== 'success') {
						$('#newsletter-error').fadeOut();
						$('#newsletter-success').fadeOut();
						$('#newsletter-loading').fadeOut();
						$('#newsletter-info').fadeOut();
						$('#newsletter-info .message').html(data.msg);
						$('#newsletter-info').fadeIn();
					} else {
						$('#newsletter-error').fadeOut();
						$('#newsletter-success').fadeOut();
						$('#newsletter-loading').fadeOut();
						$('#newsletter-info').fadeOut();
						$('#newsletter-success .message').html(data.msg);
						$('#newsletter-success').fadeIn();
					}
				}
			});
		} else {
			$('#newsletter-error').fadeOut();
			$('#newsletter-success').fadeOut();
			$('#newsletter-loading').fadeOut();
			$('#newsletter-info').fadeOut();
			$('#newsletter-error .message').html('Please enter a valid email.');
			$('#newsletter-error').fadeIn();
		}
		return false;
	});

	/*==========  Map  ==========*/
	var full_width_map;
	function initialize_full_width_map() {
		if ($('.full-width-map').length) {
			var myLatLng = new google.maps.LatLng(-37.814199, 144.961560);
			var mapOptions = {
				zoom: 15,
				center: myLatLng,
				scrollwheel: false,
				panControl: false,
				zoomControl: false,
				scaleControl: false,
				mapTypeControl: false,
				streetViewControl: false
			};
			full_width_map = new google.maps.Map(document.getElementById('full-width-map'), mapOptions);
			var marker = new google.maps.Marker({
				position: myLatLng,
				map: full_width_map,
				title: 'Envato',
				icon: './images/marker.png'
			});
		} else {
			return false;
		}
		return false;
	}
	google.maps.event.addDomListener(window, 'load', initialize_full_width_map);

	/*==========  Portfolio Map  ==========*/
	var portfolio_map;
	function initialize_portfolio_map() {
		if ($('.portfolio-map').length) {
			var myLatLng = new google.maps.LatLng(-37.814199, 144.961560);
			var mapOptions = {
				zoom: 15,
				center: myLatLng,
				scrollwheel: false,
				panControl: false,
				zoomControl: false,
				scaleControl: false,
				mapTypeControl: false,
				streetViewControl: false
			};
			portfolio_map = new google.maps.Map(document.getElementById('portfolio-map'), mapOptions);
			var marker = new google.maps.Marker({
				position: myLatLng,
				map: portfolio_map,
			});
		} else {
			return false;
		}
		return false;
	}
	google.maps.event.addDomListener(window, 'load', initialize_portfolio_map);

})(jQuery);