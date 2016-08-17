<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Employee_Details.aspx.cs" Inherits="ACA_WebApplication.Master.Employee_Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../css/Employer.css" rel="stylesheet" />
    <link href="../css/FormStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function page_load() {
            $('.grid_list').on('click', function () {
                $('.over').css( 'background', 'none');
                $(this).css({ 'background-color': '#dedede' });
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
            <asp:AsyncPostBackTrigger ControlID="btn_refresh" />
        </Triggers>
        <ContentTemplate>
           <%-- <div class="heading">Employee Information</div>--%>

            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
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
                        <asp:TextBox ID="txtsearch" CssClass="srch" ClientIDMode="AutoID" AutoPostBack="true" runat="server" placeholder="Search Employee" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                        <a href="#" class="btn medium bg-green" title="Search" style="/*background-color:#0294A5;*/ border-radius:5px;padding:0px;margin-bottom:5px;" >
             <span class="button-content">
                <i class="glyph-icon icon-search font-white"></i>
            </span>
        </a>
                            <asp:LinkButton class="btn medium bg-blue" ID="btn_refresh" ClientIDMode="AutoID" style="border-radius:5px;padding:0px;margin-bottom:5px;" runat="server" OnClick="Refresh"><span class="button-content">
                <i class="glyph-icon icon-refresh font-white"></i>
            </span></asp:LinkButton>
                        <div style="overflow-y: auto; min-height: 498px; max-height: 498px; width: 100%; overflow-x: hidden;background-color:white;">
                            <asp:Repeater ID="rptEmployee" OnItemCommand="rptEmployee_ItemCommand" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lb_emp_list" ClientIDMode="AutoID" Style="text-decoration: none;" CommandName="Edit" runat="server">
                                        <div class="grid_list over">
                                            <div class="serial bg-twitter opacity-60">
                                                <%# Eval("RowNumber") %>
                                            </div>
                                            <div class="detail">
                                                <b>Name  :</b> <%# Eval("FirstName") %>&nbsp;<%# Eval("LastName") %>
                                                <br />
                                                <b>SSN   :</b> <asp:Label ID="lbl_ssn" runat="server" Text='<%# Eval("ssn") %>'></asp:Label>
                                                <asp:HiddenField ID="hdn_employerTaxId" Value='<%# Eval("taxid") %>' runat="server" />
                                                <asp:HiddenField ID="hdn_Id" Value='<%# Eval("Id") %>' runat="server" />
                                            </div>
                                        </div>
                                        <%--<hr />--%>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <table style="width: 100%;">
                            <tr style="background-color: white;">
                                <th colspan="2">
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
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_save" />
                            <asp:AsyncPostBackTrigger ControlID="btn_reset" />
                            <asp:AsyncPostBackTrigger ControlID="rptEmployee" />
                            <asp:AsyncPostBackTrigger ControlID="lbl_close" />
                            <asp:AsyncPostBackTrigger ControlID="btn_delete" />
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1$tabHire$rptHiredata" />
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1$tabStatus$rpt_Status" />
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1$tabcoverage$rpt_coverage" />
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1$tabCoveredIndividual$rpt_IndividualData" />
                        </Triggers>
                        <ContentTemplate>
                             <div class="employee_form">
                                 <div class="heading" >Employee Information</div>
                                 <div class="employee_info1">
                                     <table style="width: 95%; height: 100%;margin:0px auto;">
                                         <tr>
                                             <td >Employer Name</td>
                                             <td colspan="3">
                                                 <asp:DropDownList ID="drp_employer" CssClass="cmb" style="width:95%;" runat="server">
                                                 </asp:DropDownList>
                                                 <asp:RequiredFieldValidator ControlToValidate="drp_employer" ID="RFV1" ForeColor="Red" ValidationGroup="save" runat="server" Display="None" ErrorMessage="A Employer is required."></asp:RequiredFieldValidator>
                                                 <ajaxToolkit:ValidatorCalloutExtender PopupPosition="BottomLeft" Width="200px" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender1" TargetControlID="RFV1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                             </td>
                                            
                                         </tr>

                                         <tr>
                                             <td>Full Name</td>
                                             <td>
                                                 <asp:TextBox ID="txt_firstname" placeholder="First Name"  class="txt" runat="server"></asp:TextBox></td>
                                             <td>
                                                 <asp:TextBox ID="txt_middlename" placeholder="Middle Name"  class="txt" runat="server"></asp:TextBox></td>
                                             <td>
                                                 <asp:TextBox ID="txt_lastname" placeholder="Last Name"  class="txt" runat="server"></asp:TextBox></td>
                                         </tr>

                                         <tr>
                                              <td>Social Security Number</td><td>
                                                <asp:TextBox ID="txt_SSN" class="txt" runat="server"></asp:TextBox>
                                                <asp:RegularExpressionValidator ForeColor="Red" ValidationGroup="save" ValidationExpression="^\d{3}-\d{2}-\d{4}$" ID="Regxval1" Display="None" ControlToValidate="txt_SSN" runat="server" ErrorMessage="Invalid SSN."></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ControlToValidate="txt_SSN" ID="RFV2" Display="None" ForeColor="Red" ValidationGroup="save" runat="server" ErrorMessage="A SSN is required."></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender Width="200px" PopupPosition="BottomLeft" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender2" TargetControlID="Regxval1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                                <ajaxToolkit:ValidatorCalloutExtender Width="200px" PopupPosition="BottomLeft" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender3" TargetControlID="RFV2" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                             </td>
                                             <td>Date of Birth</td>
                                             <td>
                                                 <asp:TextBox ID="txt_dob" class="txt" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" runat="server"></asp:TextBox>
                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  CssClass="black" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_dob" runat="server"></ajaxToolkit:CalendarExtender>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="fte" runat="server" TargetControlID="txt_dob" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                             </td>
                                             <td>

                                             </td>
                                             </tr>


                                     </table>
                               </div>
                                 <div style="width: 100%;float:left;display:inline-block;">
                                     <ajaxToolkit:TabContainer CssClass="fancy fancy-green" ID="TabContainer1" runat="server">
                                         <ajaxToolkit:TabPanel ID="tabinfo" TabIndex="0" Style="height:180px;" runat="server">
                                             <HeaderTemplate>
                                                 Information
                                             </HeaderTemplate>
                                             <ContentTemplate>
                                                 <asp:Panel ID="infopanel" runat="server">
                                                     <div class="employee_info2">
                                                         <table style="width:90%; height:165px; margin:0px auto;" border="0">
                                                             <tr>
                                                                 <td style="width:100px;">Email Address</td>
                                                                 <td colspan="3">
                                                                     <asp:TextBox ID="txt_email" class="txt" style="width:96%;" runat="server"></asp:TextBox>
                                                                 </td>
                                                             </tr>
                                                             <tr>
                                                                 <td>Address</td>
                                                                 <td>
                                                                     <asp:TextBox ID="txt_address1" class="txt" style="width:92%;" runat="server"></asp:TextBox>
                                                                 </td>
                                                                 <td style="width:100px;">Address1</td>
                                                                 <td>
                                                                     <asp:TextBox ID="txt_address2" class="txt" style="width:88%;" runat="server"></asp:TextBox>
                                                                 </td>
                                                             </tr>
                                                             <tr>
                                                                 <td>City</td>
                                                                 <td>
                                                                     <asp:TextBox ID="txt_city" class="txt"  style="width:92%;" runat="server"></asp:TextBox>
                                                                 </td>
                                                                 <td>State</td>
                                                                 <td>
                                                                     <asp:DropDownList ID="drp_state" CssClass="cmb"  style="width:91%;" runat="server">
                                                                         <asp:ListItem Value="">--Select--</asp:ListItem>
                                                                     </asp:DropDownList>
                                                                 </td>
                                                             </tr>

                                                             <tr>
                                                                 <td>Country</td>
                                                                 <td>
                                                                     <asp:DropDownList ID="drp_country" CssClass="cmb" style="width:94%;" runat="server">
                                                                         <asp:ListItem Value="">--Select--</asp:ListItem>
                                                                     </asp:DropDownList>
                                                                 </td>
                                                                 <td>Zipcode</td>
                                                                 <td>
                                                                     <asp:TextBox ID="txt_zipcode" class="txt" style="width:88%;" runat="server"></asp:TextBox>
                                                                 </td>
                                                                 
                                                             </tr>
                                                             <tr>
                                                                 <td>Salary</td>
                                                                 <td>
                                                                     <asp:TextBox ID="txt_salary" class="txt" style="width:92%;" runat="server"></asp:TextBox>
                                                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_salary" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                 </td>
                                                                 <td>Hourly</td>
                                                                 <td>
                                                                     <asp:TextBox ID="txt_hourly" class="txt" style="width:88%;" runat="server"></asp:TextBox>
                                                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_hourly" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                 </td>
                                                             </tr>
                                                         </table>
                                                         </div>
                                                         </asp:Panel>
                                             </ContentTemplate>
                                         </ajaxToolkit:TabPanel>
                                         <ajaxToolkit:TabPanel ID="tabHire" TabIndex="1" Style="height:180px;overflow-y:scroll;" runat="server">
                                             <HeaderTemplate>
                                                 Hire Data
                                             </HeaderTemplate>
                                             <ContentTemplate>
                                                 <asp:Panel ID="panelHire" runat="server">
                                                     <div class="employee_info2">
                                                         <table style="width: 80%; text-align:center; margin:0px auto;" border="0">
                                                             <tr>
                                                                 <td>Hire Date </td>
                                                                 <td>Terminate Date</td>
                                                             </tr>
                                                             <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                                 <Triggers>
                                                                     <asp:AsyncPostBackTrigger ControlID="rptHiredata" />
                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                     <asp:Repeater ID="rptHiredata" OnItemDataBound="rptHiredata_ItemDataBound" runat="server">
                                                                         <ItemTemplate>
                                                                             <tr>
                                                                                 <td>
                                                                                     <asp:TextBox ID="txt_startdate" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("startDate").ToString()) %>' CssClass="txt" runat="server"></asp:TextBox>
                                                                                     <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender1" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_startdate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                                     <asp:RequiredFieldValidator ControlToValidate="txt_startdate" ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="sub_hire" Display="None" runat="server" ErrorMessage="A Hire Date is Required."></asp:RequiredFieldValidator>
                                                                                     <ajaxToolkit:ValidatorCalloutExtender Width="200px" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftse" runat="server" TargetControlID="txt_startdate" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                 </td>
                                                                                 <td>
                                                                                     <asp:TextBox ID="txt_enddate" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("endDate").ToString()) %>' CssClass="txt" runat="server"></asp:TextBox>
                                                                                     <ajaxToolkit:CalendarExtender CssClass="black" PopupPosition="BottomRight" ID="CalendarExtender2" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_enddate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txt_enddate" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                 </td>
                                                                                 <td>
                                                                                     <asp:HiddenField ID="hdn_hireId" Value='<%# Eval("Id") %>' runat="server" />
                                                                                     <asp:HiddenField ID="hdn_hirename" Value='<%# Eval("hirename") %>' runat="server" />
                                                                                     <asp:Button ID="btn_hireplus" CssClass="imgbtnplus" ValidationGroup="sub_hire" OnClick="btn_hireplus_Click" runat="server" />
                                                                                     <asp:Button ID="btn_hireminus" CssClass="imgbtnminus" ClientIDMode="Static" Visible="false" OnClick="btn_hireminus_Click" runat="server" />
                                                                                 </td>
                                                                             </tr>
                                                                         </ItemTemplate>
                                                                     </asp:Repeater>
                                                                 </ContentTemplate>
                                                             </asp:UpdatePanel>
                                                         </table>
                                                     </div>
                                                 </asp:Panel>
                                             </ContentTemplate>
                                         </ajaxToolkit:TabPanel>
                                         <ajaxToolkit:TabPanel ID="tabStatus" TabIndex="2" Style="height:180px;overflow-y:scroll;" runat="server">
                                             <HeaderTemplate>
                                                 Status
                                             </HeaderTemplate>
                                             <ContentTemplate>
                                                 <asp:Panel ID="Panel1" runat="server">
                                                     <div class="employee_info2">
                                                         <table style="width: 80%; text-align:center; margin:0px auto;"  border="0">
                                                             <tr>
                                                                 <td>Status </td>
                                                                 <td>Status Start Date</td>
                                                                 <td>Status End Date</td>
                                                             </tr>
                                                             <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                                 <Triggers>
                                                                     <asp:AsyncPostBackTrigger ControlID="rpt_Status" />
                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                     <asp:Repeater ID="rpt_Status" OnItemDataBound="rpt_Status_ItemDataBound" runat="server">
                                                                         <ItemTemplate>
                                                                             <tr>
                                                                                 <td>
                                                                                     <asp:DropDownList ID="drp_status" CssClass="cmb" runat="server">
                                                                                     </asp:DropDownList>
                                                                                     <asp:HiddenField ID="hdn_status" runat="server" Value='<%# Eval("Status") %>' />
                                                                                     <asp:RequiredFieldValidator ControlToValidate="drp_status" ID="RequiredFieldVsdalidator1" InitialValue="" ForeColor="Red" Display="None" ValidationGroup="sub_status" runat="server" ErrorMessage="A Status is required."></asp:RequiredFieldValidator>
                                                                                     <ajaxToolkit:ValidatorCalloutExtender Width="200px" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorfCalloutExtender1" TargetControlID="RequiredFieldVsdalidator1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                                                                 </td>
                                                                                 <td>
                                                                                     <asp:TextBox ID="txt_startdate" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("startDate").ToString()) %>' CssClass="txt" runat="server"></asp:TextBox>
                                                                                     <asp:RequiredFieldValidator ControlToValidate="txt_startdate" ID="RequiredFieldValidator2" ForeColor="Red" Display="None" ValidationGroup="sub_status" runat="server" ErrorMessage="A Start Date is required."></asp:RequiredFieldValidator>
                                                                                     <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendaDrExtender2" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_startdate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                                     <ajaxToolkit:ValidatorCalloutExtender Width="200px" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloustExtender4" TargetControlID="RequiredFieldValidator2" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                                                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtener16" runat="server" TargetControlID="txt_startdate" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                 </td>
                                                                                 <td>
                                                                                     <asp:TextBox ID="txt_enddate" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("endDate").ToString()) %>' CssClass="txt" runat="server"></asp:TextBox></td>
                                                                                     <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender3" PopupPosition="BottomRight" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_enddate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txt_enddate" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                 <td>
                                                                                     <asp:HiddenField ID="hdn_statusId" Value='<%# Eval("Id") %>' runat="server" />
                                                                                     
                                                                                     <asp:Button ID="btn_statusplus" CssClass="imgbtnplus" ValidationGroup="sub_status" OnClick="btn_statusplus_Click" runat="server" />
                                                                                     <asp:Button ID="btn_statusminus" CssClass="imgbtnminus" ClientIDMode="Static" Visible="false" OnClick="btn_statusminus_Click" runat="server" />
                                                                                 </td>
                                                                             </tr>
                                                                         </ItemTemplate>
                                                                     </asp:Repeater>
                                                                 </ContentTemplate>
                                                             </asp:UpdatePanel>
                                                         </table>
                                                     </div>
                                                 </asp:Panel>
                                             </ContentTemplate>
                                         </ajaxToolkit:TabPanel>
                                         <ajaxToolkit:TabPanel ID="tabcoverage" TabIndex="3" Style="height:180px;overflow-y:scroll;" runat="server">
                                             <HeaderTemplate>
                                                 Coverage
                                             </HeaderTemplate>
                                             <ContentTemplate>
                                                 <asp:Panel ID="coveragePanel" runat="server">
                                                     <div class="employee_info2">
                                                         <table style="width: 100%; text-align:center;"  border="0">
                                                             <tr >
                                                                 <td>Union Member </td>
                                                                 <td>Contribution start</td>
                                                                 <td>Contribution End</td>
                                                                 <td>Offer Date</td>
                                                                 <td>Plan</td>
                                                                 <td>Enrolled</td>
                                                                 <td>Start Date</td>
                                                                 <td>End date</td>
                                                                 <td>CORBA</td>
                                                                 <td>CORBA start</td>
                                                                 <td>CORBA End</td>
                                                             </tr>
                                                             <asp:Repeater ID="rpt_coverage" OnItemDataBound="rpt_coverage_ItemDataBound" runat="server">
                                                                 <ItemTemplate>
                                                                     <tr>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_unionMember" runat="server" />
                                                                             <asp:HiddenField ID="hdn_unionMember" Value='<%# Eval("unionMember") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_contributionStartDate" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("contributionStartDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender3" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_contributionStartDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextxfBoxExtener16" runat="server" TargetControlID="txt_contributionStartDate" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_contributionEndDate" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("contributionEndDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender4" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_contributionEndDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txt_contributionEndDate" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_coverageOfferDate" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("coverageOfferDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender5" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_coverageOfferDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txt_coverageOfferDate" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:RequiredFieldValidator Display="None" ControlToValidate="txt_name" ID="RequiredFieldVsdalidator1" InitialValue="" ForeColor="Red" ValidationGroup="sub_coverage" runat="server" ErrorMessage="A Plan name is Required."></asp:RequiredFieldValidator>
                                                                             <asp:TextBox ID="txt_name" Text='<%# Eval("name") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:ValidatorCalloutExtender Width="200px" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldVsdalidator1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                                                             <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchPlan" MinimumPrefixLength="1"
                                                                                CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txt_name"
                                                                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                                            </ajaxToolkit:AutoCompleteExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_enrolled" runat="server" />
                                                                             <asp:HiddenField ID="hdn_enrolled" Value='<%# Eval("isEnrolled") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_coverageStartDate" AutoComplete="off" MaxLength="10" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("coverageStartDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender6" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_coverageStartDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" TargetControlID="txt_coverageStartDate" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_coverageEndDate" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("coverageEndDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender7" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_coverageEndDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" TargetControlID="txt_coverageEndDate" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_cobraEnrolled" runat="server" />
                                                                             <asp:HiddenField ID="hdn_cobraEnrolled" Value='<%# Eval("cobraEnrolled") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_cobraStartDate" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("cobraStartDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" PopupPosition="BottomRight" ID="CalendarExtender8" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_cobraStartDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" TargetControlID="txt_cobraStartDate" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_cobraEndDate" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("cobraEndDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender PopupPosition="BottomRight" CssClass="black" ID="CalendarExtender9" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_cobraEndDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" TargetControlID="txt_cobraEndDate" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:HiddenField ID="hdn_coverageId" Value='<%# Eval("Id") %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_enrollmentName" Value='<%# Eval("enrollmentName") %>' runat="server" />
                                                                             <asp:Button ID="btn_coverageplus" CssClass="imgbtnplus" ValidationGroup="sub_coverage" OnClick="btn_coverageplus_Click" runat="server" />
                                                                             <asp:Button ID="btn_coverageminus" CssClass="imgbtnminus" ClientIDMode="Static" Visible="false" OnClick="btn_coverageminus_Click" runat="server" />
                                                                         </td>
                                                                     </tr>
                                                                 </ItemTemplate>
                                                             </asp:Repeater>
                                                         </table>
                                                     </div>
                                                 </asp:Panel>
                                             </ContentTemplate>
                                         </ajaxToolkit:TabPanel>
                                         <ajaxToolkit:TabPanel ID="tabCoveredIndividual" TabIndex="4" Style="height:180px;overflow-y:scroll;" runat="server">
                                             <HeaderTemplate>
                                                 Covered Individual
                                             </HeaderTemplate>
                                             <ContentTemplate>
                                                 <asp:Panel ID="CoveredIndividualPanel" runat="server">
                                                     <div class="employee_info2" style="border: none;">
                                                         <table style="width: 100%; text-align:center;"  border="0">
                                                             <tr>
                                                                 <td>First Name</td>
                                                                 <td>Last Name</td>
                                                                 <td>SSN</td>
                                                                 <td>DOB</td>
                                                                 <td>All</td>
                                                                 <td>Jan</td>
                                                                 <td>Feb</td>
                                                                 <td>Mar</td>
                                                                 <td>Apr</td>
                                                                 <td>May</td>
                                                                 <td>Jun</td>
                                                                 <td>July</td>
                                                                 <td>Aug</td>
                                                                 <td>Sep</td>
                                                                 <td>Oct</td>
                                                                 <td>Nov</td>
                                                                 <td>Dec</td>
                                                             </tr>
                                                             <asp:Repeater ID="rpt_IndividualData" OnItemDataBound="rpt_IndividualData_ItemDataBound" runat="server">
                                                                 <ItemTemplate>
                                                                     <tr>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_first" Text='<%# Eval("firstName") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <asp:RequiredFieldValidator ID="rfv" runat="server" ValidationGroup="sub_CI" ControlToValidate="txt_first" ForeColor="Red" ErrorMessage="A first name is required." Display="None"></asp:RequiredFieldValidator>
                                                                             <ajaxToolkit:ValidatorCalloutExtender Width="200px" HighlightCssClass="validatorCalloutHighlight" ID="ValidatorCalloutExtender1" TargetControlID="rfv" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_last" Text='<%# Eval("lastName") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_ssn" Text='<%# Eval("employeeSSN") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_dob" MaxLength="10" AutoComplete="off" Text='<%# FixDateFormat(Eval("birthday").ToString()) %>' Placeholder="MM/dd/yyyy" CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender7" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_dob" runat="server"></ajaxToolkit:CalendarExtender>
                                                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" TargetControlID="txt_dob" ValidChars="./" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_all" Checked='<%# Eval("allCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_all" Value='<%# Eval("allCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_jan" Checked='<%# Eval("janCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_jan" Value='<%# Eval("janCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_feb" Checked='<%# Eval("febCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_feb" Value='<%# Eval("febCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_mar" Checked='<%# Eval("marCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_mar" Value='<%# Eval("marCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_apr" Checked='<%# Eval("aprCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_apr" Value='<%# Eval("aprCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_may" Checked='<%# Eval("mayCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_may" Value='<%# Eval("mayCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_jun" Checked='<%# Eval("junCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_jun" Value='<%# Eval("junCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_jul" Checked='<%# Eval("julCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_jul" Value='<%# Eval("julCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_aug" Checked='<%# Eval("augCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_aug" Value='<%# Eval("augCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_sep" Checked='<%# Eval("sepCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_sep" Value='<%# Eval("sepCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_oct" Checked='<%# Eval("octCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_oct" Value='<%# Eval("octCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_nov" Checked='<%# Eval("novCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_nov" Value='<%# Eval("novCoverage") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_dec" Checked='<%# Eval("decCoverage").ToString() == "0" ? false : true %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_dec" Value='<%# Eval("decCoverage") %>' runat="server" />
                                                                             <asp:HiddenField ID="hdn_CI_id" Value='<%# Eval("Id") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:Button ID="btn_CIplus" CssClass="imgbtnplus" ValidationGroup="sub_CI" OnClick="btn_CIplus_Click" runat="server" />
                                                                             <asp:Button ID="btn_CIminus" CssClass="imgbtnminus" ClientIDMode="Static" Visible="false" OnClick="btn_CIminus_Click" runat="server" />
                                                                         </td>
                                                                     </tr>
                                                                 </ItemTemplate>
                                                             </asp:Repeater>
                                                         </table>
                                                     </div>
                                                 </asp:Panel>
                                            </ContentTemplate>
                                         </ajaxToolkit:TabPanel>
                                     </ajaxToolkit:TabContainer>
                                 </div>

                                 <div class="employee_info3">
                                     <table style="width: 100%; height:100%; border:1px solid gray;background-color:white;" border="1">
                                         <tr style="background:#3A92C8; color:white;height:20px;">
                                            <td>Code Type</td>
                                            <td>All</td>
                                            <td>Jan</td>
                                            <td>Feb</td>
                                            <td>Mar</td>
                                            <td>Apr</td>
                                            <td>May</td>
                                            <td>Jun</td>
                                            <td>July</td>
                                            <td>Aug</td>
                                            <td>Sep</td>
                                            <td>Oct</td>
                                            <td>Nav</td>
                                            <td>Dec</td>
                                        </tr>
                                         <%--<asp:Repeater ID="rpt_code" OnItemDataBound="rpt_code_ItemDataBound" runat="server">
                                             <ItemTemplate>--%>
                                                 <tr style="height: 25px;border:1px solid gray; font-size: 9pt;">
                                                     <td>Offer of Coverage</td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1All" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_ALLM_COC" runat="server" Value='<%# Eval("ALLM_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1Jan" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_JAN_COC" runat="server" Value='<%# Eval("JAN_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1Feb" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_FEB_COC" runat="server" Value='<%# Eval("FEB_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1Mar" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_MAR_COC" runat="server" Value='<%# Eval("MAR_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1Apr" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_APR_COC" runat="server" Value='<%# Eval("APR_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1May" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_MAY_COC" runat="server" Value='<%# Eval("MAY_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1Jun" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_JUN_COC" runat="server" Value='<%# Eval("JUN_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1Jul" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_JUL_COC" runat="server" Value='<%# Eval("JUL_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1Aug" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_AUG_COC" runat="server" Value='<%# Eval("AUG_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1Sep" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_SEP_COC" runat="server" Value='<%# Eval("SEP_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1Oct" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_OCT_COC" runat="server" Value='<%# Eval("OCT_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1Nov" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_NOV_COC" runat="server" Value='<%# Eval("NOV_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_1Dec" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>1A</asp:ListItem>
                                                             <asp:ListItem>1B</asp:ListItem>
                                                             <asp:ListItem>1C</asp:ListItem>
                                                             <asp:ListItem>1D</asp:ListItem>
                                                             <asp:ListItem>1E</asp:ListItem>
                                                             <asp:ListItem>1F</asp:ListItem>
                                                             <asp:ListItem>1G</asp:ListItem>
                                                             <asp:ListItem>1H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_DEC_COC" runat="server" Value='<%# Eval("DEC_COC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                 </tr>
                                                 <tr style="height:25px; border:1px solid gray;font-size:9pt;">
                                                     <td>Premimum Amount</td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_ALLM_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="lbl_ALLM_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_JAN_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="lbl_JAN_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_FEB_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="lbl_FEB_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_MAR_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="lbl_MAR_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_APR_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="lbl_APR_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_MAY_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="lbl_MAY_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_JUN_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="lbl_JUN_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_JUL_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="lbl_JUL_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_AUG_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="lbl_AUG_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_SEP_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="lbl_SEP_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_OCT_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="lbl_OCT_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_NOV_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="lbl_NOV_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="lbl_DEC_LCMP" Width="40px" runat="server" Text=""></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="lbl_DEC_LCMP" ValidChars="." FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                     </td>
                                                 </tr>
                                                 <tr style="height:25px; border:1px solid gray;font-size:9pt;">
                                                     <td>Applicable Section 4980</td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2All" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_ALLM_SHC" runat="server" Value='<%# Eval("ALLM_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2Jan" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_JAN_SHC" runat="server" Value='<%# Eval("JAN_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2Feb" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_FEB_SHC" runat="server" Value='<%# Eval("FEB_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2Mar" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_MAR_SHC" runat="server" Value='<%# Eval("MAR_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2Apr" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_APR_SHC" runat="server" Value='<%# Eval("APR_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2May" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_MAY_SHC" runat="server" Value='<%# Eval("MAY_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2Jun" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_JUN_SHC" runat="server" Value='<%# Eval("JUN_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2Jul" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_JUL_SHC" runat="server" Value='<%# Eval("JUL_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2Aug" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_AUG_SHC" runat="server" Value='<%# Eval("AUG_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2Sep" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_SEP_SHC" runat="server" Value='<%# Eval("SEP_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2Oct" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_OCT_SHC" runat="server" Value='<%# Eval("OCT_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2Nov" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <%--<asp:HiddenField ID="lbl_NOV_SHC" runat="server" Value='<%# Eval("NOV_SHC") %>'></asp:HiddenField>--%>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="drp_2Dec" runat="server" style="width:80%;height:100%;border:none;float:right;">
                                                             <asp:ListItem Value="">--</asp:ListItem>
                                                             <asp:ListItem>2A</asp:ListItem>
                                                             <asp:ListItem>2B</asp:ListItem>
                                                             <asp:ListItem>2C</asp:ListItem>
                                                             <asp:ListItem>2D</asp:ListItem>
                                                             <asp:ListItem>2E</asp:ListItem>
                                                             <asp:ListItem>2F</asp:ListItem>
                                                             <asp:ListItem>2G</asp:ListItem>
                                                             <asp:ListItem>2H</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <asp:HiddenField ID="lbl_DEC_SHC" runat="server" Value=""></asp:HiddenField>
                                                         <asp:HiddenField ID="hdn_filingyear" Value='0' runat="server" />
                                                         <asp:HiddenField ID="hdn_dependent" Value='0' runat="server" />
                                                         <asp:HiddenField ID="hdn_flagEmp" Value='0' runat="server" />
                                                         <asp:HiddenField ID="hdn_disable" Value='0' runat="server" />
                                                     </td>
                                                 </tr>
                                             <%--</ItemTemplate>
                                         </asp:Repeater>--%>
                                     </table>
                                 </div>

                                 
                                <%-- Button save and cancel--%>
                              <div class="employee_info4">
                                    <asp:HiddenField ID="hdn_id" Value="0" runat="server" />
                                    <asp:HiddenField ID="hdn_EmployerTaxId" Value="" runat="server" />
                                    <asp:Button ID="btn_save" ValidationGroup="save"  CssClass="btn1" OnClick="btn_Save_Click" runat="server" Text="Save" />
                                    <asp:Button ID="btn_reset" CssClass="btn1" OnClick="btn_reset_Click" runat="server" Text="Clear" />
                                    <asp:Button ID="btn_delete" OnClientClick="return confirm('Are you sure you want to delete this Employer?');" CssClass="btn1" Visible="false" OnClick="btn_delete_Click" runat="server" Text="Delete" />
                                </div>
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
