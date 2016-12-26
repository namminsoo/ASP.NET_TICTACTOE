using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RecordPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //자신의 것을 누르는 것을 방지하기 위하여 비활성화
        btnWhole.Enabled = false;
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("MainPage.aspx");
    }
    protected void btnPersonal_Click1(object sender, EventArgs e)
    {
       Server.Transfer("RecordPpage.aspx");
    }
}