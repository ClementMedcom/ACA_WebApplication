<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="ACA_WebApplication.Employee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function page_load() {
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <link href="css/Employer.css" rel="stylesheet" />
   <%-- Script for Tab control--%>
    <script>
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
    </script>
   

 

   


   <%-- Divide a two content Page--%>
    
            <div class="hdr" >Employee Information</div>
 <div id="content3">
    <div id="content1"  >
            <div class="grid_container">
            <input type="text" class="srch" placeholder="Search Employee"/>
            <div class="employer_list">
                <div class="serial">
                    1
                </div>
                <div class="detail">
                    <b>Name  :</b> <br />
                    <b>SSN   :</b> <br /> 
                    
                </div>
            </div>
            <div class="employer_list">
                <div class="serial">
                    2
                </div>
                <div class="detail">
                    <b>Name       :</b> <br />
                    <b>SSN        :</b> <br />  
                </div>
            </div>
            <div class="employer_list">
                <div class="serial">
                    3
                </div>
                <div class="detail">
                    <b>Name       :</b> <br />
                    <b>SSN        :</b> <br />  
                </div>
            </div>
            <div class="employer_list">
                <div class="serial">
                    4
                </div>
                <div class="detail">
                    <b>Name       :</b> <br />
                    <b>SSN        :</b> <br />  
                </div>
            </div>
            <div class="employer_list">
                <div class="serial">
                    5
                </div>
                <div class="detail">
                    <b>Name       :</b> <br />
                    <b>SSN        :</b> <br />  
                </div>
            </div>
            <div class="employer_list">
                <div class="serial">
                    6
                </div>
                <div class="detail">
                    <b>Name       :</b> <br />
                    <b>SSN        :</b> <br />  
                </div>
            </div>
            </div>

    </div>
    <div id="content2">

               <table style="width:100%;height:100px; margin-right:auto;margin-left:5px">
            <%-- <tr >
                <td colspan="2">
                    <div class="hdr" >Employee Information</div>
                </td>
            </tr>--%>
             
            <tr>
                <td>Employee Name</td>
                <td colspan="5""><select  style="width:98%;" class="cmb"><option></option></select></td>
            </tr>
            <tr >
                <td>First Name</td>
                <td><input type="text" class="txt" /></td>
                <td>Middle Name</td>
                <td><input type="text" class="txt" /></td>
                <td>Last Name</td>
                <td><input type="text" class="txt" /></td>
            </tr>
            <tr>
                <td colspan="4">Social Security Number <input style="width:65%" type="text" class="txt" /></td>
                <td>Date of Birth</td>
                <td><input type="text" class="txt" /></td>
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
    <div class="tab-top-border"></div>     <%-- Default Tab--%>
    <div id="information-tab" class="tab-content active">
        <table style="width:500px;margin-left:20px" >
            <tr>
                <td>Email Address</td>
                <td >
                    <input type="text" class="txt"  /></td>
            </tr>
            <tr>
                <td>Address</td>
                <td >
                    <input type="text" class="txt"  /></td>
            </tr>
            <tr>
                <td>Address1</td>
                <td>
                    <input type="text" class="txt"  /></td>
            </tr>
            <tr>
                <td>City</td>
                <td >
                    <input type="text" class="txt"  /></td>
            </tr>
            <tr>
                <td>State</td>
                <td >
                    <select class="cmb" >
                        <option></option>
                    </select></td>
            </tr>
            <tr>
                <td>Zipcode</td>
                <td >
                    <input type="text" class="txt" /></td>
            </tr>
            <tr>
                <td>Country</td>
                <td   >
                    <select class="cmb">
                        <option></option>
                    </select></td>
            </tr>
        </table>

       
    </div>           <%-- Tab Hire Information--%>
    <div id="hireinfo-tab" class="tab-content">
         <table class="table1" border="1">
            <tr>
                <td>Hire Date </td>
                <td>Terminate Date</td>
            </tr>
          </table>
    </div>           <%-- Tab Status--%>
    <div id="status-tab" class="tab-content">  
         <table class="table1" border="1">
            <tr>
                <td>Status </td>
                <td >Status Start Date</td>
                <td >Status End Date</td>
            </tr>
           </table>
    </div>               
         <%--  Tab Coverage--%>
     <div id="coverage-tab" class="tab-content">  
         <table class="table1" border="1">
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
        </table>
    </div>
</div>    <br />     <%-- Table view content--%>
     <div style="height:90px;overflow-y:auto;">
     <table id="table" border="1">
         <tr>
             <td >Code Type </td>
             <td  >Jan</td>
             <td >Feb</td>
             <td >Mar</td>
             <td >Apr</td>
             <td>May</td>
             <td >Jun</td>
             <td >July</td>
             <td >Aug</td>
             <td>Sep</td>
             <td >Nav</td>
             <td>Dec</td>
         </tr>
        
     </table>
   </div> 
         <div style="height:90px;overflow-y:auto;">
               <table id="table" border="1">
                    <tr>
                    <td>First Name </td> <td>Last Name </td> <td>SSN </td>  <td>DOB </td>  <td>Code Type </td>  <td>Jan</td> <td>Feb</td> <td>Mar</td> <td>Apr</td> <td>May</td> <td>Jun</td> <td>July</td> <td>Aug</td> <td>Sep</td> <td>Nav</td> <td>Dec</td>
                    </tr>
                   </table>
           </div>
        <%-- Button save and cancel--%> 
                <div>
                <asp:Button ID="btn_save" runat="server" Text="Save" cssclass="btn" />
                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" cssclass="btn"/>
             </div>
        
 </div>

              

 </div>            
</asp:Content>
