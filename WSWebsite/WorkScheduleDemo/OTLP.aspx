<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OTLP.aspx.cs" Inherits="WorkScheduleDemo_OTLP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <h1>Employee Skill Registration</h1>
    </div>
    <div class="row">
        <div class="col-sm-2">
            <asp:Label ID="Label1" runat="server" Text="First Name "></asp:Label>
            <asp:TextBox ID="FirstNameBox" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Last Name "></asp:Label>
            <asp:TextBox ID="LastNameBox" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Home Phone "></asp:Label>
            <asp:TextBox ID="HomePhoneBox" runat="server"></asp:TextBox>
            <br />
        </div>
        <div class="col-sm-10">
            <asp:LinkButton ID="LinkButton1" runat="server">Register</asp:LinkButton>
            <br />
            <asp:LinkButton ID="LinkButton2" runat="server">Clear</asp:LinkButton>
        </div>
        <div>
            <asp:ListView ID="ListView1" runat="server" DataSourceID="EmployeeSkillODS"></asp:ListView>
        </div>
    </div>
</asp:Content>

