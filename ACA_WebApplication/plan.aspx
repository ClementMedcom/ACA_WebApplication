<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="plan.aspx.cs" Inherits="ACA_WebApplication.plan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
   <%-- <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>--%>
    <asp:UpdatePanel ID="upPanael" runat="server">
    <ContentTemplate>
    
    <link href="css/Employer.css" rel="stylesheet" />
    <div class="heading">PLan Details</div>
    <div class="grid_container">
        <input type="text" class="srch" placeholder="Search Employer"/>
        <div class="employer_list">
            <div class="serial">
                1
            </div>
            <div class="detail">
                <b>Plan ID  :</b> 54654654<br />
                75 Arlington Street<br />  
            </div>
        </div>
        <div class="employer_list">
            <div class="serial">
                2
            </div>
            <div class="detail">
                <b>Plan ID  :</b> 54654654<br />
                75 Arlington Street<br />  
            </div>
        </div>
        <div class="employer_list">
            <div class="serial">
                3
            </div>
            <div class="detail">
                <b>Plan ID  :</b> 54654654<br />
                75 Arlington Street<br />  
            </div>
            </div> 
        <div class="employer_list">
            <div class="serial">
                4
            </div>
            <div class="detail">
                <b>Plan ID  :</b> 54654654<br />
                75 Arlington Street<br />  
            </div>
            </div>  
        <div class="employer_list">
            <div class="serial">
                5
            </div>
            <div class="detail">
                <b>Plan ID  :</b> 54654654<br />
                75 Arlington Street<br />  
            </div>
            </div>  
        <div class="employer_list">
            <div class="serial">
                6
            </div>
            <div class="detail">
                <b>Plan ID  :</b> 54654654<br />
                75 Arlington Street<br />  
            </div>
            </div>     
    </div>
    <div class="planinfo">
        <div class="plan_header">
        <table style="width:100%;height:540px;">
            <tr><th colspan="4"">General Information</th></tr>
            <tr>
                <td >Plan Name</td>
                <td colspan="3"><input type="text" class="txt" style="width:95%;" /></td>
            </tr>
            <tr>
                <td>Funding Type</td><td><select class="cmb"><option>Select Type</option></select></td>
                <td>Plan Type</td><td><select class="cmb"><option>Select Type</option></select></td>
            </tr>
            <tr>
                <td>Waiting Period</td><td><select class="cmb"><option>Select Period</option></select></td>
                <td colspan="2"><input type="text" class="txt_short" /> Days</td>
            </tr>
            <tr>
                <td>Offered to Spouse?</td><td><select class="cmb"><option>Select Option</option></select></td>
                <td>Offered to Dependance</td><td><select class="cmb"><option>Select Option</option></select></td>
            </tr>
            <tr>
                <td >Terminates On Date of Termination</td>
                <td colspan="3"><select class="cmb"><option>Select Option</option></select></td>
            </tr>
             <tr>
                <td>Plan Renewal Month</td><td><select class="cmb"><option>Select Month</option></select></td>
                <td>Minimum Value</td><td><select class="cmb"><option>Select Value</option></select></td>
            </tr>
            <tr>
                <td colspan="4"><div class="plan_detail"> </div></td>
            </tr>
            <tr>
                 <td >Banding Type</td>
                 <td colspan="3"><select class="cmb"><option>Select Type</option></select></td>
            </tr>
            <tr>
                <td colspan="4">
                    <div class="plan_table">
                        <table style="width:100%;">
                            <tr>
                                <td>Start Value</td><td>End Value</td><td>Amount</td><td></td>

                            </tr>
                            <%--Repeter--%>
           
                            <asp:Repeater ID="rpttable" runat="server">
                        <ItemTemplate>
                          <tr>
                              <td><asp:TextBox ID="tb1" CssClass="txt" Text='<%# Eval("Start") %>' runat="server"></asp:TextBox></td>
                              <td><asp:TextBox ID="tb2" CssClass="txt" Text='<%# Eval("End") %>' runat="server"></asp:TextBox></td>
                              <td><asp:TextBox ID="tb3" CssClass="txt" Text='<%# Eval("Amount") %>' runat="server"></asp:TextBox></td>
                              <td><asp:Button ID="btnAdd" cssclass="imgbtnplus"  runat="server"  onclick="btnAdd_Click" />
                                   <asp:Button ID="btnMinus" cssclass="imgbtnminus"  runat="server"/>
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
                <td colspan="4"><input type="button" value="Save" class="btn"/><input type="button" value="Clear" class="btn"/></td>
            </tr>
            
        </table>
        </div>
    </div>
        </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
