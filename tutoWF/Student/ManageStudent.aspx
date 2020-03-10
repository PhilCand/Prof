<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageStudent.aspx.cs" Inherits="tutoWF.Student.ManageStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="title">
        <h1>
            <asp:Label Text="" ID="lblManageStudent" runat="server" /></h1>
    </div>
    <label>Votre professeur : </label><asp:Label Text="" ID="lblTeacher" runat="server" />
    <br />
    <br />
    <asp:Button Text="Editer vos informations" runat="server" CssClass="btn btn-outline-primary mb-3" ID="btn_editStudent" OnClick="btn_editStudent_Click" />
    <br />
    <div class="listeCours">
        <h3>Liste de vos cours : </h3>
        <asp:TextBox runat="server" TextMode="Date" AutoPostBack="true" CssClass="form-control float-right mr-2" ID="tbDateFin" OnTextChanged="tbDateDebut_TextChanged" />
        <label class="float-right mt-2 mr-1">Date fin :</label>

        <asp:TextBox runat="server" TextMode="Date" AutoPostBack="true" CssClass="form-control float-right mr-2" ID="tbDateDebut" OnTextChanged="tbDateDebut_TextChanged" />
        <label class="float-right mt-2 mr-1">Date début :</label>
        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" ID="gvEvent" DataKeyNames="Id" CssClass="table table-hover table-sm table-borderless " OnSorting="gvEvent_Sorting" OnRowDataBound="gvEvent_RowDataBound" OnRowDeleting="gvEvent_RowDeleting">
            <HeaderStyle CssClass="thead-dark" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Title" HeaderText="Titre" SortExpression="Title" />
                <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="40%" SortExpression="Description" >
<ItemStyle Width="40%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Start_date" HeaderText="Début" SortExpression="Start_date" />
                <asp:BoundField DataField="End_date" HeaderText="Fin" SortExpression="End_date" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" CommandName="Delete" OnClientClick="return confirmDelete()" Text="Annuler" CssClass="btn btn-outline-danger btn-sm" ID="btn_cancel" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>


        </asp:GridView>
    </div>
</asp:Content>
