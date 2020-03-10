<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomeTeacher.aspx.cs" Inherits="tutoWF.Teacher.HomeTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="title">
        <h1>
            <asp:Label Text="" ID="lblTitle" runat="server" /></h1>
    </div>
    

    <div class="jumbotron">
        <h1 class="display-4"><%= TeachersPage.FirstName %> <%=TeachersPage.Name %></h1>
        <p class="lead"><%= TeachersPage.Description %></p>
        <hr class="my-4">
        <p>Me contacter : <%= TeachersPage.Email %></p>
        <a class="btn btn-primary btn-lg" href="/Teacher/PlanningTeacher?id=<%Response.Write(Convert.ToInt32(Request.QueryString["id"]));%>" role="button">Voir le planning</a>
    </div>


    <%if (Session["Teacher"] == null)
        { %>
    <div id="title">
        <h1>Se connecter en tant qu'élève</h1>
    </div>

    <div class="login">
        <div class="dashed">
            <div class="error">
                <asp:Label ID="lblLoginError" runat="server"></asp:Label>
            </div>
            <asp:TextBox runat="server" ID="txt_mail" PlaceHolder="Mail" TextMode="Email" CssClass="form-control login-input" /><br />
            <asp:TextBox runat="server" ID="txt_mdp" PlaceHolder="Mot de passe" TextMode="Password" CssClass="form-control login-input" /><br />
            <asp:Button ID="btn_connectStudent" runat="server" Text="Se connecter" CssClass="btn btn-outline-success" OnClick="btn_connectStudent_Click" /><br />
            <br />
            <h6>Nouvel élève ? <a href="/Student/Newstudent?id=<%Response.Write(Convert.ToInt32(Request.QueryString["id"]));%>">Créer un compte</a></h6>
        </div>
    </div>
    <%} %>
</asp:Content>
