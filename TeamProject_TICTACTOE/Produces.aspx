<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Produces.aspx.cs" Inherits="Produces" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script language="javascript" type="text/javascript">
          function Madefunc() {
              location.replace("~/Produces.aspx");
              //window.open("Produces.aspx");
          }
          function ConfirmMsg() {
              var returnVal = confirm("Logout?");
              return returnVal;
          }
          function ExitMsg() {
              window.close();
          }
    </script>
    <link href="CSS/produces.css" rel="stylesheet" />
   
  
    <style type="text/css">
   @import url(http://fonts.googleapis.com/earlyaccess/hanna.css);
           .font{
            font-family: 'Hanna', sans-serif;
        }
        .auto-style6 {
            border-style: none;
            border-color: inherit;
            border-width: 0;
            font-family: "Roboto", sans-serif;
            text-transform: uppercase;
            outline: 0;
            background: #64B5F6;
            width: 18%;
            padding: 15px;
            color: #FFFFFF;
            font-size: 14px;
            -webkit-transition: all 0.3 ease;
            transition: all 0.3 ease;
            cursor: pointer;
        }
      
          
    </style>

</head>
<body>




  
<figure class="snip1540">
  <div class="profile-image"><img src="Image/wo.jpg" alt="profile-sample1" />
 
  </div>
  <figcaption>
    <h3>심우일</h3>
    <h4>조장</h4>
    <p>Which is worse, that everyone has his price, or that the price is always so low.</p>
    <p></p>
  </figcaption>
</figure>
<figure class="snip1540">
  <div class="profile-image"><img src="Image/ms.png" alt="profile-sample4" />

  </div>
  <figcaption>
    <h3>남민수</h3>
    <h4>조원</h4>
    <p>I'm killing time while I wait for life to shower me with meaning and happiness.</p>
  </figcaption>
</figure>
    <br />
<figure class="snip1540">
  <div class="profile-image"><img src="Image/ch.jpg" alt="profile-sample9" />

  </div>
  <figcaption>

    <h3>최원석</h3>
    <h4>조원</h4>
    <p>The only skills I have the patience to learn are those that have no real application in life. </p>
  </figcaption>
</figure>
    <figure class="snip1540">
  <div class="profile-image"><img src="Image/ee.jpg" alt="profile-sample9" />
  </div>
  <figcaption>

    <h3>이주경</h3>
    <h4>조원</h4>
    <p>The only skills I have the patience to learn are those that have no real application in life. </p>
  </figcaption>
</figure>
    <br/>
   <input type="button" class="auto-style6" value="뒤로가기" onClick="location.href='MainPage.aspx';"></input>
      
</body>
</html>