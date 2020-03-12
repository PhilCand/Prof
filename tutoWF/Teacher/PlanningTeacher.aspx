<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlanningTeacher.aspx.cs" Inherits="tutoWF.Teacher.PlanningTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .fc-title {
            visibility: hidden; /* hides event title */
        }
    </style>
    <div class="title mb-0">
        <h1>
            <asp:Label Text="" ID="lblPlanning" runat="server" /></h1>
    </div>
    <br />
    <div class="text-center mt-0">
        <asp:Label Text="" ID="lblConnected" runat="server" />
    </div>
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
                            defaultDate: sessionStorage.getItem("date") == null ? new Date().toISOString() : sessionStorage.getItem("date"),
                            allDaySlot: false,
                            minTime: "07:00:00",
                            maxTime: "21:00:00",
                            eventRender: function (event) {
                                if (studentId == event.event.extendedProps.student_id.toString()) {
                                    event.el.style.backgroundColor = "";
                                }
                            },
                            eventClick: function (event, jsEvent, view) {
                                var date = calendar.getDate();
                                sessionStorage.setItem("date", date.toISOString());
                                if (studentId == event.event.extendedProps.student_id.toString()) {
                                    $('#modalTitle').text(event.event.title);
                                } else $('#modalTitle').text("");
                                $('#lblStart').text(formatDate(event.event.start));
                                $('#lblEnd').text(formatDate(event.event.end));
                                $('#lblState').text(event.event.extendedProps.state);
                                $('#lblDesc').text(event.event.extendedProps.description);
                                $('#modalBody').html(event.event.description);
                                $('#eventUrl').attr('href', event.url);
                                $('#calendarModal').modal();
                                $('#MainContent_hfeventIdModal').val(event.event.id);
                                if (studentId == 0) $('#MainContent_btn_bookEvent').attr('disabled', true);
                                if (studentId > 0) $('#MainContent_btn_bookEvent').removeAttr('disabled');
                                if (event.event.backgroundColor == "grey" || event.event.start <= Date.now()) $('#MainContent_btn_bookEvent').attr('disabled', true);
                                if (event.event.extendedProps.student_id == 0) $('#MainContent_btn_freeEvent').attr('disabled', true);
                                if (event.event.start >= Date.now() && event.event.extendedProps.student_id == studentId && studentId > 0) {
                                    $('#MainContent_btn_freeEvent').removeAttr('disabled');
                                } else $('#MainContent_btn_freeEvent').attr('disabled', true);
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
                    <label id="modalTitle" />
                    <br />
                    <label>Description : </label>
                    <label id="lblDesc"></label>
                    <br />

                    <asp:HiddenField runat="server" ID="hfeventIdModal" Value="test" />
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" ID="btn_bookEvent" Text="Réserver" OnClick="btn_bookEvent_Click" />
                    <asp:Button runat="server" CssClass="btn btn-warning" ID="btn_freeEvent" Text="Libérer" OnClick="btn_freeEvent_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fermer</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
