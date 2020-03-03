<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tutoWF.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="title">
        <h1 class="display-4" >Bienvenue sur réserve ton prof</h1>
    </div>
        

    


    <div class="jumbotron">
        <h1 class="display-4">Vous êtes un élève ?</h1>
        <p class="lead">Entrez le numero fourni par votre professeur</p>
        <hr class="my-4">
        <div class="col-xs-2">
            <asp:TextBox runat="server" ID="tbTeacherId" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnGoToTeacher" CssClass="btn btn-success" Text="Valider" OnClick="btnGoToTeacher_Click" />
    </div>

    <div class="jumbotron">
        <h1 class="display-4">Vous êtes professeur ?</h1>
        <p class="lead">Connectez vous ou créez votre compte pour proposer à vos élèves de s'inscrire sur votre planning</p>
        <hr class="my-4">
        <a href="/Teacher/TeacherLogin" class="btn btn-primary btn-outline btn-lg" id="btn_connect" >Se connecter </a>
        <a href="/Teacher/Newteacher" class="btn btn-primary btn-outline btn-lg" >Créer un compte</a>



    </div>

</asp:Content>
