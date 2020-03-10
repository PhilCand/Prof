<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageTeacher.aspx.cs" Inherits="tutoWF.Teacher.ManageTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hdnTeacherId" runat="server" ClientIDMode="Static" />

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

    <%-- GridView cours --%>

    <div class="listeCours">
        <h3>Liste de vos cours : </h3>
        <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control mb-2 float-right" ID="ddlState" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="all"> Selectionner un état </asp:ListItem>
            <asp:ListItem Value="Libre"> Libre </asp:ListItem>
            <asp:ListItem Value="Réservé"> Réservé </asp:ListItem>
        </asp:DropDownList>

        <asp:TextBox runat="server" TextMode="Date" AutoPostBack="true" CssClass="form-control float-right mr-2" ID="tbDateFin" OnTextChanged="tbDateDebut_TextChanged" />
        <label class="float-right mt-2 mr-1">Date fin :</label>

        <asp:TextBox runat="server" TextMode="Date" AutoPostBack="true" CssClass="form-control float-right mr-2" ID="tbDateDebut" OnTextChanged="tbDateDebut_TextChanged" />
        <label class="float-right mt-2 mr-1">Date début :</label>
        <asp:GridView runat="server" AllowSorting="true" AutoGenerateColumns="False" ID="gvEvent" DataKeyNames="Id" CssClass="table table-hover table-sm table-borderless " OnSorting="gvEvent_Sorting">
            <HeaderStyle CssClass="thead-dark" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Title" HeaderText="Titre" SortExpression="Title" />
                <asp:BoundField DataField="Start_date" HeaderText="Début" SortExpression="Start_date" />
                <asp:BoundField DataField="End_date" HeaderText="Fin" SortExpression="End_date" />
                <asp:BoundField DataField="State" HeaderText="Etat" SortExpression="State" />
                <asp:BoundField DataField="Name" HeaderText="Nom eleve" SortExpression="Name" />
                <asp:BoundField DataField="FirstName" HeaderText="Prenom eleve" SortExpression="FirstName" />
                <asp:BoundField DataField="Email" HeaderText="Email eleve" SortExpression="Email" />
            </Columns>


        </asp:GridView>
    </div>

    <%-- GridView eleve --%>

    <div class="listeEleve">
        <h3>Liste de vos élèves : </h3>

        <asp:GridView runat="server" AllowSorting="true" AutoGenerateColumns="False" ID="gvStudent" OnRowDeleting="gvStudent_RowDeleting" DataKeyNames="Id" CssClass="table table-hover table-sm table-borderless " OnRowEditing="gvStudent_RowEditing" OnSorting="gvStudent_Sorting">
            <HeaderStyle CssClass="thead-dark" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" HeaderStyle-CssClass="thead" SortExpression="Id" />
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
    </div>
    <asp:Button Text="Ajouter un élève" runat="server" ID="btn_addStudent" CssClass="btn btn-outline-primary" OnClick="btn_addStudent_Click" />



    <%} %>
</asp:Content>



