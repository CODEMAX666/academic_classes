/*
Author       : Dreamguys
Template Name: Mentoring - Bootstrap Template
Version      : 1.0
*/

(function($) {
    "use strict";
		
	if($('.toggle-password').length > 0) {
		$(document).on('click', '.toggle-password', function() {
			$(this).toggleClass("fa-eye fa-eye-slash");
			var input = $(".pass-input");
			if (input.attr("type") == "password") {
				input.attr("type", "text");
			} else {
				input.attr("type", "password");
			}
		});
	}


	// Stick Sidebar
	
	if ($(window).width() > 767) {
		if($('.theiaStickySidebar').length > 0) {
			$('.theiaStickySidebar').theiaStickySidebar({
			  // Settings
			  additionalMarginTop: 30
			});
		}
	}
	
	// Sidebar
	
	if($(window).width() <= 991){
	var Sidemenu = function() {
		this.$menuItem = $('.main-nav a');
	};
	
	function init() {
		var $this = Sidemenu;
		$('.main-nav a').on('click', function(e) {
			if($(this).parent().hasClass('has-submenu')) {
				e.preventDefault();
			}
			if(!$(this).hasClass('submenu')) {
				$('ul', $(this).parents('ul:first')).slideUp(350);
				$('a', $(this).parents('ul:first')).removeClass('submenu');
				$(this).next('ul').slideDown(350);
				$(this).addClass('submenu');
			} else if($(this).hasClass('submenu')) {
				$(this).removeClass('submenu');
				$(this).next('ul').slideUp(350);
			}
		});
	}

	// Sidebar Initiate
	init();
	}
	
	// Select 2
	
	if($('.select').length > 0) {
		$('.select').select2({
			minimumResultsForSearch: -1,
			width: '100%'
		});
	}
	
	// Date Time Picker
	
	if($('.datetimepicker').length > 0) {
		$('.datetimepicker').datetimepicker({
			format: 'DD/MM/YYYY',
			icons: {
				up: "fas fa-chevron-up",
				down: "fas fa-chevron-down",
				next: 'fas fa-chevron-right',
				previous: 'fas fa-chevron-left'
			}
		});
	}
	
	// Floating Label

	if($('.floating').length > 0 ){
		$('.floating').on('focus blur', function (e) {
		$(this).parents('.form-focus').toggleClass('focused', (e.type === 'focus' || this.value.length > 0));
		}).trigger('blur');
	}
	
	// Mobile menu sidebar overlay
	
	$('.header-fixed').append('<div class="sidebar-overlay"></div>');
	$(document).on('click', '#mobile_btn', function() {
		$('main-wrapper').toggleClass('slide-nav');
		$('.sidebar-overlay').toggleClass('opened');
		$('html').addClass('menu-opened');
		return false;
	});
	
	$(document).on('click', '.sidebar-overlay', function() {
		$('html').removeClass('menu-opened');
		$(this).removeClass('opened');
		$('main-wrapper').removeClass('slide-nav');
	});
	
	$(document).on('click', '#menu_close', function() {
		$('html').removeClass('menu-opened');
		$('.sidebar-overlay').removeClass('opened');
		$('main-wrapper').removeClass('slide-nav');
	});
	$(document).on('click', '.notification-link', function() {
		$('.notification-wrapper').addClass('open');
		$('.sidebar-overlay').addClass('opened');
		$('html').addClass('noti-opened');
	});
	
	$(document).on('click', '#notif_close,.sidebar-overlay', function() {
		$('html').removeClass('noti-opened');
		$('.sidebar-overlay').removeClass('opened');
	});
	$('#toggle_btn').on('click',function(){
	    $('main').toggleClass('collapse-sidebar');
	});
	// Tooltip
	
	if($('[data-toggle="tooltip"]').length > 0 ){
		$('[data-toggle="tooltip"]').tooltip();
	}

	//Home popular mentor slider

    //Home popular mentor slider
    if ($('.owl-carousel').length > 0) {
        var owl = $('.owl-theme');
        owl.owlCarousel({
            loop: true,
            center: true,
            items: 3,
            margin: 0,
            autoplay: true,
            dots:true,
            autoplayTimeout: 8500,
            smartSpeed: 450,
            responsive: {
              0: {
                items: 1
              },
              768: {
                items: 2
              },
              1170: {
                items: 3
              }
            }
        });
        var owl2 = $('.owl-theme2');
        owl2.owlCarousel({
            loop:true,
            margin:10,
            items: 1,
            autoplayHoverPause:true,
            dots:false,
            slideSpeed: 6000,
            nav:false,
            mouseDrag:false,
            autoplay:true,
            animateOut: 'slideOutUp'
        });
    }

	// Add More Hours
  

    $(".add-phone-number").on('click', function () {
		
		var phonecontent = '<div class="col-12 col-md-6"><div class="form-group">' +
							'<label>Mobile</label>' +
							'<input type="text" value="+1 202-555-0125" class="form-control">' +
						'</div></div>';

			$(".mobile-numbers").append(phonecontent);
        return false;
    });
    
    $(".add-hours").on('click', function () {
        if ($total != 0) {
            var $ddid = "starttime" + $forid;
            var hourscontent = '<div class="row form-row hours-cont">' +
                '<div class="col-12 col-md-12">' +
                '<div class="row form-row">' +
                '<div class="col-12 col-md-4">' +
                '<div class="form-group">' +
                '<label>Start Time</label>' +
                '<select class="form-control" id=$ddid > ' +
                '<option>Select</option>' +
                '<option>12.00 am</option>' +
                '<option>1.00 am</option>' +
                '<option>2.00 am</option>' +
                '<option>3.00 am</option>' +
                '<option>4.00 am</option>' +
                '<option>5.00 am</option>' +
                '<option>6.00 am</option>' +
                '<option>7.00 am</option>' +
                '<option>8.00 am</option>' +
                '<option>9.00 am</option>' +
                '<option>10.00 am</option>' +
                '<option>11.00 am</option>' +
                '<option>1.00 pm</option>' +
                '<option>2.00 pm</option>' +
                '<option>3.00 pm</option>' +
                '<option>4.00 pm</option>' +
                '<option>5.00 pm</option>' +
                '<option>6.00 pm</option>' +
                '<option>7.00 pm</option>' +
                '<option>8.00 pm</option>' +
                '<option>9.00 pm</option>' +
                '<option>10.00 pm</option>' +
                '<option>11.00 pm</option>' +
                '</select>' +
                '</div>' +
                '</div>' +
                '<div class="col-12 col-md-4">' +
                '<div class="form-group">' +
                '<label>End Time</label>' +
                '<select class="form-control">' +
                '<option>Select</option>' +
                '<option>12.00 am</option>' +
                '<option>1.00 am</option>' +
                '<option>2.00 am</option>' +
                '<option>3.00 am</option>' +
                '<option>4.00 am</option>' +
                '<option>5.00 am</option>' +
                '<option>6.00 am</option>' +
                '<option>7.00 am</option>' +
                '<option>8.00 am</option>' +
                '<option>9.00 am</option>' +
                '<option>10.00 am</option>' +
                '<option>11.00 am</option>' +
                '<option>1.00 pm</option>' +
                '<option>2.00 pm</option>' +
                '<option>3.00 pm</option>' +
                '<option>4.00 pm</option>' +
                '<option>5.00 pm</option>' +
                '<option>6.00 pm</option>' +
                '<option>7.00 pm</option>' +
                '<option>8.00 pm</option>' +
                '<option>9.00 pm</option>' +
                '<option>10.00 pm</option>' +
                '<option>11.00 pm</option>' +
                '</select>' +
                '</div>' +
                '</div>' +
                ' <div class="col-12 col-md-3">' +
                '<div class="form-group">' +
                '<label>Duration</label>' +
                '<select class="select form-control" id="slotduration">' +
                '   <option selected value="30">30 mins</option>' +
                '  <option value="60">1 Hour</option>' +
                ' <option value="90">1 Hour 30 Mins</option>' +
                '<option value="120">2 Hours</option>' +
                '</select>' +
                ' </div>' +
                '</div>' +

                '<div class="col-12 col-md-1">' +
                '<div class="form-group" >' +
                '<label class="d-md-block d-sm-none d-none">&nbsp;</label> <a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a>' +
                '</div >' +
                '</div > ' +

                '</div>' +
                '</div>' +
                '</div>';
          /*  $total--;
            $forid++;
            if ($total == 0) {
                $('#addslot').hide();
            }
            if ($total != 0) {
                $('#addslot').show();
            }*/
            $(".hours-info").append(hourscontent);
            return false;
        }
       
    });
    $(".add-experience").on('click', function () {
        var lastRowId = $('.Experience:last').attr('id');
        var index = lastRowId.indexOf("_");
        var text = lastRowId.substr(index + 1);
        var divcount = parseInt(text) + 1;
        var experienceInfo = '<div style="border-bottom:inset" class="row form-row Experience" id="Exp_' + divcount + '">' +
            '<div class="col-12 col-md-4">' +
            '<div class="form-group">' +
            '<label>Category</label>' +
            '<select  class="form-control" placeholder="Category" validation = "validateDDL"id="Category' + divcount + '">' +
            //'<option value="wd">Web Development</option>'+
            //                  '<option value="bd">Business Development</option>'+
            //                  '<option value="ne">Network Engineerg</option>'+
            //                  '<option value="sqa">Software Quality Asurance</option>'+
            '</select>' +
            '</div>' +
            '</div>' +
            '<div class="col-12 col-md-4">' +
            '<div class="form-group">' +
            '<label>SubCategory</label>' +
            '<select  class="form-control" placeholder="SubCategory" validation = "validateMultiDDL" id="SubCategory' + divcount + '"multiple>' +
            //'<option value="ap">Adobe Photoshop</option>'+
            //                  '<option value="nb">Netbeans</option>'+
            //                  '<option value="vs">Visual Studio</option>'+
            //                  '<option value="st">Sublime Text 3</option>'+
            '</select>' +
            '</div>' +
            '</div>' +
            //'<div class="col-12 col-md-4">'+
            //	'<div class="form-group">'+
            //		'<label>Category Tool</label>'+
            //		'<select  class="form-control">'+
            //			'<option value="wd">Asp.net</option>'+
            //                     '<option value="bd">Java</option>'+
            //                     '<option value="ne">React</option>'+
            //                     '<option value="sqa">Node Js</option>'+
            //		'</select>'+
            //	'</div>'+
            //'</div>'+
            '<div class="col-12 col-md-3">' +
            '<div class="form-group">' +
            '<label>Designation</label>' +
            '<input type="text" class="form-control" validation = "validate" id= "Designation' + divcount + '" >' +
            '</div>' +
            '</div>' +
            '<div class="col-12 col-md-3">' +
            '<div class="form-group">' +
            '<label>Company</label>' +
            '<input type="text" class="form-control" validation = "validate" id="Company' + divcount + '" >' +
            '</div>' +
            '</div>' +
            '<div class="col-12 col-md-3">' +
            '<div class="form-group">' +
            '<label>Years From</label>' +
            //'<div class="cal-icon">' +
            '<input type="text" class="form-control datetimepicker" validation = "validateYear" id = "YearFrom' + divcount + '" >' +
            //'</div>'+
            '</div>' +
            '</div>' +
            '<div class="col-12 col-md-3">' +
            '<div class="form-group">' +
            '<label>Year To</label>' +
            //'<div class="cal-icon">'+
            '<input type="text" class="form-control datetimepicker" validation = "validateYear" id = "YearTo' + divcount + '">' +
            //'</div>'+
            '</div>' +
            '</div>' +
            '<div class="col-12 col-md-3">' +
            '<div class="form-group">' +
            '<button type="submit" class="btn btn-primary" id="saveExp' + divcount + '">Save</button>' +
            '<button href="#" style="float:right" class="btn btn-danger trash " id="deleteExp' + divcount + '" ><i class="fas fa-trash-alt"></i></button>' +
            '</div>' +
            '</div>' +
            '</div>';
        PopulateCategoryDropdown(divcount);
        //PopulateSubCategoryDropdown(divcount);
        $(".experience-info").append(experienceInfo);
        $("#Category" + divcount).change(function () {
            GetCategoryId(this, divcount);
        });
        $("#saveExp" + divcount).click(function () {
            return SubmitExperience(divcount);
        })
        $("#deleteExp" + divcount).click(function () {
            //return DeleteExp(divcount);
            $("#Exp_" + divcount).remove();
        });
        return false;
    });

    $(".add-interest").on('click', function () {
        var lastRowId = $('.interest-table tbody tr:last').attr('id');
        var index = lastRowId.indexOf("_");
        var text = lastRowId.substr(index + 1);
        var rowsCount = parseInt(text) + 1;
        var interestInfo = '<tr class="interestRow" id ="interestRowID_' + rowsCount + '">' +
            '<td class="align-middle">' +
            '<select  class="form-control" validation = "validateDDL" id="Category' + rowsCount + '">' +
            '</select>' +
            '</td>' +
            '<td class="align-middle">' +
            '<select  class="form-control" validation = "validateDDL" id="SubCategory' + rowsCount + '">' +
            '</select>' +
            '</td>' +
            '<td class="align-middle"><button href="#" class="btn btn-danger trash " id="deleteRow' + rowsCount + '"><i class="fas fa-trash-alt"></i></button></td>' +
            '</tr>';

        PopulateCategoryDropdown(rowsCount);
        $(".interest-table tbody").append(interestInfo);
        $("#Category" + rowsCount).change(function () {
            GetCategoryId(this, rowsCount);
        });
        $("#deleteRow" + rowsCount).click(function () {
            $("#interestRowID_" + rowsCount).remove();
        });
        return false;
    });


    $(".experience-info").on('click', '.trash', function () {
        $(this).closest('.experience-row').remove();
        return false;
    });
    $(".experience-info").on('click', '.trash', function () {
        $(this).closest('.experience-row').remove();
        return false;
    });
    $(".add-education").on('click', function () {

        var lastRowId = $('.education-table tbody tr:last').attr('id');
        var index = lastRowId.indexOf("_");
        var text = lastRowId.substr(index + 1);
        var rowsCount = parseInt(text) + 1;
        
        var educationRow = '<tr class="eduRow" id ="eduRowID_' + rowsCount + '">' +
            '<td class="align-middle">' +
            '<select name="degree_name" validation="validateDDL" class="form-control" id="DegreeLevelName' + rowsCount + '">' +
            
            '</td>' +
            '<td class="align-middle">' +
            '<select name="degree_name"  class="form-control" validation="validateDDL" id="DegreeTitleName' + rowsCount + '">' +
            '<option value="0">Degree Name</option>' +
            '</select>' +
            '</td>' +
            '<td class="align-middle">' +
            '<input class="form-control" name="percentage" type="text" validation = "validatePercentage" id="percentage' + rowsCount + '">' +
            '</td>' +
            '<td class="align-middle">' +
            '<input class="form-control" name="year" type="text" validation = "validateYear" id="yearFrom' +
            rowsCount + '">' +
            '</td>' +
            '<td class="align-middle">' +
            '<input class="form-control" name="year" type="text" validation = "validateYear" id="yearTo' +
            rowsCount + '">' +
            '</td>' +
            '<td class="align-middle">' +
            '<input class="form-control" name="institute" type="text" validation = "validate" id="institute' + rowsCount + '">' +
            '</td>' +
            '<td class="align-middle"><button href="#" class="btn btn-danger trash " id="deleteRow' + rowsCount + '"><i class="fas fa-trash-alt"></i></button></td>' +
            '</tr>';
        PopulateDegreeNameDropdown(rowsCount);
        //PopulateDegreeTitleDropdown(rowsCount);
        //DeleteRow(rowsCount);

        $(".education-table tbody").append(educationRow);
        $("#DegreeLevelName" + rowsCount).change(function () {
            GetDegreeId(this, rowsCount);
        });
        $("#deleteRow" + rowsCount).click(function () {
            $("#eduRowID_" + rowsCount).remove();
        });
        return false;
    });
	// Content div min height set
	$(".education-table tbody").on('click','.trash', function () {
		$(this).closest('.apped-tr').remove();
		return false;
    });
	function resizeInnerDiv() {
		var height = $(window).height();	
		var header_height = $(".header").height();
		var footer_height = $(".footer").height();
		var setheight = height - header_height;
		var trueheight = setheight - footer_height;
		$(".content").css("min-height", trueheight);
	}
	
	if($('.content').length > 0 ){
		resizeInnerDiv();
	}

	$(window).resize(function(){
		if($('.content').length > 0 ){
			resizeInnerDiv();
		}

	});
	
	// Date Range Picker
	if($('.bookingrange').length > 0) {
		var start = moment().subtract(6, 'days');
		var end = moment();

		function booking_range(start, end) {
			$('.bookingrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
		}

		$('.bookingrange').daterangepicker({
			startDate: start,
			endDate: end,
			ranges: {
				'Today': [moment(), moment()],
				'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
				'Last 7 Days': [moment().subtract(6, 'days'), moment()],
				'Last 30 Days': [moment().subtract(29, 'days'), moment()],
				'This Month': [moment().startOf('month'), moment().endOf('month')],
				'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
			}
		}, booking_range);

		booking_range(start, end);
	}
	// Chat

	var chatAppTarget = $('.chat-window');
	(function() {
		if ($(window).width() > 991)
			chatAppTarget.removeClass('chat-slide');
		
		$(document).on("click",".chat-window .chat-users-list a.media",function () {
			if ($(window).width() <= 991) {
				chatAppTarget.addClass('chat-slide');
			}
			return false;
		});
		$(document).on("click","#back_user_list",function () {
			if ($(window).width() <= 991) {
				chatAppTarget.removeClass('chat-slide');
			}	
			return false;
		});
	})();
	
	// Preloader
	
	$(window).on('load', function () {
		if($('#loader').length > 0) {
			$('#loader').delay(350).fadeOut('slow');
			$('body').delay(350).css({ 'overflow': 'visible' });
		}
	})
	var APP = {
		init: function () {
			this.processingStepAddProperty();
		},
		processingStepAddProperty: function () {
			var $step = $('.new-property-step');
			if ($step.length < 1) {
				return;
			}
			var $active_item = $step.find('.nav-link.active').parent();
			var $prev_item = $active_item.prevAll();
			if ($prev_item.length > 0) {
				$prev_item.each(function () {
					$(this).find('.step').html('<i class="fas fa-check"></i>');
				});
			}
			var $tabs = $('a[data-toggle="pill"],a[data-toggle="tab"]');
			$tabs.on('show.bs.tab', function (e) {
				$(this).find('.number').html($(this).data('number'));
				var $prev_item = $(this).parent().prevAll();
				if ($prev_item.length > 0) {
					$prev_item.each(function () {
						$(this).find('.number').html('<i class="fas fa-check"></i>');
					});
				}
				var $next_item = $(this).parent().nextAll();
				if ($next_item.length > 0) {
					$next_item.each(function () {
						var number = $(this).find('.nav-link').data('number');
						$(this).find('.number').html(number);
					});
				}
			});
			$('.prev-button').on('click', function (e) {
				e.preventDefault();
				debugger;
				var $parent = $(this).parents('.tab-pane');
				$parent.removeClass('show active');
				$parent.prev().addClass('show active');
				$parent.find('.collapsible').removeClass('show');
				$parent.prev().find('.collapsible').addClass('show');
				var id = $parent.attr('id');
				var $nav_link = $('a[href="#' + id + '"]');
				$nav_link.removeClass('active');
				$nav_link.find('.number').html($nav_link.data('number'));
				var $prev = $nav_link.parent().prev();
				$prev.find('.nav-link').addClass('active');
				var number = $parent.find('.collapse-parent').data('number');
				$parent.find('.number').html(number);
			});
			$('.next-button').on('click', function (e) {
				e.preventDefault();
				//debugger;
				var $parent = $(this).parents('.tab-pane');
				$parent.removeClass('show active');
				$parent.next().addClass('show active');
				$parent.find('.collapsible').removeClass('show');
				$parent.next().find('.collapsible').addClass('show');
				var id = $parent.attr('id');
				var $nav_link = $('a[href="#' + id + '"]');
				$nav_link.removeClass('active');
				$nav_link.find('.number').html($nav_link.data('number'));
				var $prev = $nav_link.parent().next();
				$prev.find('.nav-link').addClass('active');
				$nav_link.find('.number').html('<i class="fas fa-check"></i>');
				$parent.find('.number').html('<i class="fas fa-check"></i>');
			});
			$step.find('.collapsible').on('show.bs.collapse', function () {
				//debugger
				$(this).find('.number').html($(this).data('number'));
				var $parent = $(this).parents('.tab-pane');
				var $prev_item = $parent.prevAll();
				if ($prev_item.length > 0) {
					$prev_item.each(function () {
						$(this).find('.number').html('<i class="fas fa-check"></i>');
					});
				}
				var $next_item = $parent.nextAll();
				if ($next_item.length > 0) {
					$next_item.each(function () {
						var number = $(this).find('.collapse-parent').data('number');
						$(this).find('.number').html(number);
					});
				}
			});
		}
	};

	APP.CollapseTabsAccordion = {
		init: function () {
			this.CollapseSetUp();
		},
		CollapseSetUp: function () {
			var $tabs = $('.collapse-tabs');
			$tabs.find('.tab-pane.active .collapse-parent').attr('data-toggle', 'false');
			$tabs.find('.nav-link').on('show.bs.tab', function (e) {
				if (!$(this).hasClass('nested-nav-link')) {
					var $this_tab = $(this).parents('.collapse-tabs');
					var $tabpane = $($(this).attr('href'));
					$this_tab.find('.collapsible').removeClass('show');
					$this_tab.find('collapse-parent').addClass('collapsed');
					$this_tab.find('collapse-parent').attr('data-toggle', 'collapse');
					$tabpane.find('.collapse-parent').removeClass('collapsed');
					$tabpane.find('.collapse-parent').attr('data-toggle', 'false');
					$tabpane.find('.collapsible').addClass('show');
				}
			});
			$tabs.find('.collapsible').on('show.bs.collapse', function () {
				var $this_tab = $(this).parents('.collapse-tabs'),
					$parent = $(this).parents('.tab-pane.tab-pane-parent'),
					$id = $parent.attr('id'),
					$navItem = $this_tab.find('.nav-link'),
					$navItemClass = 'active';
				$this_tab.find('.collapse-parent').attr('data-toggle', 'collapse');
				$parent.find('.collapse-parent').attr('data-toggle', 'false');
				var $tab_pane = $this_tab.find('.tab-pane');
				if (!$tab_pane.hasClass('nested-tab-pane')) {
					$this_tab.find('.tab-pane').removeClass('show active');
				}
				$parent.addClass('show active');
				var $nav_link = $parent.parents('.collapse-tabs').find('.nav-link');
				if (!$nav_link.hasClass('nested-nav-link')) {
					$nav_link.removeClass('active');
				}
				$navItem.each(function () {
					if (!$(this).hasClass('nested-nav-link')) {
						$(this).removeClass('active');
						if ($(this).attr('href') === '#' + $id) {
							$(this).addClass($navItemClass);
						}
					}
				});
			});
		}
	};

	$(document).ready(function () {
		APP.init();
		APP.CollapseTabsAccordion.init();
	});
	var $offCanvasNav = $('.custom-sidebar-nav'),
        $offCanvasNavSubMenu = $offCanvasNav.find('.dropdown');
    
    /*Add Toggle Button With Off Canvas Sub Menu*/
    $offCanvasNavSubMenu.parent().prepend('<span class="menu-expand"><i class="fa fa-chevron-right"></i></span>');
    
    /*Close Off Canvas Sub Menu*/
    $offCanvasNavSubMenu.slideUp();
    
    /*Category Sub Menu Toggle*/
    $offCanvasNav.on('click', 'li a, li .menu-expand', function(e) {
        var $this = $(this);
        if ( ($this.parent().attr('class').match(/\b(menu-item-has-children|has-children|has-sub-menu)\b/)) && ($this.attr('href') === '#' || $this.hasClass('menu-expand')) ) {
            e.preventDefault();
            if ($this.siblings('ul:visible').length){
                $this.parent('li').removeClass('active');
                $this.siblings('ul').slideUp();
            } else {
                $this.parent('li').addClass('active');
                $this.closest('li').siblings('li').removeClass('active').find('li').removeClass('active');
                $this.closest('li').siblings('li').find('ul:visible').slideUp();
                $this.siblings('ul').slideDown();
            }
        }
    });
})(jQuery);