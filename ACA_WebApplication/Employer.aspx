<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Employer.aspx.cs" Inherits="ACA_WebApplication.Employer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="css/Employer.css" rel="stylesheet" />
   <script>
   $(document).ready(function(){
    $(".ginfo").click(function(){
        $(".tab1").css('width', '90%')
        $(".tab2").css('width', '0%')
        $(".tab1").css('transition', 'width 0.4s')
        $(".tab2").css('transition', 'width 0.4s')
    });
    $(".cdetail").click(function () {
        $(".tab2").css('width', '90%')
        $(".tab1").css('width', '0%')
        $(".tab1").css('transition', 'width 0.4s')
        $(".tab2").css('transition', 'width 0.4s')
    });
    });
</script>
   
   <div class="heading">Employer Details</div>
    <div class="grid_container">
        <input type="text" class="srch" placeholder="Search Employer"/>
        <div class="employer_list">
            <div class="serial">
                1
            </div>
            <div class="detail">
                <b>TAX ID  :</b> 54654654<br />
                <b>Employer:</b> 75 Arlington Street<br />  
            </div>
        </div>
        <div class="employer_list">
            <div class="serial">
                2
            </div>
            <div class="detail">
                <b>TAX ID  :</b> 54654654<br />
                <b>Employer:</b> 75 Arlington Street<br />  
            </div>
        </div>
        <div class="employer_list">
            <div class="serial">
                3
            </div>
            <div class="detail">
                <b>TAX ID  :</b> 54654654<br />
                <b>Employer:</b> 75 Arlington Street<br />  
            </div>
            </div>
        <div class="employer_list">
            <div class="serial">
                4
            </div>
            <div class="detail">
                <b>TAX ID  :</b> 54654654<br />
                <b>Employer:</b> 75 Arlington Street<br />  
            </div>
            </div>
        <div class="employer_list">
            <div class="serial">
                5
            </div>
            <div class="detail">
                <b>TAX ID  :</b> 54654654<br />
                <b>Employer:</b> 75 Arlington Street<br />  
            </div>
            </div>
        <div class="employer_list">
            <div class="serial">
                6
            </div>
            <div class="detail">
                <b>TAX ID  :</b> 54654654<br />
                <b>Employer:</b> 75 Arlington Street<br />  
            </div>
            </div>
        <div class="employer_list">
            <div class="serial">
                7
            </div>
            <div class="detail">
                <b>TAX ID  :</b> 54654654<br />
                <b>Employer:</b> 75 Arlington Street<br />  
            </div>
            </div>
        <div class="employer_list">
            <div class="serial">
                8
            </div>
            <div class="detail">
                <b>TAX ID  :</b> 54654654<br />
                <b>Employer:</b> 75 Arlington Street<br />  
            </div>
            </div>
        <div class="employer_list">
            <div class="serial">
                9
            </div>
            <div class="detail">
                <b>TAX ID  :</b> 54654654<br />
                <b>Employer:</b> 75 Arlington Street<br />  
            </div>
            </div>
        <div class="employer_list">
            <div class="serial">
                10
            </div>
            <div class="detail">
                <b>TAX ID  :</b> 54654654<br />
                <b>Employer:</b> 75 Arlington Street<br />  
            </div>
            </div>
        
    </div>
    <div class="employer_form">
        <div class="tab">
            <div class="ginfo">General Information</div><div class="cdetail">1094-C Details</div>
        </div>
        <div class="tab1">
                <table class="tab1_data"> 
                <tr>
                    <td>Employer Name</td><td><input type="text" class="txt" /></td>
                    <td>EIN</td><td><input type="text" class="txt" /></td>
                </tr>
                <tr>
                    <td>Filling Year</td><td><select class="cmb" ><option>Year</option></select></td>
                    <td>Address</td><td><input type="text" class="txt" /></td>
                </tr>
                <tr>
                    <td>Address2</td><td><input type="text" class="txt" /></td>
                    <td>City</td><td><input type="text" class="txt"/></td>
                </tr>
                <tr>
                    <td>State</td><td><select class="cmb"><option>Select State</option></select></td>
                    <td>Zip Code</td><td><input type="text" class="txt"/></td>
                </tr>
                <tr>
                    <td>Country</td><td><select class="cmb"><option>Select Country</option></select></td>
                    <td>Contact Name</td><td><input type="text" class="txt"/></td>
                </tr>
                <tr>
                    <td>Contact Phone</td><td><input type="text" class="txt"/></td>
                    <td>Title</td><td><input type="text" class="txt"/></td>
                </tr>
                <tr>
                    <td>Form Type</td><td><select class="cmb"><option>Select Type</option></select></td>
                    <td>Origin Code</td><td><select class="cmb"><option>Select Code</option></select></td>
                </tr>
                <tr>
                    <td>SHOP Identifier</td><td><input type="text" class="txt"/></td>
                    <td></td>
                </tr>
                </table>
                </div>
         <div class="tab2">
             <table class="tab2_data">
                        <tr><td>Certification of Eligibility</td></tr>
                        <tr><td><input type="checkbox" /> A.Qualifiying Offer Method</td></tr>
                        <tr><td><input type="checkbox" /> B.Qualifiying Offer Method Transition Relif</td></tr>
                        <tr><td><input type="checkbox" /> C.Section 4980H Transition Relif</td></tr>
                        <tr><td><input type="checkbox" /> D.98% Offer Method</td></tr>
                        <tr><td colspan="2"><table><tr><td>ALE Information</td><td><input type="checkbox" /></td><td>Disable Automatic Changes</td></tr></table></td></tr>
                        <tr><td>
                            <table class="mon_table">
                            <tr><td>Month</td><td>Minimum Coverage</td><td>Full-Time</td><td>Total</td><td>Aggregate</td><td>Sec4980H</td><td></td><td></td></tr>
                            <tr><td>All 12 Months</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Jan</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Feb</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Mar</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Apr</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>May</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Jun</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Jul</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Aug</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Sep</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Oct</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Nov</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Dec</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            </table></td>
                        </tr>
                    </table>
        </div>
        <div class="tab3">
            <div class="btn">Save</div><div class="btn">Cancel</div>
        </div>
         
        
       
            <%--<table style="width:100%;">
            <tr>
               <td colspan="2">
                <div class="gen_info" >
                <table>
                <tr>
                <td>
                <table> 
                <tr><td colspan="2"><div class="hdr">General Information</div></td></tr>
                <tr><td>Employer Name</td><td><input type="text" class="txt" /></td></tr>
                <tr><td>EIN</td><td><input type="text" class="txt" /></td></tr>
                <tr><td>Filling Year</td><td><select class="cmb" ><option>Year</option></select></td></tr>
                <tr><td>Address</td><td><input type="text" class="txt" /></td></tr>
                <tr><td>Address2</td><td><input type="text" class="txt" /></td></tr>
                <tr><td>City</td><td><input type="text" class="txt"/></td></tr>
                <tr><td>State</td><td><select class="cmb"><option>Select State</option></select></td></tr>
                <tr><td>Zip Code</td><td><input type="text" class="txt"/></td></tr>
                <tr><td>Country</td><td><select class="cmb"><option>Select Country</option></select></td></tr>
                <tr><td>Contact Name</td><td><input type="text" class="txt"/></td></tr>
                <tr><td>Contact Phone</td><td><input type="text" class="txt"/></td></tr>
                <tr><td>Title</td><td><input type="text" class="txt"/></td></tr>
                <tr><td>Form Type</td><td><select class="cmb"><option>Select Type</option></select></td></tr>
                <tr><td>Origin Code</td><td><select class="cmb"><option>Select Code</option></select></td></tr>
                <tr><td>SHOP Identifier</td><td><input type="text" class="txt"/></td></tr>
                </table>
                </div>
                               <div class="c_details" id="cdetail2">
                <table>
                <tr><td>   
                 <table>
                        <tr><td>Certification of Eligibility</td></tr>
                        <tr><td><input type="checkbox" /> A.Qualifiying Offer Method</td></tr>
                        <tr><td><input type="checkbox" /> B.Qualifiying Offer Method Transition Relif</td></tr>
                        <tr><td><input type="checkbox" /> C.Section 4980H Transition Relif</td></tr>
                        <tr><td><input type="checkbox" /> D.98% Offer Method</td></tr>
                        <tr><td colspan="2"><table><tr><td>ALE Information</td><td><input type="checkbox" /></td><td>Disable Automatic Changes</td></tr></table></td></tr>
                        <tr><td>
                            <table class="mon_table">
                            <tr><td>Month</td><td>Minimum Coverage</td><td>Full-Time</td><td>Total</td><td>Aggregate</td><td>Sec4980H</td><td></td><td></td></tr>
                            <tr><td>All 12 Months</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Jan</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Feb</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Mar</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Apr</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>May</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Jun</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Jul</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Aug</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Sep</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Oct</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Nov</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            <tr><td>Dec</td><td><input type="checkbox" /></td><td>0</td><td>0</td><td><input type="checkbox" /></td><td></td><td><input type="button" value="up" /></td><td><input type="button" value="dn" /></td></tr>
                            </table></td>
                        </tr>
                    </table>
                    </div>
                </td>
                </tr>
            <tr><td colspan="2"><input class="btn" type="button" value="Save"/><input type="button" class="btn" value="Cancel"/></td></tr> 
           </table>--%>
       </div>
</asp:Content>

