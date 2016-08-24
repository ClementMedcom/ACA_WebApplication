<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Employer_Details.aspx.cs" Inherits="ACA_WebApplication.Master.Employer_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../css/Employer.css" rel="stylesheet" />
    <link href="../css/FormStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function page_load() {
            $('.grid_list').on('click', function () {
<<<<<<< HEAD
                $('.grid_list').css('background-position', 'left');
                $(this).css({ 'background-position': 'right' });
=======
                $('.over').css( 'background', 'none');
                $(this).css({ 'background-color': '#dedede' });
>>>>>>> origin/master
            });
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_save" />
            <asp:AsyncPostBackTrigger ControlID="lbl_close" />
              <%--<asp:AsyncPostBackTrigger ControlID="rptEmployer"/>--%>
            <asp:AsyncPostBackTrigger ControlID="btn_delete" />
        </Triggers>
        <ContentTemplate>
           <%-- <div class="heading">Employer Details</div>--%>

            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_first" />
                    <asp:AsyncPostBackTrigger ControlID="btn_previous" />
                    <asp:AsyncPostBackTrigger ControlID="btn_next" />
                    <asp:AsyncPostBackTrigger ControlID="btn_last" />
                    <asp:AsyncPostBackTrigger ControlID="drp_count" />
                    <asp:AsyncPostBackTrigger ControlID="txtsearch" />
                    <asp:AsyncPostBackTrigger ControlID="btn_refresh" />
                    <asp:AsyncPostBackTrigger ControlID="btn_reset" />
                </Triggers>
                <ContentTemplate>
                    <div class="grid_container">

<<<<<<< HEAD
                        <div class="srch">
                        <asp:TextBox ID="txtsearch" cssclass="srchbox" Width="81%" ClientIDMode="AutoID" AutoPostBack="true" runat="server" placeholder="Search Employee" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                        <a href="#" class="btn medium bg-green" title="Search" style="float:right;height:100%;">
=======
                        <asp:TextBox ID="txtsearch" CssClass="srch" AutoPostBack="true" ClientIDMode="AutoID" runat="server" placeholder="Search Employer" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                        <a href="#" class="btn medium bg-green" title="Search" style="/*background-color: #0294A5; */ border-radius: 5px; padding: 0px; margin-bottom: 5px;">
>>>>>>> origin/master
                            <span class="button-content">
                                <i class="glyph-icon icon-search font-white"></i>
                            </span>
                        </a>
<<<<<<< HEAD
                        </div>
                        <asp:LinkButton class="btn medium bg-blue" ID="btn_refresh" ClientIDMode="AutoID" Style="border-radius: 5px;  height:32px; padding: 0px; margin-bottom: 3px;" runat="server" OnClick="Refresh"><span class="button-content">
                <i class="glyph-icon icon-refresh font-white"></i>
            </span></asp:LinkButton>
                        <hr / style="margin-top: 0em;margin-bottom: 0em; ">
                        <div style="overflow-y: auto; min-height: 498px; max-height: 498px; width: 100%; overflow-x: hidden; background-color: rgba(255,255,255, 0.6);">
=======
                        <asp:LinkButton class="btn medium bg-blue" ID="btn_refresh" ClientIDMode="AutoID" Style="border-radius: 5px; padding: 0px; margin-bottom: 5px;" runat="server" OnClick="Refresh"><span class="button-content">
                <i class="glyph-icon icon-refresh font-white"></i>
            </span></asp:LinkButton>

                        <div style="overflow-y: auto; min-height: 498px; max-height: 498px; width: 100%; overflow-x: hidden; background-color: white;">
>>>>>>> origin/master

                            <asp:Repeater ID="rptEmployer" OnItemCommand="rptEmployer_ItemCommand" OnItemDataBound="rptEmployer_OnItemDataBound" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lb_emp_list" ClientIDMode="AutoID" Style="text-decoration: none;" CommandName="Edit" runat="server">
                                        <div class="grid_list over">
<<<<<<< HEAD
                                            <div class="serial" id="mainemployer" runat="server">
                                                <%# Eval("RowNumber") %>
                                            </div>
                                            <div class="detail">
                                                <b>Tax Id :</b> <%# Eval("EmployerTaxId") %> &nbsp;&nbsp;&nbsp;<asp:Image Style="float: right;margin-right:20px;" ID="img_flag" runat="server" /><br />
                                                <%# Eval("name") %>
