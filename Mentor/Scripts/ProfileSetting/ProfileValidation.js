function AddFeild() {
    var phone = $('<div><input type="text" class="form-control" name="SecondaryPhoneNo" id="SecondaryPhone" validation = "validate" placeholder="Secondary Phone #"><button href="#" style="float:right" class="btn btn-danger trash " id="deletePhone" onclick=' + DeletePhone() + ' ><i class="fas fa-trash-alt"></i></button></div>')
    $("#mobileField").append(phone);
    $("#addPhone").hide();
}
function DeletePhone() {
    $("#SecondaryPhone").remove();
    $("#deletePhone").hide();
    $("#addPhone").show();
    return false;
}
//$(document).ready(function () {
//    $(".datepicker").datepicker({
//        changeMonth: true,
//        changeYear: true,
//    });
//});
