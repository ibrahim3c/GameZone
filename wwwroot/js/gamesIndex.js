//$(document).ready(function () {
//    $('.js-delete').on('click', function () {
//        var btn = $(this);
//        const swalWithBootstrapButtons = Swal.mixin({
//            customClass: {
//                confirmButton: "btn btn-success mx-2",
//                cancelButton: "btn btn-danger"
//            },
//            buttonsStyling: false
//        });
//        swalWithBootstrapButtons.fire({
//            title: "Are you sure?",
//            text: "You won't be able to revert this!",
//            icon: "warning",
//            showCancelButton: true,
//            confirmButtonText: "Yes, delete it!",
//            cancelButtonText: "No, cancel!",
//            reverseButtons: true
//        }).then((result) => {
//            if (result.isConfirmed) {
//                $.ajax({
//                    url: '/Games/Delete/' + btn.data('gameId'),
//                    success: function () {
//                        swalWithBootstrapButtons.fire({
//                            title: "Deleted!",
//                            text: "The Game has been deleted.",
//                            icon: "success"
//                        });
//                        btn.parents('tr').fadeOut();
//                    },
//                    error: function () {
//                        swalWithBootstrapButtons.fire({
//                            title: "Ooops!",
//                            text: "Something went wrong.",
//                            icon: "error"
//                        });
//                    }
//                });

//            }
//        });

//    });
//});




function DeleteGame(e) {
    var btn = e.currentTarget;  // Get the button that was clicked
    var id = btn.getAttribute('data-game-id');
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success mx-2",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Games/Delete/' + id,  // Added + for concatenation
                success: function () {
                    swalWithBootstrapButtons.fire({
                        title: "Deleted!",
                        text: "The Game has been deleted.",
                        icon: "success"
                    });
                    $(btn).parents('tr').fadeOut();  // Wrapped btn in jQuery
                },
                error: function () {
                    swalWithBootstrapButtons.fire({
                        title: "Ooops!",
                        text: "Something went wrong.",
                        icon: "error"
                    });
                }
            });
        }
    });
}
