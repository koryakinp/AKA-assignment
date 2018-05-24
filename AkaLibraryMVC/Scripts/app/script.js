function getPendingBooks() {
    var libraryId = $('#library-id').val();
    var memberId = $('#member-id').val();
    $('#book-id').empty();
    if (memberId && libraryId) {
        $.ajax({
            url: '/Book/GetSingedOutBooks?libraryId=' + libraryId + '&memberId=' + memberId,
            success: buildBooksDropDown
        });
    }
}

function buildBooksDropDown(data) {
    for (var i = 0; i < data.length; i++) {
        var value = data[i].BookId;
        var text = data[i].Title;
        $('#book-id').append('<option value=' + value + '>' + text + '</option>');
    }
}

$('#member-id').change(function () {
    getPendingBooks();
});

getPendingBooks();