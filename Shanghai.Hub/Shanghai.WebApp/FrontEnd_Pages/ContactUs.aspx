<%@ Page Title="" Language="C#" MasterPageFile="FrontFacing.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="Shanghai.WebApp.Nav_Page.ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        
        .row{
            
            margin-left:15px;
            color:#dedede;
            margin-right:15px;
            
        }
        .link{
            padding: 11px 11px 11px 11px;
    border-radius: 10px;
    font-size: 30px;
    color: white;
    background: #8bc34a;
    text-decoration: none;
    margin-bottom:10px;
    
}
       span{
           font-weight:bold;
       }
        .first-column{
            margin-bottom:20px;
        }

        .second-column iframe {display: block; width: 100%; height:450px;  border: none;}
            
        h1 {
                font-size: 60px;
                color: yellowgreen;
            }

     
    </style>
    <div class="row">
        <div class="col-md-6 first-column" style="min-height: calc(100vh - 106px);">
            <h1>Connect with Us</h1>
            
            <h3><span>Email:    </span><br />info@shanghai.com</h3>
            <h3><span>Telephone:    </span><br />780-986-1862</h3>
            <h3><span>Address:  </span> <br />5901 50 ST. Leduc, AB
                <br />
                T9E 6J4</h3>
        
                <br />
            <a href="https://www.google.ca/maps/place/Shanghai+Restaurant/@53.2728037,-113.5513213,17z/data=!3m1!4b1!4m5!3m4!1s0x539ff9a1381aa931:0x53f2e8e46fdc487e!8m2!3d53.2728005!4d-113.5491326" class="link" target="_blank">Get Directions</a>
            </div>
        <div class="col-md-6 second-column">
            
            
            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2385.9339164930134!2d-113.55132128404315!3d53.27280368791821!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x539ff9a1381aa931%3A0x53f2e8e46fdc487e!2sShanghai+Restaurant!5e0!3m2!1szh-CN!2sca!4v1516988270075" ></iframe>
        </div>
    </div>

   <%-- <div class="contactContainer">
        <div class="Container">
            <h1>Contact Us</h1>
            <br />
            <h3><span>Email:</span>info@shanghai.com</h3>
            <h3><span>Tel:</span>780-986-1862 OR 780-986-1872</h3>
            <h3><span>Address:</span> 5901 50st. Leduc, AB
                <br />
                T9E 6J4</h3>
        </div>
    </div>--%>

</asp:Content>