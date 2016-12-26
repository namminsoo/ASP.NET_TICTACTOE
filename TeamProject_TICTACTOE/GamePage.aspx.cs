using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class GamePage : System.Web.UI.Page
{
    /*
     * 관리유지될 변수들 ViewState 설정
     */
    int[,] stateArr = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    int mode = 0;
    int turn = 0;
    int turnCount = 0;
    string player1Name;
    string player2Name;
    ImageButton[,] btnArr = new ImageButton[3, 3];

    /*이미지 설정*/
    const string imgAsterisk = @"~/Image/asterisk.png";
    const string imgO = @"~/Image/o.png";
    const string imgX = @"~/Image/x.png";
    
    /*플레이어 변수 설정*/
    const int p1 = 1;//p1
    const int p2 = 2;//p2
    const int p3 = 3;//com

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["Name"] as string))
        {
            Server.Transfer("LoginPage.aspx");
        }//로그인 세션이 만료되면 로그인 페이지로 이동
        else
        {
            player1Name = Session["Name"].ToString();//추가
            player2Name = Session["opponent"].ToString();//추가

            lblInfo.Text = player1Name + " vs " + player2Name;

            if (!IsPostBack)
            {
                //세션에 저장된 mode와 turn을 불러온다
                mode = int.Parse(Session["mode"].ToString());
                turn = int.Parse(Session["turn"].ToString());
                
                //삼목판 상태 업데이트
                viewStateUpdate();

                //게임시작
                gameStart(mode, turn);

            }
            /*게임 환경 변수들 초기화*/
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    stateArr[i, j] = int.Parse(ViewState[((i * 3) + j).ToString()].ToString());//1~9
                }
            }
            mode = (int)ViewState["mode"];
            turn = (int)ViewState["turn"];
            turnCount = (int)ViewState["turnCount"];

        }
    }

    private void init()
    {
        btnArr[0, 0] = btn00; btnArr[0, 1] = btn01; btnArr[0, 2] = btn02;
        btnArr[1, 0] = btn10; btnArr[1, 1] = btn11; btnArr[1, 2] = btn12;
        btnArr[2, 0] = btn20; btnArr[2, 1] = btn21; btnArr[2, 2] = btn22;

        /* 게임 설정 초기화 */
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                stateArr[i, j] = 0;
                btnArr[i, j].ToolTip = "-";
                btnArr[i, j].ImageUrl = imgAsterisk;
                btnArr[i, j].BackColor = Color.Transparent;
            }
        }

        lbl1.Text = "??";
        lbl2.Text = "공격";
    }

    public void gameStart(int m, int firstPlayer)
    {
        init();
        mode = m;
        turn = firstPlayer;
        
        if (mode == 1)
        {
            if (turn == 1) { lbl1.Text = player1Name; }//{ lbl1.Text = "Player1"; }
            else { lbl1.Text = player2Name; }//{ lbl1.Text = "Player2"; }

        }//유저 vs 유저인 경우
        else if (mode == 2)
        {
            if (turn == 1) { lbl1.Text = player1Name; }
            else { lbl1.Text = "Computer"; comAttack(); }
        }//유저 vs 컴퓨터인 경우
    }

    private bool checkBinggo(int player)
    {
        int count1 = 0;
        int count2 = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (stateArr[i, j] == player) { count1++; }
                if (stateArr[j, i] == player) { count2++; }
            }
            if (count1 == 3) { return true; }
            else if (count2 == 3) { return true; }
            else { count1 = 0; count2 = 0; }
        }//가로,세로 체크

        /* 대각선 체크*/
        if (stateArr[0, 0] == player && stateArr[1, 1] == player && stateArr[2, 2] == player) { return true; }
        if (stateArr[0, 2] == player && stateArr[1, 1] == player && stateArr[2, 0] == player) { return true; }

        return false;
    }//빙고 여부를 체크하는 함수

    protected void btnTTT_Click(object sender, EventArgs e)
    {
        //클릭한 버튼 불러오기
        ImageButton btn = (ImageButton)sender;

        /* 버튼 위치 초기화 */
        int ar = int.Parse(btn.CommandArgument);
        int h = ar % 10;
        int w = ar / 10;

        //모드와 턴에 따라 방식이 바뀐다.
        if (mode == 1)//유저와 유저 대결
        {
            if (btn.ToolTip.ToString() == "-")
            {
                if (turn == p1)
                {
                    //선공 유저가 버튼을 클릭한다.
                    btn.ImageUrl = imgO;
                    btn.ToolTip = "o";
                    stateArr[w, h] = p1;
                    turnCount++;
                    viewStateUpdate();

                    //빙고 완성되면 게임 종료
                    if (checkBinggo(p1)) { endGame(p1); }
                    else if (turnCount == 9) { endGame(0); }

                    //다음 턴 세팅
                    lbl1.Text = player2Name;
                    turn = p2;
                    viewStateUpdate();
                }
                else if (turn == p2)
                {
                    //후공 유저가 버튼을 클릭
                    btn.ImageUrl = imgX;
                    btn.ToolTip = "x";
                    stateArr[w, h] = p2;
                    turnCount++;
                    viewStateUpdate();

                    //빙고 완성되면 게임 종료
                    if (checkBinggo(p2)) { endGame(p2); }
                    else if (turnCount == 9) { endGame(0); }

                    //다음 턴 세팅
                    lbl1.Text = player1Name;
                    turn = p1;
                    viewStateUpdate();
                }
            }
        }

        else if (mode == 2)//유저 vs 컴퓨터 대결
        {
            if (btn.ToolTip.ToString() == "-")
            {
                if (turn == p1)
                {
                    //후공 유저가 버튼을 클릭한다.
                    btn.ImageUrl = imgO;
                    btn.ToolTip = "o";
                    stateArr[w, h] = p1;
                    turnCount++;
                    viewStateUpdate();
                    
                    //빙고가 완성되면 게임 종료
                    if (checkBinggo(p1)) { endGame(p1); }
                    else if (turnCount == 9) { endGame(0); }

                    //다음 턴 세팅
                    lbl1.Text = player2Name;
                    turn = p3;
                    viewStateUpdate();

                    //다음 턴인 컴퓨터 공격
                    comAttack();
                }
                else if (turn == p3)
                {
                    //컴퓨터 차례지만, 유저가 공격을 하고 바로 컴퓨터의 수를 두게 함.
                }
            }
        }
    }//btnTTT_Click

    /* 컴퓨터 공격 */
    private void comAttack()
    {
        int w = 0, h = 0;
        bool ch = false;

        if (turnCount % 2 == 0)//선공
        {
            if (stateArr[1, 1] == 0) { w = 1; h = 1; }//컴퓨터가 선공일 때 승리 확률이 높은 중간에 무조건 말을 두게 한다.
            else
            {
                if (turnCount == 2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (stateArr[i, j] == 0) { w = i; h = j; ch = true; break; }
                        }
                        if (ch)
                        { ch = false; break; }
                    }
                }//두 번째 턴에서는 시계방향으로 빈 곳을 체크한다.
                else
                {
                    //다음 턴으로 이길 수 있는 공간 체크
                    if (stateArr[0, 0] + stateArr[1, 1] + stateArr[2, 2] == 4 && (stateArr[0, 0] == 0 || stateArr[1, 1] == 0 || stateArr[2, 2] == 0)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }
                    else if (stateArr[0, 2] + stateArr[1, 1] + stateArr[2, 0] == 4 && (stateArr[0, 2] == 0 || stateArr[1, 1] == 0 || stateArr[2, 0] == 0)) { if (stateArr[0, 2] == 0) { w = 0; h = 2; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 0] == 0) { w = 2; h = 0; } }

                    else if (stateArr[0, 0] + stateArr[0, 1] + stateArr[0, 2] == 4 && (stateArr[0, 0] == 0 || stateArr[0, 1] == 0 || stateArr[0, 2] == 0)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[0, 1] == 0) { w = 0; h = 1; } else if (stateArr[0, 2] == 0) { w = 0; h = 2; } }
                    else if (stateArr[1, 0] + stateArr[1, 1] + stateArr[1, 2] == 4 && (stateArr[1, 0] == 0 || stateArr[1, 1] == 0 || stateArr[1, 2] == 0)) { if (stateArr[1, 0] == 0) { w = 1; h = 0; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[1, 2] == 0) { w = 1; h = 2; } }
                    else if (stateArr[2, 0] + stateArr[2, 1] + stateArr[2, 2] == 4 && (stateArr[2, 0] == 0 || stateArr[2, 1] == 0 || stateArr[2, 2] == 0)) { if (stateArr[2, 0] == 0) { w = 2; h = 0; } else if (stateArr[2, 1] == 0) { w = 2; h = 1; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }

                    else if (stateArr[0, 0] + stateArr[1, 0] + stateArr[2, 0] == 4 && (stateArr[0, 0] == 0 || stateArr[1, 0] == 0 || stateArr[2, 0] == 0)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[1, 0] == 0) { w = 1; h = 0; } else if (stateArr[2, 0] == 0) { w = 2; h = 0; } }
                    else if (stateArr[0, 1] + stateArr[1, 1] + stateArr[2, 1] == 4 && (stateArr[0, 1] == 0 || stateArr[1, 1] == 0 || stateArr[2, 1] == 0)) { if (stateArr[0, 1] == 0) { w = 0; h = 1; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 1] == 0) { w = 2; h = 1; } }
                    else if (stateArr[0, 2] + stateArr[1, 2] + stateArr[2, 2] == 4 && (stateArr[0, 2] == 0 || stateArr[1, 2] == 0 || stateArr[2, 2] == 0)) { if (stateArr[0, 2] == 0) { w = 0; h = 2; } else if (stateArr[1, 2] == 0) { w = 1; h = 2; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }

                    else
                    {
                        //방어 할 곳을 체크
                        if (stateArr[0, 0] + stateArr[1, 1] + stateArr[2, 2] == 2 && (stateArr[0, 0] == 0 || stateArr[1, 1] == 0 || stateArr[2, 2] == 0) && (stateArr[0, 0] != 2 && stateArr[1, 1] != 2 && stateArr[2, 2] != 2)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }
                        else if (stateArr[0, 2] + stateArr[1, 1] + stateArr[2, 0] == 2 && (stateArr[0, 2] == 0 || stateArr[1, 1] == 0 || stateArr[2, 0] == 0) && (stateArr[0, 2] != 2 && stateArr[1, 1] != 2 && stateArr[2, 0] != 2)) { if (stateArr[0, 2] == 0) { w = 0; h = 2; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 0] == 0) { w = 2; h = 0; } }

                        else if (stateArr[0, 0] + stateArr[0, 1] + stateArr[0, 2] == 2 && (stateArr[0, 0] == 0 || stateArr[0, 1] == 0 || stateArr[0, 2] == 0) && (stateArr[0, 0] != 2 && stateArr[0, 1] != 2 && stateArr[0, 2] != 2)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[0, 1] == 0) { w = 0; h = 1; } else if (stateArr[0, 2] == 0) { w = 0; h = 2; } }
                        else if (stateArr[1, 0] + stateArr[1, 1] + stateArr[1, 2] == 2 && (stateArr[1, 0] == 0 || stateArr[1, 1] == 0 || stateArr[1, 2] == 0) && (stateArr[1, 0] != 2 && stateArr[1, 1] != 2 && stateArr[1, 2] != 2)) { if (stateArr[1, 0] == 0) { w = 1; h = 0; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[1, 2] == 0) { w = 1; h = 2; } }
                        else if (stateArr[2, 0] + stateArr[2, 1] + stateArr[2, 2] == 2 && (stateArr[2, 0] == 0 || stateArr[2, 1] == 0 || stateArr[2, 2] == 0) && (stateArr[2, 0] != 2 && stateArr[2, 1] != 2 && stateArr[2, 2] != 2)) { if (stateArr[2, 0] == 0) { w = 2; h = 0; } else if (stateArr[2, 1] == 0) { w = 2; h = 1; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }

                        else if (stateArr[0, 0] + stateArr[1, 0] + stateArr[2, 0] == 2 && (stateArr[0, 0] == 0 || stateArr[1, 0] == 0 || stateArr[2, 0] == 0) && (stateArr[0, 0] != 2 && stateArr[1, 0] != 2 && stateArr[2, 0] != 2)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[1, 0] == 0) { w = 1; h = 0; } else if (stateArr[2, 0] == 0) { w = 2; h = 0; } }
                        else if (stateArr[0, 1] + stateArr[1, 1] + stateArr[2, 1] == 2 && (stateArr[0, 1] == 0 || stateArr[1, 1] == 0 || stateArr[2, 1] == 0) && (stateArr[0, 1] != 2 && stateArr[1, 1] != 2 && stateArr[2, 1] != 2)) { if (stateArr[0, 1] == 0) { w = 0; h = 1; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 1] == 0) { w = 2; h = 1; } }
                        else if (stateArr[0, 2] + stateArr[1, 2] + stateArr[2, 2] == 2 && (stateArr[0, 2] == 0 || stateArr[1, 2] == 0 || stateArr[2, 2] == 0) && (stateArr[0, 2] != 2 && stateArr[1, 2] != 2 && stateArr[2, 2] != 2)) { if (stateArr[0, 2] == 0) { w = 0; h = 2; } else if (stateArr[1, 2] == 0) { w = 1; h = 2; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (stateArr[i, j] == 0) { w = i; h = j; ch = true; break; }
                                }
                                if (ch) { ch = false; break; }
                            }
                        }//공격, 방어 둘 다 방법이 없으면 시계방향으로 빈 곳을 체크
                    }
                }
            }
        }
        else//후공
        {
            if (stateArr[1, 1] == 0) { w = 1; h = 1; }
            else
            {
                if (turnCount == 1)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (stateArr[i, j] == 0) { w = i; h = j; ch = true; break; }
                        }
                        if (ch) { ch = false; break; }
                    }
                }//후공 처음 놓을 때는 시계 방향으로 빈 곳을 체크
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (stateArr[i, j] == 0) { w = i; h = j; ch = true; break; }
                        }
                        if (ch) { ch = false; break; }
                    }//빈 공간을 체크



                    //다음 턴으로 이길 수 있는 공간 체크
                    if (stateArr[0, 0] + stateArr[1, 1] + stateArr[2, 2] == 2 && (stateArr[0, 0] == 0 || stateArr[1, 1] == 0 || stateArr[2, 2] == 0) && (stateArr[0, 0] != 2 && stateArr[1, 1] != 2 && stateArr[2, 2] != 2)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }
                    else if (stateArr[0, 2] + stateArr[1, 1] + stateArr[2, 0] == 2 && (stateArr[0, 2] == 0 || stateArr[1, 1] == 0 || stateArr[2, 0] == 0) && (stateArr[0, 2] != 2 && stateArr[1, 1] != 2 && stateArr[2, 0] != 2)) { if (stateArr[0, 2] == 0) { w = 0; h = 2; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 0] == 0) { w = 2; h = 0; } }

                    else if (stateArr[0, 0] + stateArr[0, 1] + stateArr[0, 2] == 2 && (stateArr[0, 0] == 0 || stateArr[0, 1] == 0 || stateArr[0, 2] == 0) && (stateArr[0, 0] != 2 && stateArr[0, 1] != 2 && stateArr[0, 2] != 2)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[0, 1] == 0) { w = 0; h = 1; } else if (stateArr[0, 2] == 0) { w = 0; h = 2; } }
                    else if (stateArr[1, 0] + stateArr[1, 1] + stateArr[1, 2] == 2 && (stateArr[1, 0] == 0 || stateArr[1, 1] == 0 || stateArr[1, 2] == 0) && (stateArr[1, 0] != 2 && stateArr[1, 1] != 2 && stateArr[1, 2] != 2)) { if (stateArr[1, 0] == 0) { w = 1; h = 0; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[1, 2] == 0) { w = 1; h = 2; } }
                    else if (stateArr[2, 0] + stateArr[2, 1] + stateArr[2, 2] == 2 && (stateArr[2, 0] == 0 || stateArr[2, 1] == 0 || stateArr[2, 2] == 0) && (stateArr[2, 0] != 2 && stateArr[2, 1] != 2 && stateArr[2, 2] != 2)) { if (stateArr[2, 0] == 0) { w = 2; h = 0; } else if (stateArr[2, 1] == 0) { w = 2; h = 1; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }

                    else if (stateArr[0, 0] + stateArr[1, 0] + stateArr[2, 0] == 2 && (stateArr[0, 0] == 0 || stateArr[1, 0] == 0 || stateArr[2, 0] == 0) && (stateArr[0, 0] != 2 && stateArr[1, 0] != 2 && stateArr[2, 0] != 2)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[1, 0] == 0) { w = 1; h = 0; } else if (stateArr[2, 0] == 0) { w = 2; h = 0; } }
                    else if (stateArr[0, 1] + stateArr[1, 1] + stateArr[2, 1] == 2 && (stateArr[0, 1] == 0 || stateArr[1, 1] == 0 || stateArr[2, 1] == 0) && (stateArr[0, 1] != 2 && stateArr[1, 1] != 2 && stateArr[2, 1] != 2)) { if (stateArr[0, 1] == 0) { w = 0; h = 1; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 1] == 0) { w = 2; h = 1; } }
                    else if (stateArr[0, 2] + stateArr[1, 2] + stateArr[2, 2] == 2 && (stateArr[0, 2] == 0 || stateArr[1, 2] == 0 || stateArr[2, 2] == 0) && (stateArr[0, 2] != 2 && stateArr[1, 2] != 2 && stateArr[2, 2] != 2)) { if (stateArr[0, 2] == 0) { w = 0; h = 2; } else if (stateArr[1, 2] == 0) { w = 1; h = 2; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }

                    //방어할 곳을 체크
                    if (stateArr[0, 0] + stateArr[1, 1] + stateArr[2, 2] == 4 && (stateArr[0, 0] == 0 || stateArr[1, 1] == 0 || stateArr[2, 2] == 0)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }
                    else if (stateArr[0, 2] + stateArr[1, 1] + stateArr[2, 0] == 4 && (stateArr[0, 2] == 0 || stateArr[1, 1] == 0 || stateArr[2, 0] == 0)) { if (stateArr[0, 2] == 0) { w = 0; h = 2; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 0] == 0) { w = 2; h = 0; } }

                    else if (stateArr[0, 0] + stateArr[0, 1] + stateArr[0, 2] == 4 && (stateArr[0, 0] == 0 || stateArr[0, 1] == 0 || stateArr[0, 2] == 0)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[0, 1] == 0) { w = 0; h = 1; } else if (stateArr[0, 2] == 0) { w = 0; h = 2; } }
                    else if (stateArr[1, 0] + stateArr[1, 1] + stateArr[1, 2] == 4 && (stateArr[1, 0] == 0 || stateArr[1, 1] == 0 || stateArr[1, 2] == 0)) { if (stateArr[1, 0] == 0) { w = 1; h = 0; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[1, 2] == 0) { w = 1; h = 2; } }
                    else if (stateArr[2, 0] + stateArr[2, 1] + stateArr[2, 2] == 4 && (stateArr[2, 0] == 0 || stateArr[2, 1] == 0 || stateArr[2, 2] == 0)) { if (stateArr[2, 0] == 0) { w = 2; h = 0; } else if (stateArr[2, 1] == 0) { w = 2; h = 1; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }

                    else if (stateArr[0, 0] + stateArr[1, 0] + stateArr[2, 0] == 4 && (stateArr[0, 0] == 0 || stateArr[1, 0] == 0 || stateArr[2, 0] == 0)) { if (stateArr[0, 0] == 0) { w = 0; h = 0; } else if (stateArr[1, 0] == 0) { w = 1; h = 0; } else if (stateArr[2, 0] == 0) { w = 2; h = 0; } }
                    else if (stateArr[0, 1] + stateArr[1, 1] + stateArr[2, 1] == 4 && (stateArr[0, 1] == 0 || stateArr[1, 1] == 0 || stateArr[2, 1] == 0)) { if (stateArr[0, 1] == 0) { w = 0; h = 1; } else if (stateArr[1, 1] == 0) { w = 1; h = 1; } else if (stateArr[2, 1] == 0) { w = 2; h = 1; } }
                    else if (stateArr[0, 2] + stateArr[1, 2] + stateArr[2, 2] == 4 && (stateArr[0, 2] == 0 || stateArr[1, 2] == 0 || stateArr[2, 2] == 0)) { if (stateArr[0, 2] == 0) { w = 0; h = 2; } else if (stateArr[1, 2] == 0) { w = 1; h = 2; } else if (stateArr[2, 2] == 0) { w = 2; h = 2; } }


                }
            }
        }//후공

        //변수 초기화
        btnArr[0, 0] = btn00; btnArr[0, 1] = btn01; btnArr[0, 2] = btn02;
        btnArr[1, 0] = btn10; btnArr[1, 1] = btn11; btnArr[1, 2] = btn12;
        btnArr[2, 0] = btn20; btnArr[2, 1] = btn21; btnArr[2, 2] = btn22;

        //말을 둔다.
        btnArr[w, h].ImageUrl = imgX;
        btnArr[w, h].ToolTip = "x";
        stateArr[w, h] = p3 - 1;
        turnCount++;
        viewStateUpdate();

        //빙고를 확인 하면 게임 종료
        if (checkBinggo(p3 - 1)) { endGame(p3); }
        else if (turnCount == 9) { endGame(0); }

        //다음 턴 세팅
        lbl1.Text = player1Name;
        turn = p1;
        viewStateUpdate();
    }

    private void endGame(int winner)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Session[i.ToString() + j.ToString()] = stateArr[i, j].ToString();
            }
        }//다음 페이지에서 게임 결과를 알려주기 위해 저장
        /*승자를 저장하고 종료 페이지로 이동*/
        Session["win"] = winner.ToString();
        Server.Transfer("EndGamePage.aspx");
    }//게임 종료 함수

    private void viewStateUpdate()
    {
        //현재 상태를 ViewState에 저장
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                ViewState[((i * 3) + j).ToString()] = stateArr[i, j];//1~9
            }
        }
        //현재 상태를 ViewState에 저장
        ViewState["mode"] = mode;
        ViewState["turn"] = turn;
        ViewState["turnCount"] = turnCount;
    }//ViewStateUpdate()
}