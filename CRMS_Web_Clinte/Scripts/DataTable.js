$(document).on('click', '.EditInfo', function () {
    var rowData = $(this).parent().siblings().map(function () {
        return $(this).text();
    }).get();

    var url = "EditDVDInfo.aspx?ID=" + $.trim(rowData[0]);
    window.location = url;
    //updateData(rowData);
});

$(document).on('click', '.DeleteInfo', function () {
    var dvdId = $(this).parent().siblings().map(function () {
        return $(this).text();
    }).get();
    deleteItem($.trim(dvdId[0]));
});

function deleteItem(dvdId)
{
    $.ajax({
        type: "POST",
        url: "Models/Model.aspx/DeleteInfo",
        data: '{id: "' + dvdId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d == true) {
                window.location = "Manager.aspx";
            }
        },
        failure: function (response) {
            alert("Data Not Deleted!");
        }
    });
}

function OnSuccess(response) {
    alert(response.d);
}

function updateData(rowData) {
    $("#txtID").val($.trim(rowData[0]));
    $("#txtID").attr("readonly",true);
    $("#txtName").val($.trim(rowData[1]));
    if ($.trim(rowData[2]) == 'Male') {
        $("#Gender_0").prop("checked", true);
    }
    else {
        $("#Gender_1").prop("checked", true);
    }
    $('#DepartmentList').val($.trim(rowData[2]));
    $("#txtCgpa").val($.trim(rowData[4]));
}

function newEntry()
{
    $('#txtID').attr("readonly", false);
}