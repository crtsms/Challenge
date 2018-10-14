$(document).ready(function () {
    $('.data-table').DataTable({
        "columnDefs": [
            {
                "targets": 'action-columm',
                "orderable": false
            }
        ],
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
    });
});