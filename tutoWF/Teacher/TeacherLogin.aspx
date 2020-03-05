<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeacherLogin.aspx.cs" Inherits="tutoWF.Teacher.TeacherLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="title">
        <h1>Se connecter : Professeur</h1>
    </div>

    <div class="login">
        <div class="dashed">
            <div class="error">
                <asp:Label ID="lblLoginError" runat="server"></asp:Label>
            </div>

            <asp:TextBox runat="server" ID="txt_mail" PlaceHolder="Email" TextMode="Email" CssClass="form-control login-input" /><br />
            <asp:TextBox runat="server" ID="txt_mdp" PlaceHolder="Mot de passe" TextMode="Password" CssClass="form-control login-input" /><br />
            <asp:Button ID="btn_connecter" runat="server" Text="Se connecter" CssClass="btn btn-outline-success" OnClick="btn_connecter_Click" /><br />
            <br />
            <h6 >Nouveau prof ? <a href="Teacher/NewTeacher" class:"lblNew">Créer un compte</a></h6>
        </div>
    </div>

</asp:Content>
