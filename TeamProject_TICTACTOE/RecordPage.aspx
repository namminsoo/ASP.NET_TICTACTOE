<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecordPage.aspx.cs" Inherits="RecordPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="CSS/record.css" rel="stylesheet" />
</head>

<body>


    <div class="base-page">
        <div class="form" style="text-align: center" >
            <form id="form1" runat="server">
                <img src="Image/title.gif" width="100%" alt="" /><br />
                <br />
                <br />
                <div>
                    <asp:Button ID="btnPersonal" CssClass="button3" runat="server" Text="내 기록 보기" 
                        onclick="btnPersonal_Click1"/>&nbsp; 
                    <asp:Button ID="btnWhole" CssClass="button4" runat="server" Text="전체 기록 보기" Width="124px" /><br />
                </div>
                <asp:SqlDataSource ID="dsrcRecord" runat="server"
                    ConnectionString="<%$ ConnectionStrings:TicTacToeConnectionString %>"
                    
                    SelectCommand="SELECT Rnum, player1, player2, Result FROM record ORDER BY Rnum"></asp:SqlDataSource>

                <asp:GridView ID="gvRecord" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" AllowPaging="True" DataSourceID="dsrcRecord" Width="556px">
                    <HeaderStyle BackColor="#507CD1" Font-Bold="true" ForeColor="White" />
                    <PagerStyle BackColor="#FFFFFF" HorizontalAlign="Center" />
                    <PagerSettings Mode="NumericFirstLast"   />

                    <FooterStyle BackColor="#507CD1" Font-Bold="true" />
                    <EmptyDataTemplate>등록된 기록이 없습니다.</EmptyDataTemplate>
                    

                    <Columns>
                        <asp:BoundField DataField="Rnum" HeaderText="번호">
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="player1" HeaderText="플레이어 1">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="player2" HeaderText="플레이어 2" HeaderStyle-Width="330px">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Result" HeaderText="경기 결과">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>

                </asp:GridView>
                <br />
                <div>
                    <asp:Button CssClass="button2" ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="나가기" />
                </div>

            </form>
        </div>
    </div>






</body>
</html>
