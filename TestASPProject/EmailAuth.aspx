<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailAuth.aspx.cs" Inherits="TestASPProject.EmailAuth" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>2FactorAuth
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://www.youtube.com/watch?v=dQw4w9WgXcQ&amp;ab_channel=RickAstley">🤣</asp:HyperLink>
        </h1>
        <p class="lead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email авторизация</p>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Height="25px" TextMode="Email" Width="175px"></asp:TextBox>
        <br />
        <asp:Button ID="BtnSendCode" runat="server" CssClass="btn-success" OnClick="BtnSendCode_Click" Text="Отправить код" Width="148px" />
        <br />
        <p class="lead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Введите код</p>
        <asp:TextBox ID="txtSecretCode" runat="server" CssClass="form-control" Height="25px" Width="175px"></asp:TextBox>
        <br />
        <asp:Button ID="BtnLogin" runat="server" CssClass="btn-success" OnClick="BtnLogin_Click" Text="Войти в аккаунт" Width="148px" />
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="errorTxt" runat="server"></asp:Label>
        <br />
        <br />
    </div>

    <div class="row">
    </div>

</asp:Content>