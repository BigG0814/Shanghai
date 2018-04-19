<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs" Inherits="Shanghai.WebApp.UserControls.DatePicker" %>
<style>
    #Calender1{
        background: white;
        position: absolute;
        left: 30%;
        top: 28px;
        z-index: 900;
    }
</style>
<div style="position:relative; display:inline-block;">
    <asp:Label ID="TB_Week_Ending" style="display:inline; font-size:85%" runat="server" CssClass="" /><br /><asp:ImageButton ImageUrl="~/images/calender.jpg" OnClick="CalImg_Click" Width="30px" ID="CalImg" runat="server" ClientIDMode="Static"/> 
<asp:Calendar ID="Calender1" CssClass="table table-condensed" ClientIDMode="Static" runat="server" OnSelectionChanged="Calender1_SelectionChanged" Visible="false"></asp:Calendar>
</div>
