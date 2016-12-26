<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="CSS/login.css" rel="stylesheet" />

</head>


<body>
    <div id="Div1" class="login-page" runat="server">
        <div id="Div2" class="form" runat="server">
            <form id="Form1" class="register-form" runat="server">
                <img src="Image/title.gif" width="100%" /><br />
                <br />
                <br />

                <asp:TextBox ID="txtId" runat="server" placeholder="user name"></asp:TextBox>
                <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" placeholder="user password"></asp:TextBox>
                <button id="btnLogin" runat="server" onserverclick="btnLogin_Click" type="button" value="Login">Login</button><br />
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label><br />
                <p class="message">Not registered? <a href="JoinPage.aspx">Create an account</a></p>
            
            </form>
        </div>
    </div>
</body>
</html>
