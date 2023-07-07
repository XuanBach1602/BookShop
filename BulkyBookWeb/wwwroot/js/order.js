var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    }
    else if (url.includes("completed")) {
        loadDataTable("completed");
    }
    else if (url.includes("pending")) {
        loadDataTable("pending");
    }
    else if (url.includes("approved")) {
        loadDataTable("approved");
    }
    else {
        loadDataTable("all");
    }
});

function loadDataTable(status) {
    $('#tblData tfoot th').each(function (index) {
        if (index < 7) {
            var title = $(this).text();
            $(this).html('<input type="text" class="search-input" placeholder="Search" />');
        }
    });

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Order/GetAll?status=" + status
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "name", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderStatus", "width": "15%" },
            { "data": "orderCode", "width": "10%" },
            { "data": "orderTotal", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Order/Details?orderId=${data}"
                            class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>
                        </div>
                    `;
                },
                "width": "5%"
            }
        ],
        "initComplete": function () {
            this.api().columns().every(function (index) {
                if (index < 7) {
                    var column = this;
                    $('input', this.footer()).on('keyup change clear', function () {
                        if (column.search() !== this.value) {
                            column.search(this.value).draw();
                        }
                    });
                }
            });
        }
    });
    $('#tblData tfoot tr').appendTo('#tblData thead');
}
