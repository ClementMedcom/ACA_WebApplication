<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Plan.aspx.cs" Inherits="ACA_WebApplication.Master.Plan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
         function page_load() {
             $('.employer_list').on('click', function () {
                 $('.over').css('background-color', 'white');
                 $(this).css({ 'background-color': '#add5f3', 'border-radius': '4px' });
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
            <%--<asp:AsyncPostBackTrigger ControlID="lbl_close" />
           --%>
        </Triggers> 
  <ContentTemplate>
    <div class="heading">PLan Details</div>
      <asp:UpdatePanel ID="up2" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
          <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btn_first" />
              <asp:AsyncPostBackTrigger ControlID="btn_previous" />
              <asp:AsyncPostBackTrigger ControlID="btn_next" />
              <asp:AsyncPostBackTrigger ControlID="btn_last" />
              <asp:AsyncPostBackTrigger ControlID="drp_count" />
              <asp:AsyncPostBackTrigger ControlID="txtsearch" />
          </Triggers>
          <ContentTemplate>
              <div class="grid_container">
                  <div style="overflow-y: auto; min-height: 500px; max-height: 500px; width: 100%; overflow-x: hidden;">
                      <asp:TextBox ID="txtsearch" CssClass="srch" AutoPostBack="true" runat="server" placeholder="Search Plan" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                      <asp:Repeater ID="rptPlan" OnItemCommand="rptPlan_ItemCommand" runat="server">
                          <ItemTemplate>
                              <asp:LinkButton ID="lb_plan_list" ClientIDMode="AutoID" Style="text-decoration: none;" CommandName="select" runat="server">
                                  <div class="employer_list over">
                                      <div class="serial">
                                          <%# Eval("RowNumber") %>
                                      </div>
                                      <div class="detail">
                                          <b><%# Eval("name") %><br />
                                          </b>

                                      </div>
                                      <asp:HiddenField ID="hdn_Plan_Id" Value='<%# Eval("id") %>' runat="server" />
                                      <asp:HiddenField ID="hdn_CompanyTax_Id" Value='<%# Eval("employerId") %>' runat="server" />
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
      <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="false"  runat="server"> 
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="rptPlan" />
            <asp:AsyncPostBackTrigger ControlID="rpttable" />
            <asp:AsyncPostBackTrigger ControlID="rptPlan" />
        </Triggers>
          <ContentTemplate>
    <div class="planinfo">
        <div class="plan_header">
        <table style="width:100%;height:540px;">
            <tr><th colspan="4"">General Information</th></tr>
            <tr>
                <td >Plan Name</td>
                <td colspan="3">
                    <asp:TextBox ID="txt_planName" cssclass="txt" style="width:95%;" runat="server"></asp:TextBox>
    
            </tr>
            <tr>
                <td>Funding Type</td>
                <td><asp:DropDownList ID="drp_fundingtype" CssClass="cmb" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Plan Type</td>
                <td><asp:DropDownList ID="drp_plantype" CssClass="cmb" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Waiting Period</td>
                <td><asp:DropDownList ID="drp_waitingperiod" CssClass="cmb" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2"> 
                    <asp:TextBox ID="txt_days" cssclass="txt" style="width:15%;" runat="server">
                    </asp:TextBox> Days
                </td>
            </tr>
            <tr>
                <td>Offered to Spouse?</td>
                <td><asp:DropDownList ID="drp_spouse" CssClass="cmb" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Offered to Dependance</td>
                <td><asp:DropDownList ID="drp_dependence" CssClass="cmb" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td >Terminates On Date of Termination</td>
                    <td><asp:DropDownList ID="drp_termination" CssClass="cmb" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>            </tr>
             <tr>
                <td>Plan Renewal Month</td>
                    <td><asp:DropDownList ID="drp_renewalmonth" CssClass="cmb" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>                
                 <td>Minimum Value</td>
                 <td><asp:DropDownList ID="drp_minvalue" CssClass="cmb" runat="server">
                    <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div class="plan_detail"> 
                        <table class="codetbl" style="width:100%;text-align:center; height:100%;" border="1">
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
                                            <asp:CheckBox ID="CheckBox1" Text=" Code 1H" Checked='<%# Eval("code1H").ToString()=="0"?false:true %>' runat="server" />
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
                                            <asp:CheckBox ID="CheckBox2" Text=" Code 2H" Checked='<%# Eval("code2H").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_code2I" Text=" Code 2I" Checked='<%# Eval("code2I").ToString()=="0"?false:true %>' runat="server" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>

                </td>
            </tr>
            <tr>
                 <td >Banding Type</td>
                 <td colspan="3">
                     <asp:DropDownList ID="drp_bandingtype" CssClass="cmb" runat="server">
                     <asp:ListItem>--Select--</asp:ListItem>
                     </asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="4">
                    <div class="plan_table">
                        <table style="width:100%;">
                            <tr>
                                <td>Start Value</td><td>End Value</td><td>Amount</td><td></td>

                            </tr>
                            <%--Repeter--%>
           
                            <asp:Repeater ID="rpttable" OnItemDataBound="rpttable_ItemDataBound" runat="server">
                        <ItemTemplate>
                          <tr>
                              <td><asp:TextBox ID="tb1" CssClass="txt_short" Text='<%# Eval("bandingValueStart") %>' runat="server"></asp:TextBox></td>
                              <td><asp:TextBox ID="tb2" CssClass="txt_short" Text='<%# Eval("bandingValueEnd") %>' runat="server"></asp:TextBox></td>
                              <td><asp:TextBox ID="tb3" CssClass="txt_short" Text='<%# Eval("bandingStartDate") %>' runat="server"></asp:TextBox></td>
                              <td><asp:TextBox ID="tb4" CssClass="txt_short" Text='<%# Eval("bandingEndDate") %>' runat="server"></asp:TextBox></td>
                              <td><asp:TextBox ID="tb5" CssClass="txt_short" Text='<%# Eval("amount") %>' runat="server"></asp:TextBox></td>
                              <td><asp:Button ID="btnAdd" cssclass="imgbtnplus"  runat="server"  onclick="btnAdd_Click" />
                                   <asp:Button ID="btnMinus" Visible="false"  onclick="btnMinus_Click" cssclass="imgbtnminus"  runat="server"/>
                              </td>
                        </tr>
                     </ItemTemplate>
                   </asp:Repeater>
                
                            <%--Rpeater End--%>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btn_save" runat="server" Text="Button" class="btn" />
                    <asp:Button ID="btn_clear" runat="server" Text="Button" class="btn" />
            
        </table>
        </div>
    </div>
      </ContentTemplate>
          </asp:UpdatePanel>
      </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
