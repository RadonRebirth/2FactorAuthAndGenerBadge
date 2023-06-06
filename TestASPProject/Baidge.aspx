<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Baidge.aspx.cs" Inherits="TestASPProject.Baidge" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
    <h1>2FactorAuth
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://www.youtube.com/watch?v=dQw4w9WgXcQ&amp;ab_channel=RickAstley">🤣</asp:HyperLink>
    </h1>
                <p class="lead">Генерация бейджика<asp:Image ID="imgBadge" target="_blank" runat="server" Visible="false" CssClass="img-fluid" Height="300px" Width="400px" />
                </p>
                
                    <label for="txtFullName">Полное имя:</label>
                    <br />
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
            
                    &nbsp;<label for="txtAbout">О себе:</label><br />
                
                    <asp:TextBox ID="txtAbout" runat="server" TextMode="MultiLine" CssClass="form-control" Width="241px" MaxLength="50" style="resize: none;"></asp:TextBox>
               
                    &nbsp;<label for="fileUpload">Изображение:</label><br />
                
                    &nbsp;<asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control-file"></asp:FileUpload>
                
                <br />
                    <label for="txtLink">Ссылка на ваш ресурс:</label><br />
                
                    &nbsp;<asp:TextBox ID="txtLink" float="left" runat="server" CssClass="form-control" OnTextChanged="txtLink_TextChanged"></asp:TextBox>
        <br />
                    <asp:Button ID="btnGenerateBadge" runat="server" Text="Generate Badge" OnClick="btnGenerateBadge_Click" CssClass="btn btn-primary"></asp:Button>
            </div>
</asp:Content>