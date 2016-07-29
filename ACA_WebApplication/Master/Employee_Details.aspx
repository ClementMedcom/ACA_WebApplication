<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Employee_Details.aspx.cs" Inherits="ACA_WebApplication.Master.Employee_Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../css/Employer.css" rel="stylesheet" />
    <link href="../css/FormStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function page_load() {
            $('.employer_list').on('click', function () {
                $('.over').css( 'background-color', 'white');
                $(this).css({ 'background-color': '#add5f3', 'border-radius': '4px' });
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
            <div class="hdr">Employee Information</div>
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
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
                            <asp:TextBox ID="txtsearch" CssClass="srch" AutoPostBack="true" runat="server" placeholder="Search Employee" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                            <asp:Repeater ID="rptEmployee" OnItemCommand="rptEmployee_ItemCommand" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lb_emp_list" ClientIDMode="AutoID" Style="text-decoration: none;" CommandName="Edit" runat="server">
                                        <div class="employer_list over">
                                            <div class="serial">
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
                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_save" />
                            <asp:AsyncPostBackTrigger ControlID="btn_reset" />
                            <asp:AsyncPostBackTrigger ControlID="rptEmployee" />
                            <asp:AsyncPostBackTrigger ControlID="lbl_close" />
                            <asp:AsyncPostBackTrigger ControlID="btn_delete" />
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1$tabHire$rptHiredata" />
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1$tabStatus$rpt_Status" />
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1$tabEnrollment$rpt_coverage" />
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1$tabCoverage$rpt_IndividualData" />
                        </Triggers>
                        <ContentTemplate>
                             <div class="employee_form">
                                 <div class="employee_info1">
                                     <table style="width: 100%; height: 180px;">
                                         <tr>
                                             <td>Employer Name</td>
                                             <td>
                                                 <asp:DropDownList ID="drp_employer" CssClass="cmb" runat="server">
                                                 </asp:DropDownList>
                                             </td>
                                             <td>Social Security Number</td><td>
                                                <asp:TextBox ID="txt_SSN" class="txt" runat="server"></asp:TextBox>
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
                                             <td>Date of Birth</td>
                                             <td>
                                                 <asp:TextBox ID="txt_dob" class="txt" AutoComplete="off" placeholder="MM/dd/yyyy" runat="server"></asp:TextBox>
                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  CssClass="black" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_dob" runat="server"></ajaxToolkit:CalendarExtender>
                                             </td>
                                             <td>Email Address</td>
                                             <td>
                                                 <asp:TextBox ID="txt_email"  class="txt" runat="server"></asp:TextBox>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>Address</td>
                                             <td>
                                                 <asp:TextBox ID="txt_address1" class="txt" runat="server"></asp:TextBox>
                                             </td>
                                             <td>Address1</td>
                                             <td>
                                                 <asp:TextBox ID="txt_address2" class="txt" runat="server"></asp:TextBox>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>City</td>
                                             <td>
                                                 <asp:TextBox ID="txt_city" class="txt" runat="server"></asp:TextBox>
                                             </td>
                                             <td>State</td>
                                             <td>
                                                 <asp:DropDownList ID="drp_state" CssClass="cmb" runat="server">
                                                     <asp:ListItem Value="">--Select--</asp:ListItem>
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>

                                         <tr>
                                             <td>Zipcode</td>
                                             <td>
                                                 <asp:TextBox ID="txt_zipcode" class="txt" runat="server"></asp:TextBox>
                                             </td>
                                             <td>Country</td>
                                             <td>
                                                 <asp:DropDownList ID="drp_country" CssClass="cmb" runat="server">
                                                     <asp:ListItem Value="">--Select--</asp:ListItem>
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>Salary</td>
                                             <td>
                                                 <asp:TextBox ID="txt_salary" class="txt" runat="server"></asp:TextBox>
                                             </td>
                                             <td>Hourly</td>
                                             <td>
                                                 <asp:TextBox ID="txt_hourly" class="txt" runat="server"></asp:TextBox>
                                             </td>
                                         </tr>

                                     </table>
                               </div>
                                 <div style="width: 100%">
                                     <ajaxToolkit:TabContainer CssClass="MyTabStyle" ID="TabContainer1" runat="server">
                                         <ajaxToolkit:TabPanel ID="tabHire" TabIndex="0" Style="min-height:120px;max-height:120px;overflow-y:scroll;" runat="server">
                                             <HeaderTemplate>
                                                 Hire Data
                                             </HeaderTemplate>
                                             <ContentTemplate>
                                                 <asp:Panel ID="panelHire" runat="server">
                                                     <div class="employee_info2">
                                                         <table style="width: 90%; margin: 2px" border="0">
                                                             <tr>
                                                                 <th>Hire Date </th>
                                                                 <th>Terminate Date</th>
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
                                                                                     <asp:RequiredFieldValidator ControlToValidate="txt_startdate" ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="sub_hire" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                 </td>
                                                                                 <td>
                                                                                     <asp:TextBox ID="txt_enddate" MaxLength="10" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("endDate").ToString()) %>' CssClass="txt" runat="server"></asp:TextBox>
                                                                                     <ajaxToolkit:CalendarExtender CssClass="black" PopupPosition="BottomRight" ID="CalendarExtender2" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_enddate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                                 </td>
                                                                                 <td>
                                                                                     <asp:HiddenField ID="hdn_hireId" Value='<%# Eval("Id") %>' runat="server" />
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
                                         <ajaxToolkit:TabPanel ID="tabStatus" TabIndex="1" Style="min-height:120px;max-height:120px;overflow-y:scroll;" runat="server">
                                             <HeaderTemplate>
                                                 Status
                                             </HeaderTemplate>
                                             <ContentTemplate>
                                                 <asp:Panel ID="Panel1" runat="server">
                                                     <div class="employee_info2" style="width:80%;">
                                                         <table style="width: 97%; margin: 2px" border="0">
                                                             <tr>
                                                                 <th>Status </th>
                                                                 <th>Status Start Date</th>
                                                                 <th>Status End Date</th>
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
                                                                                     <asp:HiddenField ID="hdn_status" runat="server" Value='<%# FixDateFormat(Eval("Status").ToString()) %>' />
                                                                                     <asp:RequiredFieldValidator ControlToValidate="drp_status" ID="RequiredFieldVsdalidator1" InitialValue="" ForeColor="Red" ValidationGroup="sub_status" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                 </td>
                                                                                 <td>
                                                                                     <asp:TextBox ID="txt_startdate" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("startDate").ToString()) %>' CssClass="txt" runat="server"></asp:TextBox>
                                                                                     <asp:RequiredFieldValidator ControlToValidate="txt_startdate" ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="sub_status" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                     <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendaDrExtender2" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_startdate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                                 </td>
                                                                                 <td>
                                                                                     <asp:TextBox ID="txt_enddate" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("endDate").ToString()) %>' CssClass="txt" runat="server"></asp:TextBox></td>
                                                                                 <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender3" PopupPosition="BottomRight" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_enddate" runat="server"></ajaxToolkit:CalendarExtender>
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
                                         <ajaxToolkit:TabPanel ID="tabEnrollment" TabIndex="2" Style="min-height:120px;max-height:120px;" runat="server">
                                             <HeaderTemplate>
                                                 Coverage
                                             </HeaderTemplate>
                                             <ContentTemplate>
                                                 <asp:Panel ID="Panel2" runat="server">
                                                     <div class="employee_info3">
                                                         <table style="width: 90%;" border="0">
                                                             <tr>
                                                                 <td>Union Member </td>
                                                                 <td>Contribution start</td>
                                                                 <td>Contribution End</td>
                                                                 <td>Offer Date</td>
                                                                 <td>Plan</td>
                                                                 <td>Entrolled</td>
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
                                                                             <asp:TextBox ID="txt_contributionStartDate" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("contributionStartDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender3" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_contributionStartDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_contributionEndDate" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("contributionEndDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender4" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_contributionEndDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_coverageOfferDate" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("coverageOfferDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender5" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_coverageOfferDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="drp_plan" ID="RequiredFieldVsdalidator1" InitialValue="" ForeColor="Red" ValidationGroup="sub_coverage" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                             <asp:DropDownList ID="drp_plan" CssClass="cmb_short" runat="server">
                                                                             </asp:DropDownList>
                                                                             <asp:HiddenField ID="hdn_planId" Value='<%# Eval("PlanId") %>' runat="server" />
                                                                             <%--<asp:TextBox ID="txt_name" Text='<%# Eval("name") %>' CssClass="txt_short" runat="server"></asp:TextBox>--%>
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_enrolled" runat="server" />
                                                                             <asp:HiddenField ID="hdn_enrolled" Value='<%# Eval("isEnrolled") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_coverageStartDate" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("coverageStartDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender6" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_coverageStartDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_coverageEndDate" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("coverageEndDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender7" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_coverageEndDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:CheckBox ID="chk_cobraEnrolled" runat="server" />
                                                                             <asp:HiddenField ID="hdn_cobraEnrolled" Value='<%# Eval("cobraEnrolled") %>' runat="server" />
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_cobraStartDate" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("cobraStartDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" PopupPosition="BottomRight" ID="CalendarExtender8" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_cobraStartDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_cobraEndDate" AutoComplete="off" placeholder="MM/dd/yyyy" Text='<%# FixDateFormat(Eval("cobraEndDate").ToString()) %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender PopupPosition="BottomRight" CssClass="black" ID="CalendarExtender9" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_cobraEndDate" runat="server"></ajaxToolkit:CalendarExtender>
                                                                         </td>
                                                                         <td>
                                                                             <asp:HiddenField ID="hdn_coverageId" Value='<%# Eval("Id") %>' runat="server" />
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
                                         <ajaxToolkit:TabPanel ID="tabCoverage" TabIndex="3" Style="min-height:120px;max-height:120px;" runat="server">
                                             <HeaderTemplate>
                                                 Covered Individual
                                             </HeaderTemplate>
                                             <ContentTemplate>
                                                 <asp:Panel ID="Panel3" runat="server">
                                                     <div class="employee_info3" style="border: none;">
                                                         <table style="width: 97%; margin: 2px" border="0">
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
                                                                             <asp:RequiredFieldValidator ID="rfv" runat="server" ValidationGroup="sub_CI" ControlToValidate="txt_first" ForeColor="Red" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_last" Text='<%# Eval("lastName") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_ssn" Text='<%# Eval("employeeSSN") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                                         </td>
                                                                         <td>
                                                                             <asp:TextBox ID="txt_dob" AutoComplete="off" Text='<%# FixDateFormat(Eval("birthday").ToString()) %>' Placeholder="MM/dd/yyyy" CssClass="txt_short" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:CalendarExtender CssClass="black" ID="CalendarExtender7" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_dob" runat="server"></ajaxToolkit:CalendarExtender>
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
                                     <table style="width: 97%; margin: 2px" border="1">
                                         <tr>
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
                                         <asp:Repeater ID="rpt_code" runat="server">
                                             <ItemTemplate>
                                                 <tr>
                                                     <td>Offer of Coverage</td>
                                                     <td><%# Eval("ALLM_COC") %></td>
                                                     <td><%# Eval("JAN_COC") %></td>
                                                     <td><%# Eval("FEB_COC") %></td>
                                                     <td><%# Eval("MAR_COC") %></td>
                                                     <td><%# Eval("APR_COC") %></td>
                                                     <td><%# Eval("MAY_COC") %></td>
                                                     <td><%# Eval("JUN_COC") %></td>
                                                     <td><%# Eval("JUL_COC") %></td>
                                                     <td><%# Eval("AUG_COC") %></td>
                                                     <td><%# Eval("SEP_COC") %></td>
                                                     <td><%# Eval("OCT_COC") %></td>
                                                     <td><%# Eval("NOV_COC") %></td>
                                                     <td><%# Eval("DEC_COC") %></td>
                                                 </tr>
                                                 <tr>
                                                     <td>Premimum Amount</td>
                                                     <td><%# Eval("ALLM_LCMP") %></td>
                                                     <td><%# Eval("JAN_LCMP") %></td>
                                                     <td><%# Eval("FEB_LCMP") %></td>
                                                     <td><%# Eval("MAR_LCMP") %></td>
                                                     <td><%# Eval("APR_LCMP") %></td>
                                                     <td><%# Eval("MAY_LCMP") %></td>
                                                     <td><%# Eval("JUN_LCMP") %></td>
                                                     <td><%# Eval("JUL_LCMP") %></td>
                                                     <td><%# Eval("AUG_LCMP") %></td>
                                                     <td><%# Eval("SEP_LCMP") %></td>
                                                     <td><%# Eval("OCT_LCMP") %></td>
                                                     <td><%# Eval("NOV_LCMP") %></td>
                                                     <td><%# Eval("DEC_LCMP") %></td>
                                                 </tr>
                                                 <tr>
                                                     <td>Applicable Section 4980</td>
                                                     <td><%# Eval("ALLM_SHC") %></td>
                                                     <td><%# Eval("JAN_SHC") %></td>
                                                     <td><%# Eval("FEB_SHC") %></td>
                                                     <td><%# Eval("MAR_SHC") %></td>
                                                     <td><%# Eval("APR_SHC") %></td>
                                                     <td><%# Eval("MAY_SHC") %></td>
                                                     <td><%# Eval("JUN_SHC") %></td>
                                                     <td><%# Eval("JUL_SHC") %></td>
                                                     <td><%# Eval("AUG_SHC") %></td>
                                                     <td><%# Eval("SEP_SHC") %></td>
                                                     <td><%# Eval("OCT_SHC") %></td>
                                                     <td><%# Eval("NOV_SHC") %></td>
                                                     <td><%# Eval("DEC_SHC") %></td>
                                                 </tr>
                                             </ItemTemplate>
                                         </asp:Repeater>
                                     </table>
                                 </div>

                                 
                                <%-- Button save and cancel--%>
                              <div class="employee_info4">
                                    <asp:HiddenField ID="hdn_id" Value="0" runat="server" />
                                    <asp:Button ID="btn_save" ValidationGroup="save"  CssClass="btn" OnClick="btn_Save_Click" runat="server" Text="Save" />
                                    <asp:Button ID="btn_reset" CssClass="btn" OnClick="btn_reset_Click" runat="server" Text="Clear" />
                                    <asp:Button ID="btn_delete" OnClientClick="return confirm('Are you sure you want to delete this Employer?');" CssClass="btn" Visible="false" OnClick="btn_delete_Click" runat="server" Text="Delete" />
                                </div>
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
