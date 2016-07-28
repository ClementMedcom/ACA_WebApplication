<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Company_List.aspx.cs" Inherits="ACA_WebApplication.Master.Company_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/FormStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function page_load() {
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-box" style="width:99%;margin:2px auto; border:none;">
            <h3 class="content-box-header ui-state-default primary-bg" style="font-size:16px;text-align:center;">Company List
            </h3>
    </div>
    <div style="" class="content-box-wrapper">
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rptCompany" />
            <asp:AsyncPostBackTrigger ControlID="btn_first" />
            <asp:AsyncPostBackTrigger ControlID="btn_previous" />
            <asp:AsyncPostBackTrigger ControlID="btn_next" />
            <asp:AsyncPostBackTrigger ControlID="btn_last" />
            <asp:AsyncPostBackTrigger ControlID="drp_count" />
            <asp:AsyncPostBackTrigger ControlID="txtsearch" />
            <asp:PostBackTrigger ControlID="img_but_export" />
        </Triggers>
        <ContentTemplate>
            <%--<div class="page_heading">
                Company List
            </div>--%>
            <%--<br />--%>
            <div class="list" style="width:98%;margin:0 auto;">
               <table style="width:99%;">
                    <tr>
                        <td style="width:50%;float:left;">
                            <asp:TextBox ID="txtsearch" CssClass="txt" AutoPostBack="true" runat="server" placeholder="Search"  OnTextChanged="txtsearch_TextChanged" Width="50%" Height="100%"></asp:TextBox>
                           <a href="#" class="btn medium" title="Search" style="background-color:#0294A5; border-radius:5px;padding:0px;margin-bottom:1px;" >
             <span class="button-content">
                <i class="glyph-icon icon-search font-white"></i>
            </span>
        </a>
                        </td>
                       <%-- <td style="float:right;">Show <asp:DropDownList ID="drp_count" runat="server" AutoPostBack="True" CssClass="drp_txt" OnSelectedIndexChanged="drp_count_SelectedIndexChanged">
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList> Entries</td>--%>
                        <td style="float:right;">
                            <asp:ImageButton ID="img_but_export" runat="server" ImageUrl="~/icons/excel.png" ImageAlign="Middle" style="margin-bottom: 7px;" OnClick="ExportToExcel" />
                           | Show <asp:DropDownList ID="drp_count" runat="server" AutoPostBack="True" CssClass="drp_txt" OnSelectedIndexChanged="drp_count_SelectedIndexChanged">
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList> Entries</td>
                    </tr>
                </table>
                <div style="overflow-y:auto;min-height:390px;max-height:453px;">
                    <table border="1" cellspacing="0" class="Repeater" rules="all" style="width: 99%;">
                        <tr style="color: white;text-align: center;" class="primary-bg">
                            <td style="width: 30px">S.No </td>
                            <td style="width: 50px">Tax Id </td>
                            <td style="width: 400px">Company Name </td>
                            <td style="width: 40px">Total Employee </td>
                            <td style="width: 90px">Last Modify </td>
                            <td style="width: 50px"></td>
                        </tr>
                        <asp:Repeater ID="rptCompany" runat="server" OnItemCommand="rptCompany_ItemCommand">
                            <ItemTemplate>
                                <tr style="text-align: center;">
                                    <td>
                                        <asp:Label ID="lbl_no" runat="server" Text='<%# Eval("RowNumber") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_taxid" runat="server" Text='<%# Eval("taxid") %>' />
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("name") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_totalee" runat="server" Text='<%# Eval("totalNumberEE") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_edit" runat="server" Text='<%# Eval("lastEdit") %>' />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbl_view" runat="server" CommandArgument='<%# Eval("taxid") %>' CommandName="View" Text="View" ForeColor="Blue"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <table style="width:99%;">
                    <tr>
                        <td style="width:50%;float:left;height:25px;">
                            <asp:Label ID="lbl_result" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="float:right;">
                            <asp:Button ID="btn_first" runat="server" CssClass="btn_nav" OnClick="btn_first_Click" Text="&lt;&lt;" />
                            <asp:Button ID="btn_previous" runat="server" CssClass="btn_nav" OnClick="btn_previous_Click" Text="&lt;" />
                            <asp:Label ID="lbl_pagenum" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btn_next" runat="server" CssClass="btn_nav" OnClick="btn_next_Click" Text="&gt;" />
                            <asp:Button ID="btn_last" runat="server" CssClass="btn_nav" OnClick="btn_last_Click" Text="&gt;&gt;" />
                            <asp:HiddenField ID="hid_rowcount" runat="server" />
                        </td>
                    </tr>
                </table>
                <%--<table>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtsearch" CssClass="txt" AutoPostBack="true" runat="server" placeholder="Search" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                        </td>
                        <td colspan="4">
                            <asp:Button ID="btn_first" CssClass="btn_nav" runat="server" Text="<<" OnClick="btn_first_Click" />
                            <asp:Button ID="btn_previous" CssClass="btn_nav" runat="server" Text="<" OnClick="btn_previous_Click" />
                            <asp:Label ID="lbl_pagenum" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btn_next" CssClass="btn_nav" runat="server" Text=">" OnClick="btn_next_Click" />
                            <asp:Button ID="btn_last" CssClass="btn_nav" runat="server" Text=">>" OnClick="btn_last_Click" />
                            <asp:DropDownList ID="drp_count" runat="server" AutoPostBack="True" CssClass="drp_txt" OnSelectedIndexChanged="drp_count_SelectedIndexChanged">
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lbl_result" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hid_rowcount" runat="server" />
                        </td>
                    </tr>
                </table>--%>
                <br>
                <br></br>
                <br>
                <br></br>
                </br>
                </br>
            </div>
            <div style="clear: both;"></div>
            <%--<div id="lightDiv" runat="server" visible="false" class="white_content">
                <asp:Label runat="server" ForeColor="White" Text="" ID="lbl_msg"></asp:Label>
                <asp:LinkButton ID="lbl_close" Style="float: right; margin: 45px 13px; background-color: #130E0E; padding: 1% 3%; text-decoration: none; border-radius: 3px; color: #CEA937;"
                    Text="Close" runat="server" OnClick="lbl_close_Click"></asp:LinkButton>
            </div>
            <div id="fadeDiv" runat="server" visible="false" class="black_overlay"></div>
            </div>--%>
            
        </ContentTemplate>
    </asp:UpdatePanel>
            </div>
</asp:Content>
