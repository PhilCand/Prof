<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tutoWF.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Page d'accueil générale</h1>

    <h2>Vous êtes professeur ?</h2>
    <br />
    <h4><a href="/Teacher/TeacherLogin">Se connecter</a></h4>
    <br />
    <h4><a href="/Teacher/Newteacher">Créer un acces</a></h4>
    <br />

    <h2>Vous êtes un élève ?</h2>
    <br />
    <h4>Entrez le numero fourni par votre professeur</h4>
    <br />
    <asp:TextBox runat="server" ID="tbTeacherId" CssClass="form-control"></asp:TextBox><br />   
    <asp:Button runat="server" ID="btnGoToTeacher" CssClass="btn btn-success" Text="Valider" OnClick="btnGoToTeacher_Click"/>

</asp:Content>
