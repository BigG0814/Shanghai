<%@ Page Title="Log in" Language="C#" MasterPageFile="../FrontEnd_Pages/FrontFacing.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Shanghai.WebApp.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
        .logInContainer{
            margin: 0 auto;
        }
        .loginContent{
            margin: auto;
            width: 400px;
        }
        
    </style>
    <div class="row container mainContainer logInContainer" style="min-height: calc(100vh - 106px);">
        <asp:Panel runat="server" ID="loginPanel" Visible="false" DefaultButton="btnLogin">
        <div class="loginContent">
            <h2>Employee Log In</h2>
            <div class="col-md-12">
                <section id="loginForm">
                    <div class="form-horizontal">
                        <hr />
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="FailureText" />
                            </p>
                        </asp:PlaceHolder>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">Username</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                    CssClass="text-danger" ErrorMessage="The user name field is required." />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="Password" Text="asdf" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                            </div>
                        </div>
                        <%--<div class="form-group">
                            <div class="col-md-offset-3 col-md-10">
                                <div class="checkbox">
                                    <asp:CheckBox runat="server" ID="RememberMe" />
                                    <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                                </div>
                            </div>
                        </div>--%>
                        <div class="form-group">
                            <div class="col-md-offset-5 col-md-10">
                                <asp:Button ID="btnLogin" runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>
                    <asp:Panel runat="server" Visible="false">
                        <p class="">
                            <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Sign Up</asp:HyperLink>
                        </p>
                    </asp:Panel>

                    <%--Displays when a user is already logged in--%>
                    <asp:Panel ID="confirmPanel" runat="server" visible="false">
                        <div class="alert alert-success">
                            <p>You are already logged in.</p>
                        </div>
                    </asp:Panel>

                    <p>
                        <%-- Enable this once you have account confirmation enabled for password reset functionality
                        <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Forgot your password?</asp:HyperLink>
                        --%>
                    </p>
                </section> 
                
            </div>
           
            <asp:Panel runat="server" Visible="false">
                <div class="col-md-4">
                            <section id="socialLoginForm">
                                <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
                            </section>
                        </div>
            </asp:Panel>

      </div>  
     </asp:Panel>
    </div>
</asp:Content>
