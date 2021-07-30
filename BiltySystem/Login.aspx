<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BiltySystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html;charset=UTF-8" />
    <meta charset="utf-8" />
    <title>Bilty System : Login Page</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="" name="Vals Intertnational" />
    <meta content="" name="Jahanzeb Turk" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        
    <link rel="shortcut icon" href="assets/images/favicon.png" type="image/x-icon" />    <!-- Favicon -->
    <link rel="apple-touch-icon-precomposed" href="assets/images/apple-touch-icon-57-precomposed.png" />	<!-- For iPhone -->
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="assets/images/apple-touch-icon-114-precomposed.png" />    <!-- For iPhone 4 Retina display -->
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="assets/images/apple-touch-icon-72-precomposed.png" />    <!-- For iPad -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/images/apple-touch-icon-144-precomposed.png" />    <!-- For iPad Retina display -->




    <!-- CORE CSS FRAMEWORK - START -->
    <link href="assets/plugins/pace/pace-theme-flash.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/fonts/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/animate.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/perfect-scrollbar/perfect-scrollbar.css" rel="stylesheet" type="text/css" />
    <!-- CORE CSS FRAMEWORK - END -->

    <!-- HEADER SCRIPTS INCLUDED ON THIS PAGE - START --> 
        
        
    <link href="assets/plugins/icheck/skins/all.css" rel="stylesheet" type="text/css" media="screen" />

    <!-- HEADER SCRIPTS INCLUDED ON THIS PAGE - END --> 


    <!-- CORE CSS TEMPLATE - START -->
    <link href="assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/responsive.css" rel="stylesheet" type="text/css" />
    <!-- CORE CSS TEMPLATE - END -->
</head>
<body class=" login_page">
    <form id="mrFrm" runat="server">    
        <div class="container-fluid">
            <div class="login-wrapper row">
                <div id="login" class="login loginpage col-lg-offset-4 col-md-offset-3 col-sm-offset-3 col-xs-offset-0 col-xs-12 col-sm-6 col-lg-4">
                    <h1><a href="#" title="Login Page" tabindex="-1">Vals BiltySystem</a></h1>
                    <div id="divNotification" style="margin-top: 10px;" runat="server"></div>
                    <div id="loginform">
                        <p>
                            <label for="user_login">
                                Username<br />
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="input"></asp:TextBox>
                            </label>
                        </p>
                        <p>
                            <label for="user_pass">
                                Password <br />
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="input" TextMode="Password" style="font-size: 19px; width: 100%; padding: 9px; line-height: 25px; margin: 5px 0 15px 0; border: 1px solid transparent !important; background-color: rgba(255, 255, 255, 0.6);"></asp:TextBox>
                            </label>
                        </p>
                        <p class="submit">
                            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-accent btn-block" OnClick="btnLogin_Click" Text="Sign In" />
                        </p>
                    </div>
                    <%--<p id="nav">
                        <a class="pull-left" href="#" title="Password Lost and Found">Forgot password?</a>
                        <a class="pull-right" href="ui-register.html" title="Sign Up">Sign Up</a>
                    </p>--%>
                </div>
            </div>
        </div>
    </form>



    <!-- MAIN CONTENT AREA ENDS -->
    <!-- LOAD FILES AT PAGE END FOR FASTER LOADING -->


    <!-- CORE JS FRAMEWORK - START -->
    <script src="assets/js/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="assets/js/jquery.easing.min.js" type="text/javascript"></script>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/plugins/pace/pace.min.js" type="text/javascript"></script>
    <script src="assets/plugins/perfect-scrollbar/perfect-scrollbar.min.js" type="text/javascript"></script>
    <script src="assets/plugins/viewport/viewportchecker.js" type="text/javascript"></script>
    <script>window.jQuery || document.write('<script src="assets/js/jquery-1.11.2.min.js"><\/script>');</script>
    <!-- CORE JS FRAMEWORK - END -->


    <!-- OTHER SCRIPTS INCLUDED ON THIS PAGE - START -->

    <script src="assets/plugins/icheck/icheck.min.js" type="text/javascript"></script>
    <!-- OTHER SCRIPTS INCLUDED ON THIS PAGE - END -->


    <!-- CORE TEMPLATE JS - START -->
    <script src="assets/js/scripts.js" type="text/javascript"></script>
    <!-- END CORE TEMPLATE JS - END -->


</body>
</html>
