$(document).ready(function () {

    $("#liveSearch").on("keyup", function () {

        var term = $(this).val();

        $.ajax({

            url: `/Employees/LiveSearch`,
            data: { term: term },

            success: function (data) {
                var rows = "";
                data.forEach(function (emp) {

                    rows += `<tr>
                    <td>${emp.id}</td>
                    <td>${emp.firstName}</td>
                    <td>${emp.lastName}</td>
                    <td>${emp.email}</td>
                    <td>${emp.phoneNumber}</td>
                    <td>${emp.department}</td>
                    <td>${emp.position}</td>
                    <td>${emp.hireDate}</td>
                    <td>${emp.role}</td>
                    <td>${emp.isActive}</td>
                    

                        < td >

                        <a href="/Employees/Edit/${emp.id}">Edit</a> |
                        <a href="Employees/Details/${emp.id}">Details</a> | 
                        <a hred="Employees/Delete/${emp.id}"></a> |


                        <td />

                  </tr>`;
                });
        $("#employeeTable tbody").html(rows);
    }


        });
    });

});