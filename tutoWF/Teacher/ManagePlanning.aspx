<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagePlanning.aspx.cs" Inherits="tutoWF.ManagePlanning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hdnTeacherId" runat="server" ClientIDMode="Static" />


    <script>

        var teacherId = document.getElementById('hdnTeacherId').value;


        document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');
            var Draggable = FullCalendarInteraction.Draggable;
            var containerEl = document.getElementById('external-events');
            var Calendar = FullCalendar.Calendar;

            let xmlhttp = new XMLHttpRequest();

            new Draggable(containerEl, {
                itemSelector: '.fc-event',
                eventData: function (eventEl) {
                    return {
                        title: eventEl.innerText
                    };
                }
            });

            xmlhttp.onreadystatechange = () => {
                if (xmlhttp.readyState == 4) {
                    if (xmlhttp.status == 200) {
                        let evenements = JSON.parse(xmlhttp.responseText);


                        var calendar = new FullCalendar.Calendar(calendarEl, {
                            plugins: ['timeGrid', 'dayGrid', 'list', 'interaction'],
                            locale: 'fr',
                            events: evenements,
                            nowIndicator: true,
                            editable: true,
                            eventDrop: (infos) => {
                                let movedEvent = {
                                    start: infos.event.start.toISOString(),
                                    end: infos.event.end.toISOString(),
                                    id: infos.event.id,
                                    title: infos.event.title
                                }
                                $.ajax({
                                    url: 'http://localhost:51023/api/events/' + movedEvent.id,
                                    type: 'PUT',
                                    dataType: 'json',
                                    data: movedEvent,
                                    error: function (xhr, textStatus, errorThrown) {
                                        console.log('Error in Operation');
                                    }
                                });
                            },
                            droppable: true, // this allows things to be dropped onto the calendar
                            drop: function (info) {
                                var start = new Date(info.dateStr);
                                var end = new Date(info.dateStr);
                                end.setHours(end.getHours() + 1);

                                let newEvent = {
                                    start: start.toISOString(),
                                    end: end.toISOString(),
                                    teacher_id: teacherId
                                }

                                $.ajax({
                                    url: 'http://localhost:51023/api/events',
                                    type: 'POST',
                                    dataType: 'json',
                                    data: newEvent,
                                    success: function (result) {
                                        location.reload();
                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        console.log('Error in Operation');
                                    }
                                });

                            },
                            eventResize: function (infos) {
                                let movedEvent = {
                                    start: infos.event.start.toISOString(),
                                    end: infos.event.end.toISOString(),
                                    id: infos.event.id
                                }
                                console.log(movedEvent);
                                $.ajax({
                                    url: 'http://localhost:51023/api/events/' + movedEvent.id,
                                    type: 'PUT',
                                    dataType: 'json',
                                    data: movedEvent,
                                    error: function (xhr, textStatus, errorThrown) {
                                        console.log('Error in Operation');
                                    }
                                });
                            },
                            eventClick: function (event, jsEvent, view) {

                                $('#modalTitle').val(event.event.title);
                                $('#lblStart').text(formatDate(event.event.start));
                                $('#lblEnd').text(formatDate(event.event.end));
                                $('#modalBody').html(event.event.description);
                                $('#eventUrl').attr('href', event.url);
                                $('#calendarModal').modal();
                                $('#btn_delete').attr('onClick', 'deleteEvent(' + event.event.id + ')')
                                $('#btn_delete').click(function () {
                                    event.event.remove();
                                });
                                $('#btn_update').click(function () {
                                    let updatedEvent = {
                                        start: event.event.start.toISOString(),
                                        end: event.event.end.toISOString(),
                                        id: event.event.id,
                                        title: $('#modalTitle').val()
                                    }
                                    console.log(event.event.id);

                                    $.ajax({
                                        url: 'http://localhost:51023/api/events/' + event.event.id,
                                        type: 'PUT',
                                        dataType: 'json',
                                        data: updatedEvent,
                                        success: function (result) {
                                            location.reload();
                                        },
                                        error: function (xhr, textStatus, errorThrown) {
                                            console.log('Error in Operation');
                                        }
                                    });
                                    $('#calendarModal').modal('toggle');
                                });
                            },
                        });
                        calendar.render();
                    }
                }
            }

            xmlhttp.open('GET', 'http://localhost:51023/api/events/' + teacherId, true);
            xmlhttp.send(null);

        }); 

    </script>

    <div id='external-events'>
        <p>
            <strong>Ajouter un cours</strong>
        </p>
        <div class='fc-event'>Cours</div>
        <%--        <div class='fc-event'>My Event 2</div>
        <div class='fc-event'>My Event 3</div>
        <div class='fc-event'>My Event 4</div>
        <div class='fc-event'>My Event 5</div>--%>
        <p>
            <%--            <input type='checkbox' id='drop-remove' />--%>
            <label for='drop-remove'>Glisser / déposer</label>
        </p>
    </div>

    <div id="calendar">
    </div>

    <div id="calendarModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    
                    <h4><input type="text" name="inputTitle" placeholder="Titre" id="modalTitle" value="" /></h4>
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span> <span class="sr-only">close</span></button>
                </div>
                <div id="modalBody" class="modal-body">
                    <label>Date début : </label>
                    <label id="lblStart"></label>
                    <br />
                    <label>Date fin : </label>
                    <label id="lblEnd"></label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btn_update">Valider</button>
                    <button type="button" class="btn btn-danger" id="btn_delete">Supprimer</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fermer</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
