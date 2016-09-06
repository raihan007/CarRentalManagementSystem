$(function () {
    $('.pickUptime').datetimepicker({
        format: 'LT'
    });
});

$(function () {
    $('.PickUpDate').datetimepicker({
        format: 'MM/DD/YYYY'
    });
});

$(function () {
    $('.DropOffDate').datetimepicker({
        format: 'MM/DD/YYYY'
    });
});

$(function () {
    $('.dropOfftime').datetimepicker({
        format: 'LT'
    });
});

$("#get").click(function () {
    var d3 = $('.PickUpDate').val() + ' ' + $('.pickUptime').val();
    var d4 = $('.DropOffDate').val() + ' ' + $('.dropOfftime').val();
    var d1 = moment(d3.toString()).format('DD-MMM-YYYY H:MM');
    var d2 = moment(d4.toString()).format('DD-MMM-YYYY H:MM');
    //var date = moment("06/06/2015 11:11:11").format('DD-MMM-YYYY H:MM');
    var fromDate = parseInt(new Date(d1).getTime() / 1000);
    var toDate = parseInt(new Date(d2).getTime() / 1000);
    var timeDiff = (toDate - fromDate) / 3600;
    if (timeDiff < 0) {
        alert("Please Select Valid Pick-Up Date.");
    }
    else {

        var carId = $('#ReservationVehicleId').val();
        var pDate = $('.PickUpDate').val();
        var pTime = $('.pickUptime').val();
        var dDate = $('.DropOffDate').val();
        var dTime = $('.dropOfftime').val();

        //@*$.ajax({
        //    type: "POST",
        //    url: '@Url.Action("CheckAbailavleCar", "Reservation")',
        //    data: '{carId: "' + carId.toString() + '",pDate: "' + pDate.toString() + '",pTime: "' + pTime.toString() + '",dDate: "' + dDate.toString() + '",dTime: "' + dTime.toString() + '",timediff: "' + timeDiff.toString() + '" }',// user name or email value  
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (response) {
        //        if (response.toString() == 'false') {
        //            alert(response.toString());
        //        } else {
        //            var s = "This car is re"
        //        }
        //    },
        //    failure: function (response) {
        //        alert(response);
        //    }
        //});*@

        var cost = timeDiff * $('#Vehicle_CostPerHour').val();
        $('#Vehicle_CostPerHour').val(cost);
        var msg = 'You Have to pay ' + cost + 'Doller. Are you sure ?';
        if (confirm(msg)) {
            $("#target").submit();
        }
    }
});