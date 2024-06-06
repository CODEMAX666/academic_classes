function AddPackage(){
    debugger;
    var lastRowId = $('.package-table tbody tr:last').attr('id');
    var index = lastRowId.indexOf("_");
    var text = lastRowId.substr(index + 1);
    var rowsCount = parseInt(text) + 1;
    //alert(rowsCount);
    var packageRow = '<tr class="packageRow" id ="packageRowID_' + rowsCount + '">' +
        '<td class="align-middle">' +
        '<select name="MemberMenteeCareerLevel" validation="validateDDL" class="form-control" id="CareerLevel' + rowsCount + '">' +
        '</select>' +
        '</td>' +
        '<td class="align-middle">' +
        '<select name="MenteePackageName" validation="validateDDL" class="form-control" id="MenteePackageName' + rowsCount + '">' +
        '<option value="0">Package Name</option>' +
        '</select>'  +
        '</td>' +
        '<td class="align-middle">' +
        '<textarea class="form-control" name="MenteePackageDescription" placeholder="PackageDescription" rows="5" cols="200" readonly validation = "validate" id="PackageDescription' + rowsCount + '">' +
        '</textarea>'+
        '</td>' +
        '<td class="align-middle">' +
        '<input class="form-control" name="PackageRate" type="text" placeholder="PackageRAte" validation = "validateNum" id="PackageRate' + rowsCount + '">' +
        '</td>' +
        '<td class="align-middle">' +
        '<input class="form-control" name="ValidityStart" type="date" validation = "validate" id="ValidityStart' +
        rowsCount + '">' +
        '</td>' +
        '<td class="align-middle">' +
        '<input class="form-control" name="ValidityEnd" type="date" validation = "validate" id="ValidityEnd' +
        rowsCount + '">' +
        '</td>' +
        '<td class="align-middle">'+
        '<button type="submit" class="btn btn-primary" id="savePackage' + rowsCount + '">Save</button>' +
        '</td > ' +
        '<td class="align-middle">' +
        '<button href = "#" class="btn btn-danger trash " id = "delete' + rowsCount + '" > <i class="fas fa-trash-alt"></i></button>' +
        '</td > ' +
        '</tr>';
    
    PopulateCareerLevelDropdown(rowsCount);
    //PopulateDegreeTitleDropdown(rowsCount);
    //DeleteRow(rowsCount);

    $(".package-table tbody").append(packageRow);
    $("#CareerLevel" + rowsCount).change(function () {
        GetCareerId(this, rowsCount);
        
    });
    $("#MenteePackageName" + rowsCount).change(function () {
        GetPackageId(this, rowsCount);
    });
    $("#savePackage" + rowsCount).click(function () {
        return SubmitPackage(rowsCount);
    });
    $("#delete" + rowsCount).click(function () {
        $("#packageRowID_" + rowsCount).remove();
    });
    return false;
}
function PopulateCareerLevelDropdown(rowsCount) {
    $.ajax({
        url: '/Mentor/CareerLevelListDropdown',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        success: function (data) {
            jQuery.each(data, function (index, item) {
                $("#CareerLevel" + rowsCount).append("<option value=" + item.Value + ">" + item.Text + "</option>");
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
function SubmitPackage(divcount) {
    var Validate = true;
    var ErrorHtml = "<span2 style='display: block;' title='{#Message#}' class='tooltipMain'>" +
        "<div style='margin-top: -45px; clear: both; float: right; position: relative; left: -8px;'>" +
        "<img src='/assets/images/validation_icon.png' alt='Validation' style='float: left; border: 0px; width: 12px;' /></div></span2>";
    debugger;
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
    $("input[validation='validateNum']").each(function () {
        var Value = $(this).val();
        if (Value == "" || Value == " " || !$.isNumeric(Value)) {
            var Msg = "Numeic Value only";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        //elseif
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
    
    var obj = {};
    obj.ValidityStart = $("#ValidityStart" + divcount).val();
    obj.ValidityEnd = $("#ValidityEnd" + divcount).val();
    if (obj.ValidityStart > obj.ValidityEnd) {
        var Msg = "Please enter valid duration";
        var Msg1 = "Validity End can not be lesss than Validity Start";
        $("#ValidityStart" + divcount).after(ErrorHtml.replace("{#Message#}", Msg));
        $("#ValidityEnd" + divcount).after(ErrorHtml.replace("{#Message#}", Msg1));
        Validate = false;
    }
    else {
        $(this).nextAll('span2').remove();
    }
    if (Validate) {
        debugger;
        var obj = {};
        obj.MentorOwnPackageId = $("#PackageId" + divcount).val();
        obj.MenteePackageId = $("option:selected","#MenteePackageName" + divcount).val();
        obj.PackageRate = $("#PackageRate" + divcount).val();
        obj.ValidityStart = $("#ValidityStart" + divcount).val();
        obj.ValidityEnd = $("#ValidityEnd" + divcount).val();
        console.log(obj);
        packages = JSON.stringify({ 'packages': obj });
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/Mentor/PackageManagement',
            data: packages,
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
function DeletePackage(count) {
    if (confirm('Are you sure to delete this record ?')) {
        var obj = {};
        obj.MentorOwnPackageId = $("#PackageId" + count).val();
        obj.ValidityStart = $("#ValidityStart" + count).val();
        obj.ValidityEnd = $("#ValidityEnd" + count).val();
        packages = JSON.stringify({ 'packages': obj });
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/Mentor/PackageManagement',
            data: packages,
            success: function () {
                $('#result').html('"PassThings()" successfully called.');
            },
            failure: function (response) {
                $('#result').html(response);
            }
        });
    }
}
