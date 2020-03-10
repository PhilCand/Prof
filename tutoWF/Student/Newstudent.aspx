<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Newstudent.aspx.cs" Inherits="tutoWF.Newstudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div id="title">
            <h1>Créer un compte élève</h1>
            <asp:Label Text="" ID="lblCreateStudent" runat="server" />
        </div>


        <%if (Session["Teacher"] == null || LoggedTeacherId == UrlTeacherId)
            {  %>
        <div style="text-align: center">
            <div class="col-lg-3 mt-5" style="display: inline-block">
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

                <asp:DropDownList runat="server" ID="ddlGender" CssClass="form-control mt-4 mb-4">
                    <asp:ListItem Text="Selectionner un genre ..." Value="" />
                    <asp:ListItem Text="Homme" Value="1" />
                    <asp:ListItem Text="Femme" Value="0" />
                </asp:DropDownList>

                <asp:Button runat="server" ID="submit" Text="Créer compte" OnClick="submit_Click" CssClass="btn btn-success" /><br />

                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
            </div>
        </div>
        <%}
            else
            {%>
        <h3 class="error">Vous n'êtes pas autorisé à créer un élève pour ce professeur</h3>
        <%} %>
    </div>
</asp:Content>
