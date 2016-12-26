<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JoinPage.aspx.cs" Inherits="JoinPage"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="CSS/login.css" rel="stylesheet" />
 
</head>
<body>
    <div class="login-page" runat="server">
        <div class="form" runat="server">
            <form class="register-form" runat="server">
                <img src ="Image/title.gif" width="100%" alt=""/><br /><br /><br />
                <asp:TextBox ID="txtId" runat="server" placeholder="user name"></asp:TextBox><br />
                <asp:Button class="button2" id="btnCompareId"  runat="server" Text="중복 확인" onClick="btnCompareId_Click"/><br />
        <asp:Label ID="lblCheck" runat="server" ForeColor="Red"></asp:Label>
        <asp:RequiredFieldValidator ID="rfvtxtId" runat="server" 
            ControlToValidate="txtId" Display="Dynamic" ErrorMessage="다시 입력하세요" 
            SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" placeholder="password"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="rfvtxtPassword" runat="server" 
            ControlToValidate="txtPassWord" ErrorMessage="비밀번호를 입력하세요." 
            SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
        <asp:RegularExpressionValidator ID="revPassword" runat="server" 
            ControlToValidate="txtPassWord" Display="Dynamic" 
            ErrorMessage="비밀번호는 4-10자 사이 입니다." SetFocusOnError="True" 
            ValidationExpression="\w{4,10}" ForeColor="Red"></asp:RegularExpressionValidator>
        <asp:TextBox ID="txtPassWordConfirm" runat="server" TextMode="Password" placeholder="re_password"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="rfvtxtPasswordConfirm" runat="server" 
            ControlToValidate="txtPassWordConfirm" 
            ErrorMessage="비밀번호를 입력하세요." SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
        <asp:CompareValidator ID="cvPasswordConfirm" runat="server" 
            ControlToCompare="txtPassWord" ControlToValidate="txtPassWordConfirm" 
            Display="Dynamic" ErrorMessage="비밀번호가 맞지 않습니다." SetFocusOnError="True" 
            ForeColor="Red"></asp:CompareValidator>
        <asp:TextBox ID="txtEmail" runat="server" placeholder="email"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" 
            ErrorMessage="이메일을 입력하세요." ControlToValidate="txtEmail" 
            ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
        <asp:RegularExpressionValidator ID="revtxtEmail" runat="server" 
            ErrorMessage="이메일 주소 형식이 맞지 않습니다." ControlToValidate="txtEmail" 
            Display="Dynamic" ForeColor="Red" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        <button id="btnSubmit" runat="server" value="Submit" onserverclick="btnSubmit_Click" >Submit</button><br /><br />
        <button id="btnCancel" runat="server" CausesValidation="False" value="Cancel"
            onserverclick="btnCancel_Click">Cancel</button>
        <p class="message"> Already registered? <a href="LoginPage.aspx">Sign In</a></p>

        </form>
        </div>
    </div>
</body></html>