using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class EndGamePage : System.Web.UI.Page
{
    //DB연결 스트링 가져오기
    private string connectionString = WebConfigurationManager.ConnectionStrings["TicTacToe"].ConnectionString;

    //변수 선언
    int[,] stateArr = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    ImageButton[,] btnArr = new ImageButton[3, 3];

    //이미지 설정
    const string imgAsterisk = @"~/Image/asterisk.png";
    const string imgO = @"~/Image/o.png";
    const string imgX = @"~/Image/x.png";

    //승자, 플레이어 등 변수 선언
    int winner = 0, Rnumber = 0;
    string player1, player2, result;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["Name"] as string))
        {
            Server.Transfer("LoginPage.aspx");
        }//로그인 세션이 만료되면 로그인 페이지로
        else if (!IsPostBack)
        {
            //누가 대결했는지 세션으로 정보 가져오기
            player1 = Session["Name"].ToString();//게임 사용자
            player2 = Session["opponent"].ToString();//상대방(컴퓨터 포함)
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    stateArr[i, j] = int.Parse(Session[i.ToString() + j.ToString()].ToString());
                }
            }//게임 결과 정보 가져오기
            
            //버튼 설정
            btnArr[0, 0] = btn00; btnArr[0, 1] = btn01; btnArr[0, 2] = btn02;
            btnArr[1, 0] = btn10; btnArr[1, 1] = btn11; btnArr[1, 2] = btn12;
            btnArr[2, 0] = btn20; btnArr[2, 1] = btn21; btnArr[2, 2] = btn22;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    btnArr[i, j].ImageUrl = stateArr[i, j] == 1 ? imgO : stateArr[i, j] == 0 ? imgAsterisk : imgX;
                    btnArr[i, j].BackColor = Color.Transparent;
                }
            }//게임 결과에 맞춰 내용 변경

            //승자가 누구인지 가져오기
            winner = int.Parse(Session["win"].ToString());

            //승자에 맞춰 승리멘트 설정
            lblWinner.Text = (winner == 1 ? player1 : winner == 2 ? player2 : winner == 3 ? "Computer" : "Nobody") + " Win";
            result = (winner == 1 ? "WIN" : winner == 2 || winner == 3 ? "LOSE" : "DRAW");
            //DB 저장
            savedata();
        }
    }

    //전체 경기에서 현재 경기가 몇 번째 경기인지 찾는 함수
    public int SearchNum()
    {
        string selectSQL = "SELECT Rnum FROM dbo.record"; //데이터베이스에 저장된 가장 마지막 경기가 몇 번째인지 확인하기 위한 SQL문
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, conn);
        //int return_num;
        try
        {
            conn.Open();//DB 연결
            SqlDataReader rd;
            rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Rnumber = int.Parse(rd["Rnum"].ToString());//return_num 
            }//데이터베이스에 접근하여 가장 마지막 경기가 몇 번째인지 찾는다.
            rd.Close();
        }
        catch (Exception error)
        {
            //Response.Write(error.Message); 
        }
        finally
        {
            Rnumber++;//마지막 경기 횟수를 최신화
            conn.Close();
        }

        return Rnumber;//return_num;
    }

    //DB에 저장
    protected void savedata()
    {
        int number = SearchNum();//마지막 경기 횟수를 불러옴


        string insertSQL = "INSERT INTO dbo.record ";
        insertSQL += "(Rnum,Player1, Player2, Result)";
        insertSQL += " VALUES(@pRnum,@pPlayer1,@pPlayer2,@pResult)";//경기 기록 테이블에 진행된 경기 데이터를 입력하는 SQL문
        SqlConnection conn = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand(insertSQL, conn);
        cmd.Parameters.AddWithValue("@pRnum", number);
        cmd.Parameters.AddWithValue("@pPlayer1", player1);
        cmd.Parameters.AddWithValue("@pPlayer2", player2);
        cmd.Parameters.AddWithValue("@pResult", result);

        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();//데이터베이스 삽입
        }
        catch (Exception error)
        {
            //Response.Write(error.Message);
        }
        finally
        {
            conn.Close();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        /*불필요한 세션들 제거 */
        Session.Remove("turn");
        Session.Remove("mode");
        Session.Remove("turnCount");
        Session.Remove("win");
        Session.Remove("opponent");

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Session.Remove(i.ToString() + j.ToString());
            }
        }
        Server.Transfer("MainPage.aspx");

    }
}