<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubGamePage.aspx.cs" Inherits="SubGamePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="CSS/design.css" rel="stylesheet" />
</head>

<body>
    <div class="base-page">
        <div class="form">
            <form id="Form1" runat="server">

      <img src="Image/title.gif" width="100%" alt="" /><br />
                <hr />

                <table style="width: 50%; text-align: center;"  align="center">
                    <tr>
                        <td>
                            <asp:Image Width="110px" ID="img1" runat="server" />
                        </td>
                        <td>
                            <asp:Image Width="110px" ID="img2" runat="server" />
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUser" runat="server" Text="Label" Font-Bold="True" Font-Size="Large"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtname2" runat="server" placeholder="Guest name"></asp:TextBox>

                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnPlayer" CssClass="button12" runat="server" Text="선공" CommandArgument="1" CommandName="player1" OnCommand="btn_Click" />
                        
                        </td>
                        <td>
                            <asp:Button ID="btnCom" CssClass="button12" runat="server" Text="22 " CommandArgument="2" CommandName="player2" OnCommand="btn_Click" />
                        

                        </td>

                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnBack" CssClass="button1" runat="server" Text="뒤로가기" OnClick="btnBack_Click" />

                        </td>
                    </tr>
                </table>



            </form>
        </div>
    </div>
</body>

</html>

