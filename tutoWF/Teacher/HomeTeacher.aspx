<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomeTeacher.aspx.cs" Inherits="tutoWF.Teacher.HomeTeacher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1><asp:Label Text="" ID="lblTitle" runat="server" /></h1>
    
    <h1><asp:Label Text="" ID="lblNumber" runat="server" /></h1>
    
    <asp:Label Text="" ID="lblNumberDesc" runat="server" />


    <%if (Session["Teacher"] == null)
        { %>

    <h2>Se connecter en tant qu'élève</h2>

        <div class="error"><asp:label id="lblLoginError" runat="server"></asp:label></div>
        <br />
        <asp:TextBox runat="server" ID="txt_mail" PlaceHolder="Mail" TextMode="Email" CssClass="form-control" /><br />
        <asp:TextBox runat="server" ID="txt_mdp" PlaceHolder="Mot de passe" TextMode="Password" CssClass="form-control" /><br />
        <asp:Button ID="btn_connectStudent" runat="server" Text="Se connecter" CssClass="btn btn-success" OnClick="btn_connectStudent_Click" /><br />

        <h6>Nouvel élève ? <a href="/Student/Newstudent?id=<%Response.Write(Convert.ToInt32(Request.QueryString["id"]));%>">Créer un compte</a></h6>
        
    <%} %>

</asp:Content>
