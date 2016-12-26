<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GamePage.aspx.cs" Inherits="GamePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="CSS/game.css" rel="stylesheet" />
</head>

<body>
    <div class="base-page">
        <div class="form">
            <form id="Form1" runat="server">

                <img src="Image/title.gif" width="70%" alt="" style="padding: 45px 45px 15px 45px" /><br />
                 <asp:Label ID="lblInfo" runat="server" Text="Label" Font-Bold="True" Font-Size="Large"></asp:Label>
                <br />
                <hr />


                <table style="width: 360px; height: 360px; text-align: center;" align="center">
                    <tr>
                        <td>
                            <asp:ImageButton ID="btn00" runat="server" BackColor="#ffffff" Height="100px" CommandArgument="00" Width="100px" OnCommand="btnTTT_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btn01" runat="server" BackColor="#ffffff" Height="100px" CommandArgument="01" Width="100px" OnCommand="btnTTT_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btn02" runat="server" BackColor="#ffffff" Height="100px" CommandArgument="02" Width="100px" OnCommand="btnTTT_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="btn10" runat="server" BackColor="#ffffff" Height="100px" CommandArgument="10" Width="100px" OnCommand="btnTTT_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btn11" runat="server" BackColor="#ffffff" Height="100px" CommandArgument="11" Width="100px" OnCommand="btnTTT_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btn12" runat="server" BackColor="#ffffff" Height="100px" CommandArgument="12" Width="100px" OnCommand="btnTTT_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="btn20" runat="server" BackColor="#ffffff" Height="100px" CommandArgument="20" Width="100px" OnCommand="btnTTT_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btn21" runat="server" BackColor="#ffffff" Height="100px" CommandArgument="21" Width="100px" OnCommand="btnTTT_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btn22" runat="server" BackColor="#ffffff" Height="100px" CommandArgument="22" Width="100px" OnCommand="btnTTT_Click" />
                        </td>
                    </tr>

                </table>
                <hr />
                <br />
                <asp:Label ID="lbl1" runat="server" Text="??" Font-Bold="True" Font-Size="XX-Large" ForeColor="#FF3300"></asp:Label>
                <asp:Label ID="lbl2" runat="server" Text="  공격!!" Font-Bold="True" Font-Size="XX-Large" ForeColor="Red"></asp:Label>

                <br />
                <br />
                <br />


            </form>
        </div>
    </div>
</body>
</html>
