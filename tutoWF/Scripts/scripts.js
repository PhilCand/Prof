function formatDate(date) {

    return `${
        (date.getMonth() + 1).toString().padStart(2, '0')}/${
        date.getDate().toString().padStart(2, '0')}/${
        date.getFullYear().toString().padStart(4, '0')} ${
        date.getHours().toString().padStart(2, '0')}:${
        date.getMinutes().toString().padStart(2, '0')}:${
        date.getSeconds().toString().padStart(2, '0')}`;

}

function deleteEvent(id) {

    let deletedEvent = {
        id: id
    }

    $.ajax({
        url: 'http://localhost:51023/api/events/' + deletedEvent.id,
        type: 'DELETE',
        dataType: 'json',
        data: deletedEvent,
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
        }
    });

    $('#calendarModal').modal('toggle');


}

function confirmDelete() {
    return confirm("Confirmer la suppression ?");
}

function showSuccessAlert() {
    $("#alert_success_edit").fadeTo(2000, 500).slideUp(500, function () {
        $("#alert_success_edit").slideUp(500);
    });
}



