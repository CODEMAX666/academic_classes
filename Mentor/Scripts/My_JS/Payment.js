
    $(document).ready(function() {
        $(".radio input:radio").click(function () {
            
            var Pay_name;

            var radioValue = $("input[name='paymentType']:checked").val();
            $(".paymentDetail").css('display', 'none');

            if (radioValue == 1) { Pay_name = "Debit/Credit Card Detail"; }
            else if (radioValue == 2) { Pay_name = "Easy Pay Detail"; }
            else if (radioValue == 3) { Pay_name = "MCB Detail"; }
            else if (radioValue == 4) { Pay_name = "SIMSIM Detail"; }



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
                    $(".paymentDetail").css('display', 'none');
                    $("#visaCard").css('display', 'block');
                    $("#Payment_Method").html(Pay_name);
                    if (radioValue == 2) {
                        $('#EasyDet').css('display', 'block')
                    }
                    else {
                        $('#EasyDet').css('display', 'none')
                    }
                    if (radioValue == 3) {
                        $('#McbDet').css('display', 'block')
                    }
                        else {
                            $('#McbDet').css('display', 'none');
                        }
                    
                });
            

            $("#Payment_Submit").click(function () {
                var radioValue = $("input[name='paymentType']:checked").val();
                var tranInv = $("#Transaction").val();
                var Amount = $("#Amount").val();
                
                debugger
                $.ajax('/Payment/AddedPayment', {
                    type: 'POST',  // http method
                    data: { memberID: "19", PayM: radioValue, Tran: tranInv, amount: Amount},  // data to submit
                    success: function (data) {
                        alert(data);
                    },
                    error: function (data) {
                        alert(data);
                    }
                });
            });
           
                
            });
        });

  