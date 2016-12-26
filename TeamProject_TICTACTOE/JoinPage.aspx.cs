using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class JoinPage : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["TicTacToe"].ConnectionString;
  

    protected void Page_Load(object sender, EventArgs e)
    {
        
        btnSubmit.Disabled = true;//사용 불가

        //유효성 검사 순서를 정하기 위해서 비활성화를 걸었습니다.
        if (lblCheck.Text == "")
        {
            rfvtxtEmail.Enabled = false;
            rfvtxtPassword.Enabled = false;
            rfvtxtPasswordConfirm.Enabled = false;
        }
        lblCheck.Text = "";        
    }
    //중복된 아이디를 확인하는 함수
    protected void btnCompareId_Click(object sender, EventArgs e)
    {      
        string selectSQL = "SELECT * FROM dbo.userdata WHERE Usrname=@pUsrname";//데이터베이스에 저장되어 있는 아이디를 찾는 SQL문
        SqlConnection conn = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand(selectSQL, conn);
        cmd.Parameters.AddWithValue("@pUsrname", txtId.Text);
        try
        {
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            rd.Read();
            if (rd.HasRows)//이미 존재하는 아이디를 작성한 경우
            {
                lblCheck.Text = "중복된 ID입니다.";
            }
            else
            {
                lblCheck.Text = "사용할 수 있는 ID입니다.";
                lblCheck.ForeColor = System.Drawing.Color.Blue;
                rfvtxtEmail.Enabled = true;
                rfvtxtPassword.Enabled = true;
                rfvtxtPasswordConfirm.Enabled = true;
                btnSubmit.Disabled = false;//사용 가능하다.
                
            }
            rd.Close();

        }
        catch (Exception error)
        {

        }
        finally
        {
 
            conn.Close();
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        { 
            string insertSQL = "INSERT INTO dbo.userdata ";
            insertSQL += "(Usrname, Pssword, EmailAddress)";
            insertSQL += " VALUES(@pUsrname,@pPssword,@pEmailAddress)";//입력된 계정 정보들을 데이터베이스 유저테이블에 입력하는 SQL문입니다.

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, conn);

            cmd.Parameters.AddWithValue("@pUsrname", txtId.Text);
            cmd.Parameters.AddWithValue("@pPssword", txtPassWord.Text);
            cmd.Parameters.AddWithValue("@pEmailAddress", txtEmail.Text);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();//쿼리문 실행
                //Response.Write("" + insert);
            }
            catch (Exception error)
            {
                //string msg = (string)error.Message;
                //Response.Write(msg);
            }
            finally
            {
                conn.Close();
                Server.Transfer("LoginPage.aspx");
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {//로그인 페이지(메인 페이지)로 이동합니다.
        Server.Transfer("LoginPage.aspx");
    }
}