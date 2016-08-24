<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Company_List.aspx.cs" Inherits="ACA_WebApplication.Master.Company_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/FormStyle.css" rel="stylesheet" />
    <link href="../css/Employer.css" rel="stylesheet" />
    <script type="text/javascript">
        function page_load() {
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <asp:PostBackTrigger ControlID="img_but_excell" />
            <asp:PostBackTrigger ControlID="img_btn_pdf" />
            <asp:AsyncPostBackTrigger ControlID="btn_refresh" />
            <asp:AsyncPostBackTrigger ControlID="lbl_close" />
        </Triggers>
        <ContentTemplate>
                <%--<div class="heading">Companies List</div>--%>
            <%--<br />--%>
            <div class="list" style="width:98%;margin:0 auto;">
               <table style="width:99%;">
                    <tr>
                        <td style="width:50%;float:left;">
                       <div class="srch" style="margin-top:5px;">
                        <asp:TextBox ID="txtsearch" cssclass="srchbox" Width="90%" ClientIDMode="AutoID" AutoPostBack="true" runat="server" placeholder="Search Employee" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                        <a href="#" class="btn medium bg-green" title="Search" style="float:right;height:100%;">
                            <span class="button-content">
                                <i class="glyph-icon icon-search font-white"></i>
                            </span>
                        </a>
                        </div>
                            <asp:LinkButton class="btn medium bg-blue" ID="btn_refresh" ClientIDMode="AutoID" style="border-radius:5px; height:32px; padding: 0px; margin-bottom: 3px;" runat="server" OnClick="Refresh"><span class="button-content">
                <i class="glyph-icon icon-refresh font-white"></i>
            </span></asp:LinkButton>
                           <%-- <a href="#" class="btn medium" title="Search" style="background-color:#0294A5; border-radius:5px;padding:0px;margin-bottom:1px;" runat="server" onclick="ExportToExcel" >
             <span class="button-content">
                <i class="glyph-icon icon-refresh font-white"></i>
            </span>
        </a>--%>
                        </td>
                       <%-- <td style="float:right;">Show <asp:DropDownList ID="drp_count" runat="server" AutoPostBack="True" CssClass="drp_txt" OnSelectedIndexChanged="drp_count_SelectedIndexChanged">
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList> Entries</td>--%>
                        <td style="float:right;margin-top: 5px;">
                            <asp:ImageButton ID="img_btn_pdf" runat="server" ImageUrl="~/icons/PDF.png" ImageAlign="Middle" style="margin-bottom: 7px;" OnClick="ExportToPDF" />
                            <asp:ImageButton ID="img_but_excell" runat="server" ImageUrl="~/icons/excel.png" ImageAlign="Middle" style="margin-bottom: 7px;" OnClick="ExportToExcel" />
                           | Show <asp:DropDownList ID="drp_count" runat="server" ClientIDMode="AutoID" AutoPostBack="True" CssClass="drp_txt" OnSelectedIndexChanged="drp_count_SelectedIndexChanged">
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList> Entries</td>
                    </tr>
                </table>
                <div  <%--style="overflow-y:auto;min-height:390px;max-height:453px;"--%>>
                        <table cellspacing="0" cellpadding="0" border="0" class="Repeater" rules="all" style="width: 100%;border-color:#b1c7ca;">
                            <tr>
                                <td>
                                    <div style="width:100%; height:30px; overflow:auto;">
                                    <table style="width: 100%;">
                                        <tr style="height:30px;" class="bg-twitter opacity-60">
                                            <th style="width: 7%;">S.No </th>
                                            <th style="width: 13%;">Tax Id </th>
                                            <th style="width: 60%;">Company Name </th>
                                            <th style="width: 10%;">Total Employees </th>
                                            <th style="width: 15%;">Last Modified </th>
                                            <th style="width: 15%;padding-right:10px;">Action</th>
                                        </tr>
                                    </table>
                                        </div>
                                </td>
                            </tr>
                <tr>
                    <td>
                        <div style="width:100%; max-height:500.5px; overflow:auto;background-color:rgba(255,255,255, 0.6);">
                            <table >
                                <tbody>
                                    <asp:Repeater ID="rptCompany" runat="server" OnItemCommand="rptCompany_ItemCommand" >
                                    <ItemTemplate>
                                <tr style="text-align: center;border:1px solid #b1c7ca; line-height: 1.4;" class="thhover">
                                    <td style="width: 7%;">
                                        <asp:Label ID="lbl_no" runat="server" Text='<%# Eval("RowNumber") %>' />
                                    </td>
                                    <td style="width: 13%;">
                                        <asp:Label ID="lbl_taxid" runat="server" Text='<%# Eval("taxid") %>' />
                                    </td>
                                    <td style="text-align:left;width:60%;">
                                        <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("name") %>' />
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:Label ID="lbl_totalee" runat="server" Text='<%# Eval("totalNumberEE") %>' />
                                    </td>
                                    <td style="width: 15%;">
                                        <asp:Label ID="lbl_edit" runat="server" Text='<%# Eval("lastEdit","{0:MM/dd/yyyy}") %>' />
                                    </td>
                                    <td style="width: 15%;padding:0px 6px 0px 6px;">
                                        <asp:LinkButton ID="lbl_view" runat="server" CommandArgument='<%# Eval("taxid") %>' CommandName="View" Text="View" ForeColor="Green" style="font-size:17px;"><i class="glyph-icon icon-check"></i></asp:LinkButton>
                                    </td>
                                    <td></td>
                                </tr>
                                    </ItemTemplate>
                                    </asp:Repeater>
                        </tbody>
                            </table>  
                        </div>
                    </td>
                </tr>
                    </table>
                </div>
                <table style="width:99%;margin-top:5px;">
                    <tr>
                        <td style="width:50%;float:left;height:25px;">
                            <asp:Label ID="lbl_result" runat="server" Text=""></asp:Label>
                        </td>
                        <td  style="float:right;">
                            <div class="button-group">
                            <asp:LinkButton ID="btn_first" runat="server" ClientIDMode="AutoID" CssClass="btn medium bg-twitter radius-top-left-10 radius-bottom-left-10" ForeColor="White" OnClick="btn_first_Click" Font-Bold="False">
                                <span  class="button-content"><i class="glyph-icon icon-caret-left"></i><i class="glyph-icon icon-caret-left"></i>&nbsp;&nbsp;First</span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_previous" runat="server"  ClientIDMode="AutoID" CssClass="btn medium bg-twitter" ForeColor="White" OnClick="btn_previous_Click" Font-Bold="False">
                                <span class="button-content"><i class="glyph-icon icon-caret-left"></i>&nbsp;&nbsp;Prev</span>
                            </asp:LinkButton>  
                           <a href="javascript:;" class="btn medium bg-twitter" style="cursor:default;"><span class="glyph-icon icon-separator"><asp:Label ID="lbl_pagenum" Font-Size="12px" runat="server" Text="" ForeColor="White"></asp:Label></span></a>
                            <asp:LinkButton ID="btn_next" runat="server" ClientIDMode="AutoID" CssClass="btn medium medium bg-twitter" ForeColor="White" OnClick="btn_next_Click" Font-Bold="False">
                                <span class="button-content">Next&nbsp;&nbsp;<i class="glyph-icon icon-caret-right"></i></span>
                            </asp:LinkButton> 
                            <asp:LinkButton ID="btn_last" runat="server" ClientIDMode="AutoID" CssClass="btn medium bg-twitter radius-top-right-10 radius-bottom-right-10" ForeColor="White" OnClick="btn_last_Click" Font-Bold="False" >
                                <span class="button-content">Last&nbsp;&nbsp;<i class="glyph-icon icon-caret-right"></i><i class="glyph-icon icon-caret-right"></i></span>
                            </asp:LinkButton>
                                </div>
                            <%--<asp:Button  Text="&lt;&lt;" />--%>
                            <%--<asp:Button ID="btn_previous" runat="server" CssClass="btn_nav" OnClick="btn_previous_Click" Text="&lt;" />
                            <asp:Label ID="lbl_pagenum" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btn_next" runat="server" CssClass="btn_nav" OnClick="btn_next_Click" Text="&gt;" />
                            <asp:Button ID="btn_last" runat="server" CssClass="btn_nav" OnClick="btn_last_Click" Text="&gt;&gt;" />--%>
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
            </div>
            <div style="clear: both;"></div>
            <div id="lightDiv" runat="server" visible="false" class="white_content">
                <asp:Label runat="server" ForeColor="White" Text="" ID="lbl_msg"></asp:Label>
                <asp:LinkButton ID="lbl_close" Style="float: right; margin: 45px 13px; background-color: #130E0E; padding: 1% 3%; text-decoration: none; border-radius: 3px; color: #CEA937;"
                    Text="Close" runat="server" OnClick="lbl_close_Click"></asp:LinkButton>
            </div>
            <div id="fadeDiv" runat="server" visible="false" class="black_overlay"></div>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
            </div>
</asp:Content>
