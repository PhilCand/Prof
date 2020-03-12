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
    <asp:Button Text="Editer vos informations" runat="server" CssClass="btn btn-outline-primary mb-3" ID="btn_editTeacher" OnClick="btn_editTeacher_Click" />
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
        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" ID="gvEvent" DataKeyNames="Id" CssClass="table table-hover table-sm table-borderless " OnSorting="gvEvent_Sorting" ShowHeaderWhenEmpty="True" OnRowDeleting="gvEvent_RowDeleting" OnRowEditing="gvEvent_RowEditing" OnRowDataBound="gvEvent_RowDataBound">
            <HeaderStyle CssClass="thead-dark" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Start_date" HeaderText="Début" SortExpression="Start_date" />
                <asp:BoundField DataField="End_date" HeaderText="Fin" SortExpression="End_date" />
                <asp:BoundField DataField="State" HeaderText="Etat" SortExpression="State" />
                <asp:BoundField DataField="Name" HeaderText="Nom eleve" SortExpression="Name" />
                <asp:BoundField DataField="FirstName" HeaderText="Prenom eleve" SortExpression="FirstName" />
                <asp:BoundField DataField="Email" HeaderText="Email eleve" SortExpression="Email" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" Text="Commentaire" CssClass="btn btn-outline-info btn-sm" ID="btn_comment" OnClick="btn_comment_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" CommandName="Edit" Text="Libérer" CssClass="btn btn-outline-warning btn-sm" data-toggle="modal" ID="btn_freeEvent" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" CommandName="Delete" OnClientClick="return confirmDelete()" Text="Supprimer" CssClass="btn btn-outline-danger btn-sm" ID="btn_delete" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>


        </asp:GridView>
    </div>

    <%-- GridView eleve --%>

    <div class="listeEleve">
        <h3 class="mt-5">Liste de vos élèves : </h3>

        <asp:GridView runat="server" AllowSorting="true" AutoGenerateColumns="False" ID="gvStudent" OnRowDeleting="gvStudent_RowDeleting" DataKeyNames="Id" CssClass="table table-hover table-sm table-borderless " OnRowEditing="gvStudent_RowEditing" OnSorting="gvStudent_Sorting" ShowHeaderWhenEmpty="true">
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

    <!-- Button trigger modal -->
    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
        Launch demo modal
    </button>

    <!-- Modal -->
    <div class="modal fade" id="CommentModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Editer le commentaire</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:TextBox runat="server" TextMode="multiline" Rows="5" placeholder="Description" ID="tbModalDesc" Style="width: 100%"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" ID="btn_update_comment" Text="Valider" OnClick="btn_update_comment_Click" />
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Fermer</button>
                    <asp:HiddenField runat="server" ID="hf_event_id" />
                </div>
            </div>
        </div>
    </div>

    <%} %>
</asp:Content>



