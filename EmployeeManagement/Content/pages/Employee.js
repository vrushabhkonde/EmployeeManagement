$(document).ready(function () {
    getEmployeelist();
 
});

var saveEmployee = function () {
    var firstname = $("#txtFirstName").val();
    var lastname = $("#txtLastName").val();
    var department = $("#ddlDepartment").val();
    if (firstname == "") {
        alert("Enter First name");
    }
    else if (lastname == "") {
        alert("Enter Last name");
    }
    else {
        var model = {
            FirstName: firstname, LastName: lastname, Department: department,
        };
        $.ajax({
            url: "/Employee/SaveEmployee",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json;charset=utf-8",
            datatype: "json",
            success: function (response) {
                alert(response.message);
            }
        });
    }
}


var getEmployeelist = function () {
    $.ajax({
        url: "/Employee/GetEmployee",
        method: "post",
        //data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("#tbl_employee tbody").empty();
            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.EmployeeId + "</td><td>" + elementValue.FirstName + "</td> <td>" + elementValue.LastName + "</td><td>" + elementValue.Department + "<td/><td><input type='submit' value='Delete' onClick='deleteRegistration(" + elementValue.EmployeeId + ")'/><td/></td>" + "<td/><td><input type='submit' value='Details' ' data-toggle='modal' data-target='#myModal' onClick='getRegistrationDetails(" + elementValue.EmployeeId + ")'/></td></tr>";

            });
            $("#tbl_employee tbody").append(html);
        }
    })
}


var deleteRegistration = function (Id) {
    var model = { EmployeeId: Id };
    $.ajax({
        url: "/Employee/DeleteEmployee",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {

            alert("Employee Deleted Successfully");
        }
    });
}

var getRegistrationDetails = function (id) {
    var model = { EmployeeId: id };
    $.ajax({
        url: "/Employee/GetEmployeeDetail",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (response) {
            $("#lblRegId").text(response.model.EmployeeId);
            $("#lblName").text(response.model.FirstName);
            $("#lblMobile").text(response.model.LastName);
            $("#lblEmail").text(response.model.Department);
            var html = "";

        }
    });

}