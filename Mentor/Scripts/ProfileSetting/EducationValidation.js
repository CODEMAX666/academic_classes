
function SubmitEdu() {
    var Validate = true;
    var ErrorHtml = "<span2 style='display: block;' title='{#Message#}' class='tooltipMain'>" +
        "<div style='margin-top: -45px; clear: both; float: right; position: relative; left: -8px;'>" +
        "<img src='/assets/images/validation_icon.png' alt='Validation' style='float: left; border: 0px; width: 12px;' /></div></span2>";
    
    // TextBoxses Validate
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
    $("input[validation='validatePercentage']").each(function () {
        var Value = $(this).val();
        var re = /^((0|[1-9]\d?)(\.\d{1,2})?|100(\.00?)?)$/;
        if (Value == "" || Value == " ") {
            var Msg = "Required Field";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (!re.test(Value)) {
            var Msg = "Enter a valid percentage upto 2 decimal only";
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
    //validating duration year from to year To
    var table = $(".education-table >tbody");
    table.find('tr').each(function (i) {
        var obj = {};
        var tds = $(this).find('td');
        obj.YearFrom = $(this).find("td:eq(3) input[type='text']").val();
        obj.YearTo = $(this).find("td:eq(4) input[type='text']").val();
        if (obj.YearFrom >= obj.YearTo) {
            var Msg = "Please enter valid duration";
            var Msg1 = "Year To can't be lesss than Year From";
            $(this).find("td:eq(3) input[type='text']").after(ErrorHtml.replace("{#Message#}", Msg));
            $(this).find("td:eq(4) input[type='text']").after(ErrorHtml.replace("{#Message#}", Msg1));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });

    if (Validate) {
        var education = [];
        var table = $(".education-table >tbody");
        table.find('tr').each(function (i) {
            var obj = {};
            var tds = $(this).find('td');
            obj.DegreeLevelName = tds.eq(0).find(":selected").val();
            obj.DegreeTitleName = tds.eq(1).find(":selected").val();
            obj.Percentage = $(this).find("td:eq(2) input[type='text']").val();
            obj.YearFrom = $(this).find("td:eq(3) input[type='text']").val();
            obj.YearTo = $(this).find("td:eq(4) input[type='text']").val();
            obj.Institute = $(this).find("td:eq(5) input[type='text']").val();
            education.push(obj);
            console.log(education);

        })
        education = JSON.stringify({ 'education': education });
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/Member/MemberEducation',
            data: education,
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

function PopulateDegreeNameDropdown(rowsCount) {
    $.ajax({
        url: '/Member/DegreeLevelListDropdown',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        success: function (data) {
            jQuery.each(data, function (index, item) {
                $("#DegreeLevelName" + rowsCount).append("<option value=" + item.Value + ">" + item.Text + "</option>");
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

function PopulateDegreeTitleDropdown(rowsCount) {
    $.ajax({
        url: '/Member/DegreeTitleListDropdown',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        success: function (data) {
            jQuery.each(data, function (index, item) {
                $("#DegreeTitleName" + rowsCount).append("<option value=" + item.Value + ">" + item.Text + "</option>");
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
function DeleteRow(count) {
    if (confirm('Are you sure to delete this record ?')) {
        $("#eduRowID_" + count).remove();
    }
}