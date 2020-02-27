<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Newteacher.aspx.cs" Inherits="tutoWF.Newteacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Créer un compte enseignant</h1>
        <div class="row">
            <div class="col-md-4">

                <asp:TextBox runat="server" ID="txtName" PlaceHolder="Nom" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ErrorMessage="Nom requis" CssClass="error" />

                <asp:TextBox runat="server" ID="txtFirstName" PlaceHolder="Prenom" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtName" ErrorMessage="Nom requis" CssClass="error" />

                <asp:TextBox runat="server" ID="txtEmail" PlaceHolder="Email" TextMode="Email" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEmail" ErrorMessage="Email requis" CssClass="error" />

                <asp:TextBox runat="server" ID="txtPassword" PlaceHolder="Mot de passe" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtPassword" ErrorMessage="Mot de passe requis" CssClass="error" />

                <asp:TextBox runat="server" ID="txtPasswordConfirm" PlaceHolder="Confirmer mot de passe" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtPassword" ErrorMessage="Mot de passe requis" CssClass="error" />

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

        <asp:Button runat="server" ID="submit" Text="Créer compte" OnClick="submit_Click" CssClass="btn btn-success" /><br />

        <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>

    </div>
</asp:Content>
