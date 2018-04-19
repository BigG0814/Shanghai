<%@ Page Title="POSLogin" Language="C#" MasterPageFile="~/POSMaster.Master" AutoEventWireup="true" CodeBehind="POSLogIn.aspx.cs" Inherits="Shanghai.WebApp.POS.POSLogIn" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <div style="text-align:center; margin-top:30px;">
    <asp:Panel runat="server" style="margin:0 auto" ID="emploginpanel">
        <h1 style="font-variant:small-caps; margin-bottom:50px;">POS - Log In</h1>
        <asp:Label Text="Enter Your Employee Number" runat="server" /> <br />
        <asp:TextBox runat="server" style="display:inline-block; width:170px !important;" CssClass="form-control" TextMode="Number" min="0" max="9999" AutoCompleteType="None" step="1" ClientIDMode="Static"  ID="EmployeeIDBox" />
        <asp:Button Text="Enter" UseSubmitBehavior="true" runat="server" CssClass="btn btn-default" OnClick="ValidateEmployee_Click" />
    </asp:Panel>
    </div>
    <script>
        window.onload = function () {
            document.getElementById("EmployeeIDBox").focus();
        };
    </script>
</asp:Content>
