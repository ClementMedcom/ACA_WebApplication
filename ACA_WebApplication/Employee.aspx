<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="ACA_WebApplication.Employee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function page_load() {
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <link href="css/Employer.css" rel="stylesheet" />
    <link href="css/Employee.css" rel="stylesheet" />
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
 <div id="content3">
    <div id="content1"  >
                                               <%-- <table  style="width:100%;">
                                                    <tr>
                                                        <th>First Name </th>
                                                        <th>Last Name</th>
                                                        <th>SSN</th>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>--%>
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
            </div>

    </div>
    <div id="content2">
            <div class="hdr" >Employee Information</div><br />

               <table style="width:100%;margin-right:auto;margin-left:20px">
            <%-- <tr >
                <td colspan="2">
                    <div class="hdr" >Employee Information</div>
                </td>
            </tr>--%>
             
            <tr style="margin-left:2px;" >
                <td>Employee Name</td>
                <td >
                    <select class="cmb">
                        <option></option>
                    </select></td>
            </tr>
            <tr >
                <td>First Name</td>
                <td>
                    <input type="text" class="txt" /></td>
            </tr>
            <tr >
                <td>Middle Name</td>
                <td>
                    <input type="text" class="txt" /></td>
            </tr>
            <tr>
                <td>Last Name</td>
                <td>
                    <input type="text" class="txt" /></td>
            </tr>
            <tr>
                <td>Social Security Number</td>
                <td>
                    <input type="text" class="txt" /></td>
            </tr>
            <tr >
                <td>Date of Birth</td>
                <td>
                    <input type="text" class="txt" /></td>
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
        <table >
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
         <table id="table1" border="1">
            <tr>
                <th>Hire Date </th>
                <th >Terminate Date</th>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style1"></td>
            </tr>
        </table>
    </div>           <%-- Tab Status--%>
    <div id="status-tab" class="tab-content">  
         <table id="table1" border="1">
            <tr>
                <th class="auto-style32">Status </th>
                <th class="auto-style31">Status Start Date</th>
                <th class="auto-style33">Status End Date</th>
            </tr>
            <tr>
                <td class="auto-style32"></td>
                <td class="auto-style31"></td>
                <td class="auto-style33"></td>
            </tr>

        </table>
    </div>                <%--  Tab Coverage--%>
     <div id="coverage-tab" class="tab-content">  
         <table id="table1" border="1">
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
            <tr>

                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
</div>    <br />     <%-- Table view content--%>
     <div>
     <table id="table" border="1">
         <tr style="width=60%">
             <th >Code Type </th>
             <th  >Jan</th>
             <th >Feb</th>
             <th >Mar</th>
             <th >Apr</th>
             <th>May</th>
             <th >Jun</th>
             <th >July</th>
             <th >Aug</th>
             <th>Sep</th>
             <th >Nav</th>
             <th>Dec</th>
         </tr>
         <tr>
             <td ></td>
             <td ></td>
             <td ></td>
             <td ></td>
             <td></td>
             <td ></td>
             <td ></td>
             <td></td>
             <td></td>
             <td ></td>
             <td></td>
             <td></td>
         </tr>
     </table>
   </div>
        <br />  <br />    <br />    <br />    <br />   
     
         <div>
               <table id="table"   border="1">
                    <tr>
                                     <th>First Name </th> <th>Last Name </th> <th>SSN </th>  <th>DOB </th>  <th>Code Type </th>  <th>Jan</th> <th>Feb</th> <th>Mar</th> <th>Apr</th> <th>May</th> <th>Jun</th> <th>July</th> <th>Aug</th> <th>Sep</th> <th>Nav</th> <th >Dec</th>
                    </tr>
                    <tr>
                                       <td> </td>  <td></td> <td></td> <td></td> <td></td> <td></td> <td></td> <td></td> <td></td> <td></td> <td></td> <td></td <td></td> <td></td> <td></td> <td class="auto-style11"></td> <td class="auto-style10"></td>
                    </tr>
               </table>
           </div>
        
       
        <%-- Button save and cancel--%> 
        <br />
              <br />
        <br />
            <div>
               <%--  <table style="align-content:center;width:100%" >
                     <tr></tr>
                     <tr><th></th>
                <th><asp:Button ID="btn_save" runat="server" Text="Save Changes" Width="104px"  /></th> 
                <th> <asp:Button ID="btn_cancel" runat="server" Text="Cancel" /></th>
                         <th></th>
                    </tr>
                     <tr></tr>
                 </table>--%>
                <asp:Button ID="btn_save" runat="server" Text="Save Changes" class="btn" />
                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn"/>
             </div>
        
 </div>

              

 </div>            
</asp:Content>