=======
                                            <div class="serial bg-twitter opacity-60" id="mainemployer" runat="server">
                                                <%# Eval("RowNumber") %>
                                            </div>
                                            <div class="detail">
                                                <b>Tax Id :</b> <%# Eval("EmployerTaxId") %> &nbsp;&nbsp;&nbsp;<asp:Image Style="float: right;" ID="img_flag" runat="server" /><br />
                                                <%# Eval("name") %><br />

>>>>>>> origin/master
                                            </div>
                                            <asp:HiddenField ID="hdn_EmpTax_Id" Value='<%# Eval("EmployerTaxId") %>' runat="server" />
                                            <asp:HiddenField ID="hdn_CompanyTax_Id" Value='<%# Eval("CompanyTaxID") %>' runat="server" />
                                        </div>
<<<<<<< HEAD
                                        <hr / style="margin-top: 0em;margin-bottom: 0em; ">
=======
                                        <%--<hr />--%>
>>>>>>> origin/master
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
<<<<<<< HEAD
                        <table style="width: 100%;text-align:center;">
                            <tr style="background-color: rgba(255,255,255, 0.6);">
                                <td colspan="2">
=======
                        <table style="width: 100%;">
                            <tr style="background-color: white;">
                                <th colspan="2">
>>>>>>> origin/master
                                    <asp:Label ID="lbl_result" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hid_rowcount" runat="server" />
                                    <asp:HiddenField ID="hdn_companytax_id" runat="server" />
                                    &nbsp;&nbsp;&nbsp;
                          <%--</th>
                                <td>--%>
                                    <asp:DropDownList ID="drp_count" CssClass="drp_txt" ClientIDMode="AutoID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drp_count_SelectedIndexChanged">
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                    </asp:DropDownList><br />
<<<<<<< HEAD
                                </td>
=======
                                </th>
                            </tr>
                            <tr>
                                <th colspan='2'>
                                    <div class="button-group" style="margin: 5px 0px 0px 15px;">
                                        <asp:LinkButton ID="btn_first" ClientIDMode="AutoID" runat="server" CssClass="btn medium bg-twitter radius-top-left-10 radius-bottom-left-10" ForeColor="White" OnClick="btn_first_Click" Font-Bold="False">
                                <span  class="button-content"><i class="glyph-icon icon-caret-left"></i><i class="glyph-icon icon-caret-left"></i>&nbsp;&nbsp;First</span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btn_previous" runat="server" ClientIDMode="AutoID" CssClass="btn medium bg-twitter" ForeColor="White" OnClick="btn_previous_Click" Font-Bold="False">
                                <span class="button-content"><i class="glyph-icon icon-caret-left"></i>&nbsp;&nbsp;Prev</span>
                                        </asp:LinkButton>
                                        <a href="javascript:;" class="btn medium bg-twitter" style="cursor: default;"><span class="glyph-icon icon-separator">
                                            <asp:Label ID="lbl_pagenum" Font-Size="12px" runat="server" Text="" ForeColor="White"></asp:Label></span></a>
                                        <asp:LinkButton ID="btn_next" runat="server" ClientIDMode="AutoID" CssClass="btn medium medium bg-twitter" ForeColor="White" OnClick="btn_next_Click" Font-Bold="False">
                                <span class="button-content">Next&nbsp;&nbsp;<i class="glyph-icon icon-caret-right"></i></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btn_last" runat="server" ClientIDMode="AutoID" CssClass="btn medium bg-twitter radius-top-right-10 radius-bottom-right-10" ForeColor="White" OnClick="btn_last_Click" Font-Bold="False">
                                <span class="button-content">Last&nbsp;&nbsp;<i class="glyph-icon icon-caret-right"></i><i class="glyph-icon icon-caret-right"></i></span>
                                        </asp:LinkButton>
                                    </div>
                                </th>
