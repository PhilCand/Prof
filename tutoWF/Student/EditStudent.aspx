<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditStudent.aspx.cs" Inherits="tutoWF.Student.EditStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="title">
        <h1>Edition Eleve</h1>
    </div>

    <% if (legit == true)
        { %>
    <div style="text-align:center">
    <div class="col-lg-3 mt-5" style="display:inline-block">
        <asp:TextBox runat="server" ID="txtName" PlaceHolder="Nom" CssClass="form-control input-lg" />
        <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ErrorMessage="Nom requis" CssClass="error" />

        <asp:TextBox runat="server" ID="txtFirstName" PlaceHolder="Prenom" CssClass="form-control " />
        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtFirstName" ErrorMessage="Nom requis" CssClass="error" />

        <asp:TextBox runat="server" ID="txtEmail" PlaceHolder="Email" TextMode="Email" CssClass="form-control " />
        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEmail" ErrorMessage="Email requis" CssClass="error" />

        <asp:TextBox runat="server" ID="txtPhone" PlaceHolder="Telephone" CssClass="form-control " />
        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtPhone" ErrorMessage="Nom requis" CssClass="error" />

        <asp:TextBox runat="server" ID="txtAdress" PlaceHolder="Adresse" TextMode="MultiLine" CssClass="form-control" />

        <asp:DropDownList runat="server" ID="ddlGender" CssClass="form-control mt-4 mb-4 ">
            <asp:ListItem Text="Selectionner un genre ..." Value="" />
            <asp:ListItem Text="Homme" Value="1" />
            <asp:ListItem Text="Femme" Value="0" />
        </asp:DropDownList>

        <!-- Button trigger modal -->
        <button type="button" class="btn btn-primary mb-3" data-toggle="modal" data-target="#passwordModal">
            Changer le mot de passe
        </button>

        <br />

        <a href="<%= Session["Teacher"] == null ? "/Student/ManageStudent" : "/Teacher/ManageTeacher" %>" class="btn btn-outline-danger mb-3">Retour</a>

        <asp:Button runat="server" ID="submit" Text="Valider modifications" OnClick="submit_Click" CssClass="btn btn-outline-success mb-3" /><br />

        

        <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
<%--        <div class="reserveSpace">
            <div class="alert alert-success collapse mt-4 mb-4" role="alert" id="alert_success_edit">
                Modifications enregistrées
            </div>
        </div>--%>
    </div>
        </div>
    <!-- Modal -->
    <div class="modal fade" id="passwordModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="passwordModalLabel">Changer le mot de passe</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:TextBox runat="server" ID="txtPassword" PlaceHolder="Nouveau mot de passe" TextMode="Password" CssClass="form-control" />
                    <asp:TextBox runat="server" ID="txtPasswordConfirm" PlaceHolder="Confirmer nouveau mot de passe" TextMode="Password" CssClass="form-control mt-3" />
                    <asp:Label ID="lblMessageModal" runat="server" CssClass="error"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fermer</button>
                    <asp:Button runat="server" ID="submitmdp" CssClass="btn btn-success" Text="Enregistrer" OnClick="submitmdp_Click" />
                </div>
            </div>
        </div>
    </div>

    <%}
        else
        { %>
    <h1>Vous n'êtes pas autorisé à éditer cet élève</h1>
    <%} %>
</asp:Content>
