<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Plan.aspx.cs" Inherits="ACA_WebApplication.Master.Plan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
         function page_load() {
             $('.grid_list').on('click', function () {
                 $('.grid_list').css('background-position', 'left');
                 $(this).css({ 'background-position': 'right' });
             });
         }
    </script>
      <link href="../css/Employer.css" rel="stylesheet" />
    <link href="../css/FormStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_save" />
             <asp:AsyncPostBackTrigger ControlID="btn_clear" />
            <asp:AsyncPostBackTrigger ControlID="lbl_close" />
           
        </Triggers> 
  <ContentTemplate>
    <%--<div class="heading">PLan Details</div>--%>
      <asp:UpdatePanel ID="up2" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
          <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btn_first" />
              <asp:AsyncPostBackTrigger ControlID="btn_previous" />
              <asp:AsyncPostBackTrigger ControlID="btn_next" />
              <asp:AsyncPostBackTrigger ControlID="btn_last" />
              <asp:AsyncPostBackTrigger ControlID="drp_count" />
              <asp:AsyncPostBackTrigger ControlID="txtsearch" />
              <asp:AsyncPostBackTrigger ControlID="btn_refresh" />
          </Triggers>
          <ContentTemplate>
              <div class="grid_container">
                   <asp:TextBox ID="txtsearch" CssClass="srch" AutoPostBack="true" ClientIDMode="AutoID" runat="server" placeholder="Search Plan" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                   <a href="#" class="btn medium bg-green" title="Search" style="/*background-color:#0294A5;*/ border-radius:5px;padding:0px;margin-bottom:5px;" >
             <span class="button-content">
                <i class="glyph-icon icon-search font-white"></i>
            </span>
        </a>
                            <asp:LinkButton class="btn medium bg-blue" ID="btn_refresh" ClientIDMode="AutoID" style="border-radius:5px;padding:0px;margin-bottom:5px;" runat="server" OnClick="Refresh"><span class="button-content">
                <i class="glyph-icon icon-refresh font-white"></i>
            </span></asp:LinkButton>

                  <div style="overflow-y: auto; min-height: 498px; max-height: 498px; width: 100%; overflow-x: hidden;background-color:rgba(255,255,255, 0.6);">
                     
                      <asp:Repeater ID="rptPlan" OnItemCommand="rptPlan_ItemCommand" runat="server">
                          <ItemTemplate>
                              <asp:LinkButton ID="lb_plan_list" ClientIDMode="AutoID" Style="text-decoration: none;" CommandName="select" runat="server">
                                  <div class="grid_list over">
                                      <div class="serial">
                                          <%# Eval("RowNumber") %>
                                      </div>
                                      <div class="detail" style="margin-top:8px;">
                                          <b><%# Eval("name") %> 
                                          </b>

                                      </div>
                                      
                                      <asp:HiddenField ID="hdn_Plan_Id" Value='<%# Eval("id") %>' runat="server" />
                                      <asp:HiddenField ID="hdn_CompanyTax_Id" Value='<%# Eval("employerId") %>' runat="server" />
                                  </div>
                                  <hr / style="margin-top: 0em;margin-bottom: 0em; ">
                              </asp:LinkButton>
                          </ItemTemplate>
                      </asp:Repeater>
                  </div>
                  <table style="width: 100%;text-align:center;">
                            <tr style="background-color: white;">
                                <td colspan="2">
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
                                </td>
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
      <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="false"  runat="server"> 
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="rptPlan" />
            <asp:AsyncPostBackTrigger ControlID="rpttable" />
            <asp:AsyncPostBackTrigger ControlID="rptPlan" />
        </Triggers>
          <ContentTemplate>
    <div class="planinfo">
        <div class="heading">Plan Details</div>
        <div class="plan_header">
        <table style="width:95%; height:100%; margin:0px auto;">
            <%--<tr><th colspan="4"">General Information</th></tr>--%>
            <tr>
                <td >Plan Name</td>
                <td colspan="3">
                    <asp:TextBox ID="txt_planName" cssclass="txt" style="width:99%;" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfv_txt_planName" runat="server" ValidationGroup="Save" ControlToValidate="txt_planName" ErrorMessage="Plan Name is required." Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender Width="200px" PopupPosition="BottomLeft" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender1" TargetControlID="rfv_txt_planName" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>Funding Type</td>
                <td><asp:DropDownList ID="drp_fundingtype" CssClass="cmb" style="width:80%;" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Plan Type</td>
                <td><asp:DropDownList ID="drp_plantype" CssClass="cmb" style="width:100%;" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Waiting Period</td>
                <td><asp:DropDownList ID="drp_waitingperiod" CssClass="cmb" style="width:80%;" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2"> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txt_days" />
                    <asp:TextBox ID="txt_days" cssclass="txt" style="width:15%;" runat="server">
                    </asp:TextBox> Days
                </td>
            </tr>
            <tr>
                <td>Offered to Spouse?</td>
                <td><asp:DropDownList ID="drp_spouse" CssClass="cmb" style="width:80%;" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Offered to Dependance</td>
                <td><asp:DropDownList ID="drp_dependence" CssClass="cmb" style="width:100%;" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td >Terminates On Date of Termination</td>
                    <td><asp:DropDownList ID="drp_termination" CssClass="cmb" style="width:80%;" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>            
                <td>Minimum Value</td>
                 <td><asp:DropDownList ID="drp_minvalue" CssClass="cmb" style="width:100%;" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>Plan Renewal Month</td>
                    <td><asp:DropDownList ID="drp_renewalmonth" CssClass="cmb" style="width:80%;" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>             
                     <td >Banding Type</td>
                 <td >
                     <asp:DropDownList ID="drp_bandingtype" CssClass="cmb" style="width:100%;" runat="server">
                     <asp:ListItem>--Select--</asp:ListItem>
                     </asp:DropDownList></td>
                    </tr>

                 
            </tr>
            </Table>
            </div>
                    <div class="plan_detail">
                        <table style="width: 100%; text-align: center; height: 100%;border:1px solid gray;" border="1">
                            <asp:Repeater ID="rpt_codetbl" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chk_code1A" Text=" Code 1A" Checked='<%# Eval("code1A").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code1B" Text=" Code 1B" Checked='<%# Eval("code1B").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code1C" Text=" Code 1C" Checked='<%# Eval("code1C").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code1D" Text=" Code 1D" Checked='<%# Eval("code1D").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code1E" Text=" Code 1E" Checked='<%# Eval("code1E").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code1F" Text=" Code 1F" Checked='<%# Eval("code1F").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code1G" Text=" Code 1G" Checked='<%# Eval("code1G").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code1H" Text=" Code 1H" Checked='<%# Eval("code1H").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code1I" Text=" Code 1I" Checked='<%# Eval("code1I").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chk_code2A" Text=" Code 2A" Checked='<%# Eval("code2A").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code2B" Text=" Code 2B" Checked='<%# Eval("code2B").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code2C" Text=" Code 2C" Checked='<%# Eval("code2C").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code2D" Text=" Code 2D" Checked='<%# Eval("code2D").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code2E" Text=" Code 2E" Checked='<%# Eval("code2E").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code2F" Text=" Code 2F" Checked='<%# Eval("code2F").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code2G" Text=" Code 2G" Checked='<%# Eval("code2G").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code2H" Text=" Code 2H" Checked='<%# Eval("code2H").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code2I" Text=" Code 2I" Checked='<%# Eval("code2I").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
               
          
                    <div class="plan_table">
                        <table style="width: 90%;margin:0px auto;">
                            <tr style="text-align:left;">
                                <th>Start Value</th>
                                <th>End Value</th>
                                <th>Start Date</th>
                                <th>End Date</th>
                                <th>Amount</th>
                            </tr>
                            <%--Repeter--%>
                            <asp:Repeater ID="rpttable" OnItemDataBound="rpttable_ItemDataBound" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="tb1" CssClass="txt_short" Text='<%# Eval("bandingValueStart") %>' runat="server"></asp:TextBox></td>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers" ValidChars="."
                                                TargetControlID="tb1" />
                                        <asp:RequiredFieldValidator ID="rfv_startvalue" runat="server" ValidationGroup="Premiumadd" ControlToValidate="tb1" ForeColor="Red" ErrorMessage="Start Value is required." Display="None"></asp:RequiredFieldValidator>
                                             <ajaxToolkit:ValidatorCalloutExtender Width="200px" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender1" TargetControlID="rfv_startvalue" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                        <td>
                                            <asp:TextBox ID="tb2" CssClass="txt_short" Text='<%# Eval("bandingValueEnd") %>' runat="server"></asp:TextBox></td>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers" ValidChars="."
                                                TargetControlID="tb2" />
                                        <td>
                                            <asp:TextBox ID="tb3" CssClass="txt_short" MaxLength="10" Text='<%# FixDateFormat(Eval("bandingStartDate").ToString()) %>' runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  CssClass="black" Enabled="true" Format="MM/dd/yyyy" TargetControlID="tb3" runat="server"></ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfv_startdate" runat="server" ValidationGroup="Premiumadd" ControlToValidate="tb3" ForeColor="Red" ErrorMessage="<b>Missing field</b><br/>Start Date is required." Display="None"></asp:RequiredFieldValidator>
                                             <ajaxToolkit:ValidatorCalloutExtender Width="200px" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender2" TargetControlID="rfv_startdate" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb4" CssClass="txt_short" MaxLength="10" Text='<%# FixDateFormat(Eval("bandingEndDate").ToString()) %>' runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2"  CssClass="black" Enabled="true" Format="MM/dd/yyyy" TargetControlID="tb4" runat="server"></ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb5" CssClass="txt_short" Text='<%# Eval("amount") %>' runat="server"></asp:TextBox></td>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,Numbers" ValidChars="."
                                                TargetControlID="tb5" />
                                        <td>
                                            <asp:Button ID="btnAdd" CssClass="imgbtnplus" runat="server"  ValidationGroup="Premiumadd" OnClick="btnAdd_Click" />
                                            <asp:Button ID="btnMinus" Visible="false" OnClick="btnMinus_Click" CssClass="imgbtnminus" runat="server" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%--Rpeater End--%>
                        </table>
                    </div>
        <table>
            <tr>
                <td >
                    <asp:HiddenField ID="hdn_id" Value="0" runat="server" />
                    <asp:Button ID="btn_save" ValidationGroup="Save" OnClick="btn_Save_Click" runat="server" Text="Save" class="btn1" />
                    <asp:Button ID="btn_clear" OnClick="btn_Clear_Click" runat="server" Text="Clear" class="btn1" />
                </td>
            </tr>
        </table>
               
        </div>
        
    <%--</div>--%>
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
