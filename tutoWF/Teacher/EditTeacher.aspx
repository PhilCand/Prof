<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditTeacher.aspx.cs" Inherits="tutoWF.Teacher.EditTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Editer un compte enseignant</h1>
        <div class="row">
            <div class="col-md-4">

                <asp:TextBox runat="server" ID="txtName" PlaceHolder="Nom" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ErrorMessage="Nom requis" CssClass="error" />

                <asp:TextBox runat="server" ID="txtFirstName" PlaceHolder="Prenom" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtName" ErrorMessage="Nom requis" CssClass="error" />

                <asp:TextBox runat="server" ID="txtEmail" PlaceHolder="Email" TextMode="Email" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEmail" ErrorMessage="Email requis" CssClass="error" />

                <asp:TextBox runat="server" ID="txtPhone" PlaceHolder="Telephone" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtName" ErrorMessage="Nom requis" CssClass="error" />

                <asp:TextBox runat="server" ID="txtAdress" PlaceHolder="Adresse" TextMode="MultiLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtAdress" ErrorMessage="Adresse requiss" CssClass="error" />

                <asp:TextBox runat="server" ID="txtDescription" PlaceHolder="Description" TextMode="MultiLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtDescription" ErrorMessage="Description requise" CssClass="error" />

                <asp:DropDownList runat="server" ID="ddlGender" CssClass="form-control">
                    <asp:ListItem Text="Selectionner un genre ..." Value="" />
                    <asp:ListItem Text="Homme" Value="1" />
                    <asp:ListItem Text="Femme" Value="0" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="ddlGender" ErrorMessage="Genre requis" CssClass="error" />

            </div>
            <div class="col-md-4">
                <label>Disciplines</label>
                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lbSubject" CssClass="form-control" Height="400px" Font-Size="Large"></asp:ListBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="lbSubject" ErrorMessage="Selectionnez au moins une discipline" CssClass="error" />


            </div>

        </div>
        <!-- Button trigger modal -->
        <button type="button" class="btn btn-primary mb-3" data-toggle="modal" data-target="#passwordModal">
            Changer le mot de passe
        </button>
        <br />
        <a href="<%= Session["Teacher"] == null ? "/Student/ManageStudent" : "/Teacher/ManageTeacher" %>" class="btn btn-outline-danger mb-3">Retour</a>
        <asp:Button runat="server" ID="submit" Text="Valider modifications" OnClick="submit_Click" CssClass="btn btn-outline-success mb-3" /><br />
        <asp:Label ID="Label1" runat="server" CssClass="error"></asp:Label>
        <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
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
</asp:Content>
