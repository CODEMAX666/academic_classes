function ValidateClick() {
    var Validate = true;
    var ErrorHtml = "<span2 style='display: block;' title='{#Message#}' class='tooltipMain'>" +
        "<div style='margin-top: -45px; clear: both; float: right; position: relative; left: -8px;'>" +
        "<img src='/assets/images/validation_icon.png' alt='Validation' style='float: left; border: 0px; width: 12px;' /></div></span2>";

    debugger
    // TextBoxses Validate
    $("input[validation='validate']").each(function () {
        var Value = $(this).val();
        if (Value == "" || Value == " ") {
            var Msg = "Required Field";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    //Alphabets Allowed
    $("input[validation='validateAlphabet']").each(function () {
        debugger
        var Value = $(this).val();
        //var regex = /^[A-Z a-z]+$/;
        var regex=/^[a-zA-Z]+(\s[a-zA-Z]+)?$/;
        //var isValid = regex.test(String.fromCharCode(Value));
        if (Value == "" || Value == " ") {
            $(this).next('span2').remove();
            var Msg = "Required Field";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (!regex.test(Value)) {
            $(this).next('span2').remove();
            var Msg = "Only Alphabets and spaces allowed in between.";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    $("input[validation='validateImage']").each(function () {
        debugger
        var Value = this.files[0].size;
        if (Value > 3000000) {
            var Msg = "File Size Should be less then 3MB";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    // Email TextBoxses
    $("input[validation='validateEmail']").each(function () {
        var Value = $(this).val();
        if (Value == "" || Value == " ") {
            var Msg = "Required Field";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (IsEmail(Value) == false) {
            $(this).next('span2').remove();
            var Msg = "Not Valid Email Address";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    $("input[validation='validatePhoneNumber']").each(function () {
        debugger
        var Value = $(this).val();
        var length = Value.length;
        let re = /^[0-9]{10}$/;
        var reg = /(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})/;
        if (Value == "" || Value == " ") {
            var Msg = "Required Field";
            $(this).next('span2').remove();
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (IsNumber(Value) == false) {
            $(this).next('span2').remove();
            var Msg = "Only Numbers Allowed!";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (length != 11) {
            $(this).next('span2').remove();
            var Msg = "Number should be 11 digits!";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    //profile setting second phone number validation
    $("input[validation='validateSecPhoneNumber']").each(function () {
        debugger
        var Value = $(this).val();
        var length = Value.length;
        let re = /^[0-9]{10}$/;
        var reg = /(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})/;
        if (IsNumber(Value) == false && Value != "" && Value != " ") {
            $(this).next('span2').remove();
            var Msg = "Only Numbers Allowed!";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (Value != "" || Value == " ") {
            if (length != 11) {
                $(this).next('span2').remove();
                var Msg = "Number should be 11 digits!";
                $(this).after(ErrorHtml.replace("{#Message#}", Msg));
                Validate = false;
            }
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    //****


    $("input[ validation='validateExpirationDatePassport']").each(function () {
        var Value = $(this).val();
        if (Value == "" || Value == " " || IsValidDate(Value) == false) {
            var Msg = "Not Valid ExpirationDatePassport ";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    $("input[validation='validateDateOfBirth']").each(function () {
        debugger;
        var Value = $(this).val();
        var d = new Date(Value);
        var getYear = d.getFullYear();
        //minDate: new Date(1950, 1, 1),

        //maxDate: new Date(2021,11,1)
        debugger;
        if (Value == "" || Value == " " || IsValidDate(Value) == false || getYear < 1950) {
            var Msg = "Required Field";
            $(this).next('span2').remove();
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (getYear < 1950) {
            var Msg = "Year must be greater than 1950";
            $(this).next('span2').remove();
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }

    });


    // Compare TextBoxses
    $("input[validationMatch='validationMatch']").each(function () {
        //comment
        var Value = $(this).val();
        var CompareValue = $("#" + $(this).attr("matchid")).val();
        if (Value == "" || Value == " ") {
            $(this).next('span2').remove();
            var Msg = "Required Field";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }

        else if (Value != CompareValue || Value == "" || Value == " " || CompareValue == "" || CompareValue == " ") {
            $(this).next('span2').remove();
            var Msg = "Password Must Match";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }

        else {
            $(this).nextAll('span2').remove();
        }

    });
    $("input[validation='validation8']").each(function () {
        //comment
        var Value = $(this).val();
        var CompareValue = $("#" + $(this).attr("matchid")).val();
        if (Value == "" || Value == " ") {
            $(this).next('span2').remove();
            var Msg = "Required Field";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (Value.length < 8) {
            $(this).next('span2').remove();
            var Msg = "Password atleast 8 characters";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }


        else {
            $(this).nextAll('span2').remove();
        }

    });

    // DropDownList Validate
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
            var Msg = "Please Select";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (Value.val() == "0") {
            $(this).next('span2').remove();
            var Msg = "Please Select Valid Value";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    $("select[validation='validatePrefix']").each(function () {
        var Value = $(this).val();
        if (Value == "Prefix") {
            var Msg = "Please Select";
            //$(".select2-selection__arrow").after(ErrorHtml.replace("{#Message#}", Msg));
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    $("input[validation='validateNum']").each(function () {
        var Value = $(this).val();
        let length = Value.length;
        var Msg;
        var re = /^((0|[1-9]\d?)(\.\d{1,2})?|100(\.00?)?)$/;
        var regex = /^[0-9]+$/;
        if (Value == "" || Value == " ") {
            Msg = "Required Field";
            $(this).next('span2').remove();
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (!regex.test(Value)) {
            $(this).next('span2').remove();
            Msg = "Enter a valid number";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (Value < 1) {
            $(this).next('span2').remove();
            Msg = "Min charges 1 Rs";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else if (Value > 50000) {
            $(this).next('span2').remove();
            Msg = "Max Charges 50,000 Rs";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }

        else {
            $(this).nextAll('span2').remove();
        }
    });
    $("select[validation='validateGender']").each(function () {
        var Value = $(this).val();
        if (Value == "0") {
            var Msg = "Please Select";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    $("input[validation='validatePassengerName']").each(function () {
        //debugger;
        var Value = $(this).val();
        var FirstName = document.getElementById("FirstName").value;
        var MiddleName = document.getElementById("MiddleName").value;
        var LastName = document.getElementById("LastName").value;
        var charCount = FirstName.length + MiddleName.length + LastName.length;
        if (charCount >= 35) {
            var Msg = "Passenger Name is too long. Please shorter the Middle Name";
            $(this).after(ErrorHtml.replace("{#Message#}", Msg));
            Validate = false;
            //alert("Passenger Name is too long");
        }
        else {
            $(this).nextAll('span2').remove();
        }
    });
    return Validate;
}



function IsEmail(email) {
    var regex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+com))$/;
    //var regex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return regex.test(email);
}

function IsValidDate(dateString) {
    var regEx = /^\d{4}-\d{2}-\d{2}$/;
    if (!dateString.match(regEx)) return false;  // Invalid format
    var d = new Date(dateString);
    var dNum = d.getTime();
    if (!dNum && dNum !== 0) return false; // NaN value, Invalid date
    return d.toISOString().slice(0, 10) === dateString;
}
function IsNumber(number) {
    var regex = /^[0-9]+$/;
    return regex.test(number);
}
function changelevel() {
    debugger;
    var level = $('#levelselector').find(":selected").text();
    if (level == "Professional") {

        $('#afterlevel').show();
    }
    else if (level == "Entrepreneur") {

        $('#afterlevel').show();
    }
    else {
        $('#afterlevel').hide();
    }

    //$('#' + $(this).val()).show();
    var level = $('#levelselector').find(":selected").text();
    var elements = document.getElementsByClassName("Domain").options;
    var names = '';
    for (var i = 0; i < elements.length; i++) {
        debugger;
        names += elements[i].names;
    }
    console.log(names);
}
function select() {
    var elements = document.getElementById("Menteelist").options;

    for (var i = 0; i < elements.length; i++) {
        elements[i].selected = false;

    }
}
function ChangeOnCareerDropdown() {
    debugger;

}