>>>>>>> origin/master
                            </tr>
                            <tr>
                                <th colspan='2'>
                                    <div class="button-group" style="margin: 5px 0px 0px 0px;">
                                        <asp:LinkButton ID="btn_first" ClientIDMode="AutoID" runat="server" CssClass="btn medium bg-twitter radius-top-left-10 radius-bottom-left-10" ForeColor="White" OnClick="btn_first_Click" Font-Bold="False">
                                <span  class="button-content"><i class="glyph-icon icon-caret-left"></i><i class="glyph-icon icon-caret-left"></i>&nbsp;&nbsp;First</span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btn_previous" runat="server" ClientIDMode="AutoID" CssClass="btn medium bg-twitter" ForeColor="White" OnClick="btn_previous_Click" Font-Bold="False">
                                <span class="button-content"><i class="glyph-icon icon-caret-left"></i>&nbsp;&nbsp;Prev</span>
                                        </asp:LinkButton>
                                        <a href="javascript:;" class="btn medium bg-twitter" style="cursor: default;"><span class="glyph-icon icon-separator">
                                            <asp:Label ID="lbl_pagenum" Font-Size="12px" runat="server" Text="" ForeColor="White"></asp:Label></span></a>
                                        <asp:LinkButton ID="btn_next" runat="server" ClientIDMode="AutoID" CssClass="btn medium medium bg-twitter" ForeColor="White" OnClick="btn_next_Click" Font-Bold="False">
                                <span class="button-content">Next&nbsp;&nbsp;<i class="glyph-icon icon-caret-right"></i></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btn_last" runat="server" ClientIDMode="AutoID" CssClass="btn medium bg-twitter radius-top-right-10 radius-bottom-right-10" ForeColor="White" OnClick="btn_last_Click" Font-Bold="False">
                                <span class="button-content">Last&nbsp;&nbsp;<i class="glyph-icon icon-caret-right"></i><i class="glyph-icon icon-caret-right"></i></span>
                                        </asp:LinkButton>
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_save" />
                    <asp:AsyncPostBackTrigger ControlID="btn_reset" />
                    <asp:AsyncPostBackTrigger ControlID="rptEmployer" />
                    <asp:AsyncPostBackTrigger ControlID="lbl_close" />
                    <asp:AsyncPostBackTrigger ControlID="TabContainer1$cdetailstab$rpt_montable" />
                    <asp:AsyncPostBackTrigger ControlID="btn_delete" />
                </Triggers>
                <ContentTemplate>
                    <div class="employer_form">
                        <div class="heading">Employer Details</div>
                        <ajaxToolkit:TabContainer CssClass="fancy fancy-green" ID="TabContainer1" runat="server">
                            <ajaxToolkit:TabPanel ID="generalinfotab" TabIndex="0" runat="server" Style="height:467px;">
<<<<<<< HEAD
                                <HeaderTemplate >
                                   General Information
=======
                                <HeaderTemplate>
                                    General Information
>>>>>>> origin/master
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="generalinfopanel" runat="server">
                                        <div class="gen_info">
                                            <table style="width:80%;height:440px; margin:0 auto;">
                                                <tr>
                                                    <td style="width:200px;">Employer Name</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txt_employerName" class="txt" Style="width:95%;" runat="server"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ControlToValidate="txt_employerName" ID="RFV1" ForeColor="Red" ValidationGroup="save" runat="server" Display="None" ErrorMessage="<b> Missing Field</b><br />A name is required."></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender PopupPosition="Left" Width="200px" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender1" TargetControlID="RFV1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>EIN (eg:99-9999999)</td>
                                                    <td>
                                                        <asp:TextBox ID="txt_ein" MaxLength="10" class="txt" Style="width:95%;" runat="server"></asp:TextBox>
                                                        <br />
                                                        <asp:RegularExpressionValidator ForeColor="Red" ValidationGroup="save" ValidationExpression="^\d{2}-\d{7}$" ID="Regxval1" Display="None" ControlToValidate="txt_ein" runat="server" ErrorMessage="<b> Error Field</b><br />Invalid EIN."></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ControlToValidate="txt_ein" ID="RFV2" Display="None" ForeColor="Red" ValidationGroup="save" runat="server" ErrorMessage="<b> Missing Field</b><br />A EIN is required."></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender Width="200px" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender2" TargetControlID="Regxval1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                                        <ajaxToolkit:ValidatorCalloutExtender Width="200px" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender3" TargetControlID="RFV2" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                                    </td>
                                                    <td style="width:90px;">Filing Year</td>
                                                    <td>
                                                        <asp:DropDownList ID="drp_fillingyear" CssClass="cmb" Style="width:90%;" runat="server">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Address</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txt_address1" class="txt"  Style="width:95%;" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Address 1</td>
                                                    <td colspan="3"> 
                                                        <asp:TextBox ID="txt_address2" class="txt" Style="width:95%;" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>City</td>
                                                    <td>
                                                        <asp:TextBox ID="txt_city" class="txt" Style="width:95%;" runat="server"></asp:TextBox>
                                                    </td>
                                                
                                                    <td>State</td>
                                                    <td>
                                                        <asp:DropDownList ID="drp_state" CssClass="cmb" Style="width:90%;" runat="server">
                                                            <asp:ListItem cssclass="cmb">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                     <td>Country</td>
                                                    <td>
                                                        <asp:DropDownList ID="drp_country" CssClass="cmb" Style="width:98%;" runat="server">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>

                                                    <td>Zip Code</td>
                                                    <td>
                                                        <asp:TextBox ID="txt_zipcode" class="txt" Style="width:88%;" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Contact Name</td>
                                                    <td >
                                                        <asp:TextBox ID="txt_contactname" class="txt" Style="width:95%;" runat="server"></asp:TextBox>
                                                    </td>
                                                
                                                    <td>Contact Phone</td>
                                                    <td >
                                                        <asp:TextBox ID="txt_contactphone" class="txt" Style="width:88%;" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Title</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txt_title" class="txt" Style="width:95%;" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                   <td>SHOP Identifier</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txt_shop" class="txt" Style="width:95%;" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                               
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="cdetailstab" TabIndex="1" runat="server" Style="height:467px;">
                                <HeaderTemplate>
                                    1094 Details
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="cdetailspanel" runat="server">
                                        <div class="c_details">
