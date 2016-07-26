<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Test2.aspx.cs" Inherits="ACA_WebApplication.Test2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Original Text:<asp:TextBox ID="txto1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Next" OnClick="Button1_Click" />
    Encrypted    :<asp:TextBox ID="txte1" runat="server"></asp:TextBox>


    <br />
    <br />
    <br />
    <br />


    Encrypted    :<asp:TextBox ID="txte2" runat="server"></asp:TextBox>
    <asp:Button ID="Button2" runat="server" Text="Next" OnClick="Button2_Click" />
    Original Text:<asp:TextBox ID="txto2" runat="server"></asp:TextBox>

</asp:Content>
