<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageTeacher.aspx.cs" Inherits="tutoWF.Teacher.ManageTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%if (Session["Teacher"] != null)
        { %>
    <asp:Label Text="" ID="lblManageTeacher" runat="server" /><br />
    <h1><asp:Label Text="" ID="lblNumber" runat="server" /></h1>
    <asp:Label Text="" ID="lblNumberDesc" runat="server" /><br />
    <asp:HyperLink ID="hlPlanning" runat="server">Gestion du planning</asp:HyperLink>

    <%} %>
</asp:Content>



