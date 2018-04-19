<%@ Page Title="Shanghai Leduc" Language="C#" MasterPageFile="~/FrontEnd_Pages/FrontFacing.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Shanghai.WebApp._Default" %>

<%@ Register Src="~/UserControls/MenuItemInfoDropDown.ascx" TagPrefix="uc1" TagName="MenuItemInfoDropDown" %>
<%@ Register Src="~/UserControls/ComboItemSelector.ascx" TagPrefix="uc1" TagName="ComboItemSelector" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="heroImage clearfix">
        <h1>Shanghai Leduc</h1>
        <a href="FrontEnd_Pages/Menu.aspx" class="link-btn">Order Online</a>
    </div>
    <div class="container homePageContainer">
        <div class="restaurantHours">
            <h2>Hours of Operation:</h2>
            <h3>Daily Buffet</h3>
            <p><span>Lunch:</span> 11:30 am - 2:30 pm (Adult: $15.99, Senior $14.99)</p>
            <p><span>Dinner:</span> 4:30 pm - 8:30 pm (Adult: $16.99, Senior $15.99)</p>
            <br />
            <p><span>Weekend Special (Friday-Sunday-Holidays)</span><br />
                <span>Dinner:</span> Adult $17.99, Senior $16.99
            </p>
            <p>Under 3 yrs old is free!<br />
                Youth $11.95 (7-11 yrs old) <br />
                Child $8.95 (4-6 yrs old)
            </p>
        </div>
        
        <div class="catering">
            <p><span>Catering Available!</span> Call <a href="tel:780-986-18625">780-986-1862</a> to inquire</p>
        </div>
    </div> 
</asp:Content>