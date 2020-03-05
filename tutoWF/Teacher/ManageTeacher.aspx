<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageTeacher.aspx.cs" Inherits="tutoWF.Teacher.ManageTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%if (Session["Teacher"] != null)
        {
    %>
    <div class="title">
        <h1>
            <asp:Label Text="" ID="lblManageTeacher" runat="server" /></h1>
    </div>



    <asp:Label Text="" ID="lblNumberDesc" runat="server" />
    <asp:Label Text="" ID="lblNumber" runat="server" CssClass="studentNum" />
    <br />
    <asp:HyperLink ID="hlPlanning" runat="server" CssClass="btn btn-outline-primary mb-3">Gestion du planning</asp:HyperLink>
    <br />

    <%-- test liste eleve --%>

    <h3>Liste de vos élèves : </h3>

    <asp:GridView runat="server" AutoGenerateColumns="False" ID="gvStudent" OnRowDeleting="gvStudent_RowDeleting" DataKeyNames="Id" CssClass="table table-hover table-sm table-borderless " OnRowEditing="gvStudent_RowEditing">
        <HeaderStyle CssClass="thead-dark" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
            <asp:BoundField DataField="Name" HeaderText="Nom" SortExpression="Name" />
            <asp:BoundField DataField="FirstName" HeaderText="Prenom" SortExpression="FirstName" />
            <asp:BoundField DataField="Phone" HeaderText="Telephone" SortExpression="Phone" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" CommandName="Edit" Text="Editer" CssClass="btn btn-outline-info btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" CommandName="Delete" OnClientClick="return confirmDelete()" Text="Supprimer" CssClass="btn btn-outline-danger btn-sm" />
                </ItemTemplate>

            </asp:TemplateField>
        </Columns>

    </asp:GridView>

    <%} %>
</asp:Content>



