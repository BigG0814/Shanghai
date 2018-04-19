<%@ Page Title="" Language="C#" MasterPageFile="FrontFacing.Master" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="Shanghai.WebApp.Nav_Page.Location" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%--    <link href="../Content/location.css" rel="stylesheet" type="text/css" />--%>
    <style>
        .location{
    background-image: url("../images/location-photo.jpg");
    background-position: center;
    background-size: cover;
    /*width: 1080px;*/
    margin: 0 auto;
    min-height: calc(100vh - 150px);
    width: 70%;
    min-width: 500px;
}

.cartInfo{
    font-size: 14px !important;
}

#gMap{
  
    padding: 20px 20px;
    float:left;
}
.gWord{
    width: 450px;
    padding: 170px 0px;
    font-size: 20px;
    float: left;
}
.gWord a{
    background-color:orangered;
    font-size: 0.9em;
    color: black;
    border-radius:10px;
    padding: 1em 2em;
}
.gWord a:hover{
    text-decoration: none;
    border: 2px solid grey;
}
.gWord .locationContainer{
    background-color: #0000005e;
    color: white;
    padding: 0px;
    margin-bottom: -37px;
    margin-top:70px;
    height: 70px;
    
}
/*@media (max-width: 1700px) {
            iframe {
                width: 650px;              
                padding: 20px 20px;
                float: left;
                border-style:none;
            }

            .gWord {
                padding: 0px;
               
                margin-left: 25px;
                width: 450px;
                font-size: 20px;
                float: left;
            }

                .gWord .locationContainer {
                   
                    margin-bottom: -35px;
                    height: 80px;
                    background-color: #0000005e;
                   margin-left:20px;
                   margin-top:-2px;
                }

                .gWord a {
                    margin-left: 20px;
                }
        }
       @media (max-width: 950px) {
            iframe {
                width: 480px;              
                padding: 20px 20px;
                float: left;
                border-style:none;
            }

            .gWord {
                padding: 0px;
               
                margin-left: 25px;
                width: 450px;
                font-size: 20px;
                float: left;
            }

                .gWord .locationContainer {
                   
                    margin-bottom: -35px;
                    height: 80px;
                    background-color: #0000005e;
                   margin-left:20px;
                   margin-top:-8px;
                }

                .gWord a {
                    margin-left: 20px;
                }
        }*/

         @media (max-width: 425px) {
            iframe {
                width: 350px;              
                padding: 20px 20px;
                float: left;
                border-style:none;
            }

            .gWord {
               
                margin-top: 380px;
               
                width: 40px;
                float: left;
                font-size: 0.99em;
            }

                .gWord .locationContainer {
                    margin-left: -375px;
                    margin-bottom: 5px;
                    height: 60px;
                    background-color: #0000005e;
                    margin-top:45px;
                    width:310px;
                }

                .gWord a {
                    margin-left: -375px;
                    font-size: 0.8em;
                }
        }
        /*@media (max-height: 750px) {
             iframe {
                width: 480px;  
                height:350px;            
                padding: 20px 20px;
                float: left;
                border-style:none;
            }
             .gWord {
               
                float: left;
                font-size: 0.9em;
            }

                .gWord .locationContainer {
                    margin-left:15px;
                    margin-bottom: 5px;
                    height: 60px;
                    background-color: #0000005e;
                  
                    width:310px;
                }

                .gWord a {
                    margin-left: 15px;
                    font-size: 0.8em;
                }
          
        }
         @media (max-height: 650px) {
             iframe {
                width: 420px;  
                height:150px;            
                padding: 5px 5px;
                float: left;
                border-style:none;
            }
             .gWord {
              
                margin-top: -80px;
                float: left;
               
            }

                .gWord .locationContainer {
                    margin-left: 0px;
                    margin-bottom: -35px;
                    height: 50px;
                    background-color: #0000005e;
                    margin-top:85px;
                    width:280px;

                }

                .gWord a {
                    margin-left:0px;
                    
                }*/
         
        /*}*/
    </style>

    <div class="location ">
        <%--Google Map Link (not API)--%>
        <div id="gMap">
            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2385.9339164930134!2d-113.55132128404315!3d53.27280368791821!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x539ff9a1381aa931%3A0x53f2e8e46fdc487e!2sShanghai+Restaurant!5e0!3m2!1szh-CN!2sca!4v1516988270075" width="700" height="400"></iframe>
        </div>
        <div class="gWord">
            <div class="locationContainer">
                <p>Location: 5901 50st. Leduc, AB T9E 6J4</p>
                <p>To Order Call 780-986-1862 OR 780-986-1883</p>
                <br />
            </div>

            <a href="https://www.google.ca/maps/place/Shanghai+Restaurant/@53.2728037,-113.5513213,17z/data=!3m1!4b1!4m5!3m4!1s0x539ff9a1381aa931:0x53f2e8e46fdc487e!8m2!3d53.2728005!4d-113.5491326" class="link-btn" target="_blank">Get Direction</a>
        </div>
    </div>
</asp:Content>
