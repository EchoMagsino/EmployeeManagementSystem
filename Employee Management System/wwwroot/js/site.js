$(function () {
    const renderRows = (data, type) => {
        return data.map(item => {
            if (type === "employee") {
                return `<tr>
                    <td>${item.id}</td>
                    <td>${item.firstName}</td>
                    <td>${item.lastName}</td>
                    <td>${item.email}</td>
                    <td>${item.phoneNumber}</td>
                    <td>${item.department}</td>
                    <td>${item.position}</td>
                    <td>${item.hireDate}</td>
                    <td>${item.role}</td>
                    <td>${item.isActive}</td>
                    <td>
                        <a href="/Employees/Edit/${item.id}">Edit</a> |
                        <a href="/Employees/Details/${item.id}">Details</a> |
                        <a href="/Employees/Delete/${item.id}">Delete</a>
                    </td>
                </tr>`;
            } else {
                return `<tr>
                    
                    <td>${item.startDate}</td>
                    <td>${item.endDate}</td>
                    <td>${item.leaveType}</td>
                    <td>${item.hireDate}</td>
                    <td>${item.status}</td>
                    <td>${item.approverComments}</td>
                    <td>${item.employeeId}</td>
                    <td>
                        <a href="/LeaveRequest/Edit/${item.id}">Edit</a> |
                        <a href="/LeaveRequest/Details/${item.id}">Details</a> |
                        <a href="/LeaveRequest/Delete/${item.id}">Delete</a>
                    </td>
                </tr>`;
            }
        }).join('');
    };

    $("#liveSearch").on("keyup", function () {
        const term = $(this).val();

        $.getJSON('/Employees/LiveSearch', { term }, data => {
            $("#employeeTable tbody").html(renderRows(data, "employee"));
        });

        $.getJSON('/LeaveRequests/LiveSearch', { term }, data => {
            $("#leaveRequestTable tbody").html(renderRows(data, "leave"));
        });
    });
});