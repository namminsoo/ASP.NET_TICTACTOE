<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainPage.aspx.cs" Inherits="MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script language="javascript" type="text/javascript">
        function ExitMsg() {
            window.close();
        }
    </script>
    <link href="CSS/main.css" rel="stylesheet" />
</head>


<body>
    <div class="base-page">
        <div class="form">
            <form id="form1" runat="server">
                <img src="Image/title.gif" width="100%" alt="" /><br />
                <hr />
                <br />
                <span style="text-align: right;">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                <br />
                </span>
                <br />
                <button class="button2" id="btnGamePlayer" runat="server" onserverclick="btnGamePlayer_Click" type="button">친구와 함게 대결하기</button>
                <br />
                <br />

                <button class="button2" id="btnGameCom" runat="server" onserverclick="btnGameCom_Click" type="button">컴퓨터와 대결하기</button>
                <br />
                <br />
                <br />
             
                <button id="btnRecord" runat="server" onserverclick="btnRecord_Click" type="button">게임기록</button>
                <br />
                <br />


                <button id="btnMadeby" runat="server" onserverclick="btnMadeby_Click" type="button">만든이</button>
                <br />
                <br />
                <button id="btnLogout" runat="server" onserverclick="btnLogout_Click" type="button">로그아웃</button>
                <br />
                <br />

                <button id="btnExit" runat="server" onclick="ExitMsg()" type="button" visible="false">끝내기</button>
                <br />
                <br />

                <br />
            </form>
        </div>
    </div>
</body>
</html>