<<<<<<< HEAD
                                            <table style="width:90% ;margin:0 auto;"> 
                                                 <tr >
                                                    <td style="width:80px;">Form Type</td>
                                                    <td>
                                                        <asp:DropDownList ID="drp_formtype" CssClass="cmb" Style="width:95%;" runat="server">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width:80px;">Origin Code</td>
                                                    <td>
                                                        <asp:DropDownList ID="drp_origincode" CssClass="cmb" Style="width:99%;" runat="server">
=======
                                            <table style="width:90%;height:100%;margin:0 auto;"> 
                                                 <tr>
                                                    <td>Form Type</td>
                                                    <td>
                                                        <asp:DropDownList ID="drp_formtype" CssClass="cmb" Style="width:90%;" runat="server">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>Origin Code</td>
                                                    <td>
                                                        <asp:DropDownList ID="drp_origincode" CssClass="cmb" Style="width:90%;" runat="server">
>>>>>>> origin/master
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                
<<<<<<< HEAD
                                                </tr>
                                                <tr style="height:10px;">
                                                    <td colspan="4"></td>
=======
                                                    
                                                    
>>>>>>> origin/master
                                                </tr>
                                                <tr>
                                                    <td colspan="4">Certification of Eligibility</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:CheckBox CssClass="chk_move" ID="OfferMethod" Text="  A.Qualifying Offer Method" runat="server" /></td>
                                                
                                                    <td colspan="2">
                                                        <asp:CheckBox CssClass="chk_move" ID="OfferMethodRelief" Text="  B.Qualifying Offer Method Transition Relief" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
<<<<<<< HEAD
                                                        <asp:CheckBox CssClass="chk_move" ID="Section4980H" Text="  C.Section 4980H Transition Relief" runat="server" /></td>
                                                    <td colspan="2">
                                                        <asp:CheckBox CssClass="chk_move" ID="OfferMethod98" Text="  D.98% Offer Method" runat="server" /></td>
                                                </tr>
                                                
                                                <tr style="height:10px;">
                                                    <td colspan="4"></td>
                                                </tr>
=======
                                                        <asp:CheckBox CssClass="chk_move" ID="Section4980H" Text="  C.Section 4980H Transition Relif" runat="server" /></td>
                                                    <td colspan="2">
                                                        <asp:CheckBox CssClass="chk_move" ID="OfferMethod98" Text="  D.98% Offer Method" runat="server" /></td>
                                                </tr>
>>>>>>> origin/master
                                                <tr>
                                                    <td colspan="4">ALE Information</td>
                                                                
                                                            </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <table class="mon_table" style="width:100%;height:100%;">
                                                            <thead>
                                                                <tr>
                                                                    <td>Month</td>
                                                                    <td>Minimum Coverage</td>
                                                                    <td>Full-Time</td>
                                                                    <td>Total</td>
                                                                    <td>Aggregate</td>
                                                                    <td>Sec4980H</td>
                                                                    <%-- <td></td>
                                                            <td></td>--%>
                                                                </tr>
                                                            </thead>
                                                            <asp:Repeater ID="rpt_montable" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_month" runat="server" Text='<%# Eval("month") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="chk_minimum" Checked='<%# Eval("minimum").ToString()=="0"?false:true %>' runat="server" />
                                                                            <asp:HiddenField ID="hdn_chk_minimum" Value='<%# Eval("minimum") %>' runat="server" />
                                                                        </td>
                                                                        <td>
