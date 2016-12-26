using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RecordPpage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string p1Name = Session["Name"].ToString();//세션에 저장되어 있는 사용자 아이디를 받음

        //자신의 것을 누르는 것을 방지하기 위하여 비활성화
        btnPersonal.Enabled = false;
        dsrcRecord.SelectCommand = "SELECT Rnum, player1, player2, Result FROM record WHERE player1 = '";
        dsrcRecord.SelectCommand += p1Name.ToString() + "' ORDER BY Rnum";//현재 사용자의 아이디를 검색하여 해당 사용자가 게임한 경기 기록을 보여주기 위한 SQL 문.
                                                                         //데이터 소스 컨트롤에 입력합니다.
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("MainPage.aspx");
    }//메인 페이지 이동
    protected void btnWhole_Click(object sender, EventArgs e)
    {
        Server.Transfer("RecordPage.aspx");
    }//전체 경기기록 페이지 이동
}