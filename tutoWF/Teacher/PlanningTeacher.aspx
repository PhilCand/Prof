<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlanningTeacher.aspx.cs" Inherits="tutoWF.Teacher.PlanningTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label Text="" ID="lblPlanning" runat="server" />
    <br />
    <asp:Label Text="" ID="lblConnected" runat="server" />
    <br />
    <asp:HiddenField ID="hdnStudentId" runat="server" ClientIDMode="Static" />

    <script>
        var studentId = document.getElementById('hdnStudentId').value;

        function getUrlVars() {
            var vars = {};
            var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
                vars[key] = value;
            });
            return vars;
        }

        var teacherId = getUrlVars()["id"];

        document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');

            let xmlhttp = new XMLHttpRequest();

            xmlhttp.onreadystatechange = () => {
                if (xmlhttp.readyState == 4) {
                    if (xmlhttp.status == 200) {
                        let evenements = JSON.parse(xmlhttp.responseText);

                        var calendar = new FullCalendar.Calendar(calendarEl, {
                            plugins: ['timeGrid', 'dayGrid', 'list', 'interaction'],
                            locale: 'fr',
                            events: evenements,
                            nowIndicator: true,
                            eventClick: function (event, jsEvent, view) {

                                $('#modalTitle').text(event.event.title);
                                $('#lblStart').text(formatDate(event.event.start));
                                $('#lblEnd').text(formatDate(event.event.end));
                                $('#modalBody').html(event.event.description);
                                $('#eventUrl').attr('href', event.url);
                                $('#calendarModal').modal();
                                if (studentId > 0) $('#btn_bookEvent').removeAttr('disabled');
                                if (event.event.backgroundColor == "grey") {
                                    $('#btn_bookEvent').attr('disabled', true);
                                    $('#lblState').text("Réservé");
                                } else $('#lblState').text("Disponible");

                                $('#btn_bookEvent').click(function () {
                                    let updatedEvent = {
                                        start: event.event.start.toISOString(),
                                        end: event.event.end.toISOString(),
                                        id: event.event.id,
                                        student_id: studentId,
                                        title: $('#modalTitle').val()
                                    }

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

            xmlhttp.open('get', 'http://localhost:51023/api/events/' + teacherId, true);
            xmlhttp.send(null);

        });

    </script>

    <div id="calendar">
    </div>

    <div id="calendarModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">                    
                    <h4><label id="modalTitle" /></h4>
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
                    <label id="lblState">Disponible</label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btn_bookEvent" data-dismiss="modal" disabled>Réserver</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fermer</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
