function ValidateExp() {
    
    return Validate;
}
function SubmitExperience(divcount) {
    var Validate = true;
    var ErrorHtml = "<span2 style='display: block;' title='{#Message#}' class='tooltipMain'>" +
        "<div style='margin-top: -45px; clear: both; float: right; position: relative; left: -8px;'>" +
        "<img src='/assets/images/validation_icon.png' alt='Validation' style='float: left; border: 0px; width: 12px;' /></div></span2>";

    // TextBoxses Validate
    $("select[validation='validateDDL']").each(function () {
        var Value = $(this).val();
        if (Value == "0" || Value == "-1") {
            var Msg = "Please Select";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    $("select[validation='validateMultiDDL']").each(function () {
        var Value = $('option:selected', this);
        if (Value.length == 0) {
            var Msg = "select valid value";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (Value.val() == "0") {
            var Msg = "No value selected";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    $("input[validation='validate']").each(function () {
        var Value = $(this).val();
        if (Value == "" || Value == " ") {
            var Msg = "Required Field";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (Value.length < 2) {
            var Msg = "Enter atleast 2 or more character";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    $("input[validation='validateYear']").each(function () {
        var Value = $(this).val();
        if (Value == "" || Value == " " || !$.isNumeric(Value)) {
            var Msg = "Required Numeic Value only";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (!(Value.length == 4)) {
            var Msg = "Year should be 4digit value";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    var obj = {};
    obj.YearFrom = $("#YearFrom" + divcount).val();
    obj.YearTo = $("#YearTo" + divcount).val();
    if (obj.YearFrom > obj.YearTo) {
        var Msg = "Please enter valid duration";
        var Msg1 = "Year To can't be lesss than Year From";
        $("#YearFrom" + divcount).after(ErrorHtml.replace("{#Message#}", Msg));
        $("#YearTo" + divcount).after(ErrorHtml.replace("{#Message#}", Msg1));
        Validate = false;
    }
    else {
        $(this).nextAll('span2').remove();
    }
    if (Validate) {
        var obj = {};
        obj.MemberExperienceId = $("#ExpId" + divcount).val();
        obj.MemberCategory = $("option:selected", "#Category" + divcount).val();
        var array = $("option:selected", "#SubCategory" + divcount).map((_, e) => e.value).get();
        obj.MemberSubCategory = array.toString();
        obj.Designation = $("#Designation" + divcount).val();
        obj.Company = $("#Company" + divcount).val();
        obj.YearFrom = $("#YearFrom" + divcount).val();
        obj.YearTo = $("#YearTo" + divcount).val();
        //experience.push(obj);
        console.log(obj);
        //}
        experience = JSON.stringify({ 'experience': obj });
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/Member/MemberExperience',
            data: experience,
            success: function () {
                $('#result').html('"PassThings()" successfully called.');
            },
            failure: function (response) {
                $('#result').html(response);
            }
        });
    }
    return Validate;
}

function PopulateCategoryDropdown(divcount) {
    $.ajax({
        url: '/Member/CategoryListDropdown',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        success: function (data) {
            jQuery.each(data, function (index, item) {
                
                $("#Category" + divcount).append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });
            //$(data).each(function (Value, Text) {

            //    debugger;
            //    $("#DegreeLevelName" + rowsCount).append("<option value=" + Text.Value + ">" + Text.Text + "</option>");
            //});
        },
        error: function (xhr) {
            
            console.log(xhr.responseText);
            alert("Error has occurred..");
        }
    });
}
function PopulateSubCategoryDropdown(divcount) {
    //$("#Category" + divcount).change(function () {
    //    GetCategoryId(this, divcount)
    //})
    
    $.ajax({
        url: '/Member/SubCategoryListDropdown',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        success: function (data) {
            jQuery.each(data, function (index, item) {
                
                $("#SubCategory" + divcount).append("<option value=" + item.Value + ">" + item.Text + "</option>");
            });

        },
        error: function (xhr) {
            
            console.log(xhr.responseText);
            alert("Error has occurred..");
        }
    });
}
function DeleteExp(count) {
    if (confirm('Are you sure to delete this record ?')) {
        var obj = {};
        console.log($("#ExpId" + count).val());
        obj.MemberExperienceId = $("#ExpId" + count).val();
        experience = JSON.stringify({ 'experience': obj });
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/Member/MemberExperience',
            data: experience,
            success: function () {
                $('#result').html('"PassThings()" successfully called.');
            },
            failure: function (response) {
                $('#result').html(response);
            }
        });
    }
   // $("#Exp_" + count).remove();
}
                                            //INCLUDED IN INTEREST.CSHTML//
//function ValidateInterest() {
//    debugger;
//    var Validate = true;
//    var ErrorHtml = "<span2 style='display: block;' title='{#Message#}' class='tooltipMain'>" +
//        "<div style='margin-top: -45px; clear: both; float: right; position: relative; left: -8px;'>" +
//        "<img src='/~/assets/images/validation_icon.png' alt='Validation' style='float: left; border: 0px; width: 12px;' /></div></span2>";

//    debugger;
//    // TextBoxses Validate
//    $("select[validation='validateDDL']").each(function () {
//        var Value = $(this).val();
//        if (Value == "0" || Value == "-1") {
//            var Msg = "Please Select";
//            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
//            Validate = false;
//        }
//        else {
//            $(this).nextAll('span2').remove();
//        }
//    });
//    $("select[validation='validateMultiDDL']").each(function () {
//        var Value = $(this > 'option:selected');
//        if (Value.length == 0) {
//            var Msg = "No value selected";
//            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
//            Validate = false;
//        }
//        else {
//            $(this).nextAll('span2').remove();
//        }
//    });

//    return Validate;
//}

//function SubmitInterest() {
//    var interest = [];
//    debugger;
//    var divcount = $(".Interest").length;
//    for (i = 1; i <= divcount; i++) {
//        var obj = {};
//        obj.MemberCategory = $("option:selected", "#Category" + divcount).val();
//        var array = $("option:selected", "#SubCategory" + divcount).map((_, e) => e.value).get();
//        obj.MemberSubCategory = array.toString();
//        interest.push(obj);
//        console.log(interest);

//    }
//    interest = JSON.stringify({ 'interest': interest });
//    $.ajax({
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        type: 'POST',
//        url: '/Member/MemberInterest',
//        data: interest,
//        success: function () {
//            $('#result').html('"PassThings()" successfully called.');
//        },
//        failure: function (response) {
//            $('#result').html(response);
//        }
//    });
//}