<<<<<<< HEAD
                                                                            <asp:TextBox ID="txt_full" Style="border:none;background:none; text-align: center;width:99%;height:100%;" MaxLength="1" Text='<%# Eval("full") %>' runat="server"></asp:TextBox></td>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="filterajax" TargetControlID="txt_full" FilterType="Numbers" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_total" Style="border: none; background:none; text-align: center;width:99%;height:100%;" MaxLength="1" Text='<%# Eval("total") %>' runat="server"></asp:TextBox></td>
=======
                                                                            <asp:TextBox ID="txt_full" Style="border: none; text-align: center;width:99%;height:100%;" MaxLength="1" Text='<%# Eval("full") %>' runat="server"></asp:TextBox></td>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="filterajax" TargetControlID="txt_full" FilterType="Numbers" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_total" Style="border: none; text-align: center;width:99%;height:100%;" MaxLength="1" Text='<%# Eval("total") %>' runat="server"></asp:TextBox></td>
>>>>>>> origin/master
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txt_total" FilterType="Numbers" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                                                                       
                                                                        <td>
                                                                            <asp:CheckBox ID="chk_aggregate" Checked='<%# Eval("aggregate").ToString()=="0"?false:true %>' runat="server" />
                                                                            <asp:HiddenField ID="hdn_chk_aggregate" Value='<%# Eval("aggregate") %>' runat="server" />
                                                                        </td>
                                                                        <td>
<<<<<<< HEAD
                                                                            <asp:TextBox ID="txt_section" Style="border: none; background:none; text-align: center;width:99%;height:100%;" MaxLength="1" Text='<%# Eval("section") %>' runat="server"></asp:TextBox>
=======
                                                                            <asp:TextBox ID="txt_section" Style="border: none; text-align: center;width:99%;height:100%;" MaxLength="1" Text='<%# Eval("section") %>' runat="server"></asp:TextBox>
>>>>>>> origin/master
                                                                        </td>
                                                                        <%--<td>
                                                                        <asp:ImageButton ID="ImageButton1" CommandName="UP" CommandArgument='<%#Container.ItemIndex+1 %>' ImageUrl="~/img/up_arrow.png" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton2" CommandName="Down" CommandArgument='<%#Container.ItemIndex+1 %>' ImageUrl="~/img/down_arrow.png" runat="server" />
                                                                    </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                        </table>
                                                        </td>
                                                        </tr>
<<<<<<< HEAD
                                                <tr style="height:10px;">
                                                    <td colspan="4"></td>
                                                </tr>
=======
>>>>>>> origin/master
                                                <tr>
                                                        <td colspan="4">
                                                                    <asp:CheckBox ID="chk_disable"  Text=" Disable Automatic Changes" runat="server" />
                                                         </td>
                                                    </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                         
                        </ajaxToolkit:TabContainer>
                        <table>
                            <tr>
                                <td colspan="4">
                                    <asp:HiddenField ID="hdn_isCompany" Value="0" runat="server" />
                                    <asp:HiddenField ID="hdn_id" Value="0" runat="server" />
                                    <asp:Button ID="btn_Save" ValidationGroup="save" CssClass="btn1" OnClick="btn_Save_Click" runat="server" Text="Save" />
                                    </td>

                                    <td>
                                    <asp:Button ID="btn_reset" CssClass="btn1" OnClick="btn_reset_Click" runat="server" Text="Clear" />
                                    </td>
                                <td>
                                        <asp:Button ID="btn_delete" OnClientClick="return confirm('Are you sure you want to delete this Employer?');" CssClass="btn1" Visible="false" OnClick="btn_delete_Click" runat="server" Text="Delete" />
                                </td>
                                <td></td>
                            </tr>
                        </table>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="lightDiv" runat="server" visible="false" class="white_content">
                <asp:Label runat="server" ForeColor="White" Text="" ID="lbl_msg"></asp:Label>
                <asp:LinkButton ID="lbl_close" class="btn_popup"
                    Text="Close" runat="server" OnClick="lbl_close_Click"></asp:LinkButton>
            </div>
            <div id="fadeDiv" runat="server" visible="false" class="black_overlay"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
