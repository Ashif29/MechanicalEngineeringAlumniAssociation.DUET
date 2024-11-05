

// Defaults
var swalInit = swal.mixin({
    buttonsStyling: false,
    confirmButtonClass: 'btn btn-primary',
    cancelButtonClass: 'btn btn-light'
});

    function delete_confirm(url, paramData) {
        swalInit.fire({
            title: 'Are you sure want to delete?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Confirm'
        }).then(function (result) {
            if (result.value) {
                ajaxCall(url, paramData, "renderDeleteItem");
            }
        });



    }

    function active_inactive_confirm(url, paramData) {
        swalInit.fire({
            title: 'Are you sure want to process?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Confirm'
        }).then(function (result) {
            if (result.value) {
                ajaxCall(url, paramData, "renderActiveInactiveItem");
            }
        });

    }
    function update_status_confirm(url, paramData) {
        swalInit.fire({
            title: 'Are you sure want to update status?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Confirm'
        }).then(function (result) {
            if (result.value) {
                ajaxCall(url, paramData, "renderUpdateStatusItem");
            }
        });
    }


