using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
public partial class LoginPage : System.Web.UI.Page
{
    //세션에 정보를 저장하고, 데이터베이스의 정보를 저장하기 위한 변수 생성
    string name;
    string password;

    protected void Page_Load(object sender, EventArgs e)
    {
  
        if (!string.IsNullOrEmpty(Session["Name"] as string))
        {
            Server.Transfer("MainPage.aspx");
        }//Session["name"]에 값이 존재하는 경우에는 페이지 이동

        //로그인을 Enter를 치고 페이지 이동이 가능하게 하기 위해
        txtPassWord.Attributes.Add("onkeypress", "if(event.keyCode == 13){" + this.Page.ClientScript.GetPostBackEventReference(btnLogin, null) + ";return false;}");
        txtId.Focus();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (checkLogin())//로그인이 성공적으로 되었다면
        {
            //세션에 사용자 아이디 저장 후 페이지 이동
                Session["Name"] = name;
                Server.Transfer("MainPage.aspx");
        }
        else  //로그인이 성공하지 못했다면
        {
            lblError.Text = "아이디 혹은 비밀번호가 틀렸습니다. 다시 작성해주세요.";
        }
    }

    protected void btnJoin_Click(object sender, EventArgs e)
    {//회원 가입을 위한 페이지 이동
        Server.Transfer("JoinPage.aspx");
    }

    private bool checkLogin()
    {
        bool check = false;
        
        //DB연결을 스트링 가져오기
        string connectionString = WebConfigurationManager.ConnectionStrings["TicTacToe"].ConnectionString;
        
        //아이디와 패스워드 맞는지 체크
        string selectSQL = "SELECT * FROM dbo.userdata WHERE Usrname=@pUsrname and Pssword=@pPssword";
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, conn);

        cmd.Parameters.AddWithValue("@pUsrname", txtId.Text);
        cmd.Parameters.AddWithValue("@pPssword", txtPassWord.Text);

        try
        {
            conn.Open();
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            /*데이터베이스 읽어오기*/
            rd.Read();

            name = rd["Usrname"].ToString();
            password = rd["Pssword"].ToString();
            rd.Close();

            //읽어온 데이터와 사용자가 입력한 데이터가 서로 맞는지 확인
            if (name == txtId.Text && password == txtPassWord.Text)
            {
                check = true;
            }
        }
        catch (Exception error)
        {
            //lblError.Text = error.Message;
        }
        finally
        {
            conn.Close();
        }
        return check;
    }
}