<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ACA_WebApplication.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="UTF-8">
    <title>Medcom Login</title>
    <link rel="stylesheet" href="css/login.css">


    <!-- Medcom Admin CSS Core -->
    <link rel="stylesheet" type="text/css" href="../assets/css/master/Main.min.css" />

    <script type="text/javascript" src="js/jquery.min.js.js"></script>
    <script type="text/javascript">
        function chk_login() {
            var param = $('#txt_uname').val() + ',' + $('#txt_pwd').val() + ',' + $("#chkRememberMe").is(':checked');
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
                failure: function (response) {
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
                <%--<div class="content-box-wrapper pad20A pad0B">--%>
                    <div class="form-row pad10A pad0B">
                        <i class="glyph-icon icon-user ui-state-default font-black font-size-14"></i>
                        <asp:TextBox ID="txt_uname" ValidationGroup="login" AutoComplete="off" class="txt" placeholder="User Name" runat="server"></asp:TextBox>
                        <%--<div class="form-input col-md-10">

                            <div class="form-input-icon">
                                <i class="glyph-icon icon-envelope-alt ui-state-default"></i>
                                <input placeholder="Email address" data-type="email" data-trigger="change" data-required="true" type="text" name="login_email" id="login_email" />
                            </div>
                        </div>--%>
                    </div>
                    <div class="form-row pad10A pad0B">
                        <i class="glyph-icon icon-unlock-alt ui-state-default font-black font-size-14"></i>
                        <asp:TextBox ID="txt_pwd" name="login_pass" TextMode="Password" ValidationGroup="login" class="txt" placeholder="Password" runat="server"></asp:TextBox>
                        <%--<div class="form-input col-md-10">
                            <div class="form-input-icon">
                                <i class="glyph-icon icon-unlock-alt ui-state-default"></i>
                                <input placeholder="Password" data-trigger="keyup" data-rangelength="[3,25]" type="password" name="login_pass" id="login_pass" />
                                
                            </div>
                        </div>--%>
                    </div>
                    <div class="form-row pad20L pad20R">
                        <div class="form-checkbox-radio pad20L pad20T col-md-10">
                            <asp:CheckBox ID="chkRememberMe" runat="server" class="font-black opacity-60" Text="Remember password?" />
                            <%--<input type="checkbox" name="remember-password" id="remember-password" />
                            <label for="remember-password" class="pad5L font-black opacity-60">Remember password?</label>--%>
                        </div>
                       <%--<div class="form-checkbox-radio">
                            <div class="forget font-blue" id="fget">Forgot Password</div>
                        </div>--%>
                    </div>
                <%--</div>--%>
                <div class="form-row pad10T">
                    <div class="form-checkbox-radio">
                <img src="img/log_load1.gif" style="display: none;width:50px;" id="log_load" />
                <asp:Button ID="btn_login" CssClass="btn" runat="server" Text="Login" OnClientClick="return chk_login()" />
                        </div>
                     
                    </div>
                <div class="form-row pad10T">
                        <asp:Label ID="lb_status" CssClass="status" Style="margin: 0px 30px; font-size: 10pt;" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                <%--<span class="bar"></span>--%>
            </div>
        </form>

        <%--<div class="forget_form" id="forget_form">
            <form>
                Need your email to recover password<br />
                <input type="text" class="txt" placeholder="Email id" name="txt_mailid" /><br />
                <br />
                <input type="button" onclick="" class="btn" value="Submit" name="btn_submit" />
                <input type="button" id="btn_close" class="btn" value="Close" name="btn_submit" />
            </form>
        </div>--%>
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
