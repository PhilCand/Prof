<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="tutoWF.Success" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="loginMessage">

        <p>Compte créé avec succès.</p>

        <%if (Convert.ToInt32(Request.QueryString["id"]) > 0)
            { %>
        <a href="/Teacher/HomeTeacher?id=<% Response.Write(Convert.ToInt32(Request.QueryString["id"])); %>">Se connecter</a>
        <%} %>

        <%else
            { %>
        <a href="/Teacher/TeacherLogin">Se connecter</a>
        <%} %>
    </div>
</asp:Content>
