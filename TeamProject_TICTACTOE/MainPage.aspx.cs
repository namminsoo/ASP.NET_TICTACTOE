using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainPage : System.Web.UI.Page
{
    string p_id;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(Session["Name"] as string))
        {
            Server.Transfer("LoginPage.aspx");
        }//Session["name"]에 값이 존재하지 않는 경우에는 페이지 이동
        else
        {
            p_id = (string)Session["Name"].ToString();//사용자 정보를 저장함.
            Label1.Text = p_id.ToString() + "님 안녕하세요~";

        }//값이 존재하면 사용자 아이디 화면에 출력하기
    }

    protected void btnGamePlayer_Click(object sender, EventArgs e)
    {
        //유저 vs 유저의 대결
        Session["mode"] = "1";
        Server.Transfer("SubGamePage.aspx");
    }

    protected void btnGameCom_Click(object sender, EventArgs e)
    {
        //유저 vs 컴퓨터의 대결
        Session["mode"] = "2";
        Server.Transfer("SubGamePage.aspx");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {   
        //사용자 정보(아이디) 세션을 종료
        Session.Remove("Name");
        Server.Transfer("LoginPage.aspx");
    }
    protected void btnRecord_Click(object sender, EventArgs e)
    {//경기 기록 보기 페이지
        Server.Transfer("RecordPage.aspx");
    }
    protected void btnMadeby_Click(object sender, EventArgs e)
    {//만든이들 보기 페이지
        Server.Transfer("Produces.aspx");
    }
}