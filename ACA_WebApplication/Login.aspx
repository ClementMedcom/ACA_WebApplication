<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ACA_WebApplication.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="UTF-8">
    <title>Medcom Login</title>
    <link rel="stylesheet" href="css/login.css">
    <script type="text/javascript" src="js/jquery.min.js.js"></script>
    <script type="text/javascript">
        function chk_login() {
            var param = $('#txt_uname').val() + ',' + $('#txt_pwd').val();
            $('#lb_status').hide();
            $('#btn_login').hide();
            $('#log_load').show();
            $.ajax({
                type: "POST",
                url: "Login.aspx/chk_login",
                data: '{param: "' + param + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function(response) {
                    alert(response.d);
                }
            });
            return false;
        }
        function OnSuccess(response) {
            
            if (response.d == "1") {
                window.location = "../Master/Company_List.aspx";
            } else {
                $('#txt_pwd').val("");
                $('#lb_status').show();
                $('#btn_login').show();
                $('#log_load').hide();
                $("#lb_status").text("Invalid Username and Password");
            }
            
        }
    </script>
</head>
<body>    
    
        <div class="container">
            <div class="logo">
                <img src="assets/images/logo.png" width="250" height="80" />
            </div>
            <form id="login" runat="server">
                
            <div class="loginfrm" id="loginfrm">
                
                
                    <span class="bar"></span>
                
            
                <asp:TextBox ID="txt_uname" ValidationGroup="login" AutoComplete="off" class="txt" placeholder="User Name"  runat="server"></asp:TextBox>
                <asp:TextBox ID="txt_pwd" TextMode="Password" ValidationGroup="login" class="txt" placeholder="Password" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="lb_status" CssClass="status" style="margin: 0px 30px;font-size: 10pt;" runat="server" ForeColor="Red"></asp:Label>
                <br />
               <%-- <asp:Button ID="btn_login" CssClass="btn" ValidationGroup="login" runat="server" Text="Login" OnClick="btn_login_Click" />--%>
                 
                <img src="img/log_load.gif" style="display:none;" id="log_load" />
                <%--<input type="button" onclick="return chk_login()" class="btn" value="Login" id="btn_login" name="btn_login" />--%>
                <asp:Button ID="btn_login" CssClass="btn"  runat="server" Text="Login" OnClientClick="return chk_login()" />
                <br />
                <div class="forget" id="fget">Forgot Password</div> 
               
            </div>
          </form>
        
    <div class="forget_form" id="forget_form">
        <form> 
        Need your email to recover password<br />
        <input type="text" class="txt" placeholder="Email id" name="txt_mailid" /><br /><br />
        <input type="button" onclick="" class="btn" value="Submit" name="btn_submit" />
        <input type="button" id="btn_close" class="btn" value="Close" name="btn_submit" />
            </form>
    </div>
            </div>
</body>
</html>
 <script>
        $("#fget").click(function () {
            $("#forget_form").css("left", "100%");
            $("#loginfrm").css("left", "100%");

            $("#forget_form").css("transition", "left .6s");
            $("#loginfrm").css("transition", "left .6s");
        });

        $("#btn_close").click(function () {
            $("#forget_form").css("left", "-100%");
            $("#loginfrm").css("left", "0%");
            $("#forget_form").css("transition", "left .6s");
            $("#loginfrm").css("transition", "left .6s");
        });
</script>
