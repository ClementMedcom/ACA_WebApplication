<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ACA_WebApplication.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="UTF-8">
    <title>Medcom Login</title>
    <link rel="stylesheet" href="css/login.css">
    <script src="js/jquery.min.js.js"></script>
</head>
<body>    
    
        <div class="container">
            <div class="logo">
                <img src="assets/images/logo.png" width="250" height="80" />
            </div>
            <form id="login" runat="server">
                
            <div class="loginfrm" id="loginfrm">
                <asp:TextBox ID="txt_uname" ValidationGroup="login" AutoComplete="off" class="txt" placeholder="User Name"  runat="server"></asp:TextBox>
                <asp:TextBox ID="txt_pwd" TextMode="Password" ValidationGroup="login" class="txt" placeholder="Password" runat="server"></asp:TextBox>
                <br /><br />
                <asp:Button ID="btn_login" CssClass="btn" ValidationGroup="login" runat="server" Text="Login" OnClick="btn_login_Click" />
                <%--<input type="button" onclick="window.location='Employer.aspx'" class="btn" value="Login" name="btn_login" />--%>
                <br />
                <div class="forget" id="fget">Forget Password</div> 
                <asp:Label ID="lb_status" CssClass="status" runat="server" Font-Size="X-Small" ForeColor="Red"></asp:Label>
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
