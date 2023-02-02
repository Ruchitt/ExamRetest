var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "Employee/GetAll"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "description", "width": "25%" },
            { "data": "isActive", "width": "25%" },
            {
                "data": "employeeId",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Employee/Upsert/?id=${data}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                       <a onclick="Delete(${data})"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Delete</a>
					</div>
                        `
                },
                "width": "25%"
            }
        ]
    });
}
    function Delete(idd) {
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = "/Employee/Delete/" + idd;
            }
        })
    }
