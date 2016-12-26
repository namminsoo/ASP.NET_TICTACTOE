using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubGamePage : System.Web.UI.Page
{
    int mode; //1 : 사용자 대 사용자, 2 : 사용자 대 컴퓨터
    string p1Name;
    /*이미지 설정*/
    const string man = @"~/Image/man.png";
    const string woman = @"~/Image/woman.png";
    const string com = @"~/Image/com.png";


    protected void Page_Load(object sender, EventArgs e)
    {
          if (string.IsNullOrEmpty(Session["Name"] as string))
        {
            Server.Transfer("LoginPage.aspx");
        }//로그인 세션이 만료되면 로그인 페이지로 이동
        else
        {
            /*변수 세팅*/
            p1Name = Session["Name"].ToString();
        
            lblUser.Text = p1Name;
            mode = int.Parse(Session["mode"].ToString());

            /* mode에 맞춰 이미지 설정 */
            img1.ImageUrl = man;
            img2.ImageUrl = mode == 1 ? woman : com;
            
            /*mode에 맞춰 멘트 설정*/
            if (mode == 1)
            {
                btnCom.Text = "Guest 선공";
            }//유저 vs 유저
            else if (mode == 2)
            {
                btnCom.Text = "Com 선공";
                txtname2.Enabled = false;
                txtname2.Text = "Computer";
                Session["opponent"] = "Computer";
            }//유저 vs 컴퓨터
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        //2플레이어, 컴퓨터 이름 설정
        if (txtname2.Text == "")
        {
            Session["opponent"] = "Guest";
        }
        else
        {
            Session["opponent"] =mode==2?"Computer": "Guest-"+txtname2.Text;
        }
        if (btn.CommandName == "player1")
        {
            //모드와 턴의 값 1을 넘겨주면서 페이지 이동
            Session["mode"] = mode;
            Session["turn"] = "1";
            Server.Transfer("GamePage.aspx");

        }//선공
        else if (btn.CommandName == "player2")
        {
            //모드와 턴의 값 2를 넘겨주면서 페이지 이동
            Session["mode"] = mode;
            Session["turn"] = "2";
            Server.Transfer("GamePage.aspx");
        }//후공
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Server.Transfer("MainPage.aspx");
    }//메인 페이지로 가기
}