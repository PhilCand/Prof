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
                            defaultDate: sessionStorage.getItem("date") == null ? new Date().toISOString() : sessionStorage.getItem("date"),
                            editable: true,
                            minTime: "07:00:00",
                            maxTime: "21:00:00",
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
                            allDaySlot: false,
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
                                        var date = calendar.getDate();
                                        sessionStorage.setItem("date", date.toISOString());
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
                                var date = calendar.getDate();
                                sessionStorage.setItem("date", date.toISOString());
                                $('#lblModalTitle').text(event.event.title);
                                $('#lblStart').text(formatDate(event.event.start));
                                $('#lblEnd').text(formatDate(event.event.end));
                                $('#lblState').text(event.event.extendedProps.state);
                                $('#MainContent_tbModalDesc').text(event.event.extendedProps.description);
                                $('#modalBody').html(event.event.description);
                                $('#eventUrl').attr('href', event.url);
                                $('#calendarModal').modal();
                                $('#MainContent_hfeventIdModal').val(event.event.id);
                                if (event.event.extendedProps.student_id == 0) $('#MainContent_btn_freeEvent').attr('disabled', true);
                                if (event.event.backgroundColor == "grey" && event.event.start >= Date.now()) {
                                    $('#MainContent_btn_freeEvent').removeAttr('disabled');                                   
                                }
                            },
                        });

                        calendar.render();

                    }
                }
            }

            xmlhttp.open('GET', 'http://localhost:51023/api/events/' + teacherId, true);
            xmlhttp.send(null);

        });
        $('.maj').click(function () {
            alert("ok");
            sessionStorage.setItem("date", date.toISOString());
        });

    </script>

    <div id='external-events'>
        <p>
            <strong>Ajouter un cours</strong>
        </p>
        <div class='fc-event'>Cours</div>
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
 
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span> <span class="sr-only">close</span></button>
                </div>
                <div id="modalBody" class="modal-body">
                    <label>Date début : </label>
                    <label id="lblStart"></label>
                    <br />
                    <label>Date fin : </label>
                    <label id="lblEnd"></label>
                    <br />
                    <label>Etat : </label>
                    <label id="lblState"></label>
                    <br />
                    <label>Eleve : </label>
                    <label id="lblModalTitle"></label>
                    <br />
                    <label>Description : </label>
                    <br />
                    <asp:TextBox runat="server" TextMode="multiline" Rows="5" placeholder="Description" ID="tbModalDesc" Style="width: 100%"></asp:TextBox>
                    
                    <asp:HiddenField runat="server" ID="hfeventIdModal" Value="test" />


                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" ID="btn_update" Text="Valider" OnClick="btn_update_Click" />
                    <asp:Button runat="server" CssClass="btn btn-warning" ID="btn_freeEvent" Text="Libérer" OnClick="btn_freeEvent_Click" />
                    <asp:Button runat="server" CssClass="btn btn-danger" ID="btn_delete" Text="Supprimer" OnClick="btn_delete_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fermer</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
