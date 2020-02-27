<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeacherLogin.aspx.cs" Inherits="tutoWF.Teacher.TeacherLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

            <h1>Se connecter : Professeur</h1>

        <div class="error"><asp:label id="lblLoginError" runat="server"></asp:label></div>
               
        <asp:TextBox runat="server" ID="txt_mail" PlaceHolder="Email" TextMode="Email" CssClass="form-control" /><br />
        <asp:TextBox runat="server" ID="txt_mdp" PlaceHolder="Mot de passe" TextMode="Password" CssClass="form-control" /><br />
        <asp:Button ID="btn_connecter" runat="server" Text="Se connecter" CssClass="btn btn-success" OnClick="btn_connecter_Click" /><br />

        <h6>Nouveau prof ? <a href="Teacher/NewTeacher">Créer un compte</a></h6>

</asp:Content>
