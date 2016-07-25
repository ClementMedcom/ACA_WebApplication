<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Employee_Details.aspx.cs" Inherits="ACA_WebApplication.Master.Employee_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../css/Employer.css" rel="stylesheet" />
    <link href="../css/FormStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function page_load() {
            $(document).ready(function () {
                var activeTabIndex = -1;
                var tabNames = ["information", "hireinfo", "status", "coverage"];

                $(".tab-menu > li").click(function (e) {
                    for (var i = 0; i < tabNames.length; i++) {
                        if (e.target.id == tabNames[i]) {
                            activeTabIndex = i;
                        } else {
                            $("#" + tabNames[i]).removeClass("active");
                            $("#" + tabNames[i] + "-tab").css("display", "none");
                        }
                    }
                    $("#" + tabNames[activeTabIndex] + "-tab").fadeIn();
                    $("#" + tabNames[activeTabIndex]).addClass("active");
                    return false;
                });
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
            <div id="content3">
                <div id="content1">
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
                                    <asp:TextBox ID="txtsearch" CssClass="srch" AutoPostBack="true" runat="server" placeholder="Search Employer" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                                    <asp:Repeater ID="rptEmployee" OnItemCommand="rptEmployee_ItemCommand" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lb_emp_list" ClientIDMode="AutoID" Style="text-decoration: none;" CommandName="Edit" runat="server">
                                                <div class="employer_list">
                                                    <div class="serial">
                                                        <%# Eval("RowNumber") %>
                                                    </div>
                                                    <div class="detail">
                                                        <b>Name  :</b><%# Eval("Name") %>
                                                        <br />
                                                        <b>SSN   :</b><asp:Label ID="lbl_ssn" runat="server" Text='<%# Eval("ssn") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdn_employerTaxId" Value='<%# Eval("taxid") %>' runat="server" />
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
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_save" />
                            <asp:AsyncPostBackTrigger ControlID="btn_reset" />
                            <asp:AsyncPostBackTrigger ControlID="rptEmployee" />
                            <asp:AsyncPostBackTrigger ControlID="lbl_close" />
                            <asp:AsyncPostBackTrigger ControlID="btn_delete" />
                            <asp:AsyncPostBackTrigger ControlID="rptHiredata" />
                            <asp:AsyncPostBackTrigger ControlID="rpt_Status" />
                            <asp:AsyncPostBackTrigger ControlID="rpt_coverage" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="content2">
                                <table style="width: 100%; height: 100px; margin-right: auto; margin-left: 5px">
                                    <tr>
                                        <td>Employer Name</td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txt_employer_name" style="width:98%;" class="txt" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>First Name</td>
                                        <td>
                                            <asp:TextBox ID="txt_firstname" class="txt" runat="server"></asp:TextBox>
                                        <td>Middle Name</td>
                                        <td>
                                            <asp:TextBox ID="txt_middlename" class="txt" runat="server"></asp:TextBox>
                                        <td>Last Name</td>
                                        <td>
                                            <asp:TextBox ID="txt_lastname" class="txt" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">Social Security Number
                                        <asp:TextBox ID="txt_SSN" class="txt" runat="server"></asp:TextBox>
                                        <td>Date of Birth</td>
                                        <td>
                                            <asp:TextBox ID="txt_dob" class="txt" runat="server"></asp:TextBox>
                                            <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txt_dob" runat="server"></ajaxToolkit:CalendarExtender>--%>
                                        </td>
                                    </tr>
                                </table>
                                <%--    Tab Menu--%>
                                <div id="tab-container">
                                    <ul class="tab-menu">
                                        <li id="information" class="active">Information</li>
                                        <li id="hireinfo">Hire Information</li>
                                        <li id="status">Status</li>
                                        <li id="coverage">Coverage </li>
                                    </ul>
                                    <div class="clear"></div>
                                    <div class="tab-top-border"></div>
                                    <%-- Default Tab--%>
                                    <div id="information-tab" class="tab-content active">
                                        <table style="width: 500px; margin-left: 20px">
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
                                            <tr>
                                                <td>Email Address</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txt_email" Width="95%" class="txt" runat="server"></asp:TextBox>
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
                                                <td >
                                                    <asp:DropDownList ID="drp_state" CssClass="cmb" runat="server">
                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Zipcode</td>
                                                <td >
                                                    <asp:TextBox ID="txt_zipcode" class="txt" runat="server"></asp:TextBox>
                                                </td>
                                                <td>Country</td>
                                                <td>
                                                    <asp:DropDownList ID="drp_country" CssClass="cmb" runat="server">
                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <%-- Tab Hire Information--%>
                                    <div id="hireinfo-tab" class="tab-content">
                                        <table class="table1" border="1">
                                            <tr>
                                                <th>Hire Date </th>
                                                <th>Terminate Date</th>
                                            </tr>
                                            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="rptHiredata" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:Repeater ID="rptHiredata" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_startdate" Text='<%# Eval("startDate") %>' runat="server"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_enddate" Text='<%# Eval("endDate") %>' runat="server"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:HiddenField ID="hdn_hireId" Value='<%# Eval("Id") %>' runat="server" />
                                                                    <asp:Button ID="btn_hireplus" OnClick="btn_hireplus_Click" runat="server" Text="+" />
                                                                    <asp:Button ID="btn_hireminus" ClientIDMode="Static" OnClick="btn_hireminus_Click" runat="server" Text="-" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </table>
                                    </div>
                                    <%-- Tab Status--%>
                                    <div id="status-tab" class="tab-content">
                                        <table class="table1" border="1">
                                            <tr>
                                                <th>Status </th>
                                                <th>Status Start Date</th>
                                                <th>Status End Date</th>
                                            </tr>
                                            <asp:Repeater ID="rpt_Status" OnItemDataBound="rpt_Status_ItemDataBound" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="drp_status" runat="server">
                                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td><asp:TextBox ID="txt_startdate" Text='<%# Eval("startDate") %>'  runat="server"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txt_enddate" Text='<%# Eval("endDate") %>' runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:HiddenField ID="hdn_statusId" Value='<%# Eval("Id") %>' runat="server" />
                                                            <asp:Button ID="btn_statusplus" OnClick="btn_statusplus_Click" runat="server" Text="+" />
                                                            <asp:Button ID="btn_statusminus" ClientIDMode="Static" OnClick="btn_statusminus_Click" runat="server" Text="-" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                    <%--  Tab Coverage--%>
                                    <div id="coverage-tab" class="tab-content">
                                        <div >
                                        <table class="table1" style="width:60%;overflow:auto;"border="1">
                                            <tr>
                                                <th>Union Member </th>
                                                <th>Contribution start</th>
                                                <th>Contribution End</th>
                                                <th>Offer Date</th>
                                                <th>Plan</th>
                                                <th>Entrolled</th>
                                                <th>Start Date</th>
                                                <th>End date</th>
                                                <th>CORBA</th>
                                                <th>CORBA start</th>
                                                <th>CORBA End</th>
                                            </tr>
                                             <asp:Repeater ID="rpt_coverage" OnItemDataBound="rpt_coverage_ItemDataBound" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chk_unionMember" runat="server" />
                                                            <asp:HiddenField ID="hdn_unionMember" Value='<%# Eval("unionMember") %>' runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_contributionStartDate" Text='<%# Eval("contributionStartDate") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_contributionEndDate" Text='<%# Eval("contributionEndDate") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_coverageOfferDate" Text='<%# Eval("coverageOfferDate") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_name" Text='<%# Eval("name") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chk_enrolled" runat="server" />
                                                            <asp:HiddenField ID="hdn_enrolled" Value='<%# Eval("isEnrolled") %>' runat="server" />
                                                        </td>
                                                         <td>
                                                            <asp:TextBox ID="txt_coverageStartDate" Text='<%# Eval("coverageStartDate") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_coverageEndDate" Text='<%# Eval("coverageEndDate") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chk_cobraEnrolled" runat="server" />
                                                            <asp:HiddenField ID="hdn_cobraEnrolled" Value='<%# Eval("cobraEnrolled") %>' runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_cobraStartDate" Text='<%# Eval("cobraStartDate") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_cobraEndDate" Text='<%# Eval("cobraEndDate") %>' CssClass="txt_short" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdn_coverageId" Value='<%# Eval("Id") %>' runat="server" />
                                                            <asp:Button ID="btn_coverageplus" OnClick="btn_coverageplus_Click" runat="server" Text="+" />
                                                            <asp:Button ID="btn_coverageminus" ClientIDMode="Static" OnClick="btn_coverageminus_Click" runat="server" Text="-" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                            </div>
                                    </div>
                                </div>
                                <br />
                                <%-- Table view content--%>
                                <div style="height: 90px; overflow-y: auto;">
                                    <table id="table" border="1">
                                        <tr>
                                            <td>Code Type </td>
                                            <td>Jan</td>
                                            <td>Feb</td>
                                            <td>Mar</td>
                                            <td>Apr</td>
                                            <td>May</td>
                                            <td>Jun</td>
                                            <td>July</td>
                                            <td>Aug</td>
                                            <td>Sep</td>
                                            <td>Nav</td>
                                            <td>Dec</td>
                                        </tr>

                                    </table>
                                </div>
                                <div style="height: 90px; overflow-y: auto;">
                                    <table id="table" border="1">
                                        <tr>
                                            <td>First Name </td>
                                            <td>Last Name </td>
                                            <td>SSN </td>
                                            <td>DOB </td>
                                            <td>Code Type </td>
                                            <td>Jan</td>
                                            <td>Feb</td>
                                            <td>Mar</td>
                                            <td>Apr</td>
                                            <td>May</td>
                                            <td>Jun</td>
                                            <td>July</td>
                                            <td>Aug</td>
                                            <td>Sep</td>
                                            <td>Nav</td>
                                            <td>Dec</td>
                                        </tr>
                                    </table>
                                </div>
                                <%-- Button save and cancel--%>
                                <div>
                                    <asp:HiddenField ID="hdn_id" Value="0" runat="server" />
                                    <asp:Button ID="btn_save" ValidationGroup="save"  CssClass="btn" OnClick="btn_Save_Click" runat="server" Text="Save" />
                                    <asp:Button ID="btn_reset" CssClass="btn" OnClick="btn_reset_Click" runat="server" Text="Clear" />
                                    <asp:Button ID="btn_delete" OnClientClick="return confirm('Are you sure you want to delete this Employer?');" CssClass="btn" Visible="false" OnClick="btn_delete_Click" runat="server" Text="Delete" />
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                
            </div>
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
