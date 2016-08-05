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
                <img src="img/logo.jpg" />
            </div>
            <form id="login" runat="server">
                
            <div>
                <asp:TextBox ID="txt_uname" ValidationGroup="login"  class="txt" placeholder="User Name"  runat="server"></asp:TextBox>
                 <asp:TextBox ID="txt_pwd" TextMode="Password" ValidationGroup="login" class="txt" placeholder="Password" runat="server"></asp:TextBox>
                </div> 
            <div>
                <br />
                <asp:Button ID="btn_login" CssClass="btn" ValidationGroup="login" runat="server" Text="Login" OnClick="btn_login_Click" />
                <%--<input type="button" onclick="window.location='Employer.aspx'" class="btn" value="Login" name="btn_login" />--%>
                <br />
                <div class="forget" id="fget">Forget Password</div> 
                <asp:Label ID="lb_status" CssClass="status" runat="server" Font-Size="X-Small" ForeColor="Red"></asp:Label>
            </div>
          </form>
        </div>
    <div class="forget_form" id="forget_form">
        <form> 
        What is your E-Mail ID ?<br />
        <input type="text" class="txt" placeholder="Mail id" name="txt_mailid" /><br /><br />
        <input type="button" onclick="" class="btn" value="Submit" name="btn_submit" />
        <input type="button" id="btn_close" class="btn" value="Close" name="btn_submit" />
            </form>
    </div>
</body>
</html>
 <script>
        $("#fget").click(function () {
            $("#forget_form").css("top", "53%");
            $("#forget_form").css("transition", "top .8s");
        });
        $("#btn_close").click(function () {
            $("#forget_form").css("top", "-50%");
            $("#forget_form").css("transition", "top .8s");
        });
</script>
