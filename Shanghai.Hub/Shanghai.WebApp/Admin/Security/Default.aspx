<%@ Page Title="" Language="C#" MasterPageFile="../../BackEnd.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Shanghai.WebApp.Admin.Security.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class=" row jumbotron">
        <h1>User Administration</h1>
    </div>
    <div class="row">
        <div class="col-md-12">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#users" data-toggle="tab">Users</a></li>
                <li><a href="#roles" data-toggle="tab">Security Roles</a></li>
                <li><a href="#unregistered" data-toggle="tab">Unassigned Users</a></li>
            </ul>
             <div class="tab-content">
                <div id="users" class="tab-pane fade in active">
                    <h3>Registered Users in the Site</h3>
                    <asp:ListView ID="UsersListView" runat="server"
                        DataKeyNames="UserId" ItemType="Shanghai.Security.Entities.UserProfile" DataSourceID="UsersDataSource">
                        <AlternatingItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label Text='<%# Eval("FullName") %>' runat="server" ID="FullNameLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("RoleMemberships") %>' runat="server" ID="RoleMembershipsLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("UserId") %>' runat="server" ID="UserIdLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("UserName") %>' runat="server" ID="UserNameLabel" /></td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EditItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                                    <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                                </td>
                                <td>
                                    <asp:TextBox Text='<%# Bind("FullName") %>' runat="server" ID="FullNameTextBox" /></td>
                                <td>
                                    <asp:TextBox Text='<%# Bind("RoleMemberships") %>' runat="server" ID="RoleMembershipsTextBox" /></td>
                                <td>
                                    <asp:TextBox Text='<%# Bind("UserId") %>' runat="server" ID="UserIdTextBox" /></td>
                                <td>
                                    <asp:TextBox Text='<%# Bind("UserName") %>' runat="server" ID="UserNameTextBox" /></td>
                            </tr>
                        </EditItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <InsertItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                                    <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                                </td>
                                <td>
                                    <asp:TextBox Text='<%# Bind("FullName") %>' runat="server" ID="FullNameTextBox" /></td>
                                <td>
                                    <asp:TextBox Text='<%# Bind("RoleMemberships") %>' runat="server" ID="RoleMembershipsTextBox" /></td>
                                <td>
                                    <asp:TextBox Text='<%# Bind("UserId") %>' runat="server" ID="UserIdTextBox" /></td>
                                <td>
                                    <asp:TextBox Text='<%# Bind("UserName") %>' runat="server" ID="UserNameTextBox" /></td>
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label Text='<%# Eval("FullName") %>' runat="server" ID="FullNameLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("RoleMemberships") %>' runat="server" ID="RoleMembershipsLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("UserId") %>' runat="server" ID="UserIdLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("UserName") %>' runat="server" ID="UserNameLabel" /></td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table runat="server" id="itemPlaceholderContainer" style="" border="0">
                                            <tr runat="server" style="">
                                                <th runat="server">FullName</th>
                                                <th runat="server">RoleMemberships</th>
                                                <th runat="server">UserId</th>
                                                <th runat="server">UserName</th>
                                            </tr>
                                            <tr runat="server" id="itemPlaceholder"></tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="">
                                        <asp:DataPager runat="server" ID="DataPager1">
                                            <Fields>
                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label Text='<%# Eval("FullName") %>' runat="server" ID="FullNameLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("RoleMemberships") %>' runat="server" ID="RoleMembershipsLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("UserId") %>' runat="server" ID="UserIdLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("UserName") %>' runat="server" ID="UserNameLabel" /></td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:ObjectDataSource ID="UsersDataSource" runat="server" OldValuesParameterFormatString="original_{0}" 
                        SelectMethod="ListAllUsers" TypeName="Shanghai.Security.BLL.UserManager"></asp:ObjectDataSource>
                </div>
             </div>
        </div>
    </div>
</asp:Content>
