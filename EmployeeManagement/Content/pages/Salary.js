$(document).ready(function () {
    getEmployeelist();
    getsalarylist();
 
 
});

var saveSalary = function () {
    var empid = $("#ddlEmployee").val();
    var salary = $("#txtSalary").val();
    var salarymonth = $("#txtSalaryMonth").val();
    if (salary == "") {
        alert("Enter Salary");
    } else {
        var model = {
            Salary: salary, EmployeeId: empid, SalaryMonth: salarymonth,
        };
        $.ajax({
            url: "/Salary/SaveSalary",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json;charset=utf-8",
            datatype: "json",
            success: function (response) {
                alert(response.message);
                getsalarylist();
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
            $("#ddlEmployee").empty();
            $.each(response.model, function (index, elementValue) {
                html += "<option value=" + elementValue.EmployeeId + " >" + elementValue.FirstName +"  "+elementValue.LastName + "</option>";

            });
            $("#ddlEmployee").append(html);
        }
    })
}

var getsalarylist = function () {
    $.ajax({
        url: "/Salary/GetSalary",
        method: "post",
        //data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("#tbl_salary tbody").empty();
            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.TransactionId + "</td><td>" + elementValue.EmployeeId + "</td> <td>" + elementValue.Salary + "</td><td>" + elementValue.SalaryMonth + "</td></tr>";

            });
            $("#tbl_salary tbody").append(html);
        }
    })
}

var getEmployeeSalReport = function () {
    var empid = $("#ddlEmployee").val();
    var fromdate = $("#FromDate").val();
    var todate = $("#Todate").val();

    var model = {
        FromDate: fromdate, EmployeeId: empid, ToDate: todate,
        };
    $.ajax({
        url: "/Salary/GetEmployeeSalReport",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("#tbl_salaryrep tbody").empty();
            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.TransactionId + "</td><td>" + elementValue.EmployeeId + "</td> <td>" + elementValue.FirstName + "</td>";
                html += "<td>" + elementValue.LastName + "</td><td>" + elementValue.Salary + "</td><td>" + elementValue.SalaryMonth +  "</td></tr>";
            });
            $("#tbl_salaryrep tbody").append(html);
            $.noConflict();
            $('#tbl_salaryrep').DataTable({
                scrollY: '20vh',
                //scrollCollapse: true,
                //scrollY: false,
                scrollX: true,
                pageLength: 10,
                //responsive: true,
                dom: 'lBfgitp',
                buttons: [
                    //{ extend: 'copy' },
                    { extend: 'csv' },
                    { extend: 'excel', title: 'FileData' },
                    { extend: 'pdf', title: 'FileData' }

                ],
            });
        }
    })
}
var ExporttoExcel = function () {
    var empid = $("#ddlEmployee").val();
    var fromdate = $("#FromDate").val();
    var todate = $("#Todate").val();

    var model = {
        FromDate: fromdate, EmployeeId: empid, ToDate: todate,
    };
    $.ajax({
        url: "/Salary/ExportToExcel",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
           
        }
    })
}


