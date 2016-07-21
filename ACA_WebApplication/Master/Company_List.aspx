<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Company_List.aspx.cs" Inherits="ACA_WebApplication.Master.Company_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/FormStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function page_load() {
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rptCompany" />
            <asp:AsyncPostBackTrigger ControlID="btn_first" />
            <asp:AsyncPostBackTrigger ControlID="btn_previous" />
            <asp:AsyncPostBackTrigger ControlID="btn_next" />
            <asp:AsyncPostBackTrigger ControlID="btn_last" />
            <asp:AsyncPostBackTrigger ControlID="drp_count" />
            <asp:AsyncPostBackTrigger ControlID="txtsearch" />
        </Triggers>
        <ContentTemplate>
            <div class="page_heading">
                Company List
            </div>
            <br />
            <div class="list" style="width:98%;margin:0 auto;">
                <div style="overflow-y:auto;min-height:390px;max-height:390px;">
                    <table class="Repeater" cellspacing="0" rules="all" style="width: 99%;" border="1">
                        <tr style="background-color:#2381e9; color: white;">
                            <th scope="col" style="width: 80px">Sl No
                            </th>
                            <th scope="col" style="width: 100px">Tax Id
                            </th>
                            <th scope="col" style="width: 300px">Company Name
                            </th>
                            <th scope="col" style="width: 100px">Total Employee
                            </th>
                            <th scope="col" style="width: 150px">Last Modify
                            </th>
                            <th scope="col" style="width: 50px">View
                            </th>
                        </tr>
                        <asp:Repeater ID="rptCompany" OnItemCommand="rptCompany_ItemCommand" runat="server">
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
                                        <asp:LinkButton ID="lbl_view" CommandName="View" CommandArgument='<%# Eval("taxid") %>' Text="View" runat="server"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <table>
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
                </table>
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
</asp:Content>
