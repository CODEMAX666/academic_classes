
var $count = 0, $remaing = 0, $startindex = 0;
$(document).ready(function () {
    //    var interval;
    //    function startTicker(){
    //    $("#ticker04 li:first").slideUp(function () {
    //        $(this).appendTo($("#ticker04")).slideDown();
    //        });
    //    }

    //   setInterval(startTicker, 1000);
    //Training DropdownList
    $.ajax({

        url: "/Home/GetTrainingTitleList",
        type: 'POST',
        dataType: 'json',
        success: function (data) {

            $(data).each(function (Value, Text) {


                $('#training').append("<option value=" + Text.Value + ">" + Text.Text + "</option>");
            });
        }
    });

    // Career List

    $.ajax({
        url: "/Member/GetMeCareerList",
        type: 'POST',
        dataType: 'json',
        success: function (data) {

            $(data).each(function (Value, Text) {


                $('#career').append("<option value=" + Text.Value + ">" + Text.Text + "</option>");
            });
        }
    });

    //$('#career').addClass("select");

    $('#career').on('change', function () {

        $.ajax({
            url: "/Member/GetMeDomainList",
            type: 'POST',
            dataType: 'json',

            data: { CurrentCareerLevelId: $('#career').val() },
            success: function (data) {
                $('#domaindd').html('').append('<option value="0">Select Domain</option>');
                $('#categorydd').html('').append('<option value="0">Select Category</option>');
                $('#sub_categorydd').html('').append('<option value="0">Select Sub-Category</option>');
                $(data).each(function (Value, Text) {

                    $('#domaindd').append("<option value=" + Text.Value + ">" + Text.Text + "</option>");
                });
            }
        });
    });


    $('#domaindd').on('change', function () {
        $.ajax({
            url: "Member/GetMeCategoryList",
            type: 'POST',
            dataType: 'json',

            data: { CurrentDomainId: $('#domaindd').val() },
            success: function (data) {
                $('#categorydd').html('').append('<option value="0">Select Category</option>');
                $('#sub_categorydd').html('').append('<option value="0">Select Sub-Category</option>');
                $(data).each(function (Value, Text) {

                    $('#categorydd').append("<option value=" + Text.Value + ">" + Text.Text + "</option>");
                });
            }
        });
    });

    $('#categorydd').on('change', function () {
        $.ajax({
            url: "/Member/GetMeSubCategoryList",
            type: 'POST',
            dataType: 'json',

            data: { CurrentCategoryId: $('#categorydd').val() },
            success: function (data) {
                $('#sub_categorydd').html('').append('<option value="0">Select Sub-Category</option>');
                $(data).each(function (Value, Text) {
                    $('#sub_categorydd').append("<option value=" + Text.Value + ">" + Text.Text + "</option>");
                });
            }
        });
    });
    // DropDowns End
    var careervalue = -1, domainvalue = -1, categoryvalue = -1, subcategoryvalue = -1;
    DisplayJobList(careervalue, domainvalue, categoryvalue, subcategoryvalue);
    //DisplayJobList(0, 0, 0, 0);
    $('#search').click(function () {

        if ($('#career').val() == 0) {
            careervalue = -1;
        }
        else {
            careervalue = $('#career').val();
        }
        if ($('#domaindd').val() == 0) {
            domainvalue = -1;
        }
        else {
            domainvalue = $('#domaindd').val();
        }
        if ($('#categorydd').val() == 0) {
            categoryvalue = -1;
        }
        else {
            categoryvalue = $('#categorydd').val();
        }

        if ($('#sub_categorydd').val() == 0) {
            subcategoryvalue = -1;
        }
        else {
            subcategoryvalue = $('#sub_categorydd').val();
        }

        if (careervalue != -1 || domainvalue != -1 || categoryvalue != -1 || subcategoryvalue != -1) {
            $remaing = 0;
            $startindex = 0;
            $count = 0;
            $('#ticker04').empty();
            $('#ticker03').empty();
            DisplayJobList(careervalue, domainvalue, categoryvalue, subcategoryvalue);

        }
        //ajax call
    });
    function DisplayJobList(cid, did, catid, subcatid) {
        var $iterations = 2;
        var $size;
        $startindex = $count;
        $.ajax({
            url: '/Home/GetFilteredJobList',
            dataType: "json",
            method: 'GET',
            dataType: 'json',
            data: {

                mcareerid: cid,
                mdomianid: did,
                mcategoryid: catid,
                msubcategoryid: subcatid,


            },
            success: function (data) {
                debugger
                $('#ticker04').empty();

                $(data).each(function (index, emp) {

                    debugger
                    $('#ticker04').append('<li class="table-row"><div class= "table-col">' + emp.Title + '</div><div class="table-col">' + emp.AppClosingDate + '</div><div class="table-col">' + emp.MemberCareerLevelName + '</div><div class="table-col">' + emp.Job_Kpis + '</div><div class="table-col"><a href="#">Reffer ME</a></div></li>');

                });

            }

        });
        //AjaxCall to get filtered Training list
        $.ajax({
            url: '/Home/GetFilteredTrainingList',
            dataType: "json",
            method: 'GET',
            dataType: 'json',
            data: {

                mcareerid: cid,
                mdomianid: did,
                mcategoryid: catid,
                msubcategoryid: subcatid,


            },
            success: function (data) {
                debugger

                $('#ticker03').empty();
                $(data).each(function (index, emp) {

                    debugger
                    $('#ticker03').append('<li class="table-row"><div class= "table-col">' + emp.Title + '</div><div class="table-col" id="date">' + emp.StartDate + '</div><div class="table-col">' + emp.TotalHours + '</div><div class="table-col"><a href="#">Book Now</a></div></li>');

                });

            }

        });
    }
});
