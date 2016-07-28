<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="ACA_WebApplication.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/FormStyle.css" rel="stylesheet" />
    <div class="content-box box-toggle">
                    <h3 class="content-box-header ui-state-default">
                        <span class="float-left" style="color:black;font-weight:bold;">Source File Type</span>
                        <a href="#" class="float-right icon-separator btn toggle-button" title="Toggle Box">
                            <i class="glyph-icon icon-toggle icon-chevron-up"></i>
                        </a>
                    </h3>
                    <div style="" class="content-box-wrapper">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Table">
                    <asp:ListItem > 1095-C Datasheet</asp:ListItem>
                    <asp:ListItem > 1095-B Datasheet</asp:ListItem>
                    <asp:ListItem > Employer Information</asp:ListItem>
                    <asp:ListItem > Employee Information</asp:ListItem>
                    <asp:ListItem > Dependent Information</asp:ListItem>
                </asp:RadioButtonList>
                        <br />
            <asp:Button ID="Button1" runat="server" CssClass="btn_nav" Text="Map Fields" />
                        <br />
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="Label">Number of heading rows to skip? </asp:Label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                <asp:ListItem> Update data only(match by SSN)</asp:ListItem>
                <asp:ListItem> Delete Existing data and upload</asp:ListItem>
            </asp:CheckBoxList>
            <br />
            <asp:Button ID="Button2" runat="server"  CssClass="btn_nav" Text="Upload" />
                    </div>
    </div>
    <%--<div class="content-box box-toggle">
                    <h3 class="content-box-header ui-state-default">
                        <span class="float-left" style="color:black;font-weight:bold;">Source File Type</span>
                        <a href="#" class="float-right icon-separator btn toggle-button" title="Toggle Box">
                            <i class="glyph-icon icon-toggle icon-chevron-up"></i>
                        </a>
                    </h3>
        <div style="" class="content-box-wrapper">
            <asp:Label ID="Label1" runat="server" Text="Label">Number of heading rows to skip? </asp:Label><asp:TextBox ID="txt_answer" runat="server"></asp:TextBox>
            <asp:CheckBoxList ID="chk_update_delete" runat="server">
                <asp:ListItem> Update data only(match by SSN)</asp:ListItem>
                <asp:ListItem> Delete Existing data and upload</asp:ListItem>
            </asp:CheckBoxList>
            </br>
            <asp:Button ID="btn_upload" runat="server"  CssClass="btn_nav" Text="Upload" />
        </div>
    </div>--%>
</asp:Content>
