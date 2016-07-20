<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Employer_Details.aspx.cs" Inherits="ACA_WebApplication.Master.Employer_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/Employer.css" rel="stylesheet" />
    <link href="../css/FormStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_save" />
            <asp:AsyncPostBackTrigger ControlID="lbl_close" />
            <asp:AsyncPostBackTrigger ControlID="btn_delete" />
        </Triggers>
        <ContentTemplate>
            <div class="heading">Employer Details</div>

            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rptEmployer" />
                    <asp:AsyncPostBackTrigger ControlID="btn_first" />
                    <asp:AsyncPostBackTrigger ControlID="btn_previous" />
                    <asp:AsyncPostBackTrigger ControlID="btn_next" />
                    <asp:AsyncPostBackTrigger ControlID="btn_last" />
                    <asp:AsyncPostBackTrigger ControlID="drp_count" />
                    <asp:AsyncPostBackTrigger ControlID="txtsearch" />
                </Triggers>
                <ContentTemplate>
                    <div class="grid_container">
                        <div style="overflow-y:auto;min-height:500px;max-height:500px;width:100%;overflow-x: hidden;">
                        <asp:TextBox ID="txtsearch" CssClass="srch" AutoPostBack="true" runat="server" placeholder="Search Employer" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                        <asp:Repeater ID="rptEmployer" OnItemCommand="rptEmployer_ItemCommand" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="lb_emp_list" Style="text-decoration: none;" CommandName="Edit" runat="server">
                                    <div class="employer_list">
                                        <div class="serial">
                                            <%# Eval("RowNumber") %>
                                        </div>
                                        <div class="detail">
                                            <b>Tax Id :</b> <%# Eval("EmployerTaxId") %><br />
                                            <%# Eval("name") %><br />
                                           
                                        </div>
                                        <asp:HiddenField ID="hdn_EmpTax_Id" Value='<%# Eval("EmployerTaxId") %>' runat="server" />
                                        <asp:HiddenField ID="hdn_CompanyTax_Id" Value='<%# Eval("CompanyTaxID") %>' runat="server" />
                                    </div>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                            </div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_first" CssClass="btn_nav" runat="server" Text="<<" OnClick="btn_first_Click" />
                                    <asp:Button ID="btn_previous" CssClass="btn_nav" runat="server" Text="<" OnClick="btn_previous_Click" />
                                    <asp:Label ID="lbl_pagenum" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="btn_next" CssClass="btn_nav" runat="server" Text=">" OnClick="btn_next_Click" />
                                    <asp:Button ID="btn_last" CssClass="btn_nav" runat="server" Text=">>" OnClick="btn_last_Click" />
                                    <asp:DropDownList ID="drp_count" CssClass="drp_txt" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drp_count_SelectedIndexChanged">
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                    </asp:DropDownList><br />
                                    <asp:Label ID="lbl_result" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hid_rowcount" runat="server" />
                                    <asp:HiddenField ID="hdn_companytax_id" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" ChildrenAsTriggers="false"  runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_save" />
                    <asp:AsyncPostBackTrigger ControlID="btn_reset" />
                    <asp:AsyncPostBackTrigger ControlID="rptEmployer"/>
                    <asp:AsyncPostBackTrigger ControlID="lbl_close" />
                    <asp:AsyncPostBackTrigger ControlID="rpt_montable"/>
                    <asp:AsyncPostBackTrigger ControlID="btn_delete" />
                </Triggers>
                <ContentTemplate>
                    <div class="employer_form">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2">
                                    <div class="gen_info" id="ginfo2">
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <div class="hdr" id="ginfo">General Information</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Employer Name</td>
                                                <td>
                                                    <asp:TextBox ID="txt_employerName" class="txt" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>EIN</td>
                                                <td>
                                                    <asp:TextBox ID="txt_ein" class="txt" runat="server"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender TargetControlID="txt_ein" Mask="99-9999999"
                                                        MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                        MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" Enabled="true"
                                                        ErrorTooltipEnabled="True" runat="server" ID="mskD" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Filling Year</td>
                                                <td>
                                                    <asp:DropDownList ID="drp_fillingyear" CssClass="cmb" runat="server">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Address</td>
                                                <td>
                                                    <asp:TextBox ID="txt_address1" class="txt" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Address</td>
                                                <td>
                                                    <asp:TextBox ID="txt_address2" class="txt" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>City</td>
                                                <td>
                                                    <asp:TextBox ID="txt_city" class="txt" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>State</td>
                                                <td>
                                                    <asp:DropDownList ID="drp_state" CssClass="cmb" runat="server">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Zip Code</td>
                                                <td>
                                                    <asp:TextBox ID="txt_zipcode" class="txt" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Country</td>
                                                <td>
                                                    <asp:DropDownList ID="drp_country" CssClass="cmb" runat="server">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Contact Name</td>
                                                <td>
                                                    <asp:TextBox ID="txt_contactname" class="txt" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Contact Phone</td>
                                                <td>
                                                    <asp:TextBox ID="txt_contactphone" class="txt" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Title</td>
                                                <td>
                                                    <asp:TextBox ID="txt_title" class="txt" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Form Type</td>
                                                <td>
                                                    <asp:DropDownList ID="drp_formtype" CssClass="cmb" runat="server">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Origin Code</td>
                                                <td>
                                                    <asp:DropDownList ID="drp_origincode" CssClass="cmb" runat="server">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>SHOP Identifier</td>
                                                <td>
                                                    <asp:TextBox ID="txt_shop" class="txt" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="c_details" id="cdetail2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="hdr" id="cdetail">1094-C Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Certification of Eligibility</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox CssClass="chk_move" ID="OfferMethod" Text="A.Qualifiying Offer Method" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox CssClass="chk_move"  ID="OfferMethodRelief" Text=" B.Qualifiying Offer Method Transition Relif" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox CssClass="chk_move" ID="Section4980H" Text="C.Section 4980H Transition Relif" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox CssClass="chk_move" ID="OfferMethod98" Text="D.98% Offer Method" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="1">
                                                    <table>
                                                        <tr>
                                                            <td>ALE Information</td>
                                                            <td>
                                                                <asp:CheckBox ID="chk_disable" CssClass="chk_move"  Text="Disable Automatic Changes" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table class="mon_table">
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
                                                        <asp:Repeater ID="rpt_montable" OnItemDataBound="rpt_montable_OnItemDataBound" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_month" runat="server" Text='<%# Eval("month") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chk_minimum" runat="server" />
                                                                        <asp:HiddenField ID="hdn_chk_minimum" Value='<%# Eval("minimum") %>' runat="server" />
                                                                    </td>
                                                                    <td><asp:TextBox ID="txt_full" style="border:none;border-bottom:1px solid gray;"  width= "40px" Text='<%# Eval("full") %>' runat="server"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txt_total" style="border:none;border-bottom:1px solid gray;"  width= "40px" Text='<%# Eval("total") %>' runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chk_aggregate" runat="server" />
                                                                        <asp:HiddenField ID="hdn_chk_aggregate" Value='<%# Eval("aggregate") %>' runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_section" style="border:none;border-bottom:1px solid gray;"  width= "40px" Text='<%# Eval("section") %>' runat="server"></asp:TextBox>
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
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:HiddenField ID="hdn_isCompany" Value="0" runat="server" />
                                    <asp:HiddenField ID="hdn_id" Value="0" runat="server" />
                                    <asp:Button ID="btn_Save" CssClass="btn" OnClick="btn_Save_Click" runat="server" Text="Save" />
                                    <asp:Button ID="btn_reset" CssClass="btn" OnClick="btn_reset_Click" runat="server" Text="Clear" />
                                    <asp:Button ID="btn_delete" CssClass="btn" Visible="false" OnClick="btn_delete_Click" runat="server" Text="Delete" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="lightDiv" runat="server" visible="false" class="white_content">
                <asp:Label runat="server" ForeColor="White" Text="" ID="lbl_msg"></asp:Label>
                <asp:LinkButton ID="lbl_close" Style="float: right; margin: 45px 13px; background-color: #130E0E; padding: 1% 3%; text-decoration: none; border-radius: 3px; color: #CEA937;"
                    Text="Close" runat="server" OnClick="lbl_close_Click"></asp:LinkButton>
            </div>
            <div id="fadeDiv" runat="server" visible="false" class="black_overlay"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
